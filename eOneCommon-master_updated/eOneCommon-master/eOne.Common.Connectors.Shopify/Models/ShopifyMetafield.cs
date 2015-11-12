using System;

namespace eOne.Common.Connectors.Shopify.Models
{
    public class ShopifyMetafield : ConnectorEntityModel
    {

        public DateTime? created_at { get; set; }
        public string description { get; set; }
        public string id { get; set; }
        public string key { get; set; }
        public string @namespace { get; set; }
        public string owner_id { get; set; }
        public string owner_resource { get; set; }
        public string value { get; set; }
        public string value_type { get; set; }
        public string updated_at { get; set; }

    }
}
