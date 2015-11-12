using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Stripe.Models
{
    class StripeCustomerDiscount : DataConnectorEntityModel
    {
        public StripeCustomerDiscountCoupon coupon { get; set; }
        
    }
}
