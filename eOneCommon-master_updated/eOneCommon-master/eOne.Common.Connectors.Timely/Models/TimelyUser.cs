using System;
using System.ComponentModel;

namespace eOne.Common.Connectors.Timely.Models
{
    public class TimelyUser : ConnectorEntityModel
    {

        #region Enums

        public enum TimelyUserLevel
        {
            [Description("Administrator")]
            admin,
            [Description("Normal")]
            normal,
            [Description("Limited")]
            limited
        }

        #endregion

        #region Default properties

        [FieldSettings("Name", DefaultField = true, Description = true)]
        public string name { get; set; }

        [FieldSettings("Email", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEmail)]
        public string email { get; set; }

        #endregion

        #region Properties

        [FieldSettings("User level", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(TimelyUserLevel))]
        public TimelyUserLevel user_level { get; set; }

        [FieldSettings("Time zone")]
        public string time_zone { get; set; }

        [FieldSettings("Active", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool active { get; set; }

        [FieldSettings("Deleted", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool deleted { get; set; }

        [FieldSettings("Hide hourly rate", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool hide_hourly_rate { get; set; }

        [FieldSettings("ID", KeyNumber = 1, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int id { get; set; }

        #endregion

        #region Hidden properties

        public int updated_at { get; set; }
        public TimelyAvatar avatar { get; set; }
        public int account_id { get; set; }
        public decimal hour_rate { get; set; } 

        #endregion

        #region Calculations

        [FieldSettings("Updated date", Modified = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime updated_at_date => FromEpochSeconds(updated_at);

        [FieldSettings("User link", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string user_url => $"https://timelyapp.com/{account_id}/users/{id}";

        #endregion

    }
}
