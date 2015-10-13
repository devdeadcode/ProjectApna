using System.ComponentModel;

namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalPaymentTerm
    {

        public enum PaypalPaymentTermType
        {
            [Description("Due on receipt")]
            DUE_ON_RECEIPT, 
            [Description("Net 10")]
            NET_10,
            [Description("Net 15")]
            NET_15,
            [Description("Net 30")]
            NET_30,
            [Description("Net 45")]
            NET_45
        }

        public PaypalPaymentTermType term_type { get; set; }
        public string due_date { get; set; }

    }
}