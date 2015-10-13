using System.ComponentModel;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Trello.Models
{
    public class TrelloCheckItem : DataConnectorEntityModel
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
