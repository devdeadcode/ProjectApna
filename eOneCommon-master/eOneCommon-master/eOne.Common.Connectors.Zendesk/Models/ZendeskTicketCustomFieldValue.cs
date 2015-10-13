using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Zendesk.Models
{
    public class ZendeskTicketCustomFieldValue : DataConnectorEntityModel
    {

        public int id { get; set; }
        public string value { get; set; }

    }
}
