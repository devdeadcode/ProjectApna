using System;
using eOne.Common.Connectors.MailChimp.Models.Campaign;
using eOne.Common.Connectors.MailChimp.Models.List;

namespace eOne.Common.Connectors.MailChimp.Models.Conversation
{
    public class MailChimpConversationData : ConnectorEntityModel
    {

        public string id { get; set; }
        public int message_count { get; set; }
        public string campaign_id { get; set; }
        public string list_id { get; set; }
        public int unread_messages { get; set; }
        public string from_label { get; set; }
        public string from_email { get; set; }
        public string subject { get; set; }

        public MailChimpCampaignCollection campaign { get; set; }
        public MailChimpListCollection list { get; set; }
        public MailChimpConversationMessage last_message { get; set; }

        public string last_message_from_label => last_message.from_label;
        public string last_message_from_email => last_message.from_email;
        public string last_message_subject => last_message.subject;
        public string last_message_message => last_message.message;
        public bool last_message_read => last_message.read;
        public DateTime last_message_timestamp => last_message.timestamp;

    }
}


//        "": {
//            "type": "object",
//            "title": "Last Message",
//            "description": "The most recent message in the conversation",
//            "properties": {
//                "from_label": {
//                    "type": "string",
//                    "title": "From Label",
//                    "description": "A label representing the sender of this message",
//                    "readonly": true,
//                    "example": "Urist McVankab"
//                },
//                "from_email": {
//                    "type": "string",
//                    "title": "From Email",
//                    "description": "A label representing the email of the sender of this message",
//                    "readonly": true,
//                    "example": "urist.mcvankab@freddiesjokes.com"
//                },
//                "subject": {
//                    "type": "string",
//                    "title": "Subject",
//                    "description": "The subject of this message",
//                    "readonly": true,
//                    "example": "Re: Freddie Likes Jokes"
//                },
//                "message": {
//                    "type": "string",
//                    "title": "Message",
//                    "description": "The plain-text content of the message",
//                    "readonly": true,
//                    "example": "Where can I get a cool hat like the one Freddie is wearing?"
//                },
//                "read": {
//                    "id": "read",
//                    "type": "boolean",
//                    "title": "Read",
//                    "description": "Whether or not this message has been marked as read",
//                    "example": false
//                },
//                "timestamp": {
//                    "type": "string",
//                    "title": "Timestamp",
//                    "description": "Date the message was either sent or received",
//                    "readonly": true,
//                    "example": "2015-07-15 14:54:54"
//                }
//            }
//        },
