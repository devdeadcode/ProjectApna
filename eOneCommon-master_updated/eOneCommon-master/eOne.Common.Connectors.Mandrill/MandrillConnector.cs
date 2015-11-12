using System;
using System.Collections.Generic;
using eOne.Common.Actions;
using eOne.Common.Connectors.Mandrill.Models;
using eOne.Common.Connectors.Service;
using eOne.Common.Query;

namespace eOne.Common.Connectors.Mandrill
{
    public class MandrillConnector : RestConnector
    {

        #region Entity IDs

        public const int EntityIdSubaccount = 1;
        public const int EntityIdTag = 2;
        public const int EntityIdSender = 3;
        public const int EntityIdWhitelist = 4;
        public const int EntityIdExport = 5;
        public const int EntityIdRejection = 6;
        public const int EntityIdMostClickedUrl = 7;
        public const int EntityIdTemplate = 8;
        public const int EntityIdScheduledEmail = 9;
        public const int EntityIdSentEmail = 10;

        #endregion

        public MandrillConnector()
        {
            Name = "Mandrill";
            Group = ConnectorGroup.MailingList;
            BaseUrl = "https://mandrillapp.com/api/1.0/";
            ConnectorMethod = RestConnectorMethod.Post;
            AddSetup();
        }

        #region Public methods

        public override void Initialise()
        {
            base.Initialise();
            AddEntity(EntityIdSubaccount, "Subaccounts", typeof(MandrillSubaccount));
            AddEntity(EntityIdTag, "Tags", typeof(MandrillTag));
            AddEntity(EntityIdSender, "Senders", typeof(MandrillSender));
            AddEntity(EntityIdWhitelist, "Whitelisted email addresses", typeof(MandrillWhitelist));
            AddEntity(EntityIdExport, "Exports", typeof(MandrillExport));
            AddEntity(EntityIdRejection, "Email rejection blacklist", typeof(MandrillReject));
            AddEntity(EntityIdMostClickedUrl, "Most clicked URLs", typeof(MandrillUrl));
            AddEntity(EntityIdTemplate, "Templates", typeof(MandrillTemplate));
            AddEntity(EntityIdScheduledEmail, "Scheduled emails", typeof(MandrillMessage));
            AddEntity(EntityIdSentEmail, "Sent emails", typeof(MandrillMessage));
        }

        public override string GetEndpoint(ConnectorQuery query)
        {
            if (query == null) return string.Empty;
            switch (query.Entity.Id)
            {
                case EntityIdSubaccount:
                    return "subaccounts/list.json";
                case EntityIdTag:
                    return "tags/list.json";
                case EntityIdSender:
                    return "senders/list.json";
                case EntityIdWhitelist:
                    return "whitelists/list.json";
                case EntityIdExport:
                    return "exports/list.json";
                case EntityIdRejection:
                    return "rejects/list.json";
                case EntityIdMostClickedUrl:
                    return "urls/list.json";
                case EntityIdTemplate:
                    return "templates/list.json";
                case EntityIdScheduledEmail:
                    return "messages/list-scheduled.json";
                case EntityIdSentEmail:
                    return "messages/search.json";
            }
            return string.Empty;
        }

        public override IEnumerable<object> Deserialize(string data, ConnectorQuery query)
        {
            switch (query.Entity.Id)
            {
                case EntityIdSubaccount:
                    return DeserializeJson<List<MandrillSubaccount>>(data);
                case EntityIdTag:
                    return DeserializeJson<List<MandrillTag>>(data);
                case EntityIdSender:
                    return DeserializeJson<List<MandrillSender>>(data);
                case EntityIdWhitelist:
                    return DeserializeJson<List<MandrillWhitelist>>(data);
                case EntityIdExport:
                    return DeserializeJson<List<MandrillExport>>(data);
                case EntityIdRejection:
                    return DeserializeJson<List<MandrillReject>>(data);
                case EntityIdMostClickedUrl:
                    return DeserializeJson<List<MandrillUrl>>(data);
                case EntityIdTemplate:
                    return DeserializeJson<List<MandrillTemplate>>(data);
                case EntityIdScheduledEmail:
                    return DeserializeJson<List<MandrillMessage>>(data);
                case EntityIdSentEmail:
                    return DeserializeJson<List<MandrillMessage>>(data);
            }
            return null;
        }

        public override void RunAction(ConnectorAction action, List<Tuple<string, string>> parameters)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
