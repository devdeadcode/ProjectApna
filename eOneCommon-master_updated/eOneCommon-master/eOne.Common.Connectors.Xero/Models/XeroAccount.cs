using System.ComponentModel;

namespace eOne.Common.Connectors.Xero.Models
{
    public class XeroAccount
    {

        #region Enums

        public enum XeroAccountType
        {
            [Description("Bank")]
            BANK,	
            [Description("Current asset")]
            CURRENT,	
            [Description("Current liability")]
            CURRLIAB,	
            [Description("Depreciation")]
            DEPRECIATN,
            [Description("Direct Costs")]
            DIRECTCOSTS,
            [Description("Equity")]
            EQUITY,
            [Description("Expense")]
            EXPENSE,
            [Description("Fixed Asset")]
            FIXED,
            [Description("Inventory Asset")]
            INVENTORY,
            [Description("Liability")]
            LIABILITY,
            [Description("Non-current asset")]
            NONCURRENT,	
            [Description("Other income")]
            OTHERINCOME,
            [Description("Overhead")]
            OVERHEADS,
            [Description("Prepayment")]
            PREPAYMENT,
            [Description("Revenue")]
            REVENUE,
            [Description("Sales")]
            SALES,
            [Description("Non-current liability")]
            TERMLIAB,
            [Description("PAYG liability")]
            PAYGLIABILITY,
            [Description("Superannuation expense")]
            SUPERANNUATIONEXPENSE,
            [Description("Superannuation liability")]
            SUPERANNUATIONLIABILITY,
            [Description("Wages expense")]
            WAGESEXPENSE,
            [Description("Wages payable liability")]
            WAGESPAYABLELIABILITY
        }
        public enum XeroAccountTaxType
        {
            [Description("None")]
            NONE,
            [Description("Input")]
            INPUT,
            [Description("Output")]
            OUTPUT,
            [Description("Sales tax on imports")]
            GSTONIMPORTS
        }
        public enum XeroAccountClass
        {
            [Description("Asset")]
            ASSET,
            [Description("Equity")]
            EQUITY,
            [Description("Expense")]
            EXPENSE,
            [Description("Liability")]
            LIABILITY,
            [Description("Revenue")]
            REVENUE,
        }
        public enum XeroAccountStatus
        {
            [Description("Active")]
            ACTIVE,
            [Description("Archived")]
            ARCHIVED
        }
        public enum XeroAccountSystemAccountType
        {
            [Description("Accounts Receivable")]	
            DEBTORS,
            [Description("Accounts Payable")]	
            CREDITORS,
            [Description("Bank Revaluations")]	
            BANKCURRENCYGAIN,
            [Description("GST / VAT")]	
            GST,
            [Description("GST On Imports")]	
            GSTONIMPORTS,
            [Description("Historical Adjustment")]	
            HISTORICAL,
            [Description("Realised Currency Gains")]	
            REALISEDCURRENCYGAIN,
            [Description("Retained Earnings")]	
            RETAINEDEARNINGS,
            [Description("Rounding")]	
            ROUNDING,
            [Description("Tracking Transfers")]	
            TRACKINGTRANSFERS,
            [Description("Unpaid Expense Claims")]	
            UNPAIDEXPCLM,
            [Description("Unrealised Currency Gains")]
            UNREALISEDCURRENCYGAIN,
            [Description("Wages Payable")]
            WAGEPAYABLES
        }

        #endregion

        #region Default properties

        [FieldSettings("Account code", DefaultField = true)]
        public string Code { get; set; }

        [FieldSettings("Name", DefaultField = true)]
        public string Name { get; set; }

        [FieldSettings("Description", DefaultField = true)]
        public string Description { get; set; }

        [FieldSettings("Class", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(XeroAccountClass))]
        public XeroAccountClass Class { get; set; }

        [FieldSettings("Type", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(XeroAccountType))]
        public XeroAccountType Type { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Account ID")]
        public string AccountID { get; set; }

        [FieldSettings("Tax type", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(XeroAccountTaxType))]
        public XeroAccountTaxType TaxType { get; set; }

        [FieldSettings("Enable payments to account", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool EnablePaymentsToAccount { get; set; }

        [FieldSettings("Bank account number")]
        public string BankAccountNumber { get; set; }

        [FieldSettings("Currency code")]
        public string CurrencyCode { get; set; }

        [FieldSettings("Status", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(XeroAccountStatus))]
        public XeroAccountStatus Status { get; set; }

        [FieldSettings("System account", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(XeroAccountSystemAccountType))]
        public XeroAccountSystemAccountType SystemAccount { get; set; }

        [FieldSettings("Reporting code")]
        public string ReportingCode { get; set; }

        [FieldSettings("Reporting code name")]
        public string ReportingCodeName { get; set; }

        [FieldSettings("Has attachments", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool HasAttachments { get; set; }

        #endregion

    }
}