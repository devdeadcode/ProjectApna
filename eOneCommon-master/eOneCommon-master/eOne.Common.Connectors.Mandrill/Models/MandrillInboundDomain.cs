using System;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Mandrill.Models
{
    public class MandrillInboundDomain : DataConnectorEntityModel
    {

        public string domain { get; set; }
        public bool valid_mx { get; set; }

        public DateTime created_at { get; set; }

        public DateTime created_at_date => created_at.Date;

        public DateTime created_at_time => Time(created_at);
    }
}