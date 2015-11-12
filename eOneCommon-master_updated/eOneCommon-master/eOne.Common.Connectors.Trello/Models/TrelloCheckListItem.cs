using System.ComponentModel;

namespace eOne.Common.Connectors.Trello.Models
{
    public class TrelloCheckItem : ConnectorEntityModel
    {

        public enum TrelloCheckItemStatus
        {
            [Description("Incomplete")]
            incomplete,
            [Description("Complete")]
            complete
        }

        public string id { get; set; }

        public TrelloCheckItemStatus state { get; set; }

        public string name { get; set; }

    }
}
