using System.ComponentModel;

namespace eOne.Common.Connectors.Shopify.Models
{
    public class ShopifyOrderRisk : ConnectorEntityModel
    {

        public enum ShopifyOrderRiskRecommendation
        {
            [Description("Cancel")]
            cancel, 
            [Description("Investigate")]
            investigate, 
            [Description("Accept")]
            accept
        }

        public bool cause_cancel { get; set; }

        public bool display { get; set; }

        public string id { get; set; }

        public string order_id { get; set; }

        public string message { get; set; }

        public ShopifyOrderRiskRecommendation recommendation { get; set; }

        public decimal score { get; set; }

        public string source { get; set; }

    }
}
