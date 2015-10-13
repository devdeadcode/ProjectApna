﻿using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalPlanList : DataConnectorEntityModel
    {

        public List<PaypalBillingPlan> plans { get; set; }
        public int total_items { get; set; }
        public int total_pages { get; set; }
        public List<PaypalLink> links { get; set; }

    }
}
