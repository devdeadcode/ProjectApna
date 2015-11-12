using System.Collections.Generic;

namespace eOne.Common.Connectors.Shopify.Models
{
    public class ShopifyCustomerList : ConnectorEntityModel
    {

        public List<ShopifyCustomer> customers { get; set; }

    }
}
