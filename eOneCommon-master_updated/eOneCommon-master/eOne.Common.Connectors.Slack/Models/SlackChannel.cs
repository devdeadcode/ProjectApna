using System;

namespace eOne.Common.Connectors.Slack.Models
{
    public class SlackChannel : ConnectorEntityModel
    {

        [FieldSettings("Name", DefaultField = true, Description = true)]
        public string name { get; set; }

        [FieldSettings("Topic", DefaultField = true)]
        public string topic_value => topic == null ? string.Empty : topic.value;

        [FieldSettings("Purpose", DefaultField = true)]
        public string purpose_value => purpose == null ? string.Empty : purpose.value;

        [FieldSettings("Number of members", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int num_members { get; set; }

        [FieldSettings("Channel ID", KeyNumber = 1)]
        public string id { get; set; }

        [FieldSettings("Created by user ID")]
        public string creator { get; set; }

        [FieldSettings("Archived", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool is_archived { get; set; }

        public bool is_member { get; set; }

        public SlackDescription topic { get; set; }
        public SlackDescription purpose { get; set; }
        public SlackUser creator_user {get;set;}
        public long created { get; set; }

        [FieldSettings("Created date", FieldTypeId = Connector.FieldTypeIdDate, Created = true)]
        public DateTime created_date => FromEpochSeconds(created);

        [FieldSettings("Created by username")]
        public string creator_user_name => creator_user == null ? string.Empty : creator_user.name;

        [FieldSettings("Created by user color")]
        public string creator_user_color => creator_user == null ? string.Empty : creator_user.color;

        [FieldSettings("Created by user first name")]
        public string creator_user_first_name => creator_user == null ? string.Empty : creator_user.first_name;

        [FieldSettings("Created by user last name")]
        public string creator_user_last_name => creator_user == null ? string.Empty : creator_user.last_name;

        [FieldSettings("Created by user real name")]
        public string creator_user_real_name => creator_user == null ? string.Empty : creator_user.real_name;

        [FieldSettings("Created by user email", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string creator_user_email => creator_user == null ? string.Empty : creator_user.email;

        [FieldSettings("Created by user skype", FieldTypeId = Connector.FieldTypeIdSkype)]
        public string creator_user_skype => creator_user == null ? string.Empty : creator_user.skype;

        [FieldSettings("Created by user phone number", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string creator_user_phone => creator_user == null ? string.Empty : creator_user.phone;

        public string creator_user_image_24 => creator_user == null ? string.Empty : creator_user.image_24;
        public string creator_user_image_32 => creator_user == null ? string.Empty : creator_user.image_32;
        public string creator_user_image_48 => creator_user == null ? string.Empty : creator_user.image_48;
        public string creator_user_image_72 => creator_user == null ? string.Empty : creator_user.image_72;
        public string creator_user_image_192 => creator_user == null ? string.Empty : creator_user.image_192;
        public bool creator_user_deleted => creator_user?.deleted ?? false;
        public bool creator_user_is_admin => creator_user?.is_admin ?? false;
        public bool creator_user_is_owner => creator_user?.is_owner ?? false;
        public bool creator_user_has_2fa => creator_user?.has_2fa ?? false;
        public bool creator_user_has_files => creator_user?.has_files ?? false;

    }
}