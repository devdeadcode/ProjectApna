using System;
using System.ComponentModel;

namespace eOne.Common.Connectors.Mandrill.Models
{
    public class MandrillExport : ConnectorEntityModel
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

        [FieldSettings("Type", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(MandrillExportType))]
        public MandrillExportType type { get; set; }

        [FieldSettings("State", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(MandrillExportState))]
        public MandrillExportState state { get; set; }

        [FieldSettings("Result URL", DefaultField = true, FieldTypeId = Connector.FieldTypeIdUrl)]
        public string result_url { get; set; }

        [FieldSettings("Created at date", Created = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime created_at { get; set; }

        [FieldSettings("Finished at date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime finished_at { get; set; }

        #endregion

        #region Hidden properties

        public string id { get; set; }
        

        #endregion

        #region Calculations
        
        [FieldSettings("Created at time", Created = true, FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime created_at_time => created_at;
        
        [FieldSettings("Finished at time", FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime finished_at_time => finished_at;

        #endregion

    }
}