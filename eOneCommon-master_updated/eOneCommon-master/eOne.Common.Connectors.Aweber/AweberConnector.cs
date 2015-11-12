using System;
using System.Collections.Generic;
using eOne.Common.Actions;
using eOne.Common.Connectors.Service;
using eOne.Common.Query;

namespace eOne.Common.Connectors.Aweber
{
    public class AweberConnector : RestConnector
    {

        #region Constants

        #region Entity IDs



        #endregion

        #region Action IDs



        #endregion

        #endregion

        public AweberConnector()
        {
            Name = "AWeber";
            Group = ConnectorGroup.MailingList;

            Key = "Ak6l1NFtqgJ7K3dtWl2eI5v1";
            Secret = "GTe8uhueIW5BRGyxA7eCf1Odn5PbtI5a795t11gj";

            AuthenticationType = ServiceConnectorAuthenticationType.OAuth1;

        }

        public override void Initialise()
        {
            base.Initialise();

            AddEntity(1, "");

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
