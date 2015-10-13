using System;
using System.Collections.Generic;
using eOne.Common.DataConnectors.Rest;

namespace eOne.Common.Connectors.HubSpot
{
    public class HubSpotConnector : RestConnector
    {
        
        public HubSpotConnector()
        {
            Name = "HubSpot";
            Group = ConnectorGroup.CRM;
        }
        
        public override void Initialise()
        {
            throw new NotImplementedException();
        }

        public override void RunAction(ConnectorAction action, List<Tuple<string, string>> parameters)
        {
            throw new NotImplementedException();
        }

        public override string GetEndpoint(ConnectorQuery query)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<object> Deserialize(string data, ConnectorQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
