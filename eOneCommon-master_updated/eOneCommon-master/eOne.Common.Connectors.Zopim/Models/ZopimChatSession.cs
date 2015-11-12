using System;

namespace eOne.Common.Connectors.Zopim.Models
{
    public class ZopimChatSession : ConnectorEntityModel
    {

        public string city { get; set; }
        public DateTime end_date { get; set; }
        public string ip { get; set; }
        public string region { get; set; }
        public string id { get; set; }
        public string platform { get; set; }
        public string user_agent { get; set; }
        public string country_code { get; set; }
        public string country_name { get; set; }
        public DateTime start_date { get; set; }
        public string browser { get; set; }

    }
}