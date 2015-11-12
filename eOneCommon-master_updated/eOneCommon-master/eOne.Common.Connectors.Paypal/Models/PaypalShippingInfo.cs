namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalShippingInfo : ConnectorEntityModel
    {

        public string first_name { get; set; }
        public string last_name { get; set; }
        public string business_name { get; set; }
        public PaypalAddress address { get; set; }

    }
}