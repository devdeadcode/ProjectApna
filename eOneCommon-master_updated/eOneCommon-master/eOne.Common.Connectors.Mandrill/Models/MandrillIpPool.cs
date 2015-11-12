using System;
using System.Collections.Generic;

namespace eOne.Common.Connectors.Mandrill.Models
{
    public class MandrillIpPool : ConnectorEntityModel
    {

        public string name { get; set; }
        public DateTime created_at { get; set; }
        public List<MandrillIp> ips { get; set; }

        public DateTime created_at_time => created_at;
    }
}
