namespace eOne.Common.Connectors.Insightly.Models
{
    public class InsightlyOrganisationLink : ConnectorEntityModel
    {

        public int? ORG_LINK_ID { get; set; }
        public int? FIRST_ORGANISATION_ID { get; set; }
        public int? SECOND_ORGANISATION_ID { get; set; }
        public int? RELATIONSHIP_ID { get; set; }
        public string DETAILS { get; set; }

    }
}
