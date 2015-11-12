using System;
using System.Collections.Generic;
using eOne.Common.Actions;
using eOne.Common.Connectors.Sendloop.Models;
using eOne.Common.Connectors.Service;
using eOne.Common.Helpers;
using eOne.Common.Query;

namespace eOne.Common.Connectors.Sendloop
{
    public class SendloopConnector : RestConnector
    {

        #region Constants

        #region Entity IDs

        public const int EntityIdList = 1;
        public const int EntityIdSubscriber = 2;
        public const int EntityIdSuppression = 3;
        public const int EntityIdCampaign = 4;

        #endregion

        #region Action IDs

        public const int ActionIdAddSubscriber = 1;
        public const int ActionIdPauseCampaign = 2;
        public const int ActionIdResumeCampaign = 3;
        public const int ActionIdSendCampaign = 4;

        #endregion

        #endregion

        public SendloopConnector()
        {
            Name = "Sendloop";
            Group = ConnectorGroup.MailingList;
            ConnectorMethod = RestConnectorMethod.Post;
            AuthenticationType = ServiceConnectorAuthenticationType.UrlParameter;
        }

        #region Methods

        public override void Initialise()
        {
            base.Initialise();
            BaseUrl = $"http://{SitePrefix}.sendloop.com/api/v3";
            AddEntities();
        }

        public override string GetEndpoint(ConnectorQuery query)
        {
            switch (query.Entity.Id)
            {
                case EntityIdList:
                case EntityIdSubscriber:
                    return "List.GetList/json";
                case EntityIdSuppression:
                    return "Suppression.List.Get/json";
                case EntityIdCampaign:
                    return "Campaign.GetList/json";
            }
            return string.Empty;
        }

        public override List<Tuple<string, string>> GetParameters(ConnectorQuery query)
        {
            var parameters = TupleHelper.CreateTupleStringList("APIKey", Key);

            switch (query.Entity.Id)
            {
                case EntityIdSuppression:
                    TupleHelper.AppendTupleStringList(parameters, "ListID", "0");
                    break;
                case EntityIdCampaign:
                    var statusRestriction = query.FindRestrictionByFieldAndType("CampaignStatus", ConnectorRestriction.ConnectorRestrictionType.Equals);
                    if (statusRestriction == null)
                    {
                        TupleHelper.AppendTupleStringList(parameters, "IgnoreDrafts", "0", "IgnoreSending", "0", "IgnorePaused", "0", "IgnoreSent", "0", "IgnoreFailed", "0", "IgnoreApproval", "0");
                    }
                    else
                    {
                        switch (statusRestriction.Values[0].Constant)
                        {
                            case "Draft":
                                TupleHelper.AppendTupleStringList(parameters, "IgnoreDrafts", "0");
                                break;
                            case "Sending":
                                TupleHelper.AppendTupleStringList(parameters, "IgnoreSending", "0");
                                break;
                            case "Paused":
                                TupleHelper.AppendTupleStringList(parameters, "IgnorePaused", "0");
                                break;
                            case "Sent":
                                TupleHelper.AppendTupleStringList(parameters, "IgnoreSent", "0");
                                break;
                            case "Failed":
                                TupleHelper.AppendTupleStringList(parameters, "IgnoreFailed", "0");
                                break;
                            case "Pending":
                                TupleHelper.AppendTupleStringList(parameters, "IgnoreApproval", "0");
                                break;
                            default:
                                TupleHelper.AppendTupleStringList(parameters, "IgnoreDrafts", "0", "IgnoreSending", "0", "IgnorePaused", "0", "IgnoreSent", "0", "IgnoreFailed", "0", "IgnoreApproval", "0");
                                break;
                        }
                    }
                    break;
            }

            return parameters;
        }

        public override IEnumerable<object> Deserialize(string data, ConnectorQuery query)
        {
            switch (query.Entity.Id)
            {
                case EntityIdList:
                    var listCollection = DeserializeJson<SendloopListCollection>(data);
                    // todo - don't get stats if fields not used in query
                    foreach (var list in listCollection.Lists)
                    {
                        var statisticsData = GetResponse("Report.List.Overall/json", TupleHelper.CreateTupleStringList("ListID", list.ListID.ToString()));
                        var statistics = DeserializeJson<SendloopListStatisticsCollection>(statisticsData);
                        list.Statistics = statistics.Statistics;
                    }
                    return listCollection.Lists;
                case EntityIdSubscriber:
                    var subscribers = new List<SendloopSubscriber>();
                    var subscriberListCollection = DeserializeJson<SendloopListCollection>(data);
                    foreach (var list in subscriberListCollection.Lists)
                    {
                        var subscriberData = GetResponse("Subscriber.Browse/json", TupleHelper.CreateTupleStringList("ListID", list.ListID.ToString()));
                        var subscriberCollection = DeserializeJson<SendloopSubscriberCollection>(subscriberData);
                        foreach (var subscriber in subscriberCollection.Subscribers)
                        {
                            subscriber.List = list;
                            subscribers.Add(subscriber);
                        }
                    }
                    return subscribers;
                case EntityIdSuppression:
                    var suppressionCollection = DeserializeJson<SendloopSuppressionCollection>(data);
                    return suppressionCollection.SuppressionList;
                case EntityIdCampaign:
                    var campaignCollection = DeserializeJson<SendloopCampaignCollection>(data);
                    return campaignCollection.Campaigns;
            }
            return null;
        }

        public override void RunAction(ConnectorAction action, List<Tuple<string, string>> parameters)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Helpers

        public void AddEntities()
        {
            var listEntity = AddEntity(EntityIdList, "Lists", typeof(SendloopList));
            var subscriberEntity = AddEntity(EntityIdSubscriber, "Subscribers", typeof(SendloopSubscriber));
            AddEntity(EntityIdSuppression, "Suppressions", typeof(SendloopSuppression));
            var campaignEntity = AddEntity(EntityIdCampaign, "Campaigns", typeof(SendloopCampaign));

            // set default max records
            foreach (var entity in Entities) entity.DefaultMaxRecords = 100;

            // add relationships
            listEntity.AddRelatedEntity("Subscribers", subscriberEntity, "ListID", "ListID");

            // add favorites
            AddCampaignFavorites(campaignEntity);

            // add actions
            AddListActions(listEntity);
            AddCampaignActions(campaignEntity);
        }
        public void AddListActions(ConnectorEntity entity)
        {
            var addSubscriberAction = entity.AddAction(ActionIdAddSubscriber, "Add subscriber");
            addSubscriberAction.AddParameter("Email address", FieldTypeIdEmail);
        }
        public void AddCampaignFavorites(ConnectorEntity entity)
        {
            var draftFavorite = entity.AddFavorite("Drafts");
            draftFavorite.Query.AddFields("CampaignName", "Subject");
            draftFavorite.Query.AddRestriction("CampaignStatus", ConnectorRestriction.ConnectorRestrictionType.Equals, "Draft");

            var sendingFavorite = entity.AddFavorite("Sending");
            sendingFavorite.Query.AddFields("CampaignName", "Subject", "TotalRecipients", "SendDateValue");
            sendingFavorite.Query.AddRestriction("CampaignStatus", ConnectorRestriction.ConnectorRestrictionType.Equals, "Sending");

            var sentFavorite = entity.AddFavorite("Sent");
            sentFavorite.Query.AddFields("CampaignName", "Subject", "TotalRecipients", "SendDateValue", "SendProcessFinishedOnDateValue");
            sentFavorite.Query.AddRestriction("CampaignStatus", ConnectorRestriction.ConnectorRestrictionType.Equals, "Sent");

            var pausedFavorite = entity.AddFavorite("Paused");
            pausedFavorite.Query.AddFields("CampaignName", "Subject", "TotalRecipients");
            pausedFavorite.Query.AddRestriction("CampaignStatus", ConnectorRestriction.ConnectorRestrictionType.Equals, "Paused");

            var failedFavorite = entity.AddFavorite("Failed");
            failedFavorite.Query.AddFields("CampaignName", "Subject", "TotalRecipients");
            failedFavorite.Query.AddRestriction("CampaignStatus", ConnectorRestriction.ConnectorRestrictionType.Equals, "Failed");

            var pendingFavorite = entity.AddFavorite("Pending approval");
            pendingFavorite.Query.AddFields("CampaignName", "Subject", "TotalRecipients");
            pendingFavorite.Query.AddRestriction("CampaignStatus", ConnectorRestriction.ConnectorRestrictionType.Equals, "Pending");
        }
        public void AddCampaignActions(ConnectorEntity entity)
        {
            var pauseAction = entity.AddAction(ActionIdPauseCampaign, "Pause");
            pauseAction.AddParameter("id", "CampaignID");
            pauseAction.AddCondition("CampaignStatus", ConnectorRestriction.ConnectorRestrictionType.Equals, "Sending");

            var resumeAction = entity.AddAction(ActionIdResumeCampaign, "Resume");
            resumeAction.AddParameter("id", "CampaignID");
            resumeAction.AddCondition("CampaignStatus", ConnectorRestriction.ConnectorRestrictionType.Equals, "Paused");

            var sendAction = entity.AddAction(ActionIdSendCampaign, "Send");
            sendAction.AddParameter("id", "CampaignID");
            sendAction.AddCondition("CampaignStatus", ConnectorRestriction.ConnectorRestrictionType.Equals, "Draft");
        }

        #endregion

    }
}
