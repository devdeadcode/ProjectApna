using System.Collections.Generic;

namespace eOne.Common.Connectors.Shopify.Models
{
    public class ShopifyArticleList : ConnectorEntityModel
    {

        public List<ShopifyArticle> articles { get; set; }

    }
}
