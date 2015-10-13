using System;
using System.Collections.Generic;
using eOne.Common.DataConnectors.Rest;

namespace eOne.Common.Connectors.Asana
{
    public class AsanaConnector : RestConnector
    {
        #region Entity IDs
        public const int EntityIdEvent = 1;
        public const int EntityIdProject = 2;
        public const int EntityIdStory = 3;
        public const int EntityIdTask = 4;
        public const int EntityIdTeam = 5;
        public const int EntityIdUser = 6;
        #endregion

        public AsanaConnector()
        {
            Name = "Asana";
            Group = ConnectorGroup.ToDoList;
            AuthenticationType = RestConnectorAuthenticationType.OAuth2;
            BaseUrl = "https://app.asana.com/api/1.0/";
            AuthorizationUri = "https://app.asana.com/-/oauth_authorize";
            AccessTokenUri = "https://app.asana.com/-/oauth_token";
            Key = "56897457769754";
            Secret = "70feb4f1b5d2f24d8b4d180c9924c2c1";
            AddSetup();
        }
        
        public override void Initialise()
        {
            AddSetup();
        }

        public override string GetEndpoint(ConnectorQuery query)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<object> Deserialize(string data, ConnectorQuery query)
        {
            throw new NotImplementedException();
        }

        public override void RunAction(ConnectorAction action, List<Tuple<string, string>> parameters)
        {
            throw new NotImplementedException();
        }

    }
}
