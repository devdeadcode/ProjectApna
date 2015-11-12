using System.Collections.Generic;

namespace eOne.Common.Connectors.Shopify.Models
{
    public class ShopifyProductList : ConnectorEntityModel
    {

        public List<ShopifyProduct> products { get; set; }

    }
}
