using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Zendesk.Models
{
    public class ZendeskTicketVia : DataConnectorEntityModel
    {

        public string channel { get; set; }
        public ZendeskTicketViaSource source { get; set; }

    }
}
