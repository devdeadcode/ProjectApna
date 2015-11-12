using System;
using System.Collections.Generic;
using eOne.Common.Actions;
using eOne.Common.Connectors.Service;
using eOne.Common.Query;

namespace eOne.Common.Connectors.Asana
{
    public class AsanaConnector : RestConnector
    {
        
        public AsanaConnector()
        {
            Name = "Asana";
            Group = ConnectorGroup.ToDoList;
            AddSetup();
        }
        
        public override void Initialise()
        {
            base.Initialise();
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
