using System.ComponentModel;

namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalTerm : ConnectorEntityModel
    {

        public enum PaypalTermType
        {
            [Description("Monthly")]
            MONTHLY,
            [Description("Weekly")]
            WEEKLY,
            [Description("Yearly")]
            YEARLY
        }

        public string id { get; set; }
        public PaypalTermType type { get; set; }
        public decimal max_billing_amount { get; set; }
        public decimal amount_range { get; set; }
        public string buyer_editable { get; set; }
        public int occurrences { get; set; }

    }
}
