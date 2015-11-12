using System.Collections.Generic;

namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalPlanList : ConnectorEntityModel
    {

        public List<PaypalBillingPlan> plans { get; set; }
        public int total_items { get; set; }
        public int total_pages { get; set; }
        public List<PaypalLink> links { get; set; }

    }
}
