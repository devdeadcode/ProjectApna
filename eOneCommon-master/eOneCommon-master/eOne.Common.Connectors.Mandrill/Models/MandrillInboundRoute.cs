using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Mandrill.Models
{
    public class MandrillInboundRoute : DataConnectorEntityModel
    {

        public string id { get; set; }
        public string pattern { get; set; }
        public string url { get; set; }

    }
}
