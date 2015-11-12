using System;

namespace eOne.Common.Connectors.Mandrill.Models
{
    public class MandrillWhitelist : ConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Email address", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEmail)]
        public string email { get; set; }

        [FieldSettings("Detail", DefaultField = true)]
        public string detail { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Created at date", Created = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime created_at { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Created at time", Created = true, FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime created_at_time => created_at;

        #endregion

    }
}

