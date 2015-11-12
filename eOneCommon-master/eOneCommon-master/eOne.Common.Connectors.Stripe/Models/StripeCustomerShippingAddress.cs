using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Stripe.Models
{
    class StripeCustomerShippingAddress : DataConnectorEntityModel
    {
        public string line1 { get; set; }

        public string line2 { get; set; }

        public string city { get; set; }

        public string state { get; set; }
        
        public string postal_code { get; set; }

        public string country { get; set; }
        
    }
}
