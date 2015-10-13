namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalTax
    {

        public string id { get; set; }
        public string name { get; set; }
        public decimal percent { get; set; }
        public PaypalCurrency amount { get; set; }

    }
}
