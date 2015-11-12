namespace eOne.Common.Connectors.Shopify.Models
{
    public class ShopifyPaymentDetails : ConnectorEntityModel
    {

        public string avs_result_code { get; set; }
        public string credit_card_bin { get; set; }
        public string cvv_result_code { get; set; }
        public string credit_card_number { get; set; }
        public string credit_card_company { get; set; }

    }
}
