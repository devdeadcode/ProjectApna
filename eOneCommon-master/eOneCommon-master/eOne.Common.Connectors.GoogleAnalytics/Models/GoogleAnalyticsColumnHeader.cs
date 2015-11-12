using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.GoogleAnalytics.Models
{
    public class GoogleAnalyticsColumnHeader : DataConnectorEntityModel
    {

        public string name { get; set; }
        public string columnType { get; set; }
        public string dataType { get; set; }

        public string updatedName => name.Remove(0, 3);

    }
}
