using System;

namespace eOne.Common.Connectors.MailChimp.Models.Conversation
{
    public class MailChimpConversationMessage
    {

        public string id { get; set; }
        public string conversation_id { get; set; }
        public string list_id { get; set; }
        public string from_label { get; set; }
        public string from_email { get; set; }
        public string subject { get; set; }
        public string message { get; set; }
        public bool read { get; set; }
        public DateTime timestamp { get; set; }

    }
}