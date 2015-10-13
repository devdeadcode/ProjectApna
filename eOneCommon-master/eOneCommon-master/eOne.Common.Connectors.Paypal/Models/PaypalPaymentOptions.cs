using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalPaymentOptions : DataConnectorEntityModel
    {

        public string allowed_payment_method { get; set; }

    }
}
