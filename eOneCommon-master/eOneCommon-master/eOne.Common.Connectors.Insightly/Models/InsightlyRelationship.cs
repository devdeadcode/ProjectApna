using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Insightly.Models
{
    public class InsightlyRelationship : DataConnectorEntityModel
    {

        public int? RELATIONSHIP_ID { get; set; }
        public string FORWARD_TITLE { get; set; }
        public string FORWARD { get; set; }
        public string REVERSE_TITLE { get; set; }
        public string REVERSE { get; set; }
        public bool FOR_CONTACTS { get; set; }
        public bool FOR_ORGANISATIONS { get; set; }

    }
}
