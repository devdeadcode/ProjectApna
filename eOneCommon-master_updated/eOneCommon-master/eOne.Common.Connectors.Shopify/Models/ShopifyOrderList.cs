using System.Collections.Generic;

namespace eOne.Common.Connectors.Shopify.Models
{
    public class ShopifyOrderList : ConnectorEntityModel
    {

        public List<ShopifyOrder> orders { get; set; }

    }
}
