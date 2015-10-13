using System;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Zendesk.Models
{
    public class ZendeskGroup : DataConnectorEntityModel
    {

        public int id { get; set; }
        public string url { get; set; }
        public bool deleted { get; set; }
        public string name { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        public DateTime created_at_date => created_at.Date;

        public DateTime created_at_time => Time(created_at);
    }
}
