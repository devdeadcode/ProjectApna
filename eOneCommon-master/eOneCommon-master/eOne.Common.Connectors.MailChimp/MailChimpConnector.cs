using System;
using System.Collections.Generic;
using System.Net;
using eOne.Common.Connectors.MailChimp.Models;
using eOne.Common.DataConnectors;
using eOne.Common.DataConnectors.Rest;
using RestSharp;

namespace eOne.Common.Connectors.MailChimp
{
    public class MailChimpConnector : RestConnector
    {

        #region Entity IDs

        public const int EntityTypeIdList = 1;
        public const int EntityTypeIdCampaign = 2;
        public const int EntityTypeIdSubscriber = 3;

        #endregion

        public MailChimpConnector()
        {
            Name = "MailChimp";
            Group = ConnectorGroup.MailingList;
            BaseUrl = "https://{0}.api.mailchimp.com/2.0/";
            AuthenticationType = RestConnectorAuthenticationType.OAuth2;
            AuthorizationUri = "https://login.mailchimp.com/oauth2/authorize";
            AccessTokenUri = "https://login.mailchimp.com/oauth2/token";
            CallbackUrl = "http://www.popdock.com/callbacks/mailchimp";
            ClientId = "548227577004";
            Secret = "9110e9ca529d76c4d5191f01714da53c";
            ConnectorMethod = RestConnectorMethod.Post;
            AddSetup();
        }

        #region Public methods

        public override void Initialise()
        {
            // get datacenter from metadata call
            var client = new RestClient("https://login.mailchimp.com");
            var request = new RestRequest("oauth2/metadata", Method.GET);
            request.AddHeader("Authorization", $"OAuth {Token}");
            var response = client.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var metadata = DeserializeJson<MailChimpMetadata>(response.Content);
                BaseUrl = string.Format(BaseUrl, metadata.dc);
                
                AddEntity(EntityTypeIdList, "Lists", typeof(MailChimpListData));
                AddEntity(EntityTypeIdCampaign, "Campaigns", typeof(MailChimpCampaignData));
            }
        }

        public override string GetEndpoint(ConnectorQuery query)
        {
            if (query == null) return string.Empty;
            switch (query.Entity.Id)
            {
                case EntityTypeIdCampaign:
                    return $"{"campaigns"}/{"list"}.json";
                case EntityTypeIdList:
                    return $"{"lists"}/{"list"}.json";
            }
            return string.Empty; 
        }

        public override List<Tuple<string, string>> GetHeaders(DataConnectorEntity entity)
        {
            var headers = new List<Tuple<string, string>>
            {
                new Tuple<string, string>("Authorization", $"OAuth {Token}")
            };
            return headers;
        }

        public override List<Tuple<string, string>> GetUrlParameters(ConnectorQuery query)
        {
            var parameters = new List<Tuple<string, string>>
            {
                new Tuple<string, string>("apikey", Key)
            };
            return parameters;
        }

        public override IEnumerable<object> Deserialize(string data, ConnectorQuery query)
        {
            switch (query.Entity.Id)
            {
                case EntityTypeIdCampaign:
                    var campaignList = DeserializeJson<MailChimpCampaign>(data);
                    return campaignList.data;
                case EntityTypeIdList:
                    var listList = DeserializeJson<MailChimpList>(data);
                    return listList.data;
            }
            return null;
        }

        public override void RunAction(ConnectorAction action, List<Tuple<string, string>> parameters)
        {
            throw new NotImplementedException();
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

        #endregion

    }
}
