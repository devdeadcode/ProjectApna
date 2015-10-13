using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.GoogleAnalytics.Models
{
    public class GoogleAnalyticsProfileInfo : DataConnectorEntityModel
    {

        public string profileId { get; set; }
        public string accountId { get; set; }
        public string webPropertyId { get; set; }
        public string internalWebPropertyId { get; set; }
        public string profileName { get; set; }
        public string tableId { get; set; }

    }
}

