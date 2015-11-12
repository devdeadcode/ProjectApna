using System;

namespace eOne.Common.Connectors.BaseCamp.Models
{
    public class BaseCampPerson : ConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Name", DefaultField = true)]
        public string name { get; set; }

        [FieldSettings("Email", FieldTypeId = Connector.FieldTypeIdEmail, DefaultField = true)]
        public string email_address { get; set; }

        [FieldSettings("URL", FieldTypeId = Connector.FieldTypeIdUrl, DefaultField = true)]
        public string app_url { get; set; }

        #endregion

        #region Properties

        [FieldSettings("ID", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int id { get; set; }

        [FieldSettings("Identity ID", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int identity_id { get; set; }

        [FieldSettings("Administrator", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool admin { get; set; }

        [FieldSettings("Trashed", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool trashed { get; set; }

        [FieldSettings("Avatar", FieldTypeId = Connector.FieldTypeIdImage)]
        public string avatar_url { get; set; }

        [FieldSettings("Fullsize avatar", FieldTypeId = Connector.FieldTypeIdImage)]
        public string fullsize_avatar_url { get; set; }

        [FieldSettings("Created at date", Created = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime created_at { get; set; }

        [FieldSettings("Updated at date", Modified = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime updated_at { get; set; }

        #endregion

        #region Hidden properties

        public string url { get; set; }

        #endregion

    }
}