namespace eOne.Common.Connectors.Insightly.Models
{
    public class InsightlyEventLink : ConnectorEntityModel
    {

        public int? EVENT_LINK_ID { get; set; }
        public int? EVENT_ID { get; set; }
        public int? CONTACT_ID { get; set; }
        public int? ORGANISATION_ID { get; set; }
        public int? OPPORTUNITY_ID { get; set; }
        public int? PROJECT_ID { get; set; }

    }
}
