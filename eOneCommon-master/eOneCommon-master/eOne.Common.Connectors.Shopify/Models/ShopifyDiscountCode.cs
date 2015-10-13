using System.ComponentModel;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Shopify.Models
{
    public class ShopifyDiscountCode : DataConnectorEntityModel
    {

        public enum ShopifyDiscountCodeType
        {
            [Description("Percentage")]
            percentage,
            [Description("Shipping")]
            shipping,
            [Description("Fixed amount")]
            fixed_amount
        }

        public decimal amount { get; set; }
        public string code { get; set; }
        public ShopifyDiscountCodeType type { get; set; }

    }
}
