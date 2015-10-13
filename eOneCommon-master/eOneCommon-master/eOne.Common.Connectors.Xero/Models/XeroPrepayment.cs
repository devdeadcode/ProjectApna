using System;
using System.Collections.Generic;

namespace eOne.Common.Connectors.Xero.Models
{
    public class XeroPrepayment
    {

        public enum XeroPrepaymentType
        {
            RECEIVE_PREPAYMENT,
            SPEND_PREPAYMENT
        }
        public enum XeroPrepaymentStatus
        {
            AUTHORISED,
            PAID,
            VOIDED
        }
        public enum XeroPrepaymentLineAmountType
        {
            Exclusive,
            Inclusive,
            NoTax
        }

        public XeroContact Contact { get; set; }
        public DateTime Date { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalTax { get; set; }
        public decimal Total { get; set; }
        public DateTime UpdatedDateUTC { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime FullyPaidOnDate { get; set; }
        public string PrepaymentID { get; set; }
        public decimal CurrencyRate { get; set; }
        public decimal RemainingCredit { get; set; }
        public List<XeroAllocation> Allocations { get; set; }
        public bool HasAttachments { get; set; }
        public string Type { get; set; }
        public XeroPrepaymentType PrepaymentType => (XeroPrepaymentType)Enum.Parse(typeof(XeroPrepaymentType), Type.Replace('-', '_'));
        public XeroPrepaymentStatus Status { get; set; }
        public XeroPrepaymentLineAmountType LineAmountTypes { get; set; }
        public List<XeroInvoiceLineItem> LineItems { get; set; }

    }
}