using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Shopify.Models
{
    public class ShopifyProductOption : DataConnectorEntityModel
    {

        public string id { get; set; }
        public string name { get; set; }
        public int position { get; set; }
        public string product_id { get; set; }

    }
}
