using System;
using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Xero.Models
{
    public class XeroJournalLine : DataConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Journal number")]
        public int JournalNumber => Journal.JournalNumber;

        [FieldSettings("Account code", DefaultField = true)]
        public string AccountCode { get; set; }

        [FieldSettings("Account name", DefaultField = true)]
        public string AccountName { get; set; }

        [FieldSettings("Account type", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(XeroAccount.XeroAccountType))]
        public XeroAccount.XeroAccountType AccountType { get; set; }

        [FieldSettings("Net amount", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal NetAmount { get; set; }

        [FieldSettings("Tax amount", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal TaxAmount { get; set; }

        [FieldSettings("Gross amount", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal GrossAmount { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Description")]
        public string Description { get; set; }

        [FieldSettings("Tax type", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(XeroAccount.XeroAccountTaxType))]
        public XeroAccount.XeroAccountTaxType TaxType { get; set; }

        [FieldSettings("Tax name")]
        public string TaxName { get; set; }

        [FieldSettings("Line ID")]
        public string JournalLineID { get; set; }

        [FieldSettings("Account ID")]
        public string AccountID { get; set; }

        #endregion

        #region Hidden properties

        public XeroJournal Journal { get; set; }
        public List<XeroTrackingCategory> TrackingCategories { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Net debit amount")]
        public decimal NetDebit => NetAmount > 0 ? NetAmount : 0;

        [FieldSettings("Net credit amount")]
        public decimal NetCredit => NetAmount > 0 ? 0 : NetAmount;

        [FieldSettings("Gross debit amount")]
        public decimal GrossDebit => GrossAmount > 0 ? GrossAmount : 0;

        [FieldSettings("Gross credit amount")]
        public decimal GrossCredit => GrossAmount > 0 ? 0 : GrossAmount;

        [FieldSettings("Tax debit amount")]
        public decimal TaxDebit => TaxAmount > 0 ? TaxAmount : 0;

        [FieldSettings("Tax credit amount")]
        public decimal TaxCredit => TaxAmount > 0 ? 0 : TaxAmount;

        [FieldSettings("Journal date")]
        public DateTime JournalDate => Journal.JournalDate;

        [FieldSettings("Journal source type")]
        public XeroJournal.XeroJournalSourceType JournalSourceType => Journal.SourceType;

        [FieldSettings("Journal source ID")]
        public string JournalSourceID => Journal.SourceID;

        [FieldSettings("Journal ID")]
        public string JournalID => Journal.JournalID;

        [FieldSettings("Journal reference")]
        public string JournalReference => Journal.Reference;

        #endregion

    }
}