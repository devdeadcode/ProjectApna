using System;
using System.Collections.Generic;
using eOne.Common.DataConnectors.Rest;

namespace eOne.Common.Connectors.FreshBooks
{
    public class FreshBooksConnector : RestConnector
    {
        
        public FreshBooksConnector()
        {
            Name = "Freshbooks";
            Group = ConnectorGroup.ERP;
            AddSetup();
        }

        public override void Initialise()
        {
            AuthenticationType = RestConnectorAuthenticationType.OAuth1;
            BaseUrl = "https://{0}.freshbooks.com/api/2.1/xml-in";
            Key = "popdock";
            AccessTokenUri = "https://{0}.freshbooks.com/oauth/oauth_access.php";
            AuthorizationUri = "https://{0}.freshbooks.com/oauth/oauth_authorize.php";
            // "https://{0}.freshbooks.com/oauth/oauth_request.php";
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
