using System;
using System.Collections.Generic;
using System.ComponentModel;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Xero.Models
{
    public class XeroReceipt : DataConnectorEntityModel
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

        [FieldSettings("Receipt number", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int ReceiptNumber { get; set; }

        [FieldSettings("User name", DefaultField = true)]
        public string UserName => User == null ? string.Empty : User.Name;

        [FieldSettings("Date", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime Date { get; set; }

        [FieldSettings("Status", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(XeroReceiptStatus))]
        public XeroReceiptStatus Status { get; set; }

        [FieldSettings("Total", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal Total { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Reference")]
        public string Reference { get; set; }

        [FieldSettings("Subtotal", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal SubTotal { get; set; }

        [FieldSettings("Total tax", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal TotalTax { get; set; }

        [FieldSettings("Receipt ID")]
        public string ReceiptID { get; set; }

        [FieldSettings("Has attachments", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool HasAttachments { get; set; }

        [FieldSettings("Link", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string Url { get; set; }

        [FieldSettings("Line amount types", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(XeroReceiptLineAmountType))]
        public XeroReceiptLineAmountType LineAmountTypes { get; set; }

        #endregion

        #region Hidden properties

        public XeroContact Contact { get; set; }
        public XeroUser User { get; set; }
        public List<XeroReceiptLineItem> LineItems { get; set; }
        public DateTime UpdatedDateUTC { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Updated date", FieldTypeId = DataConnector.FieldTypeIdDate, Modified = true)]
        public DateTime UpdatedDate => UpdatedDateUTC.Date;

        [FieldSettings("Updated time", FieldTypeId = DataConnector.FieldTypeIdTime, Modified = true)]
        public DateTime UpdatedTime => Time(UpdatedDateUTC);

        [FieldSettings("Contact name")]
        public string ContactName => Contact.Name;

        [FieldSettings("Contact status", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(XeroContact.XeroContactStatus))]
        public XeroContact.XeroContactStatus ContactStatus => Contact.ContactStatus;

        #endregion

    }
}