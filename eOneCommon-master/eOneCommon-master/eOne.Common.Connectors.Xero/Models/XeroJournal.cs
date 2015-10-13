using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Xero.Models
{
    public class XeroJournal : DataConnectorEntityModel
    {

        #region Enums

        public enum XeroJournalSourceType
        {
            [Description("Accounts Receivable Invoice")]
            ACCREC,	
            [Description("Accounts Payable Invoice")]
            ACCPAY,	
            [Description("Accounts Receivable Credit Note")]
            ACCRECCREDIT,	
            [Description("Accounts Payable Credit Note")]
            ACCPAYCREDIT,	
            [Description("Payment on an Accounts Receivable Invoice")]
            ACCRECPAYMENT,	
            [Description("Payment on an Accounts Payable Invoice")]
            ACCPAYPAYMENT,	
            [Description("Accounts Receivable Credit Note Payment")]
            ARCREDITPAYMENT,	
            [Description("Accounts Payable Credit Note Payment")]
            APCREDITPAYMENT,	
            [Description("Receive Money Bank Transaction")]
            CASHREC,	
            [Description("Spend Money Bank Transaction")]
            CASHPAID,	
            [Description("Bank Transfer")]
            TRANSFER,	
            [Description("Accounts Receivable Prepayment")]
            ARPREPAYMENT,	
            [Description("Accounts Payable Prepayment")]
            APPREPAYMENT,	
            [Description("Accounts Receivable Overpayment")]
            AROVERPAYMENT,	
            [Description("Accounts Payable Overpayment")]
            APOVERPAYMENT,	
            [Description("Expense Claim")]
            EXPCLAIM,	
            [Description("Expense Claim Payment")]
            EXPPAYMENT,	
            [Description("Manual Journal")]
            MANJOURNAL,	
            [Description("Payslip")]
            PAYSLIP,
            [Description("Payroll Payable")]
            WAGEPAYABLE,
            [Description("Payroll Expense")]
            INTEGRATEDPAYROLLPE	
        }

        #endregion

        #region Default properties

        [FieldSettings("Journal number", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int JournalNumber { get; set; }

        [FieldSettings("Journal date", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime JournalDate { get; set; }

        [FieldSettings("Source type", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(XeroJournalSourceType))]
        public XeroJournalSourceType SourceType { get; set; }

        [FieldSettings("Source ID", DefaultField = true)]
        public string SourceID { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Journal ID")]
        public string JournalID { get; set; }

        [FieldSettings("Reference")]
        public string Reference { get; set; }

        #endregion

        #region Hidden properties

        public List<XeroJournalLine> JournalLines { get; set; }
        public DateTime CreatedDateUTC { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Number of lines", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int NumberOfLines => JournalLines?.Count ?? 0;

        [FieldSettings("Total net debits", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal TotalNetDebits
        {
            get
            {
                return JournalLines?.Sum(line => line.NetDebit) ?? 0;
            }
        }

        [FieldSettings("Total net credits", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal TotalNetCredits
        {
            get
            {
                return JournalLines?.Sum(line => line.NetCredit) ?? 0;
            }
        }

        [FieldSettings("Total gross debits", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal TotalGrossDebits
        {
            get
            {
                return JournalLines?.Sum(line => line.GrossDebit) ?? 0;
            }
        }

        [FieldSettings("Total gross credits", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal TotalGrossCredits
        {
            get
            {
                return JournalLines?.Sum(line => line.GrossCredit) ?? 0;
            }
        }

        [FieldSettings("Total tax debits", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal TotalTaxDebits
        {
            get
            {
                return JournalLines?.Sum(line => line.TaxDebit) ?? 0;
            }
        }

        [FieldSettings("Total tax credits", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal TotalTaxCredits
        {
            get
            {
                return JournalLines?.Sum(line => line.TaxCredit) ?? 0;
            }
        }

        [FieldSettings("Created date", FieldTypeId = DataConnector.FieldTypeIdDate, Created = true)]
        public DateTime CreatedDate => CreatedDateUTC.Date;

        [FieldSettings("Created time", FieldTypeId = DataConnector.FieldTypeIdTime, Created = true)]
        public DateTime CreatedTime => Time(CreatedDateUTC);

        #endregion


    }
}