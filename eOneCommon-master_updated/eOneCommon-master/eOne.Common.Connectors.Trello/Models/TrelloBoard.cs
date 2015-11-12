using System;

namespace eOne.Common.Connectors.Trello.Models
{
    public class TrelloBoard : ConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Name", DefaultField = true)]
        public string name { get; set; }

        [FieldSettings("Description", DefaultField = true)]
        public string desc { get; set; }

        [FieldSettings("Short url", DefaultField = true, FieldTypeId = Connector.FieldTypeIdUrl)]
        public string shortUrl { get; set; }

        #endregion

        #region Properties

        [FieldSettings("ID")]
        public string id { get; set; }

        [FieldSettings("Closed", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool closed { get; set; }

        [FieldSettings("Pinned", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool pinned { get; set; }

        [FieldSettings("Starred", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool starred { get; set; }

        [FieldSettings("Url", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string url { get; set; }

        [FieldSettings("Date of last activity", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? dateLastActivity { get; set; }

        [FieldSettings("Date last viewed", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? dateLastView { get; set; }

        #endregion

    }

}
