using System;

namespace eOne.Common.Connectors.Infusionsoft.Models
{
    public class InfusionsoftInvoiceItem : ConnectorEntityModel
    {

        public int? CommissionStatus { get; set; }
        public DateTime? DateCreated { get; set; }
        public string Description { get; set; }
        public decimal? Discount { get; set; }
        public int? Id { get; set; }
        public decimal? InvoiceAmt { get; set; }
        public int? InvoiceId { get; set; }
        public int? OrderItemId { get; set; }

    }
}
