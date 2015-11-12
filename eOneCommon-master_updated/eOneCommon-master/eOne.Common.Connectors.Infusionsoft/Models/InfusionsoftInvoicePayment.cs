using System;

namespace eOne.Common.Connectors.Infusionsoft.Models
{
    public class InfusionsoftInvoicePayment : ConnectorEntityModel
    {

        public decimal? Amt { get; set; }
        public int? Id { get; set; }
        public int? InvoiceId { get; set; }
        public DateTime? PayDate { get; set; }
        public string PayStatus { get; set; }
        public int? PaymentId { get; set; }
        public int? SkipCommission { get; set; }

    }
}
