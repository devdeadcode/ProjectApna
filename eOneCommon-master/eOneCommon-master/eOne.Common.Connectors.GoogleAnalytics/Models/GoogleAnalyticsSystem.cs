using eOne.Common.DataConnectors;
using System.Collections.Generic;

namespace eOne.Common.Connectors.GoogleAnalytics.Models
{
    public class GoogleAnalyticsSystem : DataConnectorEntityModel
    {
        public string flashVersion { get; set; }

        public string javaEnabled { get; set; }

        public string language { get; set; }

        public string screenColors { get; set; }

        public string screenResolutions { get; set; }
    }
}
