using eOne.Common.DataConnectors;
using System.Collections.Generic;

namespace eOne.Common.Connectors.GoogleAnalytics.Models
{
    public class GoogleAnalyticsAudience : DataConnectorEntityModel
    {
        public string userGender { get; set; }

        public string userAgeBracket { get; set; }
    }
}
