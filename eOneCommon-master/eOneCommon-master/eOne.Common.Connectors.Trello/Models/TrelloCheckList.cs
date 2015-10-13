using System;
using System.Collections.Generic;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Trello.Models
{
    public class TrelloCheckList : DataConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Name", DefaultField = true)]
        public string name { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Card name")]
        public string cardName => card.name;

        [FieldSettings("Card description")]
        public string cardDesc => card.desc;

        [FieldSettings("Card closed")]
        public bool cardClosed => card.closed;

        [FieldSettings("Card due date")]
        public DateTime cardDue => card.dueDate;

        [FieldSettings("Card email")]
        public string cardEmail => card.email;

        [FieldSettings("Card short Url", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string cardShortUrl => card.shortUrl;

        [FieldSettings("Card Url", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string cardUrl => card.url;

        [FieldSettings("List name")]
        public string listName => list.name;

        [FieldSettings("List closed")]
        public bool listClosed => list.closed;

        [FieldSettings("Board name")]
        public string boardName => board.name;

        [FieldSettings("Board description")]
        public string boardDesc => board.desc;

        [FieldSettings("Board closed")]
        public bool boardClosed => board.closed;

        [FieldSettings("Board pinned")]
        public bool boardPinned => board.pinned;

        [FieldSettings("Board starred")]
        public bool boardStarred => board.starred;

        [FieldSettings("Board Url", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string boardUrl => board.url;

        [FieldSettings("Board short Url", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string boardShortUrl => board.shortUrl;

        [FieldSettings("Number of check items", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int NumberOfCheckItems => checkItems.Count;

        [FieldSettings("Number of completed check items", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int NumberOfCompletedCheckItems
        {
            get
            {
                return checkItems.Count(item => item.state == TrelloCheckItem.TrelloCheckItemStatus.complete);
            }
        }

        [FieldSettings("Number of incomplete check items", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int NumberOfIncompleteCheckItems
        {
            get
            {
                return checkItems.Count(item => item.state == TrelloCheckItem.TrelloCheckItemStatus.incomplete);
            }
        }

        [FieldSettings("Completed", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool Completed => NumberOfCheckItems == NumberOfCompletedCheckItems;

        #endregion

        #region Hidden properties

        public TrelloCard card { get; set; }
        public TrelloList list { get; set; }
        public TrelloBoard board { get; set; }
        public List<TrelloCheckItem> checkItems { get; set; }

        #endregion

    }
}
