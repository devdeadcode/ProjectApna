using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Stripe.Models
{
    class StripeDisputes : DataConnectorEntityModel
    {
        public List<StripeDisputesData> data { get; set; } 
    }
}
