using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Insightly.Models
{
    public class InsightlyNoteLink : DataConnectorEntityModel
    {

        public int? NOTE_LINK_ID { get; set; }
        public int? NOTE_ID { get; set; }
        public int? CONTACT_ID { get; set; }
        public int? ORGANISATION_ID { get; set; }
        public int? OPPORTUNITY_ID { get; set; }
        public int? PROJECT_ID { get; set; }

    }
}
