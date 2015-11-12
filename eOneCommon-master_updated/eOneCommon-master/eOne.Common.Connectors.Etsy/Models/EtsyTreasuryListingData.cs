namespace eOne.Common.Connectors.Etsy.Models
{
    public class EtsyTreasuryListingData : ConnectorEntityModel
    {

        public enum EtsyTreasuryListingDataState
        {
            active,
            sold_out
        }

        public int user_id { get; set; }
        public string title { get; set; }
        public decimal price { get; set; }
        public IsoCurrency currency_code { get; set; }
        public int listing_id { get; set; }
        public EtsyTreasuryListingDataState state { get; set; }
        public string shop_name { get; set; }
        public int image_id { get; set; }
        public string image_url_75x75 { get; set; }
        public string image_url_170x135 { get; set; }

    }
}