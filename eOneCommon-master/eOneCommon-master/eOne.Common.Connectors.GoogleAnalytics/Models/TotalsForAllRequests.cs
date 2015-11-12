using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.GoogleAnalytics.Models
{
    public class TotalsForAllRequests : DataConnectorEntityModel
    {
        public string users { get; set; }

        public string pageViews { get; set; }

        public string newUsers { get; set; }
    }
}
