using System;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Mandrill.Models
{
    public class MandrillMessage : DataConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("From email", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string from_email { get; set; }

        [FieldSettings("To email", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string to { get; set; }

        [FieldSettings("Subject", DefaultField = true)]
        public string subject { get; set; }

        #endregion

        #region Hidden properties

        public string _id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime send_at { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Created at date", Created = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime created_at_date => created_at.Date;

        [FieldSettings("Created at time", Created = true, FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime created_at_time => Time(created_at);

        [FieldSettings("Send at date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime send_at_date => send_at.Date;

        [FieldSettings("Send at time", FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime send_at_time => Time(send_at);

        #endregion

    }
}