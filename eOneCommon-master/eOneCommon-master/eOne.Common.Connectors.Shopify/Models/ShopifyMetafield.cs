using System;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Shopify.Models
{
    public class ShopifyMetafield : DataConnectorEntityModel
    {

        public DateTime? created_at { get; set; }
        public DateTime created_at_date
        {
            get
            {
                return created_at?.Date ?? DateTime.MinValue;
            }
            set
            {
                created_at = value;
            }
        }
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
