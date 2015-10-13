using System;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.BaseCamp.Models
{
    public class BaseCampPerson : DataConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Name", DefaultField = true)]
        public string name { get; set; }

        [FieldSettings("Email", FieldTypeId = DataConnector.FieldTypeIdEmail, DefaultField = true)]
        public string email_address { get; set; }

        [FieldSettings("URL", FieldTypeId = DataConnector.FieldTypeIdUrl, DefaultField = true)]
        public string app_url { get; set; }

        #endregion

        #region Properties

        [FieldSettings("ID", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int id { get; set; }

        [FieldSettings("Identity ID", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int identity_id { get; set; }

        [FieldSettings("Administrator", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool admin { get; set; }

        [FieldSettings("Trashed", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool trashed { get; set; }

        [FieldSettings("Avatar", FieldTypeId = DataConnector.FieldTypeIdImage)]
        public string avatar_url { get; set; }

        [FieldSettings("Fullsize avatar", FieldTypeId = DataConnector.FieldTypeIdImage)]
        public string fullsize_avatar_url { get; set; }

        #endregion

        #region Hidden properties

        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string url { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Created at date", Created = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime CreatedAtDate => created_at.Date;

        [FieldSettings("Updated at date", Modified = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime UpdatedAtDate => updated_at.Date;

        #endregion

    }
}