using System;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Mandrill.Models
{
    public class MandrillWarmup : DataConnectorEntityModel
    {

        public bool warming_up { get; set; }
        public DateTime start_at { get; set; }
        public DateTime end_at { get; set; }

        public DateTime start_at_date => start_at.Date;

        public DateTime start_at_time => Time(start_at);

        public DateTime end_at_date => end_at.Date;

        public DateTime end_at_time => Time(end_at);
    }
}
