using System.Collections.Generic;

namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalCapture : PaypalResourceBase
    {

        public enum PaypalCaptureState
        {
            pending, 
            completed, 
            refunded, 
            partially_refunded
        }

        public bool is_final_capture { get; set; }
        public PaypalCaptureState state { get; set; }
        public decimal transaction_fee { get; set; }
        public List<PaypalLink> links { get; set; }

    }
}