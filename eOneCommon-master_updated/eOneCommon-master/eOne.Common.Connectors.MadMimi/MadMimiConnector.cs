using System;
using System.Collections.Generic;
using eOne.Common.Actions;
using eOne.Common.Connectors.MadMimi.Models;
using eOne.Common.Connectors.Service;
using eOne.Common.Helpers;
using eOne.Common.Query;

namespace eOne.Common.Connectors.MadMimi
{
    public class MadMimiConnector : RestConnector
    {

        #region Constants

        #region Entity IDs

        public const int EntityIdAudienceList = 1;
        public const int EntityIdAudienceMember = 2;
        public const int EntityIdPromotion = 3;
        public const int EntityIdMailing = 4;
        public const int EntityIdMailingRecipients = 5;
        public const int EntityIdReadStats = 7;
        public const int EntityIdClickedStats = 8;
        public const int EntityIdUnsubscribedStats = 9;
        public const int EntityIdBouncedStats = 10;
        public const int EntityIdForwardedStats = 11;
        public const int EntityIdAbusedStats = 12;

        #endregion

        #region Action IDs

        public const int ActionIdAddListMember = 1;

        #endregion

        #endregion

        public MadMimiConnector()
        {
            Name = "Mad Mimi";
            Group = ConnectorGroup.MailingList;
            AuthenticationType = ServiceConnectorAuthenticationType.UrlParameter;
            SerializationType = ServiceConnectorSerializationType.Xml;
            BaseUrl = "https://api.madmimi.com";
        }

        public override void Initialise()
        {
            base.Initialise();

            var listEntity = AddEntity(EntityIdAudienceList, "Lists", typeof(MadMimiAudienceList));
            var memberEntity = AddEntity(EntityIdAudienceMember, "Contacts", typeof(MadMimiAudience));
            var promotionEntity = AddEntity(EntityIdPromotion, "Promotions", typeof(MadMimiPromotion));
            var mailingEntity = AddEntity(EntityIdMailing, "Mailings", typeof(MadMimiMailing));
            var recipientEntity = AddEntity(EntityIdMailingRecipients, "");
            //var readEntity = AddEntity(EntityIdReadStats, "");
            //var clickEntity = AddEntity(EntityIdClickedStats, "");
            //var unsubscribeEntity = AddEntity(EntityIdUnsubscribedStats, "");
            //var bounceEntity = AddEntity(EntityIdBouncedStats, "");
            //var forwardEntity = AddEntity(EntityIdForwardedStats, "");
            //var abuseEntity = AddEntity(EntityIdAbusedStats, "");

            //listEntity.AddRelatedEntity("Members", memberEntity, "", "");
            //promotionEntity.AddRelatedEntity("Mailings", mailingEntity, "", "");
            //mailingEntity.AddRelatedEntity("Statistics", mailingStatsEntity, "", "");
            //mailingEntity.AddRelatedEntity("Sent", sentEntity, "", "");
            //mailingEntity.AddRelatedEntity("Read", readEntity, "", "");
            //mailingEntity.AddRelatedEntity("Clicked", clickEntity, "", "");
            //mailingEntity.AddRelatedEntity("Unsubscribed", unsubscribeEntity, "", "");
            //mailingEntity.AddRelatedEntity("Bounced", bounceEntity, "", "");
            //mailingEntity.AddRelatedEntity("Forwarded", forwardEntity, "", "");
            //mailingEntity.AddRelatedEntity("Abuses", abuseEntity, "", "");

            foreach (var entity in Entities) entity.DefaultMaxRecords = 100;

        }

        public override string GetEndpoint(ConnectorQuery query)
        {
            switch (query.Entity.Id)
            {
                case EntityIdAudienceList:
                    return "audience_lists/lists.xml";
                case EntityIdAudienceMember:
                    return "audience_members.xml";
                case EntityIdPromotion:
                case EntityIdMailing:
                    return "promotions.xml";
            }
            return string.Empty;
        }

        public override List<Tuple<string, string>> GetParameters(ConnectorQuery query)
        {
            var parameters = TupleHelper.CreateTupleStringList("api_key", Key, "username", Username, "page", query.Page.ToString());
            return parameters;
        }

        public override IEnumerable<object> Deserialize(string data, ConnectorQuery query)
        {
            switch (query.Entity.Id)
            {
                case EntityIdPromotion:
                    var promotionCollection = DeserializeXml<MadMimiPromotionCollection>(data);
                    return promotionCollection.promotion;
                case EntityIdMailing:
                    var mailings = new List<MadMimiMailing>();
                    var mailingPromotionCollection = DeserializeXml<MadMimiPromotionCollection>(data);
                    foreach (var promotion in mailingPromotionCollection.promotion)
                    {
                        foreach (var mailing in promotion.mailings)
                        {
                            var mailingStatisticsData = GetResponse($"promotions/{promotion.id}/mailings/{mailing.id}.xml");
                            mailing.statistics = DeserializeXml<MadMimiMailingStatistics>(mailingStatisticsData);
                            mailing.promotion = promotion;
                            mailings.Add(mailing);
                        }
                    }
                    return mailings;
                case EntityIdAudienceMember:
                    var memberCollection = DeserializeXml<MadMimiAudienceCollection>(data);
                    return memberCollection.member;
            }
            return null;
        }

        public override void RunAction(ConnectorAction action, List<Tuple<string, string>> parameters)
        {
            throw new NotImplementedException();
        }

    }
}
