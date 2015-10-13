using System;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Xero.Models
{
    public class XeroBankTransfer : DataConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("From bank account", DefaultField = true)]
        public string FromBankAccountName => FromBankAccount.Name;

        [FieldSettings("To bank account", DefaultField = true)]
        public string ToBankAccountName => ToBankAccount.Name;

        [FieldSettings("Date", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime Date { get; set; }

        [FieldSettings("Amount", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal Amount { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Bank transfer ID")]
        public string BankTransferID { get; set; }

        [FieldSettings("Currency rate")]
        public decimal CurrencyRate { get; set; }

        [FieldSettings("From bank transaction ID")]
        public string FromBankTransactionID { get; set; }

        [FieldSettings("To bank transaction ID")]
        public string ToBankTransactionID { get; set; }

        [FieldSettings("Has attachments")]
        public bool HasAttachments { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("From bank account code")]
        public string FromBankAccountCode => FromBankAccount.Code;

        [FieldSettings("From bank account ID")]
        public string FromBankAccountID => FromBankAccount.AccountID;

        [FieldSettings("To bank account code")]
        public string ToBankAccountCode => ToBankAccount.Code;

        [FieldSettings("To bank account ID")]
        public string ToBankAccountID => ToBankAccount.AccountID;

        #endregion

        #region Hidden properties

        public XeroAccount FromBankAccount { get; set; }
        public XeroAccount ToBankAccount { get; set; }

        #endregion

    }
}