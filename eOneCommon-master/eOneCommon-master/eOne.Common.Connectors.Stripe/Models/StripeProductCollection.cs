using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Stripe.Models
{
    class StripeProductCollection : DataConnectorEntityModel
    {
        public List<StripeProducts> data { get; set; } 
    }
}
