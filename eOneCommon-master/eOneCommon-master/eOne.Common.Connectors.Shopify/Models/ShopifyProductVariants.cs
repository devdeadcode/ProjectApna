using System;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Shopify.Models
{
    public class ShopifyProductVariants : DataConnectorEntityModel
    {

        public string barcode { get; set; }

        public decimal? compare_at_price { get; set; }

        public DateTime? created_at { get; set; }

        public string fulfillment_service { get; set; } // enum 

        public int grams { get; set; }

        public string id { get; set; }

        public string inventory_management { get; set; }

        public int inventory_quantity { get; set; }

        public int old_inventory_quantity { get; set; }

        public int inventory_quantity_adjustment { get; set; }

        public ShopifyMetafield metafield { get; set; } 

        public int position { get; set; }

        public decimal? price { get; set; }

        public string product_id { get; set; }

        public bool requires_shipping { get; set; }

        public string sku { get; set; }

        public bool taxable { get; set; }

        public string title { get; set; }

        public DateTime? updated_at { get; set; }

    }
}
