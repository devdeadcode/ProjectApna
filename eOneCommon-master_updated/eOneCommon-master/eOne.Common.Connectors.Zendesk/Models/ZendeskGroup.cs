using System;

namespace eOne.Common.Connectors.Zendesk.Models
{
    public class ZendeskGroup : ConnectorEntityModel
    {

        public int id { get; set; }
        public string url { get; set; }
        public bool deleted { get; set; }
        public string name { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        public DateTime created_at_time => created_at;
    }
}
