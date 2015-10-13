using System;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Mandrill.Models
{
    public class MandrillWhitelist : DataConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Email address", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string email { get; set; }

        [FieldSettings("Detail", DefaultField = true)]
        public string detail { get; set; }

        #endregion

        #region Hidden properties

        public DateTime created_at { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Created at date", Created = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime created_at_date => created_at.Date;

        [FieldSettings("Created at time", Created = true, FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime created_at_time => Time(created_at);

        #endregion

    }
}

