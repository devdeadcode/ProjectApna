namespace eOne.Common.Connectors.Infusionsoft.Models
{
    public class InfusionsoftSubscriptionPlan : ConnectorEntityModel
    {

        public bool? Active { get; set; }
        public string Cycle { get; set; }
        public int? Frequency { get; set; }
        public int? Id { get; set; }
        public decimal? PlanPrice { get; set; }
        public decimal? PreAuthorizeAmount { get; set; }
        public int? ProductId { get; set; }
        public bool? Prorate { get; set; }

    }
}
