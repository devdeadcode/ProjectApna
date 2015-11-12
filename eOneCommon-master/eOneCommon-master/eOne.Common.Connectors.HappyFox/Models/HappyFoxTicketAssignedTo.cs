using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.HappyFox.Models
{
    public class HappyFoxTicketAssignedTo : DataConnectorEntityModel
    {
        public string name { get; set; }

        public string email { get; set; }

        public int id { get; set; }

        public bool active { get; set; }

        public HappyFoxStaffRole role { get; set; }

        public string role_name => role == null ? string.Empty : role.name;

        public int role_id => role?.id ?? 0;
        
        
    }
}
