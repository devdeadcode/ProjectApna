using System;
using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Xero.Models
{
    public class XeroInvoiceLineItem
    {

        #region Default properties

        [FieldSettings("Invoice type", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(XeroInvoice.XeroInvoiceType))]
        public XeroInvoice.XeroInvoiceType InvoiceType => Invoice.Type;

        [FieldSettings("Invoice number", DefaultField = true)]
        public string InvoiceNumber => Invoice == null ? string.Empty : Invoice.InvoiceNumber;

        [FieldSettings("Description", DefaultField = true)]
        public string Description { get; set; }

        [FieldSettings("Quantity", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal Quantity { get; set; }

        [FieldSettings("Unit amount", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal UnitAmount { get; set; }

        [FieldSettings("Line amount", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal LineAmount { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Tax amount", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal TaxAmount { get; set; }

        [FieldSettings("Item code")]
        public string ItemCode { get; set; }

        [FieldSettings("Discount rate", FieldTypeId = DataConnector.FieldTypeIdPercentage)]
        public decimal DiscountRate { get; set; }

        [FieldSettings("Tax type", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(XeroAccount.XeroAccountTaxType))]
        public XeroAccount.XeroAccountTaxType TaxType { get; set; }

        [FieldSettings("Account code")]
        public string AccountCode { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Item description")]
        public string ItemDescription => Item == null ? string.Empty : Item.Description;

        [FieldSettings("Item purchase unit price", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal ItemPurchaseUnitPrice => Item?.PurchaseUnitPrice ?? 0;

        [FieldSettings("Item purchase account code")]
        public string ItemPurchaseAccountCode => Item == null ? string.Empty : Item.PurchaseAccountCode;

        [FieldSettings("Item sales unit price", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal ItemSalesUnitPrice => Item?.SalesUnitPrice ?? 0;

        [FieldSettings("Item sales account code")]
        public string ItemSalesAccountCode => Item == null ? string.Empty : Item.SalesAccountCode;

        [FieldSettings("Contact name")]
        public string ContactName => Invoice.ContactName;

        [FieldSettings("Invoice date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime InvoiceDate => Invoice.Date;

        [FieldSettings("Invoice status", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(XeroInvoice.XeroInvoiceType))]
        public XeroInvoice.XeroInvoiceStatus InvoiceStatus => Invoice.Status;

        [FieldSettings("Due date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime InvoiceDueDate => Invoice.DueDate;

        [FieldSettings("Amount type", FieldTypeId = DataConnector.FieldTypeIdEnum)]
        public XeroInvoice.XeroInvoiceLineAmountType InvoiceLineAmountTypes => Invoice.LineAmountTypes;

        [FieldSettings("Currency code")]
        public string InvoiceCurrencyCode => Invoice.CurrencyCode;

        [FieldSettings("Invoice ID")]
        public string InvoiceID => Invoice.InvoiceID;

        [FieldSettings("Fully paid on date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime InvoiceFullyPaidOnDate => Invoice.FullyPaidOnDate;

        [FieldSettings("Invoice reference")]
        public string InvoiceReference => Invoice.Reference;

        [FieldSettings("Branding theme ID")]
        public string InvoiceBrandingThemeID => Invoice.BrandingThemeID;

        [FieldSettings("Invoice link", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string InvoiceUrl => Invoice.Url;

        [FieldSettings("Currency rate", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal InvoiceCurrencyRate => Invoice.CurrencyRate;

        [FieldSettings("Sent to contact", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool InvoiceSentToContact => Invoice.SentToContact;

        [FieldSettings("Expected payment date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime InvoiceExpectedPaymentDate => Invoice.ExpectedPaymentDate;

        [FieldSettings("Planned payment date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime InvoicePlannedPaymentDate => Invoice.PlannedPaymentDate;

        [FieldSettings("Invoice has attachments", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool InvoiceHasAttachments => Invoice.HasAttachments;

        [FieldSettings("Updated date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime InvoiceUpdatedDate => Invoice.UpdatedDate;

        [FieldSettings("Updated time", FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime UpdatedTime => Invoice.UpdatedTime;

        [FieldSettings("Supplier", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool IsSupplier => Invoice.IsSupplier;

        [FieldSettings("Customer", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool IsCustomer => Invoice.IsCustomer;

        [FieldSettings("Contact status", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(XeroContact.XeroContactStatus))]
        public XeroContact.XeroContactStatus ContactStatus => Invoice.ContactStatus;

        [FieldSettings("Contact email address", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string EmailAddress => Invoice.EmailAddress;

        [FieldSettings("Default phone number", FieldTypeId = DataConnector.FieldTypeIdPhone)]
        public string DefaultPhoneNumber => Invoice.DefaultPhoneNumber;

        [FieldSettings("Mobile phone number", FieldTypeId = DataConnector.FieldTypeIdPhone)]
        public string MobilePhoneNumber => Invoice.MobilePhoneNumber;

        [FieldSettings("Fax number", FieldTypeId = DataConnector.FieldTypeIdPhone)]
        public string FaxNumber => Invoice.FaxNumber;

        [FieldSettings("DDI phone number", FieldTypeId = DataConnector.FieldTypeIdPhone)]
        public string DdiPhoneNumber => Invoice.DdiPhoneNumber;

        [FieldSettings("Street address")]
        public string StreetAddress => Invoice.StreetAddress;

        [FieldSettings("Street address - Line 1")]
        public string StreetAddress1 => Invoice.StreetAddress1;

        [FieldSettings("Street address - Line 2")]
        public string StreetAddress2 => Invoice.StreetAddress2;

        [FieldSettings("Street address - Line 3")]
        public string StreetAddress3 => Invoice.StreetAddress3;

        [FieldSettings("Street address - Line 4")]
        public string StreetAddress4 => Invoice.StreetAddress4;

        [FieldSettings("Street address - City")]
        public string StreetCity => Invoice.StreetCity;

        [FieldSettings("Street address - Postal code")]
        public string StreetPostalCode => Invoice.StreetPostalCode;

        [FieldSettings("Street address - Attention to")]
        public string StreetAttentionTo => Invoice.StreetAttentionTo;

        [FieldSettings("Street address - Country")]
        public string StreetCountry => Invoice.StreetCountry;

        [FieldSettings("Street address - Region")]
        public string StreetRegion => Invoice.StreetRegion;

        [FieldSettings("PO Box address")]
        public string PoBoxAddress => Invoice.PoBoxAddress;

        [FieldSettings("PO Box address - Line 1")]
        public string PoBoxAddress1 => Invoice.PoBoxAddress1;

        [FieldSettings("PO Box address - Line 2")]
        public string PoBoxAddress2 => Invoice.PoBoxAddress2;

        [FieldSettings("PO Box address - Line 3")]
        public string PoBoxAddress3 => Invoice.PoBoxAddress3;

        [FieldSettings("PO Box address - Line 4")]
        public string PoBoxAddress4 => Invoice.PoBoxAddress4;

        [FieldSettings("PO Box address - City")]
        public string PoBoxCity => Invoice.PoBoxCity;

        [FieldSettings("PO Box address - Postal code")]
        public string PoBoxPostalCode => Invoice.PoBoxPostalCode;

        [FieldSettings("PO Box address - Attention to")]
        public string PoBoxAttentionTo => Invoice.PoBoxAttentionTo;

        [FieldSettings("PO Box address - Country")]
        public string PoBoxCountry => Invoice.PoBoxCountry;

        [FieldSettings("PO Box address - Region")]
        public string PoBoxRegion => Invoice.PoBoxRegion;

        [FieldSettings("Delivery address")]
        public string DeliveryAddress => Invoice.DeliveryAddress;

        [FieldSettings("Delivery address - Line 1")]
        public string DeliveryAddress1 => Invoice.DeliveryAddress1;

        [FieldSettings("Delivery address - Line 2")]
        public string DeliveryAddress2 => Invoice.DeliveryAddress2;

        [FieldSettings("Delivery address - Line 3")]
        public string DeliveryAddress3 => Invoice.DeliveryAddress3;

        [FieldSettings("Delivery address - Line 4")]
        public string DeliveryAddress4 => Invoice.DeliveryAddress4;

        [FieldSettings("Delivery address - City")]
        public string DeliveryCity => Invoice.DeliveryCity;

        [FieldSettings("Delivery address - Postal code")]
        public string DeliveryPostalCode => Invoice.DeliveryPostalCode;

        [FieldSettings("Delivery address - Attention to")]
        public string DeliveryAttentionTo => Invoice.DeliveryAttentionTo;

        [FieldSettings("Delivery address - Country")]
        public string DeliveryCountry => Invoice.DeliveryRegion;

        [FieldSettings("Delivery address - Region")]
        public string DeliveryRegion => Invoice.DeliveryRegion;

        #endregion

        #region Hidden properties

        public List<XeroTrackingCategory> Tracking { get; set; }
        public XeroItem Item { get; set; }
        public XeroInvoice Invoice { get; set; }

        #endregion

    }
}