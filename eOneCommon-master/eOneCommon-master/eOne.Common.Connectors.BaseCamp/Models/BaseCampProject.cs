using System;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.BaseCamp.Models
{
    class BaseCampProject : DataConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Name", DefaultField = true)]
        public string name { get; set; }

        [FieldSettings("Description", DefaultField = true)]
        public string description { get; set; }

        [FieldSettings("URL", FieldTypeId = DataConnector.FieldTypeIdUrl, DefaultField = true)]
        public string app_url { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Id", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int id { get; set; }

        [FieldSettings("Template", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool template { get; set; }

        [FieldSettings("Archived", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool archived { get; set; }

        [FieldSettings("Starred", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool starred { get; set; }

        [FieldSettings("Trashed", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool trashed { get; set; }

        [FieldSettings("Draft", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool draft { get; set; }

        [FieldSettings("Client project", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool is_client_project { get; set; }

        [FieldSettings("Color")]
        public string color { get; set; }

        #endregion

        #region Hidden properties

        public DateTime updated_at { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Updated at date", Modified = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime UpdatedAtDate => updated_at.Date;

        #endregion

    }
}
