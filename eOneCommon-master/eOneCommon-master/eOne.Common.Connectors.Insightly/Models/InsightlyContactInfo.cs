using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Insightly.Models
{
    public class InsightlyContactInfo : DataConnectorEntityModel
    {

        public int? CONTACT_INFO_ID { get; set; }
        public string TYPE { get; set; }
        public string SUBTYPE { get; set; }
        public string LABEL { get; set; }
        public string DETAIL { get; set; }

    }
}
