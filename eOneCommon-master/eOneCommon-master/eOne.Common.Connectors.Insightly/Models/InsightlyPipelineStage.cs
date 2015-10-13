using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Insightly.Models
{
    public class InsightlyPipelineStage : DataConnectorEntityModel
    {

        public int? PIPELINE_ID { get; set; }
        public int? STAGE_ID { get; set; }
        public string STAGE_NAME { get; set; }
        public int? STAGE_ORDER { get; set; }
        public int? OWNER_USER_ID { get; set; }

    }
}
