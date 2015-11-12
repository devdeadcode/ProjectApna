using System.ComponentModel;

namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalRefundDetail
    {

        public enum PaypalRefundDetailType
        {
            [Description("PayPal")]
            PAYPAL,
            [Description("External")]
            EXTERNAL,
            [Description("None")]
            NONE,
        }

        public PaypalRefundDetailType type { get; set; }
        public string date { get; set; }
        public string note { get; set; }

    }
}