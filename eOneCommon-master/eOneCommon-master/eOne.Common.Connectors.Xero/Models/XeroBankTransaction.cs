using System;
using System.Collections.Generic;

namespace eOne.Common.Connectors.Xero.Models
{
    public class XeroBankTransaction
    {

        public enum XeroBankTransactionType
        {
            RECEIVE,
            RECEIVE_OVERPAYMENT,
            RECEIVE_PREPAYMENT,
            SPEND,
            SPEND_OVERPAYMENT,
            SPEND_PREPAYMENT,
            RECEIVE_TRANSFER,
            SPEND_TRANSFER
        }
        public enum XeroBankTransactionStatus
        {
            AUTHORISED,
            DELETED
        }
        public enum XeroBankTransactionLineAmountType
        {
            Exclusive,
            Inclusive,
            NoTax
        }

        public string Type { get; set; }
        public XeroBankTransactionType TransactionType => (XeroBankTransactionType)Enum.Parse(typeof(XeroBankTransactionType), Type.Replace('-', '_'));
        public XeroContact Contact { get; set; }
        public List<XeroBankTransactionLineItem> Lineitems { get; set; }
        public XeroAccount BankAccount { get; set; }
        public bool IsReconciled { get; set; }
        public DateTime Date { get; set; }
        public string Reference { get; set; }
        public string CurrencyCode { get; set; }
        public decimal CurrencyRate { get; set; }
        public string Url { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalTax { get; set; }
        public decimal Total { get; set; }
        public string BankTransactionID { get; set; }
        public DateTime UpdatedDateUTC { get; set; }
        public bool HasAttachments { get; set; }
        public XeroBankTransactionStatus Status { get; set; }
        public XeroBankTransactionLineAmountType LineAmountTypes { get; set; }

    }
}
