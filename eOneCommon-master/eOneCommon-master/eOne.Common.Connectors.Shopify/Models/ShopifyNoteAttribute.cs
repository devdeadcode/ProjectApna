using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Shopify.Models
{
    public class ShopifyNoteAttribute : DataConnectorEntityModel
    {

        public string name { get; set; }
        public string value { get; set; }

    }
}
