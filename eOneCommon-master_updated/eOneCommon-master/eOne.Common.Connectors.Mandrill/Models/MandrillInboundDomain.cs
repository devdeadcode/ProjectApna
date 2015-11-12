using System;

namespace eOne.Common.Connectors.Mandrill.Models
{
    public class MandrillInboundDomain : ConnectorEntityModel
    {

        public string domain { get; set; }
        public bool valid_mx { get; set; }

        public DateTime created_at { get; set; }

        public DateTime created_at_time => created_at;
    }
}