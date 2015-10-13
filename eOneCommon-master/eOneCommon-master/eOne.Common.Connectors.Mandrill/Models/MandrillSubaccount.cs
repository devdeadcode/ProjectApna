using System;
using System.ComponentModel;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Mandrill.Models
{
    public class MandrillSubaccount : DataConnectorEntityModel
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

        [FieldSettings("Status", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(MandrillSubaccountStatus))]
        public MandrillSubaccountStatus status { get; set; }

        [FieldSettings("Emails sent this week", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int sent_weekly { get; set; }

        [FieldSettings("Emails sent this month", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int sent_monthly { get; set; }

        [FieldSettings("Total number of emails sent", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int sent_total { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Reputation", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int reputation { get; set; }

        [FieldSettings("Hourly quota", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int custom_quota { get; set; }

        #endregion

        #region Hidden properties

        public string id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime first_sent_at { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Created at date", Created = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime created_at_date => created_at.Date;

        [FieldSettings("Created at time", Created = true, FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime created_at_time => Time(created_at);

        [FieldSettings("First sent at date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime first_sent_at_date => first_sent_at.Date;

        [FieldSettings("First sent at time", FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime first_sent_at_time => Time(first_sent_at);

        #endregion

    }
}