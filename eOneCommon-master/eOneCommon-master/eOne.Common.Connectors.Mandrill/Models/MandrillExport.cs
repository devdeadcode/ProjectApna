using System;
using System.ComponentModel;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Mandrill.Models
{
    public class MandrillExport : DataConnectorEntityModel
    {

        #region Enums

        public enum MandrillExportType
        {
            [Description("Activity")]
            activity,
            [Description("Rejection")]
            reject,
            [Description("Whitelist")]
            whitelist
        }
        public enum MandrillExportState
        {
            [Description("Waiting")]
            waiting,
            [Description("Working")]
            working,
            [Description("Complete")]
            complete,
            [Description("Error")]
            error,
            [Description("Expired")]
            expired
        }

        #endregion

        #region Default properties

        [FieldSettings("Type", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(MandrillExportType))]
        public MandrillExportType type { get; set; }

        [FieldSettings("State", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(MandrillExportState))]
        public MandrillExportState state { get; set; }

        [FieldSettings("Result URL", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string result_url { get; set; }

        #endregion

        #region Hidden properties

        public string id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime finished_at { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Created at date", Created = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime created_at_date => created_at.Date;

        [FieldSettings("Created at time", Created = true, FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime created_at_time => Time(created_at);

        [FieldSettings("Finished at date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime finished_at_date => finished_at.Date;

        [FieldSettings("Finished at time", FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime finished_at_time => Time(finished_at);

        #endregion

    }
}