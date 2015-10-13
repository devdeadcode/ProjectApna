using System;
using System.Collections.Generic;

namespace eOne.Common.Connectors.Xero.Models
{
    public class XeroOverpayment
    {

        public enum XeroOverpaymentType
        {
            RECEIVE_OVERPAYMENT,
            SPEND_OVERPAYMENT
        }
        public enum XeroOverpaymentStatus
        {
            AUTHORISED,
            PAID,
            VOIDED
        }
        public enum XeroOverpaymentLineAmountType
        {
            Exclusive,
            Inclusive,
            NoTax
        }

        public string Type { get; set; }
        public XeroOverpaymentType OverpaymentType => (XeroOverpaymentType)Enum.Parse(typeof(XeroOverpaymentType), Type.Replace('-', '_'));
        public XeroContact Contact { get; set; }
        public DateTime Date { get; set; }
        public XeroOverpaymentStatus Status { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalTax { get; set; }
        public decimal Total { get; set; }
        public DateTime UpdatedDateUTC { get; set; }
        public string CurrencyCode { get; set; }
        public string OverpaymentID { get; set; }
        public DateTime FullyPaidOnDate { get; set; }
        public decimal RemainingCredit { get; set; }
        public bool HasAttachments { get; set; }
        public List<XeroAllocation> Allocations { get; set; }
        public decimal CurrencyRate { get; set; }
        public XeroOverpaymentLineAmountType LineAmountTypes { get; set; }
        public List<XeroInvoiceLineItem> LineItems { get; set; }

    }
}