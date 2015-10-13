using System.Collections.Generic;
using System.ComponentModel;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalPayer : DataConnectorEntityModel
    {

        public enum PaypalPayerPaymentMethod
        {
            [Description("Credit card")]
            credit_card,
            [Description("PayPal")]
            paypal
        }
        public enum PaypalPayerStatus
        {
            [Description("Verified")]
            VERIFIED,
            [Description("Unverified")]
            UNVERIFIED
        }

        public PaypalPayerPaymentMethod payment_method { get; set; }
        public PaypalPayerStatus status { get; set; }
        public List<PaypalCreditCard> funding_instruments { get; set; }
        public PaypalPayerInfo payer_info { get; set; }

    }
}
