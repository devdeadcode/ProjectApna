using System;
using System.ComponentModel;

namespace eOne.Common.Connectors.Xero.Models
{
    public class XeroPayment
    {

        public enum XeroPaymentStatus
        {
            AUTHORISED,
            DELETED
        }
        public enum XeroPaymentType
        {
            [Description("Accounts Receivable Payment")]
            ACCRECPAYMENT,	
            [Description("Accounts Payable Payment")]
            ACCPAYPAYMENT,	
            [Description("Accounts Receivable Credit Payment (Refund)")]
            ARCREDITPAYMENT,	
            [Description("Accounts Payable Credit Payment (Refund)")]
            APCREDITPAYMENT,	
            [Description("Accounts Receivable Overpayment Payment (Refund)")]
            AROVERPAYMENTPAYMENT,	
            [Description("Accounts Receivable Prepayment Payment (Refund)")]
            ARPREPAYMENTPAYMENT,	
            [Description("Accounts Payable Prepayment Payment (Refund)")]
            APPREPAYMENTPAYMENT,
            [Description("Accounts Payable Overpayment Payment (Refund)")]
            APOVERPAYMENTPAYMENT	
        }

        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string PaymentID { get; set; }
        public decimal CurrencyRate { get; set; }
        public string Reference { get; set; }
        public bool IsReconciled { get; set; }
        public DateTime UpdatedDateUTC { get; set; }
        public XeroAccount Account { get; set; }
        public XeroInvoice Invoice { get; set; }
        public XeroCreditNote CreditNote { get; set; }
        public XeroPrepayment Prepayment { get; set; }
        public XeroOverpayment Overpayment { get; set; }
        public XeroPaymentStatus Status { get; set; }
        public XeroPaymentType PaymentType { get; set; }

    }
}