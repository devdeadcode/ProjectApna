namespace eOne.Common.Connectors.Slack.Models
{
    public class SlackGroupMember
    {

        public SlackGroupMember(SlackGroup addGroup, SlackUser addUser)
        {
            group = addGroup;
            user = addUser;
        }

        [FieldSettings("Group name", DefaultField = true)]
        public string group_name => group == null ? string.Empty : group.name;

        [FieldSettings("Username", DefaultField = true)]
        public string user_name => user == null ? string.Empty : user.name;

        [FieldSettings("Real name", DefaultField = true)]
        public string user_real_name => user == null ? string.Empty : user.real_name;

        [FieldSettings("Email", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEmail)]
        public string user_email => user == null ? string.Empty : user.email;

        public SlackGroup group { get; set; }
        public SlackUser user { get; set; }

        [FieldSettings("Group ID", KeyNumber = 1)]
        public string group_id => group == null ? string.Empty : group.id;

        [FieldSettings("Archived group", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool group_is_archived => @group?.is_archived ?? false;

        [FieldSettings("Group topic")]
        public string group_topic => group == null ? string.Empty : group.topic_value;

        [FieldSettings("Group purpose")]
        public string group_purpose => group == null ? string.Empty : group.purpose_value;

        [FieldSettings("User ID", KeyNumber = 2)]
        public string user_id => user == null ? string.Empty : user.id;

        [FieldSettings("Deleted", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool user_deleted => user?.deleted ?? false;

        [FieldSettings("Color", FieldTypeId = Connector.FieldTypeIdColor)]
        public string user_color => user == null ? string.Empty : user.color;

        [FieldSettings("First name")]
        public string user_first_name => user == null ? string.Empty : user.first_name;

        [FieldSettings("Last name")]
        public string user_last_name => user == null ? string.Empty : user.last_name;

        [FieldSettings("Skype", FieldTypeId = Connector.FieldTypeIdSkype)]
        public string user_skype => user == null ? string.Empty : user.skype;

        [FieldSettings("Phone number", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string user_phone => user == null ? string.Empty : user.phone;

    }
}
