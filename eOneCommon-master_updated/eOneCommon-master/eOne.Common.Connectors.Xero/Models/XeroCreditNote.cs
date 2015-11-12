using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace eOne.Common.Connectors.Xero.Models
{
    public class XeroCreditNote
    {

        public enum XeroCreditNoteType
        {
            [Description("Accounts payable")]
            ACCPAYCREDIT,
            [Description("Accounts receivable")]
            ACCRECCREDIT
        }
        public enum XeroCreditNoteStatus
        {
            DRAFT,
            SUBMITTED,
            DELETED,
            AUTHORISED,
            PAID,
            VOIDED
        }
        public enum XeroCreditNoteLineAmountType
        {
            Exclusive,
            Inclusive,
            NoTax
        }

        public string Reference { get; set; }
        public XeroContact Contact { get; set; }
        public DateTime Date { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalTax { get; set; }
        public decimal Total { get; set; }
        public DateTime UpdatedDateUTC { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime FullyPaidOnDate { get; set; }
        public string CreditNoteID { get; set; }
        public string CreditNoteNumber { get; set; }
        public bool SentToContact { get; set; }
        public decimal CurrencyRate { get; set; }
        public decimal RemainingCredit { get; set; }
        public string BrandingThemeID { get; set; }
        public bool HasAttachments { get; set; }
        public List<XeroAllocation> Allocations { get; set; }
        public XeroCreditNoteType Type { get; set; }
        public XeroCreditNoteStatus Status { get; set; }
        public XeroCreditNoteLineAmountType LineAmountTypes { get; set; }
        public List<XeroInvoiceLineItem> LineItems { get; set; }

    }
}