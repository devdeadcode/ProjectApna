using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.GoogleAnalytics.Models
{
    public class GoogleAnalyticsTime : DataConnectorEntityModel
    {
        public string date { get; set; }

        public string year { get; set; }

        public string month { get; set; }

        public string week { get; set; }

        public string day_of_month { get; set; }

        public string hour { get; set; }

        public string day_of_week { get; set; }
    }
}
