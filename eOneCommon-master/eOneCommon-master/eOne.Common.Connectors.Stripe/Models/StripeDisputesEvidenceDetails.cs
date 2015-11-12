using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Stripe.Models
{
    class StripeDisputesEvidenceDetails : DataConnectorEntityModel
    {
        public bool has_evidence { get; set; }

        public decimal? past_due { get; set; }

        public int? submission_count { get; set; }

        public long due_by { get; set; }
    }
}
