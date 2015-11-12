using System.ComponentModel;

namespace eOne.Common.Connectors.Zendesk.Models
{
    public class ZendeskTicketSatisfactionRating : ConnectorEntityModel
    {

        public enum ZendeskTicketSatisfactionRatingScore
        {
            [Description("Good")]
            good,
            [Description("Offered")]
            offered,
            [Description("Unoffered")]
            unoffered,
            [Description("Bad")]
            bad
        }
        
        public int id { get; set; }
        public ZendeskTicketSatisfactionRatingScore? score { get; set; }
        public string comment { get; set; }

    }
}
