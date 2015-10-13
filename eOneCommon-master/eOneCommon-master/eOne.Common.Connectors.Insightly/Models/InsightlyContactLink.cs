using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Insightly.Models
{
    public class InsightlyContactLink : DataConnectorEntityModel
    {

        public int? CONTACT_LINK_ID { get; set; }
        public int? FIRST_CONTACT_ID { get; set; }
        public int? SECOND_CONTACT_ID { get; set; }
        public int? RELATIONSHIP_ID { get; set; }
        public string DETAILS { get; set; }

    }
}
