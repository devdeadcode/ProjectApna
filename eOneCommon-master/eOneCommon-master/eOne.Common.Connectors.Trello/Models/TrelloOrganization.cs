using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Trello.Models
{
    public class TrelloOrganization : DataConnectorEntityModel
    {

        public string id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string desc { get; set; }
        public string url { get; set; }
        public string website { get; set; }

    }
}
