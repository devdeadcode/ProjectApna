using System;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Trello.Models
{
    public class TrelloCard : DataConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Name")]
        public string name { get; set; }

        [FieldSettings("Description")]
        public string desc { get; set; }

        [FieldSettings("Short URL", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string shortUrl { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Id")]
        public string id { get; set; }

        [FieldSettings("Closed", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool closed { get; set; }

        [FieldSettings("Email", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string email { get; set; }

        [FieldSettings("URL", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string url { get; set; }

        #endregion

        #region Hidden properties

        public DateTime? dateLastActivity { get; set; }
        public DateTime? due { get; set; }
        public TrelloBoard board { get; set; }
        public TrelloList list { get; set; }

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

        [FieldSettings("Due date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime dueDate
        {
            get
            {
                return due?.Date ?? DateTime.MinValue;
            }
            set
            {
                due = value;
            }
        }

        [FieldSettings("List name")]
        public string listName
        {
            get
            {
                return list == null ? string.Empty : list.name;
            }
            set
            {
                list.name = value;
            }
        }

        [FieldSettings("List closed", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool listClosed
        {
            get
            {
                return list != null && list.closed;
            }
            set
            {
                list.closed = value;
            }
        }

        [FieldSettings("Board name")]
        public string boardName
        {
            get
            {
                return board == null ? string.Empty : board.name;
            }
            set
            {
                board.name = value;
            }
        }

        [FieldSettings("Board desciption")]
        public string boardDesc
        {
            get
            {
                return board == null ? string.Empty : board.desc;
            }
            set
            {
                board.desc = value;
            }
        }

        [FieldSettings("Board closed", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool boardClosed
        {
            get
            {
                return board != null && board.closed;
            }
            set
            {
                board.closed = value;
            }
        }

        [FieldSettings("Board pinned", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool boardPinned
        {
            get
            {
                return board != null && board.pinned;
            }
            set
            {
                board.pinned = value;
            }
        }

        [FieldSettings("Board starred", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool boardStarred
        {
            get
            {
                return board != null && board.starred;
            }
            set
            {
                board.starred = value;
            }
        }

        [FieldSettings("Board URL", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string boardUrl
        {
            get
            {
                return board == null ? string.Empty : board.url;
            }
            set
            {
                board.url = value;
            }
        }

        [FieldSettings("Board Short URL", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string boardShortUrl
        {
            get
            {
                return board == null ? string.Empty : board.shortUrl;
            }
            set
            {
                board.shortUrl = value;
            }
        }

        #endregion

    }
}
