using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Stripe.Models
{
    class StripeCustomerShipping : DataConnectorEntityModel
    {
        public StripeCustomerShippingAddress address { get; set; }

        public string name { get; set; }

        public string phone { get; set; }
    }
}
