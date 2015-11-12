using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Stripe.Models
{
    class StripeTransferBankAccount : DataConnectorEntityModel
    {
        public string bank_name { get; set; }
        public string routing_number { get; set; }
        public string country { get; set; }
    }
}
