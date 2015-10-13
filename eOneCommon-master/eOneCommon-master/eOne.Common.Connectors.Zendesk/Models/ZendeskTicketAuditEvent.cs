using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Zendesk.Models
{
    public class ZendeskTicketAuditEvent : DataConnectorEntityModel
    {

        public int id { get; set; }
        public string type { get; set; }
        public string body { get; set; }
        public bool @public { get; set; }
        public List<ZendeskAttachment> attachments { get; set; }

    }
}
