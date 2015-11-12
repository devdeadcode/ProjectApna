using System;

namespace eOne.Common.Connectors.Mandrill.Models
{
    public class MandrillMessage : ConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("From email", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEmail)]
        public string from_email { get; set; }

        [FieldSettings("To email", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEmail)]
        public string to { get; set; }

        [FieldSettings("Subject", DefaultField = true)]
        public string subject { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Created at date", Created = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime created_at { get; set; }

        [FieldSettings("Send at date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime send_at { get; set; }

        #endregion

        #region Hidden properties

        public string _id { get; set; }
        

        #endregion

        #region Calculations
        
        [FieldSettings("Created at time", Created = true, FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime created_at_time => created_at;

        [FieldSettings("Send at time", FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime send_at_time => send_at;

        #endregion

    }
}