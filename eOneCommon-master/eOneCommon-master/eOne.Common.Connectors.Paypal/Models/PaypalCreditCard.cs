using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalCreditCard : DataConnectorEntityModel
    {

        public enum PaypalCreditCardType
        {
            visa, 
            mastercard, 
            discover, 
            amex
        }

        public PaypalAddress billing_address { get; set; }
        public PaypalCreditCardType type { get; set; }
        public string number { get; set; }
        public int expire_month { get; set; }
        public int expire_year { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }

    }
}
