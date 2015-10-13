using System;
using System.Collections.Generic;
using eOne.Common.DataConnectors.Rest;

namespace eOne.Common.Connectors.Intacct
{
    public class IntacctConnector : RestConnector
    {
        
        public IntacctConnector()
        {
            Name = "Intacct";
            Group = ConnectorGroup.ERP;
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
