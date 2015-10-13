using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Shopify.Models
{
    public class ShopifyOrderList : DataConnectorEntityModel
    {

        public List<ShopifyOrder> orders { get; set; }

    }
}
