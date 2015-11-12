namespace eOne.Common.Connectors.Slack.Models
{
    public class SlackMessage : ConnectorEntityModel
    {

        [FieldSettings("Channel name", DefaultField = true)]
        public string channel_name => channel == null ? string.Empty : channel.name;

        [FieldSettings("Username", DefaultField = true)]
        public string user_name => user_object == null ? string.Empty : user_object.name;

        [FieldSettings("Message", DefaultField = true)]
        public string text { get; set; }
        
        public string type { get; set; }
        public string subtype { get; set; }

        [FieldSettings("User ID")]
        public string user { get; set; }

        public string ts { get; set; }
        public SlackChannel channel { get; set; }
        public SlackUser user_object { get; set; }

        [FieldSettings("Channel ID")]
        public string channel_id => channel == null ? string.Empty : channel.id;
        
    }
}
