namespace eOne.Common.Connectors.Infusionsoft.Models
{
    public class InfusionsoftOpportunityStage : ConnectorEntityModel
    {

        public int Id { get; set; }
        public string StageName { get; set; }
        public int StageOrder { get; set; }
        public int TargetNumDays { get; set; }

    }
}
