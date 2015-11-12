using System;

namespace eOne.Common.Connectors.Mandrill.Models
{
    public class MandrillWarmup : ConnectorEntityModel
    {

        public bool warming_up { get; set; }
        public DateTime start_at { get; set; }
        public DateTime end_at { get; set; }

        public DateTime start_at_time => start_at;

        public DateTime end_at_time => end_at;
    }
}
