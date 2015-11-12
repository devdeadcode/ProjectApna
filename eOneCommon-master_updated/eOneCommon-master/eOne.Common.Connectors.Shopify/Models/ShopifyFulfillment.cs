using System;

namespace eOne.Common.Connectors.Shopify.Models
{
    public class ShopifyFulfillment : ConnectorEntityModel
    {

        public DateTime? created_at { get; set; }
        public string id { get; set; }
        public string order_id { get; set; }
        public string status { get; set; }
        public string tracking_company { get; set; }
        public string tracking_number { get; set; }
        public DateTime? updated_at { get; set; }

    }
}
