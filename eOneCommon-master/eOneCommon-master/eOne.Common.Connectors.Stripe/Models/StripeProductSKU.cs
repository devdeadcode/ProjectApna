using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Stripe.Models
{
    class StripeProductSKU : DataConnectorEntityModel
    {
        public string url { get; set; }

        public List<StripeProductSKUData> data { get; set; }

        public int count { get; set; }
    }
}
