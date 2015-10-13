using eOne.Common.DataConnectors;
using System.Collections.Generic;

namespace eOne.Common.Connectors.GoogleAnalytics.Models
{
    public class GoogleAnalyticsAudience : DataConnectorEntityModel
    {
        public string gender { get; set; }

        public string age_bracket { get; set; }
    }
}
