using System.Collections.Generic;
using System.ComponentModel;

namespace eOne.Common.Connectors.Etsy.Models
{
    public class EtsyReceipt : ConnectorEntityModel
    {

        public enum EtsyReceiptPaymentMethod
        {
            [Description("PayPal")]
            pp,
            [Description("Credit card")]
            cc,
            [Description("Check")]
            ck,
            [Description("Money order")]
            mo,
            [Description("Other")]
            other
        }

        public int receipt_id { get; set; }
        public int order_id { get; set; }
        public int seller_user_id { get; set; }
        public int buyer_user_id { get; set; }
        public float creation_tsz { get; set; }
        public float last_modified_tsz { get; set; }
        public string name { get; set; }
        public string first_line { get; set; }
        public string second_line { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public int country_id { get; set; }
        public EtsyReceiptPaymentMethod payment_method { get; set; }
        public string payment_email { get; set; }
        public string message_from_seller { get; set; }
        public string message_from_buyer { get; set; }
        public bool was_paid { get; set; }
        public decimal total_tax_cost { get; set; }
        public decimal total_vat_cost { get; set; }
        public decimal total_price { get; set; }
        public decimal total_shipping_cost { get; set; }
        public IsoCurrency currency_code { get; set; }
        public string message_from_payment { get; set; }
        public bool was_shipped { get; set; }
        public string buyer_email { get; set; }
        public string seller_email { get; set; }
        public decimal discount_amt { get; set; }
        public decimal subtotal { get; set; }
        public decimal grandtotal { get; set; }
        public decimal adjusted_grandtotal { get; set; }
        public List<EtsyReceiptShipment> shipments { get; set; }

        public int number_of_shipments => shipments?.Count ?? 0;
    }
}