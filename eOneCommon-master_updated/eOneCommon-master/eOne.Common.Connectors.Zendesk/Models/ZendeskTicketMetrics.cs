using System.Collections.Generic;

namespace eOne.Common.Connectors.Zendesk.Models
{
    public class ZendeskTicketMetrics : ZendeskCore
    {

        public List<ZendeskTicketMetric> ticket_metrics { get; set; }

    }
}
