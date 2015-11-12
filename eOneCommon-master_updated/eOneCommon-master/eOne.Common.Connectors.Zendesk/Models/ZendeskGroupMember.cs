using System;

namespace eOne.Common.Connectors.Zendesk.Models
{
    class ZendeskGroupMember : ConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Group name", DefaultField = true)]
        public string group_name => @group.name;

        [FieldSettings("User name", DefaultField = true)]
        public string user_name => user.name;

        [FieldSettings("User email", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEmail)]
        public string user_email => user.email;

        [FieldSettings("Default group", DefaultField = true, FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool @default { get; set; }

        [FieldSettings("Created at date", Created = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime created_at { get; set; }

        [FieldSettings("Updated at date", Modified = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime updated_at { get; set; }

        #endregion

        #region Hidden properties

        public int id { get; set; }
        public int user_id { get; set; }
        public int group_id { get; set; }
        
        public ZendeskUser user { get; set; }
        public ZendeskGroup group { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("User phone", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string user_phone => user.phone;

        [FieldSettings("User role", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(ZendeskUser.ZendeskUserRole))]
        public ZendeskUser.ZendeskUserRole user_role => user.role;

        [FieldSettings("Created at time", Created = true, FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime created_at_time => created_at;

        [FieldSettings("Updated at time", Modified = true, FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime updated_at_time => updated_at;

        #endregion

    }
}