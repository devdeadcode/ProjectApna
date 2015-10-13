using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Shopify.Models
{
    public class ShopifyTaxLine : DataConnectorEntityModel
    {

        public decimal price { get; set; }
        public decimal rate { get; set; }
        public string title { get; set; }

    }
}
