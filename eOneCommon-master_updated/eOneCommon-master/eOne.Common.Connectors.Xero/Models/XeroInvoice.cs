using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace eOne.Common.Connectors.Xero.Models
{
    public class XeroInvoice : ConnectorEntityModel
    {

        #region Enums

        public enum XeroInvoiceType
        {
            [Description("Accounts receivable")]
            ACCREC,
            [Description("Accounts payable")]
            ACCPAY
        }
        public enum XeroInvoiceStatus
        {
            [Description("Draft")]
            DRAFT,
            [Description("Submitted")]
            SUBMITTED,
            [Description("Deleted")]
            DELETED,
            [Description("Authorised")]
            AUTHORISED,
            [Description("Paid")]
            PAID,
            [Description("Voided")]
            VOIDED
        }
        public enum XeroInvoiceLineAmountType
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

        [FieldSettings("Type", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(XeroInvoiceType))]
        public XeroInvoiceType Type { get; set; }

        [FieldSettings("Invoice number", DefaultField = true)]
        public string InvoiceNumber { get; set; }

        [FieldSettings("Contact name", DefaultField = true)]
        public string ContactName => Contact.Name;

        [FieldSettings("Date", DefaultField = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime Date { get; set; }

        [FieldSettings("Total", DefaultField = true, FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal Total { get; set; }

        [FieldSettings("Status", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(XeroInvoiceType))]
        public XeroInvoiceStatus Status { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Due date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime DueDate { get; set; }

        [FieldSettings("Line amount types", FieldTypeId = Connector.FieldTypeIdEnum)]
        public XeroInvoiceLineAmountType LineAmountTypes { get; set; }

        [FieldSettings("Subtotal", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal SubTotal { get; set; }

        [FieldSettings("Total tax", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal TotalTax { get; set; }

        [FieldSettings("Currency code")]
        public string CurrencyCode { get; set; }

        [FieldSettings("Invoice ID")]
        public string InvoiceID { get; set; }

        [FieldSettings("Amount due", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal AmountDue { get; set; }

        [FieldSettings("Amount paid", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal AmountPaid { get; set; }

        [FieldSettings("Amount credited", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal AmountCredited { get; set; }

        [FieldSettings("Fully paid on date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime FullyPaidOnDate { get; set; }

        [FieldSettings("Reference")]
        public string Reference { get; set; }

        [FieldSettings("Branding theme ID")]
        public string BrandingThemeID { get; set; }

        [FieldSettings("Link", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string Url { get; set; }

        [FieldSettings("Currency rate", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal CurrencyRate { get; set; }

        [FieldSettings("Sent to contact", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool SentToContact { get; set; }

        [FieldSettings("Expected payment date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime ExpectedPaymentDate { get; set; }

        [FieldSettings("Planned payment date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime PlannedPaymentDate { get; set; }

        [FieldSettings("Total discount", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal TotalDiscount { get; set; }

        [FieldSettings("Has attachments", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool HasAttachments { get; set; }

        [FieldSettings("Updated date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime UpdatedDateUTC { get; set; }

        #endregion

        #region Hidden properties

        public XeroContact Contact { get; set; }
        public List<XeroInvoiceLineItem> LineItems { get; set; }
        public List<XeroCreditNote> CreditNotes { get; set; }
        public List<XeroPayment> Payments { get; set; }
        public List<XeroPrepayment> Prepayments { get; set; }
        public List<XeroOverpayment> Overpayments { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Updated time", FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime UpdatedTime => UpdatedDateUTC;

        [FieldSettings("Supplier", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool IsSupplier { get; set; }

        [FieldSettings("Customer", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool IsCustomer { get; set; }

        [FieldSettings("Contact status", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(XeroContact.XeroContactStatus))]
        public XeroContact.XeroContactStatus ContactStatus { get; set; }

        [FieldSettings("Contact email address", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string EmailAddress { get; set; }

        [FieldSettings("Default phone number", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string DefaultPhoneNumber => Contact.DefaultPhoneNumber;

        [FieldSettings("Mobile phone number", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string MobilePhoneNumber => Contact.MobilePhoneNumber;

        [FieldSettings("Fax number", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string FaxNumber => Contact.FaxNumber;

        [FieldSettings("DDI phone number", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string DdiPhoneNumber => Contact.DdiPhoneNumber;

        [FieldSettings("Street address", FieldTypeId = Connector.FieldTypeIdAddress)]
        public string StreetAddress => Contact.StreetAddress;

        [FieldSettings("Street address - Line 1")]
        public string StreetAddress1 => Contact.StreetAddress1;

        [FieldSettings("Street address - Line 2")]
        public string StreetAddress2 => Contact.StreetAddress2;

        [FieldSettings("Street address - Line 3")]
        public string StreetAddress3 => Contact.StreetAddress3;

        [FieldSettings("Street address - Line 4")]
        public string StreetAddress4 => Contact.StreetAddress4;

        [FieldSettings("Street address - City")]
        public string StreetCity => Contact.StreetCity;

        [FieldSettings("Street address - Postal code")]
        public string StreetPostalCode => Contact.StreetPostalCode;

        [FieldSettings("Street address - Attention to")]
        public string StreetAttentionTo => Contact.StreetAttentionTo;

        [FieldSettings("Street address - Country")]
        public string StreetCountry => Contact.StreetCountry;

        [FieldSettings("Street address - Region")]
        public string StreetRegion => Contact.StreetRegion;

        [FieldSettings("PO Box address", FieldTypeId = Connector.FieldTypeIdAddress)]
        public string PoBoxAddress => Contact.PoBoxAddress;

        [FieldSettings("PO Box address - Line 1")]
        public string PoBoxAddress1 => Contact.PoBoxAddress1;

        [FieldSettings("PO Box address - Line 2")]
        public string PoBoxAddress2 => Contact.PoBoxAddress2;

        [FieldSettings("PO Box address - Line 3")]
        public string PoBoxAddress3 => Contact.PoBoxAddress3;

        [FieldSettings("PO Box address - Line 4")]
        public string PoBoxAddress4 => Contact.PoBoxAddress4;

        [FieldSettings("PO Box address - City")]
        public string PoBoxCity => Contact.PoBoxCity;

        [FieldSettings("PO Box address - Postal code")]
        public string PoBoxPostalCode => Contact.PoBoxPostalCode;

        [FieldSettings("PO Box address - Attention to")]
        public string PoBoxAttentionTo => Contact.PoBoxAttentionTo;

        [FieldSettings("PO Box address - Country")]
        public string PoBoxCountry => Contact.PoBoxCountry;

        [FieldSettings("PO Box address - Region")]
        public string PoBoxRegion => Contact.PoBoxRegion;

        [FieldSettings("Delivery address", FieldTypeId = Connector.FieldTypeIdAddress)]
        public string DeliveryAddress => Contact.DeliveryAddress;

        [FieldSettings("Delivery address - Line 1")]
        public string DeliveryAddress1 => Contact.DeliveryAddress1;

        [FieldSettings("Delivery address - Line 2")]
        public string DeliveryAddress2 => Contact.DeliveryAddress2;

        [FieldSettings("Delivery address - Line 3")]
        public string DeliveryAddress3 => Contact.DeliveryAddress3;

        [FieldSettings("Delivery address - Line 4")]
        public string DeliveryAddress4 => Contact.DeliveryAddress4;

        [FieldSettings("Delivery address - City")]
        public string DeliveryCity => Contact.DeliveryCity;

        [FieldSettings("Delivery address - Postal code")]
        public string DeliveryPostalCode => Contact.DeliveryPostalCode;

        [FieldSettings("Delivery address - Attention to")]
        public string DeliveryAttentionTo => Contact.DeliveryAttentionTo;

        [FieldSettings("Delivery address - Country")]
        public string DeliveryCountry => Contact.DeliveryRegion;

        [FieldSettings("Delivery address - Region")]
        public string DeliveryRegion => Contact.DeliveryRegion;

        [FieldSettings("Contact name")]
        public string ContactID => Contact.ContactID;

        #endregion

    }
}