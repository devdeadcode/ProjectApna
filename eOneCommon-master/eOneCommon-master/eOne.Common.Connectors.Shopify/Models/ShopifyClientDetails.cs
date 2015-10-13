using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Shopify.Models
{
    public class ShopifyClientDetails : DataConnectorEntityModel
    {

        public string accept_language { get; set; }
        public int? browser_height { get; set; }
        public string browser_ip { get; set; }
        public int? browser_width { get; set; }
        public string session_hash { get; set; }
        public string user_agent { get; set; }

    }
}
