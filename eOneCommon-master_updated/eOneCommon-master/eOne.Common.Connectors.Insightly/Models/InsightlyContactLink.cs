namespace eOne.Common.Connectors.Insightly.Models
{
    public class InsightlyContactLink : ConnectorEntityModel
    {

        public int? CONTACT_LINK_ID { get; set; }
        public int? FIRST_CONTACT_ID { get; set; }
        public int? SECOND_CONTACT_ID { get; set; }
        public int? RELATIONSHIP_ID { get; set; }
        public string DETAILS { get; set; }

    }
}
