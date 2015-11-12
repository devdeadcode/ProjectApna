using System;
using System.Collections.Generic;
using eOne.Common.Actions;
using eOne.Common.Query;
using UserVoice;

namespace eOne.Common.Connectors.UserVoice
{
    public class UserVoiceConnector : Connector
    {

        static string SUBDOMAIN_NAME = "uservoice";
        static string API_KEY = "oQt2BaunWNuainc8BvZpAm";
        static string API_SECRET = "3yQMSoXBpAwuK3nYHR0wpY6opE341inL9a2HynGF2";
        static string SSO_KEY = "982c88f2df72572859e8e23423eg87ed";
        static string CALLBACK = "http://localhost:4567/";

        public UserVoiceConnector()
        {
            Name = "UserVoice";
            Group = ConnectorGroup.Helpdesk;
        }
        
        public override void Initialise()
        {
            base.Initialise();
        }

        public override void RunAction(ConnectorAction action, List<Tuple<string, string>> parameters)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<object> GetRecords(ConnectorQuery query)
        {
            var client = new Client(SUBDOMAIN_NAME, API_KEY, API_SECRET, CALLBACK);
            //return client.GetCollection("/api/v1/suggestions?sort=newest");
            return null;
        }

    }
}
