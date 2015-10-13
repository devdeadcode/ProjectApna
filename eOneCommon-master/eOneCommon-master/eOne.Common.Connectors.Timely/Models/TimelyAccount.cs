using System;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Timely.Models
{
    public class TimelyAccount : DataConnectorEntityModel
    {

        public int id { get; set; }
        public string name { get; set; }
        public string from { get; set; }
        public int max_users { get; set; }
        public int max_projects { get; set; }
        public int num_users { get; set; }
        public int num_projects { get; set; }
        public int plan_id { get; set; }
        public string plan_name { get; set; }
        public DateTime next_charge { get; set; }
        public TimelyCurrency currency { get; set; }

    }
}


