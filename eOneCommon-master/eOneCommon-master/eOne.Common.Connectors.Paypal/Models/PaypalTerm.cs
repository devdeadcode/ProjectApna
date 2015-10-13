using System.ComponentModel;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalTerm : DataConnectorEntityModel
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
