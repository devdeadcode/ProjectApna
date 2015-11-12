using System.Collections.Generic;

namespace eOne.Common.Connectors.Shopify.Models
{
    public class ShopifyShippingLine : ConnectorEntityModel
    {

        public string code { get; set; }
        public decimal price { get; set; }
        public string source { get; set; }
        public string title { get; set; }
        public List<ShopifyTaxLine> tax_lines { get; set; }

    }
}
