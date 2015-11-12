using System.Collections.Generic;

namespace eOne.Common.Connectors.Zendesk.Models
{
    public class ZendeskTicketAuditEvent : ConnectorEntityModel
    {

        public int id { get; set; }
        public string type { get; set; }
        public string body { get; set; }
        public bool @public { get; set; }
        public List<ZendeskAttachment> attachments { get; set; }

    }
}
