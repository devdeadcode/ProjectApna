using System.Collections.Generic;

namespace eOne.Common.Connectors.Etsy.Models
{
    public class EtsyCart : ConnectorEntityModel
    {

        public int cart_id { get; set; }
        public string shop_name { get; set; }
        public string message_to_seller { get; set; }
        public int destination_country_id { get; set; }
        public string coupon_code { get; set; }
        public IsoCurrency currency_code { get; set; }
        public string total { get; set; }
        public string subtotal { get; set; }
        public string tax_cost { get; set; }
        public string shipping_cost { get; set; }
        public string discount_amount { get; set; }
        public string shipping_discount_amount { get; set; }
        public string tax_discount_amount { get; set; }
        public string url { get; set; }
        public bool is_download_only { get; set; }
        public bool has_vat { get; set; }
        public List<EtsyCartListing> listings { get; set; }

    }
}

//shipping_option	private	cart_rw	ShippingOption	The selected shipping option identifier for the cart
