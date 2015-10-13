using System;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Trello.Models
{
    public class TrelloBoard : DataConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Name", DefaultField = true)]
        public string name { get; set; }

        [FieldSettings("Description", DefaultField = true)]
        public string desc { get; set; }

        [FieldSettings("Short url", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string shortUrl { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Id")]
        public string id { get; set; }

        [FieldSettings("Closed", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool closed { get; set; }

        [FieldSettings("Pinned", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool pinned { get; set; }

        [FieldSettings("Starred", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool starred { get; set; }

        [FieldSettings("Url", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string url { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Date of last activity", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime dateOfLastActivity
        {
            get
            {
                return dateLastActivity?.Date ?? DateTime.MinValue;
            }
            set
            {
                dateLastActivity = value;
            }
        }

        [FieldSettings("Date last viewed", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime dateOfLastView
        {
            get
            {
                return dateLastView?.Date ?? DateTime.MinValue;
            }
            set
            {
                dateLastView = value;
            }
        }

        #endregion

        #region Hidden properties

        public DateTime? dateLastActivity { get; set; }
        public DateTime? dateLastView { get; set; }

        #endregion

    }

}
