using System;
using System.Collections.Generic;

namespace eOne.Common.Connectors.Xero.Models
{
    public class XeroReceiptLineItem : ConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Receipt number", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int ReceiptNumber => Receipt.ReceiptNumber;

        [FieldSettings("User name", DefaultField = true)]
        public string UserName => Receipt.UserName;

        [FieldSettings("Description", DefaultField = true)]
        public string Description { get; set; }

        [FieldSettings("Unit amount", DefaultField = true, FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal UnitAmount { get; set; }

        [FieldSettings("Quantity", DefaultField = true, FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal Quantity { get; set; }

        [FieldSettings("Line amount", DefaultField = true, FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal LineAmount { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Discount rate", FieldTypeId = Connector.FieldTypeIdPercentage)]
        public decimal DiscountRate { get; set; }

        [FieldSettings("Tax type", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(XeroAccount.XeroAccountTaxType))]
        public XeroAccount.XeroAccountTaxType TaxType { get; set; }

        [FieldSettings("Account code")]
        public string AccountCode { get; set; }

        #endregion

        #region Hidden properties

        public List<XeroTrackingCategory> Tracking { get; set; }
        public XeroReceipt Receipt { get; set; }

        #endregion

        #region Calculated fields

        [FieldSettings("Receipt date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime ReceiptDate => Receipt.Date;

        [FieldSettings("Receipt status", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(XeroReceipt.XeroReceiptStatus))]
        public XeroReceipt.XeroReceiptStatus ReceiptStatus => Receipt.Status;

        [FieldSettings("Reference")]
        public string ReceiptReference => Receipt.Reference;

        [FieldSettings("Receipt ID")]
        public string ReceiptID => Receipt.Reference;

        [FieldSettings("Receipt has attachments", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool ReceiptHasAttachments => Receipt.HasAttachments;

        [FieldSettings("Receipt link", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string ReceiptUrl => Receipt.Url;

        [FieldSettings("Line amount type", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(XeroReceipt.XeroReceiptLineAmountType))]
        public XeroReceipt.XeroReceiptLineAmountType ReceiptLineAmountTypes => Receipt.LineAmountTypes;

        [FieldSettings("Contact name")]
        public string ContactName => Receipt.ContactName;

        [FieldSettings("Contact status", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(XeroContact.XeroContactStatus))]
        public XeroContact.XeroContactStatus ContactStatus => Receipt.ContactStatus;

        #endregion

    }
}