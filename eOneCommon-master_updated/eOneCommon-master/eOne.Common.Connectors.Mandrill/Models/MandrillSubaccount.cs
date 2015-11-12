using System;
using System.ComponentModel;

namespace eOne.Common.Connectors.Mandrill.Models
{
    public class MandrillSubaccount : ConnectorEntityModel
    {

        #region Enums

        public enum MandrillSubaccountStatus
        {
            [Description("Active")]
            active,
            [Description("Paused")]
            paused
        }

        #endregion

        #region Default properties

        [FieldSettings("Name", DefaultField = true)]
        public string name { get; set; }

        [FieldSettings("Status", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(MandrillSubaccountStatus))]
        public MandrillSubaccountStatus status { get; set; }

        [FieldSettings("Emails sent this week", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int sent_weekly { get; set; }

        [FieldSettings("Emails sent this month", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int sent_monthly { get; set; }

        [FieldSettings("Total number of emails sent", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int sent_total { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Reputation", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int reputation { get; set; }

        [FieldSettings("Hourly quota", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int custom_quota { get; set; }

        [FieldSettings("Created at date", Created = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime created_at { get; set; }

        [FieldSettings("First sent at date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime first_sent_at { get; set; }

        #endregion

        #region Hidden properties

        public string id { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Created at time", Created = true, FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime created_at_time => created_at;
        
        [FieldSettings("First sent at time", FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime first_sent_at_time => first_sent_at;

        #endregion

    }
}