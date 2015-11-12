using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Stripe.Models
{
    class StripeProductSKUProductDimension : DataConnectorEntityModel
    {
        public decimal height { get; set; }
        public decimal length { get; set; }
        public decimal weight { get; set; }
        public decimal width { get; set; }
    }
}
