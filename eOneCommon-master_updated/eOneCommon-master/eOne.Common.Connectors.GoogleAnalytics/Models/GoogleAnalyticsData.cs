using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.GoogleAnalytics.Models
{
    public class GoogleAnalyticsData : DataConnectorEntityModel
    {

        public string kind { get; set; }
        public string id { get; set; }
        public string selfLink { get; set; }
        public bool containsSampledData { get; set; }
        public int itemsPerPage { get; set; }
        public int totalResults { get; set; }
        public string previousLink { get; set; }
        public string nextLink { get; set; }
        public string sampleSize { get; set; }
        public string sampleSpace { get; set; }
        public GoogleAnalyticsProfileInfo profileInfo { get; set; }
        public List<GoogleAnalyticsColumnHeader> columnHeaders { get; set; }
        public List<List<string>> rows { get; set; }

    }
}