namespace eOne.Common.Connectors.Insightly.Models
{
    public class InsightlyLink : ConnectorEntityModel
    {

        public int? LINK_ID { get; set; }
        public int? CONTACT_ID { get; set; }
        public int? OPPORTUNITY_ID { get; set; }
        public int? ORGANISATION_ID { get; set; }
        public int? PROJECT_ID { get; set; }
        public int? SECOND_PROJECT_ID { get; set; }
        public int? SECOND_OPPORTUNITY_ID { get; set; }
        public string ROLE { get; set; }
        public string DETAILS { get; set; }

    }
}
