namespace eOne.Common.Connectors.Zendesk.Models
{
    public class ZendeskTicketVia : ConnectorEntityModel
    {

        public string channel { get; set; }
        public ZendeskTicketViaSource source { get; set; }

    }
}
