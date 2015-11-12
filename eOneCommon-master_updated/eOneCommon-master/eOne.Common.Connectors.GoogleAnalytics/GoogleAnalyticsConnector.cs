using System;
using System.Collections.Generic;
using eOne.Common.Connectors.GoogleAnalytics.Models;
using eOne.Common.DataConnectors.Rest;
using eOne.Common.DataConnectors.Service;

namespace eOne.Common.Connectors.GoogleAnalytics
{
    public class GoogleAnalyticsConnector : RestConnector
    {

        #region Entity IDs

        public const int EntityIdData = 1;

        #endregion

        public GoogleAnalyticsConnector()
        {
            Name = "Google Analytics";
            Group = ConnectorGroup.Other;
            Key = "";
            Secret = "";
            AuthenticationType = ServiceConnectorAuthenticationType.OAuth2;
            BaseUrl = "https://www.googleapis.com/analytics/v3/data/ga";
            AuthorizationUri = "https://accounts.google.com/o/oauth2/auth";
            AccessTokenUri = "https://www.googleapis.com/oauth2/v3/token";
            AddSetup();
        }

        #region Public methods

        public override void Initialise()
        {
            base.Initialise();
            // todo - create new model with each field as a separate property
            AddEntity(EntityIdData, "Analytics data", typeof(GoogleAnalyticsData));
        }

        public override string GetEndpoint(ConnectorQuery query)
        {
            return BaseUrl;
        }

        public override IEnumerable<object> Deserialize(string data, ConnectorQuery query)
        {
            var analyticsData = DeserializeJson<GoogleAnalyticsData>(data);

            // todo - convert into list of values
            return null;
        }

        public override void RunAction(ConnectorAction action, List<Tuple<string, string>> parameters)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
