using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Shopify.Models
{
    public class ShopifyCustomerList : DataConnectorEntityModel
    {

        public List<ShopifyCustomer> customers { get; set; }

    }
}
