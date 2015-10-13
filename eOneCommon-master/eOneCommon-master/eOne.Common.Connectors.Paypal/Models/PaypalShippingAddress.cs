namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalShippingAddress : PaypalAddress
    {

        public enum PaypalAddressType
        {
            residential,
            business,
            mailbox
        }

        public string recipient_name { get; set; }
        public PaypalAddressType type { get; set; }

    }
}
