using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Zendesk.Models
{
    public class ZendeskTicketSatisfactionRating : DataConnectorEntityModel
    {

        public enum ZendeskTicketSatisfactionRatingScore
        {
            good
        }
        
        public int id { get; set; }
        public ZendeskTicketSatisfactionRatingScore score { get; set; }
        public string comment { get; set; }

    }
}
