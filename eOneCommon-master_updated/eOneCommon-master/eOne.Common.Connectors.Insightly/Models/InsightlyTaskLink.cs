namespace eOne.Common.Connectors.Insightly.Models
{
    public class InsightlyTaskLink : ConnectorEntityModel
    {

        public int TASK_LINK_ID { get; set; }
        public int? TASK_ID { get; set; }
        public int? CONTACT_ID { get; set; }
        public int? ORGANISATION_ID { get; set; }
        public int? OPPORTUNITY_ID { get; set; }
        public int? PROJECT_ID { get; set; }

    }
}
