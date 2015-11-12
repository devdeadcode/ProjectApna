namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalRedirectUrl : ConnectorEntityModel
    {

        public string return_url { get; set; }
        public string cancel_url { get; set; }
    }
}

