using System;
using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Xero.Models
{
    public class XeroReceiptLineItem : DataConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Receipt number", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int ReceiptNumber => Receipt.ReceiptNumber;

        [FieldSettings("User name", DefaultField = true)]
        public string UserName => Receipt.UserName;

        [FieldSettings("Description", DefaultField = true)]
        public string Description { get; set; }

        [FieldSettings("Unit amount", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal UnitAmount { get; set; }

        [FieldSettings("Quantity", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal Quantity { get; set; }

        [FieldSettings("Line amount", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal LineAmount { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Discount rate", FieldTypeId = DataConnector.FieldTypeIdPercentage)]
        public decimal DiscountRate { get; set; }

        [FieldSettings("Tax type", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(XeroAccount.XeroAccountTaxType))]
        public XeroAccount.XeroAccountTaxType TaxType { get; set; }

        [FieldSettings("Account code")]
        public string AccountCode { get; set; }

        #endregion

        #region Hidden properties

        public List<XeroTrackingCategory> Tracking { get; set; }
        public XeroReceipt Receipt { get; set; }

        #endregion

        #region Calculated fields

        [FieldSettings("Receipt date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime ReceiptDate => Receipt.Date;

        [FieldSettings("Receipt status", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(XeroReceipt.XeroReceiptStatus))]
        public XeroReceipt.XeroReceiptStatus ReceiptStatus => Receipt.Status;

        [FieldSettings("Reference")]
        public string ReceiptReference => Receipt.Reference;

        [FieldSettings("Receipt ID")]
        public string ReceiptID => Receipt.Reference;

        [FieldSettings("Receipt has attachments", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool ReceiptHasAttachments => Receipt.HasAttachments;

        [FieldSettings("Receipt link", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string ReceiptUrl => Receipt.Url;

        [FieldSettings("Line amount type", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(XeroReceipt.XeroReceiptLineAmountType))]
        public XeroReceipt.XeroReceiptLineAmountType ReceiptLineAmountTypes => Receipt.LineAmountTypes;

        [FieldSettings("Contact name")]
        public string ContactName => Receipt.ContactName;

        [FieldSettings("Contact status", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(XeroContact.XeroContactStatus))]
        public XeroContact.XeroContactStatus ContactStatus => Receipt.ContactStatus;

        #endregion

    }
}