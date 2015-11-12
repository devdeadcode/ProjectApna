using System;

namespace eOne.Common.Connectors.Trello.Models
{
    public class TrelloCard : ConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Name")]
        public string name { get; set; }

        [FieldSettings("Description")]
        public string desc { get; set; }

        [FieldSettings("Short URL", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string shortUrl { get; set; }

        #endregion

        #region Properties

        [FieldSettings("ID")]
        public string id { get; set; }

        [FieldSettings("Closed", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool closed { get; set; }

        [FieldSettings("Email", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string email { get; set; }

        [FieldSettings("URL", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string url { get; set; }

        [FieldSettings("Date of last activity", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? dateLastActivity { get; set; }

        [FieldSettings("Due date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? due { get; set; }

        #endregion

        #region Hidden properties

        public TrelloBoard board { get; set; }
        public TrelloList list { get; set; }

        #endregion

        #region Calculations

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

        [FieldSettings("List closed", FieldTypeId = Connector.FieldTypeIdYesNo)]
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

        [FieldSettings("Board closed", FieldTypeId = Connector.FieldTypeIdYesNo)]
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

        [FieldSettings("Board pinned", FieldTypeId = Connector.FieldTypeIdYesNo)]
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

        [FieldSettings("Board starred", FieldTypeId = Connector.FieldTypeIdYesNo)]
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

        [FieldSettings("Board URL", FieldTypeId = Connector.FieldTypeIdUrl)]
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

        [FieldSettings("Board Short URL", FieldTypeId = Connector.FieldTypeIdUrl)]
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
