namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalRefund : PaypalResourceBase
    {

        public enum PaypalRefundState
        {
            pending,
            completed,
            failed
        }

        public PaypalRefundState state { get; set; }
        public string capture_id { get; set; }

    }
}