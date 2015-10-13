using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Shopify.Models
{
    public class ShopifyShippingLine : DataConnectorEntityModel
    {

        public string code { get; set; }
        public decimal price { get; set; }
        public string source { get; set; }
        public string title { get; set; }
        public List<ShopifyTaxLine> tax_lines { get; set; }

    }
}
