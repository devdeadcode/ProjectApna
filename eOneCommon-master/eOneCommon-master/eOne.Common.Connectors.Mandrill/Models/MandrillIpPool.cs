using System;
using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Mandrill.Models
{
    public class MandrillIpPool : DataConnectorEntityModel
    {

        public string name { get; set; }
        public DateTime created_at { get; set; }
        public List<MandrillIp> ips { get; set; }

        public DateTime created_at_date => created_at.Date;

        public DateTime created_at_time => Time(created_at);
    }
}
