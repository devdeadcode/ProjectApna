namespace eOne.Common.Connectors.Trello.Models
{
    public class TrelloList : ConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Name", DefaultField = true)]
        public string name { get; set; }

        [FieldSettings("Closed", DefaultField = true, FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool closed { get; set; }

        #endregion

        #region Properties

        [FieldSettings("ID")]
        public string id { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Board name")]
        public string boardName => board.name;

        [FieldSettings("Board desciption")]
        public string boardDesc => board.desc;

        [FieldSettings("Board closed", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool boardClosed => board.closed;

        [FieldSettings("Board pinned", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool boardPinned => board.pinned;

        [FieldSettings("Board starred", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool boardStarred => board.starred;

        [FieldSettings("Board URL", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string boardUrl => board.url;

        [FieldSettings("Board Short URL", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string boardShortUrl => board.shortUrl;

        #endregion

        #region Hidden properties

        public TrelloBoard board { get; set; }

        #endregion

    }
}
