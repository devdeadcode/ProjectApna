namespace eOne.Common.Connectors.Insightly.Models
{
    public class InsightlyPipeline : ConnectorEntityModel
    {

        public int? PIPELINE_ID { get; set; }
        public string PIPELINE_NAME { get; set; }
        public bool FOR_OPPORTUNITIES { get; set; }
        public bool FOR_PROJECTS { get; set; }
        public int? OWNER_USER_ID { get; set; }

    }
}
