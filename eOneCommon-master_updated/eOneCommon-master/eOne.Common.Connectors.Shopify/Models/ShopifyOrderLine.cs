using System.Collections.Generic;

namespace eOne.Common.Connectors.Shopify.Models
{
    public class ShopifyOrderLine : ConnectorEntityModel
    {

        public int fulfillable_quantity { get; set; }
        public string fulfillment_service { get; set; }
        public string fulfillment_status { get; set; }
        public int grams { get; set; }
        public string id { get; set; }
        public decimal price { get; set; }
        public string product_id { get; set; }
        public int quantity { get; set; }
        public bool requires_shipping { get; set; }
        public string sku { get; set; }
        public string title { get; set; }
        public string variant_id { get; set; }
        public string variant_title { get; set; }
        public string vendor { get; set; }
        public string name { get; set; }
        public bool gift_card { get; set; }
        public bool taxable { get; set; }
        public List<ShopifyTaxLine> tax_lines { get; set; }

    }
}
