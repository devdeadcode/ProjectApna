using System;
using System.Collections.Generic;
using System.Net;
using eOne.Common.Connectors.MailChimp.Models;
using eOne.Common.Connectors.MailChimp.Models.Campaign;
using eOne.Common.Connectors.MailChimp.Models.List;
using RestSharp;
using System.Linq;
using eOne.Common.Actions;
using eOne.Common.Connectors.Service;
using eOne.Common.Helpers;
using eOne.Common.Query;
using eOne.Common.Setup;

namespace eOne.Common.Connectors.MailChimp
{
    public class MailChimpConnector : RestConnector
    {

        #region Constants

        #region Entity IDs

        public const int EntityIdList = 1;
        public const int EntityIdCampaign = 2;
        public const int EntityIdCampaignFeedback = 3;
        public const int EntityIdListAbuse = 4;
        public const int EntityIdListActivity = 5;
        public const int EntityIdListGrowth = 6;
        public const int EntityIdCampaignClickDetails = 7;
        public const int EntityIdCampaignClickMembers = 8;

        #endregion

        #endregion

        public MailChimpConnector()
        {
            Name = "MailChimp";
            Group = ConnectorGroup.MailingList;

            BaseUrl = "https://{0}.api.mailchimp.com/3.0/";
            ConnectorMethod = RestConnectorMethod.Get;

            AuthenticationType = ServiceConnectorAuthenticationType.OAuth2;
            AuthorizationUri = "https://login.mailchimp.com/oauth2/authorize";
            AccessTokenUri = "https://login.mailchimp.com/oauth2/token";
            CallbackUrl = "http://www.popdock.com/callbacks/mailchimp";
            ClientId = "548227577004";
            Secret = "9110e9ca529d76c4d5191f01714da53c";
            
            AddSetup();
        }

        #region Public methods

        public override void Initialise()
        {
            base.Initialise();
            
            // get datacenter from metadata call
            var client = new RestClient("https://login.mailchimp.com");
            var request = new RestRequest("oauth2/metadata", Method.GET);
            request.AddHeader("Authorization", $"OAuth {Token}");
            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var metadata = DeserializeJson<MailChimpMetadata>(response.Content);
                if (string.IsNullOrWhiteSpace(metadata.dc)) return;
                BaseUrl = string.Format(BaseUrl, metadata.dc);
                AddEntities();
            }
        }

        public override string GetEndpoint(ConnectorQuery query)
        {
            if (query == null) return string.Empty;
            switch (query.Entity.Id)
            {
                case EntityIdCampaign:
                    return "campaigns";
                case EntityIdCampaignFeedback:
                case EntityIdCampaignClickDetails:
                    var campaignIdRestriction = query.FindRestrictionByFieldAndType("campaign_id", ConnectorRestriction.ConnectorRestrictionType.Equals);
                    if (campaignIdRestriction == null) return "campaigns";
                    switch (query.Entity.Id)
                    {
                        case EntityIdCampaignFeedback:
                            return $"campaigns/{campaignIdRestriction.Values[0]}/feedback";
                        case EntityIdCampaignClickDetails:
                            return $"reports/{campaignIdRestriction.Values[0]}/click-details";
                    }
                    return string.Empty;
                case EntityIdList:
                    return "lists";
                case EntityIdListGrowth:
                case EntityIdListAbuse:
                case EntityIdListActivity:
                    var listIdRestriction = query.FindRestrictionByFieldAndType("list_id", ConnectorRestriction.ConnectorRestrictionType.Equals);
                    if (listIdRestriction == null) return "lists";
                    switch (query.Entity.Id)
                    {
                        case EntityIdListGrowth:
                            return $"lists/{listIdRestriction.Values[0]}/growth-history";
                        case EntityIdListAbuse:
                            return $"lists/{listIdRestriction.Values[0]}/abuse-reports";
                        case EntityIdListActivity:
                            return $"lists/{listIdRestriction.Values[0]}/activity";
                    }
                    return string.Empty;
            }
            return string.Empty; 
        }

        public override List<Tuple<string, string>> GetParameters(ConnectorQuery query)
        {
            return TupleHelper.CreateTupleStringList("apikey", Key);
        }

        public override IEnumerable<object> Deserialize(string data, ConnectorQuery query)
        {
            switch (query.Entity.Id)
            {
                case EntityIdCampaign:
                    return DeserializeCampaigns(data);
                case EntityIdCampaignFeedback:
                    return DeserializeCampaignFeedback(data, query);
                case EntityIdList:
                    return DeserializeLists(data);
                case EntityIdListGrowth:
                    return DeserializeListGrowth(data, query);
                case EntityIdListAbuse:
                    return DeserializeListAbuse(data, query);
                case EntityIdListActivity:
                    return DeserializeListActivity(data, query);
                case EntityIdCampaignClickDetails:
                    return DeserializeCampaignClickDetails(data, query);
            }
            return null;
        } 

        public override void RunAction(ConnectorAction action, List<Tuple<string, string>> parameters)
        {
            var list_id = FindParameterValue(parameters, "id");
            var addSubscriber = new MailChimpListAddSubscriber
            {
                email_address = FindParameterValue(parameters, "Email address"),
                status = MailChimpListAddSubscriber.MailChimpListAddSubscriberStatus.subscribed,
                merge_fields = new List<Tuple<string, string>>()
            };
            var mergeFieldData = GetResponse($"lists/{list_id}/merge-fields");
            var mergeFields = DeserializeJson<MailChimpListMergeFieldCollection>(mergeFieldData);
            foreach (var mergeField in mergeFields.merge_fields)
            {
                TupleHelper.AppendTupleStringList(addSubscriber.merge_fields, mergeField.name, FindParameterValue(parameters, mergeField.name));
            }
            RunPostAction($"lists/{list_id}/members", addSubscriber);
        }

        #endregion

        #region Private methods

        private new void AddSetup()
        {
            var step1 = new ConnectorSetup.ConnectorSetupStep
            {
                Number = 1,
                Header = "Connector Credentials",
                TopDescription = "Please enter in a name for your new connector, along with your MailChimp API Key. ",
                BottomDescription = "Click Next to grant access to your MailChimp account."
            };
            step1.Fields.Add(new ConnectorSetupField(ConnectorSetupField.FieldNameName, "Connector name", ConnectorSetupField.ConnectorSetupFieldType.String, "MailChimp", true));
            step1.Fields.Add(new ConnectorSetupField(ConnectorSetupField.FieldNameKey, "API Key", ConnectorSetupField.ConnectorSetupFieldType.String, "", true));
            Setup.Steps.Add(step1);
            var step2 = new ConnectorSetup.ConnectorSetupStep
            {
                Number = 2,
                Header = "Complete installation",
                BottomDescription = "Click Finish to complete the installation."
            };
            Setup.Steps.Add(step2);
        }

        private void AddEntities()
        {
            var listEntity = AddEntity(EntityIdList, "Lists", typeof(MailChimpList));
            var listGrowthEntity = AddEntity(EntityIdListGrowth, "List growth", typeof(MailChimpListGrowth));
            var listAbuseEntity = AddEntity(EntityIdListAbuse, "List abuse reports", typeof(MailChimpListAbuse));
            var listActivityEntity = AddEntity(EntityIdListActivity, "List activity", typeof(MailChimpListActivity));
            var campaignEntity = AddEntity(EntityIdCampaign, "Campaigns", typeof(MailChimpCampaign));
            var campaignFeedbackEntity = AddEntity(EntityIdCampaignFeedback, "Campaign feedback", typeof(MailChimpCampaignFeedback));
            var campaignClickDetailsEntity = AddEntity(EntityIdCampaignClickDetails, "Campaign click summary", typeof(MailChimpCampaignClickDetails));
            var campaignClickMembersEntity = AddEntity(EntityIdCampaignClickMembers, "Campaign clicks", typeof(MailChimpCampaignClickDetailMember));

            listEntity.AddRelatedEntity("Growth", listGrowthEntity, "id", "list_id");
            listEntity.AddRelatedEntity("Abuse reports", listAbuseEntity, "id", "list_id");
            listEntity.AddRelatedEntity("Activity", listActivityEntity, "id", "list_id");
            campaignEntity.AddRelatedEntity("Feedback", campaignFeedbackEntity, "id", "campaign_id");
            campaignEntity.AddRelatedEntity("Click summary", campaignClickDetailsEntity, "id", "campaign_id");
            campaignEntity.AddRelatedEntity("Clicks", campaignClickMembersEntity, "id", "campaign_id");
            campaignClickDetailsEntity.AddRelatedEntity("Clicks", campaignClickMembersEntity, "id", "email_id");

            AddListFavorites(listEntity);
            AddCampaignFavorites(campaignEntity);
            //todo - add more favorites

            AddListActions(listEntity);

            foreach (var entity in Entities) entity.DefaultMaxRecords = 250;
        }

        #region Deserialize

        public List<MailChimpList> DeserializeLists(string data)
        {
            var listCollection = DeserializeJson<MailChimpListCollection>(data);
            return listCollection.lists;
        }
        public List<MailChimpCampaign> DeserializeCampaigns(string data)
        {
            var campaignCollection = DeserializeJson<MailChimpCampaignCollection>(data);
            return campaignCollection.campaigns;
        }
        public List<MailChimpCampaignFeedback> DeserializeCampaignFeedback(string data, ConnectorQuery query)
        {
            var campaignIdRestriction = query.FindRestrictionByFieldAndType("campaign_id", ConnectorRestriction.ConnectorRestrictionType.Equals);
            if (campaignIdRestriction == null)
            {
                var campaignCollection = DeserializeJson<MailChimpCampaignCollection>(data);
                var feedback = new List<MailChimpCampaignFeedback>();
                foreach (var campaign in campaignCollection.campaigns)
                {
                    var feedbackData = GetResponse($"campaigns/{campaign.id}/feedback");
                    var feedbackCollection = DeserializeJson<MailChimpCampaignFeedbackCollection>(feedbackData);
                    foreach (var campaignFeedback in feedbackCollection.feedback) campaignFeedback.campaign = campaign;
                    feedback.AddRange(feedbackCollection.feedback);
                }
                return feedback;
            }
            var campaignFeedbackCollection = DeserializeJson<MailChimpCampaignFeedbackCollection>(data);
            var campaignObject = DeserializeCampaign(campaignFeedbackCollection.campaign_id);
            foreach (var campaignFeedback in campaignFeedbackCollection.feedback) campaignFeedback.campaign = campaignObject;
            return campaignFeedbackCollection.feedback;
        }
        public List<MailChimpCampaignClickDetails> DeserializeCampaignClickDetails(string data, ConnectorQuery query)
        {
            var campaignIdRestriction = query.FindRestrictionByFieldAndType("campaign_id", ConnectorRestriction.ConnectorRestrictionType.Equals);
            if (campaignIdRestriction == null)
            {
                var campaignCollection = DeserializeJson<MailChimpCampaignCollection>(data);
                var clickDetails = new List<MailChimpCampaignClickDetails>();
                foreach (var campaign in campaignCollection.campaigns)
                {
                    var clickDetailsData = GetResponse($"reports/{campaign.id}/click-details");
                    var clickDetailsCollection = DeserializeJson<MailChimpCampaignClickDetailsCollection>(clickDetailsData);
                    foreach (var urls in clickDetailsCollection.urls_clicked) urls.campaign = campaign;
                    clickDetails.AddRange(clickDetailsCollection.urls_clicked);
                }
                return clickDetails;
            }
            var campaignClickDetailsCollection = DeserializeJson<MailChimpCampaignClickDetailsCollection>(data);
            var campaignObject = DeserializeCampaign(campaignClickDetailsCollection.campaign_id);
            foreach (var campaignClickDetails in campaignClickDetailsCollection.urls_clicked) campaignClickDetails.campaign = campaignObject;
            return campaignClickDetailsCollection.urls_clicked;
        }
        public List<MailChimpListGrowth> DeserializeListGrowth(string data, ConnectorQuery query)
        {
            var listIdRestriction = query.FindRestrictionByFieldAndType("list_id", ConnectorRestriction.ConnectorRestrictionType.Equals);
            if (listIdRestriction == null)
            {
                var listCollection = DeserializeJson<MailChimpListCollection>(data);
                if (listCollection.total_items == 0) return null;
                var growth = new List<MailChimpListGrowth>();
                foreach (var list in listCollection.lists)
                {
                    var growthData = GetResponse($"lists/{list.id}/growth-history");
                    var growthCollection = DeserializeJson<MailChimpListGrowthCollection>(growthData);
                    if (growthCollection.total_items > 0)
                    {
                        foreach (var listGrowth in growthCollection.history) listGrowth.list = list;
                        growth.AddRange(growthCollection.history);
                    }
                }
                return growth;
            }
            var listGrowthCollection = DeserializeJson<MailChimpListGrowthCollection>(data);
            if (listGrowthCollection.total_items == 0) return null;
            var listObject = DeserializeList(listGrowthCollection.list_id);
            foreach (var listGrowth in listGrowthCollection.history) listGrowth.list = listObject;
            return listGrowthCollection.history;
        }
        public List<MailChimpListAbuse> DeserializeListAbuse(string data, ConnectorQuery query)
        {
            var listIdRestriction = query.FindRestrictionByFieldAndType("list_id", ConnectorRestriction.ConnectorRestrictionType.Equals);
            if (listIdRestriction == null)
            {
                var listCollection = DeserializeJson<MailChimpListCollection>(data);
                if (listCollection.total_items == 0) return null;
                var abuse = new List<MailChimpListAbuse>();
                foreach (var list in listCollection.lists)
                {
                    var abuseData = GetResponse($"lists/{list.id}/abuse-reports");
                    var abuseCollection = DeserializeJson<MailChimpListAbuseCollection>(abuseData);
                    if (abuseCollection.total_items > 0)
                    {
                        foreach (var abuseReport in abuseCollection.abuse_reports) abuseReport.list = list;
                        abuse.AddRange(abuseCollection.abuse_reports);
                    }
                }
                return abuse;
            }
            var listAbuseCollection = DeserializeJson<MailChimpListAbuseCollection>(data);
            if (listAbuseCollection.total_items == 0) return null;
            var listObject = DeserializeList(listAbuseCollection.list_id);
            foreach (var listAbuse in listAbuseCollection.abuse_reports) listAbuse.list = listObject;
            return listAbuseCollection.abuse_reports;
        }
        public List<MailChimpListActivity> DeserializeListActivity(string data, ConnectorQuery query)
        {
            var listIdRestriction = query.FindRestrictionByFieldAndType("list_id", ConnectorRestriction.ConnectorRestrictionType.Equals);
            if (listIdRestriction == null)
            {
                var listCollection = DeserializeJson<MailChimpListCollection>(data);
                if (listCollection.total_items == 0) return null;
                var activities = new List<MailChimpListActivity>();
                foreach (var list in listCollection.lists)
                {
                    var activityData = GetResponse($"lists/{list.id}/activity");
                    var activityCollection = DeserializeJson<MailChimpListActivityCollection>(activityData);
                    if (activityCollection.total_items > 0)
                    {
                        foreach (var activity in activityCollection.activity) activity.list = list;
                        activities.AddRange(activityCollection.activity);
                    }
                }
                return activities;
            }
            var listActivityCollection = DeserializeJson<MailChimpListActivityCollection>(data);
            if (listActivityCollection.total_items == 0) return null;
            var listObject = DeserializeList(listActivityCollection.list_id);
            foreach (var listAbuse in listActivityCollection.activity) listAbuse.list = listObject;
            return listActivityCollection.activity;
        }
        private MailChimpList DeserializeList(string list_id)
        {
            var listData = GetResponse($"lists/{list_id}");
            return DeserializeJson<MailChimpList>(listData);
        }
        private MailChimpCampaign DeserializeCampaign(string campaign_id)
        {
            var campaignData = GetResponse($"campaigns/{campaign_id}");
            return DeserializeJson<MailChimpCampaign>(campaignData);
        }

        #endregion

        #region Favorites

        private static void AddListFavorites(ConnectorEntity entity)
        {
            var publicFavorite = entity.AddFavorite("Public lists", true);
            publicFavorite.Query.AddRestriction("visibility", ConnectorRestriction.ConnectorRestrictionType.Equals, "pub");

            var privateFavorite = entity.AddFavorite("Private lists", true);
            privateFavorite.Query.AddRestriction("visibility", ConnectorRestriction.ConnectorRestrictionType.Equals, "prv");
        }
        private static void AddCampaignFavorites(ConnectorEntity entity)
        {
            var regularFavorite = entity.AddFavorite("Regular");
            regularFavorite.Query.AddFields("settings_title", "status", "emails_sent");
            regularFavorite.Query.AddRestriction("type", ConnectorRestriction.ConnectorRestrictionType.Equals, "regular");

            var plaintextFavorite = entity.AddFavorite("Plain text");
            plaintextFavorite.Query.AddFields("settings_title", "status", "emails_sent");
            plaintextFavorite.Query.AddRestriction("type", ConnectorRestriction.ConnectorRestrictionType.Equals, "plaintext");

            var absplitFavorite = entity.AddFavorite("A/B split");
            absplitFavorite.Query.AddFields("settings_title", "status", "emails_sent", "ab_split_opts_split_test", "ab_split_opts_pick_winner");
            absplitFavorite.Query.AddRestriction("type", ConnectorRestriction.ConnectorRestrictionType.Equals, "absplit");

            var rssFavorite = entity.AddFavorite("RSS");
            rssFavorite.Query.AddFields("settings_title", "status", "emails_sent", "rss_opts_feed_url", "rss_opts_frequency");
            rssFavorite.Query.AddRestriction("type", ConnectorRestriction.ConnectorRestrictionType.Equals, "rss");

            var variateFavorite = entity.AddFavorite("Variate");
            variateFavorite.Query.AddFields("settings_title", "status", "emails_sent", "variate_settings_number_of_combinations");
            variateFavorite.Query.AddRestriction("type", ConnectorRestriction.ConnectorRestrictionType.Equals, "variate");
        }

        #endregion

        #region Actions

        private void AddListActions(ConnectorEntity entity)
        {
            var actionId = 0;
            var listData = GetResponse("lists");
            var listCollection = DeserializeJson<MailChimpListCollection>(listData);
            foreach (var list in listCollection.lists)
            {
                actionId++;
                var action = entity.AddAction(actionId, "Add subscriber");
                action.AddParameter("id", "id");
                action.AddParameter("Email address", FieldTypeIdEmail);
                var mergeFieldData = GetResponse($"lists/{list.id}/merge-fields");
                var mergeFields = DeserializeJson<MailChimpListMergeFieldCollection>(mergeFieldData);
                foreach (var mergeField in mergeFields.merge_fields.Where(mergeField => mergeField.@public).OrderBy(mergeField => mergeField.display_order))
                {
                    var fieldType = GetActionMergeFieldType(mergeField.type);
                    var parameter = action.AddParameter(mergeField.name, fieldType);
                    parameter.Required = mergeField.required;
                    parameter.HelpText = mergeField.help_text;
                }
                action.AddCondition("id", ConnectorRestriction.ConnectorRestrictionType.Equals, list.id);
            }
        }
        private static int GetActionMergeFieldType(string type)
        {
            switch (type)
            {
                case "number":
                    return FieldTypeIdInteger;
                case "check_box":
                    return FieldTypeIdYesNo;
                case "radio_button":
                case "drop_down":
                    return FieldTypeIdEnum;
                case "date":
                case "birthday":
                    return FieldTypeIdDate;
                case "phone":
                    return FieldTypeIdPhone;
                case "website":
                    return FieldTypeIdUrl;
                case "image":
                    return FieldTypeIdImage;
                default:
                    return FieldTypeIdString;
            }
        }

        #endregion

        #endregion

    }
}
