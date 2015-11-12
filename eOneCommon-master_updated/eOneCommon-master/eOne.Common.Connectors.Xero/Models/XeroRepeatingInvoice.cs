using System.Collections.Generic;

namespace eOne.Common.Connectors.Xero.Models
{
    public class XeroRepeatingInvoice
    {

        public enum XeroRepeatingInvoiceType
        {
            ACCPAY,
            ACCREC
        }
        public enum XeroRepeatingInvoiceStatus
        {
            DRAFT,
            AUTHORISED
        }
        public enum XeroRepeatingInvoiceLineAmountType
        {
            Exclusive,
            Inclusive,
            NoTax
        }

        public XeroRepeatingInvoiceType Type { get; set; }
        public XeroContact Contact { get; set; }
        public string Reference { get; set; }
        public string CurrencyCode { get; set; }
        public XeroRepeatingInvoiceStatus Status { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalTax { get; set; }
        public decimal Total { get; set; }
        public string RepeatingInvoiceID { get; set; }
        public bool HasAttachments { get; set; }
        public XeroRepeatingInvoiceSchedule Schedule { get; set; }
        public List<XeroRepeatingInvoiceLineItem> LineItems { get; set; }
        public XeroRepeatingInvoiceLineAmountType LineAmountTypes { get; set; }
        public string BrandingThemeID { get; set; }

    }
}