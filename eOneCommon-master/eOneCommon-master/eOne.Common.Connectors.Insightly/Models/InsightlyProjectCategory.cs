using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Insightly.Models
{
    public class InsightlyProjectCategory : DataConnectorEntityModel
    {

        public int? CATEGORY_ID { get; set; }
        public string CATEGORY_NAME { get; set; }
        public bool ACTIVE { get; set; }
        public string BACKGROUND_COLOR { get; set; }

    }
}
