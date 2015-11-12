using System;
using System.Collections.Generic;

namespace eOne.Common.Connectors.Zendesk.Models
{
    public class ZendeskTicketComment : ConnectorEntityModel
    {

        public string type { get; set; }
        public string body { get; set; }

        public int id { get; set; }
        public DateTime created_at { get; set; }
        public int author_id { get; set; }
        public List<ZendeskAttachment> attachments { get; set; }
        public ZendeskTicketVia via { get; set; }
        public bool @public { get; set; }

        public int NumberOfAttachments => attachments.Count;

        public DateTime created_at_time => created_at;

        // public ZendeskMetadata metadata { get; set; }

    }
}