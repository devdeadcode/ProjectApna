using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace eOne.Common.Connectors.Xero.Models
{
    public class XeroReceipt : ConnectorEntityModel
    {

        #region Enums

        public enum XeroReceiptStatus
        {
            [Description("Draft")]
            DRAFT,
            [Description("Submitted")]
            SUBMITTED,
            [Description("Authorised")]
            AUTHORISED,
            [Description("Declined")]
            DECLINED
        }
        public enum XeroReceiptLineAmountType
        {
            [Description("Exclusive")]
            Exclusive,
            [Description("Inclusive")]
            Inclusive,
            [Description("No tax")]
            NoTax
        }

        #endregion

        #region Default properties

        [FieldSettings("Receipt number", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int ReceiptNumber { get; set; }

        [FieldSettings("User name", DefaultField = true)]
        public string UserName => User == null ? string.Empty : User.Name;

        [FieldSettings("Date", DefaultField = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime Date { get; set; }

        [FieldSettings("Status", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(XeroReceiptStatus))]
        public XeroReceiptStatus Status { get; set; }

        [FieldSettings("Total", DefaultField = true, FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal Total { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Reference")]
        public string Reference { get; set; }

        [FieldSettings("Subtotal", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal SubTotal { get; set; }

        [FieldSettings("Total tax", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal TotalTax { get; set; }

        [FieldSettings("Receipt ID")]
        public string ReceiptID { get; set; }

        [FieldSettings("Has attachments", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool HasAttachments { get; set; }

        [FieldSettings("Link", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string Url { get; set; }

        [FieldSettings("Line amount types", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(XeroReceiptLineAmountType))]
        public XeroReceiptLineAmountType LineAmountTypes { get; set; }

        [FieldSettings("Updated date", FieldTypeId = Connector.FieldTypeIdDate, Modified = true)]
        public DateTime UpdatedDateUTC { get; set; }

        #endregion

        #region Hidden properties

        public XeroContact Contact { get; set; }
        public XeroUser User { get; set; }
        public List<XeroReceiptLineItem> LineItems { get; set; }
        
        #endregion

        #region Calculations

        [FieldSettings("Updated time", FieldTypeId = Connector.FieldTypeIdTime, Modified = true)]
        public DateTime UpdatedTime => UpdatedDateUTC;

        [FieldSettings("Contact name")]
        public string ContactName => Contact.Name;

        [FieldSettings("Contact status", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(XeroContact.XeroContactStatus))]
        public XeroContact.XeroContactStatus ContactStatus => Contact.ContactStatus;

        #endregion

    }
}