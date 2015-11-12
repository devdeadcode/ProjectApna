namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalLink : ConnectorEntityModel
    {

        public enum PaypalLinkRel
        {
            self,
            parent_payment,
            execute,
            refund
        }
        public enum PaypalLinkMethod
        {
            GET,
            PUT,
            POST,
            DELETE
        }

        public string href { get; set; }
        public PaypalLinkRel rel { get; set; }
        public PaypalLinkMethod method { get; set; }

    }
}
