using System;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Insightly.Models
{
    class InsightlyOpportunityStateReason : DataConnectorEntityModel
    {

        public int? OPPORTUNITY_ID { get; set; }
        public DateTime DATE_CHANGED_UTC { get; set; }
        public string FOR_OPPORTUNITY_STATE { get; set; }
        public int? STATE_REASON_ID { get; set; }
        public string STATE_REASON { get; set; }

    }
}
