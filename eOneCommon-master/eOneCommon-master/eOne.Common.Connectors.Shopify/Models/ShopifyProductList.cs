using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Shopify.Models
{
    public class ShopifyProductList : DataConnectorEntityModel
    {

        public List<ShopifyProduct> products { get; set; }

    }
}
