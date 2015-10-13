using System.ComponentModel;

namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalPaymentDetail
    {

        public enum PaypalPaymentDetailType
        {
            [Description("PayPal")]
            PAYPAL,
            [Description("External")]
            EXTERNAL,
            [Description("None")]
            NONE,
        }
        public enum PaypalPaymentDetailTransactionType
        {
            [Description("Sale")]
            SALE,
            [Description("Authorization")]
            AUTHORIZATION,
            [Description("Capture")]
            CAPTURE,
            [Description("None")]
            NONE
        }
        public enum PaypalPaymentDetailMethod
        {
            [Description("Bank transfer")]
            BANK_TRANSFER,
            [Description("Cash")]
            CASH,
            [Description("Check")]
            CHECK,
            [Description("Credit card")]
            CREDIT_CARD,
            [Description("Debit card")]
            DEBIT_CARD,
            [Description("PayPal")]
            PAYPAL,
            [Description("Wire transfer")]
            WIRE_TRANSFER,
            [Description("Other")]
            OTHER,
            [Description("None")]
            NONE
        }

        public PaypalPaymentDetailType type { get; set; }
        public string transaction_id { get; set; }
        public PaypalPaymentDetailTransactionType transaction_type { get; set; }
        public string date { get; set; }
        public PaypalPaymentDetailMethod method { get; set; }
        public string note { get; set; }

    }
}