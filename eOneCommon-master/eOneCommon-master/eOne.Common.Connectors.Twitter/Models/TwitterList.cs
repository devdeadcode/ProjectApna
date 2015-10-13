using System;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Twitter.Models
{
    public class TwitterList : DataConnectorEntityModel
    {

        public enum TwitterListMode
        {
            // todo - determine other modes
            @public
        }

        public string slug { get; set; }
        public string name { get; set; }
        public DateTime created_at { get; set; }
        public string uri { get; set; }
        public int subscriber_count { get; set; }
        public int member_count { get; set; }
        public TwitterListMode mode { get; set; }
        public string id_str { get; set; }
        public int id { get; set; }
        public string full_name { get; set; }
        public string description { get; set; }
        public TwitterUser user { get; set; }
        public bool following { get; set; }

    }
}