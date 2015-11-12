namespace eOne.Common.Connectors.Slack.Models
{
    public class SlackUser : ConnectorEntityModel
    {

        [FieldSettings("Username", DefaultField = true, Description = true)]
        public string name { get; set; }

        [FieldSettings("Real name", DefaultField = true)]
        public string real_name => profile == null ? string.Empty : profile.real_name;

        [FieldSettings("Email", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEmail)]
        public string email => profile == null ? string.Empty : profile.email;

        [FieldSettings("User ID", KeyNumber = 1)]
        public string id { get; set; }

        [FieldSettings("Deleted", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool deleted { get; set; }

        [FieldSettings("Color", FieldTypeId = Connector.FieldTypeIdColor)]
        public string color { get; set; }

        [FieldSettings("Administrator", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool is_admin { get; set; }

        [FieldSettings("Owner", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool is_owner { get; set; }

        [FieldSettings("Two factor authentication", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool has_2fa { get; set; }

        [FieldSettings("Has files", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool has_files { get; set; }

        public SlackUserProfile profile { get; set; }

        [FieldSettings("First name")]
        public string first_name => profile == null ? string.Empty : profile.first_name;

        [FieldSettings("Last name")]
        public string last_name => profile == null ? string.Empty : profile.last_name;

        [FieldSettings("Skype", FieldTypeId = Connector.FieldTypeIdSkype)]
        public string skype => profile == null ? string.Empty : profile.skype;

        [FieldSettings("Phone number", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string phone => profile == null ? string.Empty : profile.phone;

        public string image_24 => profile == null ? string.Empty : profile.image_24;
        public string image_32 => profile == null ? string.Empty : profile.image_32;
        public string image_48 => profile == null ? string.Empty : profile.image_48;
        public string image_72 => profile == null ? string.Empty : profile.image_72;
        public string image_192 => profile == null ? string.Empty : profile.image_192;

    }
}