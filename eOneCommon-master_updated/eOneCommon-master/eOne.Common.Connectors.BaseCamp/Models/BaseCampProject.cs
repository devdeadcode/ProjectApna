using System;

namespace eOne.Common.Connectors.BaseCamp.Models
{
    class BaseCampProject : ConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Name", DefaultField = true)]
        public string name { get; set; }

        [FieldSettings("Description", DefaultField = true)]
        public string description { get; set; }

        [FieldSettings("URL", FieldTypeId = Connector.FieldTypeIdUrl, DefaultField = true)]
        public string app_url { get; set; }

        #endregion

        #region Properties

        [FieldSettings("ID", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int id { get; set; }

        [FieldSettings("Template", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool template { get; set; }

        [FieldSettings("Archived", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool archived { get; set; }

        [FieldSettings("Starred", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool starred { get; set; }

        [FieldSettings("Trashed", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool trashed { get; set; }

        [FieldSettings("Draft", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool draft { get; set; }

        [FieldSettings("Client project", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool is_client_project { get; set; }

        [FieldSettings("Color")]
        public string color { get; set; }

        [FieldSettings("Updated at date", Modified = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime updated_at { get; set; }

        #endregion

    }
}
