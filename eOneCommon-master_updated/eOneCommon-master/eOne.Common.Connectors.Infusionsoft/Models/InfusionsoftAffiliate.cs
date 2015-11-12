namespace eOne.Common.Connectors.Infusionsoft.Models
{
    public class InfusionsoftAffiliate
    {

        [FieldSettings("Name", DefaultField = true)]
        public string AffName { get; set; }

        [FieldSettings("Code", DefaultField = true)]
        public string AffCode { get; set; }
        
        public int? ContactId { get; set; }
        public int? DefCommissionType { get; set; }
        public int? Id { get; set; }
        public int? LeadCookieFor { get; set; }
        public int? NotifyLead { get; set; }
        public int? NotifySale { get; set; }
        public int? ParentId { get; set; }
        public int? PayoutType { get; set; }
        public int? Status { get; set; }

        public decimal? LeadAmt { get; set; }
        public decimal? LeadPercent { get; set; }
        public decimal? SaleAmt { get; set; }
        public decimal? SalePercent { get; set; }

    }
}
