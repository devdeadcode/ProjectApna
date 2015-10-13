using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Zendesk.Models
{
    public class ZendeskMinutesTaken : DataConnectorEntityModel
    {

        public int calendar { get; set; }
        public int business { get; set; }

    }
}
