using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Insightly.Models
{
    public class InsightlyEmailLink : DataConnectorEntityModel
    {

        public int? EMAIL_LINK_ID { get; set; }
        public int? EMAIL_ID { get; set; }
        public int? CONTACT_ID { get; set; }
        public int? ORGANISATION_ID { get; set; }
        public int? OPPORTUNITY_ID { get; set; }
        public int? PROJECT_ID { get; set; }

    }
}
