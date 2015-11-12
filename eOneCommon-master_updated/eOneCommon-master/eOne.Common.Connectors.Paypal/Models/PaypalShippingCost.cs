namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalShippingCost
    {

        public PaypalCurrency amount { get; set; }
        public PaypalTax tax { get; set; }

    }
}