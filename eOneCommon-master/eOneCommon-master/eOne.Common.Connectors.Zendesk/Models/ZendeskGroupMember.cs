using System;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Zendesk.Models
{
    class ZendeskGroupMember : DataConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Group name", DefaultField = true)]
        public string group_name => @group.name;

        [FieldSettings("User name", DefaultField = true)]
        public string user_name => user.name;

        [FieldSettings("User email", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string user_email => user.email;

        [FieldSettings("Default group", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool @default { get; set; }

        #endregion

        #region Hidden properties

        public int id { get; set; }
        public int user_id { get; set; }
        public int group_id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public ZendeskUser user { get; set; }
        public ZendeskGroup group { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("User phone", FieldTypeId = DataConnector.FieldTypeIdPhone)]
        public string user_phone => user.phone;

        [FieldSettings("User role", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(ZendeskUser.ZendeskUserRole))]
        public ZendeskUser.ZendeskUserRole user_role => user.role;

        [FieldSettings("Created at date", Created = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime created_at_date => created_at.Date;

        [FieldSettings("Created at time", Created = true, FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime created_at_time => Time(created_at);

        [FieldSettings("Updated at date", Modified = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime updated_at_date => updated_at.Date;

        [FieldSettings("Updated at time", Modified = true, FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime updated_at_time => Time(updated_at);

        #endregion

    }
}