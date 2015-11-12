using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalInvoice : ConnectorEntityModel
    {

        #region Enums

        public enum PaypalInvoiceStatus
        {
            [Description("Draft")]
            DRAFT,
            [Description("Sent")]
            SENT,
            [Description("Paid")]
            PAID,
            [Description("Marked as paid")]
            MARKED_AS_PAID,
            [Description("Cancelled")]
            CANCELLED,
            [Description("Refunded")]
            REFUNDED,
            [Description("Partially refunded")]
            PARTIALLY_REFUNDED,
            [Description("Marked as refunded")]
            MARKED_AS_REFUNDED
        }

        #endregion

        #region Default properties

        [FieldSettings("Invoice number", DefaultField = true)]
        public string number { get; set; }

        [FieldSettings("Date", DefaultField = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime InvoiceDate => DateFromString(invoice_date);

        [FieldSettings("Status", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(PaypalInvoiceStatus))]
        public PaypalInvoiceStatus status { get; set; }

        [FieldSettings("Total", DefaultField = true, FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal total_amount { get; set; }

        #endregion

        #region Properties

        [FieldSettings("URL", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string uri { get; set; }

        [FieldSettings("Note")]
        public string note { get; set; }

        [FieldSettings("Merchant memo")]
        public string merchant_memo { get; set; }

        [FieldSettings("Logo", FieldTypeId = Connector.FieldTypeIdImage)]
        public string logo_url { get; set; }

        [FieldSettings("Invoice ID", KeyNumber = 1)]
        public string id { get; set; }

        [FieldSettings("Terms")]
        public string terms { get; set; }

        [FieldSettings("Tax inclusive", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool tax_inclusive { get; set; }

        [FieldSettings("Tax calculated after discount", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool tax_calculated_after_discount { get; set; }

        #endregion

        #region Hidden properties

        public List<PaypalInvoiceItem> items { get; set; }
        public PaypalPaymentTerm payment_term { get; set; }
        public PaypalCost discount { get; set; }
        public PaypalShippingCost shipping_cost { get; set; }
        public PaypalCustomAmount custom { get; set; }
        public List<PaypalPaymentDetail> payments { get; set; }
        public List<PaypalRefundDetail> refunds { get; set; }
        public PaypalMetadata metadata { get; set; }
        public PaypalMerchantInfo merchant_info { get; set; }
        public List<PaypalBillingInfo> billing_info { get; set; }
        public PaypalShippingInfo shipping_info { get; set; }
        public string invoice_date { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Number of items", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int number_of_items => items?.Count ?? 0;

        [FieldSettings("Subtotal", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal subtotal
        {
            get
            {
                return items?.Sum(item => item.extended_price) ?? 0;
            }
        }

        [FieldSettings("Created date", Created = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime metadata_created_date => metadata == null ? DateTime.MinValue : DateFromString(metadata.created_date);

        [FieldSettings("Created by", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string metadata_created_by => metadata == null ? string.Empty : metadata.created_by;

        [FieldSettings("Cancelled date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime metadata_cancelled_date => metadata == null ? DateTime.MinValue : DateFromString(metadata.cancelled_date);

        [FieldSettings("Cancelled by", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string metadata_cancelled_by => metadata == null ? string.Empty : metadata.cancelled_by;

        [FieldSettings("Last updated date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime metadata_last_updated_date => metadata == null ? DateTime.MinValue : DateFromString(metadata.last_updated_date);

        [FieldSettings("Last updated by", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string metadata_last_updated_by => metadata == null ? string.Empty : metadata.last_updated_by;

        [FieldSettings("First sent date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime metadata_first_sent_date => metadata == null ? DateTime.MinValue : DateFromString(metadata.first_sent_date);

        [FieldSettings("Last sent date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime metadata_last_sent_date => metadata == null ? DateTime.MinValue : DateFromString(metadata.last_sent_date);

        [FieldSettings("Last sent by", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string metadata_last_sent_by => metadata == null ? string.Empty : metadata.last_sent_by;

        [FieldSettings("Payment terms", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(PaypalPaymentTerm.PaypalPaymentTermType))]
        public PaypalPaymentTerm.PaypalPaymentTermType payment_term_type => payment_term.term_type;

        [FieldSettings("Due date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime payment_term_due_date => DateFromString(payment_term.due_date);

        [FieldSettings("Shipping cost amount", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal shipping_cost_amount => shipping_cost.amount.value;

        [FieldSettings("Shipping cost tax amount", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal shipping_cost_tax => shipping_cost.tax.amount.value;

        [FieldSettings("Shipping cost tax percentage", FieldTypeId = Connector.FieldTypeIdPercentage)]
        public decimal shipping_cost_tax_percent => shipping_cost.tax.percent;

        [FieldSettings("Shipping cost tax name")]
        public string shipping_cost_tax_name => shipping_cost.tax.name;

        [FieldSettings("Refund type", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(PaypalRefundDetail.PaypalRefundDetailType))]
        public PaypalRefundDetail.PaypalRefundDetailType refund_type
        {
            get
            {
                if (refunds == null || refunds.Count == 0) return PaypalRefundDetail.PaypalRefundDetailType.NONE;
                return refunds[0].type;
            }
        }

        [FieldSettings("Refund date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime refund_date
        {
            get
            {
                if (refunds == null || refunds.Count == 0) return DateTime.MinValue;
                return DateFromString(refunds[0].date);
            }
        }

        [FieldSettings("Refund note")]
        public string refund_note
        {
            get
            {
                if (refunds == null || refunds.Count == 0) return string.Empty;
                return refunds[0].note;
            }
        }

        [FieldSettings("Payment type", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(PaypalPaymentDetail.PaypalPaymentDetailType))]
        public PaypalPaymentDetail.PaypalPaymentDetailType payment_type
        {
            get
            {
                if (payments == null || payments.Count == 0) return PaypalPaymentDetail.PaypalPaymentDetailType.NONE;
                return payments[0].type;
            }
        }

        [FieldSettings("Payment transaction type", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(PaypalPaymentDetail.PaypalPaymentDetailTransactionType))]
        public PaypalPaymentDetail.PaypalPaymentDetailTransactionType payment_transaction_type
        {
            get
            {
                if (payments == null || payments.Count == 0) return PaypalPaymentDetail.PaypalPaymentDetailTransactionType.NONE;
                return payments[0].transaction_type;
            }
        }

        [FieldSettings("Payment method", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(PaypalPaymentDetail.PaypalPaymentDetailMethod))]
        public PaypalPaymentDetail.PaypalPaymentDetailMethod payment_method
        {
            get
            {
                if (payments == null || payments.Count == 0) return PaypalPaymentDetail.PaypalPaymentDetailMethod.NONE;
                return payments[0].method;
            }
        }

        [FieldSettings("Payment transaction ID")]
        public string payment_transaction_id
        {
            get
            {
                if (payments == null || payments.Count == 0) return string.Empty;
                return payments[0].transaction_id;
            }
        }

        [FieldSettings("Payment date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime payment_date
        {
            get
            {
                if (payments == null || payments.Count == 0) return DateTime.MinValue;
                return DateFromString(payments[0].date);
            }
        }

        [FieldSettings("Payment note")]
        public string payment_note
        {
            get
            {
                if (payments == null || payments.Count == 0) return string.Empty;
                return payments[0].note;
            }
        }

        [FieldSettings("Discount percentage", FieldTypeId = Connector.FieldTypeIdPercentage)]
        public decimal discount_percent => discount?.percent ?? 0;

        [FieldSettings("Discount amount", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal discount_amount => discount?.amount?.value ?? 0;

        [FieldSettings("Merchant email", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string merchant_info_email => merchant_info == null ? string.Empty : merchant_info.email;

        [FieldSettings("Merchant first name", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string merchant_info_first_name => merchant_info == null ? string.Empty : merchant_info.first_name;

        [FieldSettings("Merchant last name", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string merchant_info_last_name => merchant_info == null ? string.Empty : merchant_info.last_name;

        [FieldSettings("Merchant name", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string merchant_info_name => $"{merchant_info_first_name.Trim()} {merchant_info_last_name.Trim()}".Trim();

        [FieldSettings("Merchant address", FieldTypeId = Connector.FieldTypeIdAddress)]
        public string merchant_info_address => merchant_info?.address == null ? string.Empty : merchant_info.address.address;

        [FieldSettings("Merchant address 1")]
        public string merchant_info_address_line1 => merchant_info?.address == null ? string.Empty : merchant_info.address.line1;

        [FieldSettings("Merchant address 2")]
        public string merchant_info_address_line2 => merchant_info?.address == null ? string.Empty : merchant_info.address.line2;

        [FieldSettings("Merchant city")]
        public string merchant_info_address_city => merchant_info?.address == null ? string.Empty : merchant_info.address.city;

        [FieldSettings("Merchant state")]
        public string merchant_info_address_state => merchant_info?.address == null ? string.Empty : merchant_info.address.state;

        [FieldSettings("Merchant postal code")]
        public string merchant_info_address_postal_code => merchant_info?.address == null ? string.Empty : merchant_info.address.postal_code;

        [FieldSettings("Merchant country code")]
        public string merchant_info_address_country_code => merchant_info?.address == null ? string.Empty : merchant_info.address.country_code;

        [FieldSettings("Merchant address phone number")]
        public string merchant_info_address_phone => merchant_info?.address == null ? string.Empty : merchant_info.address.phone;

        [FieldSettings("Merchant business name")]
        public string merchant_info_business_name => merchant_info == null ? string.Empty : merchant_info.business_name;

        [FieldSettings("Merchant phone number")]
        public string merchant_info_phone => merchant_info == null ? string.Empty : merchant_info.phone;

        [FieldSettings("Merchant fax number")]
        public string merchant_info_fax => merchant_info == null ? string.Empty : merchant_info.fax;

        [FieldSettings("Merchant website")]
        public string merchant_info_website => merchant_info == null ? string.Empty : merchant_info.website;

        [FieldSettings("Merchant tax ID")]
        public string merchant_info_tax_id => merchant_info == null ? string.Empty : merchant_info.tax_id;

        [FieldSettings("Merchant additional info")]
        public string merchant_info_additional_info => merchant_info == null ? string.Empty : merchant_info.additional_info;

        [FieldSettings("Shipping first name")]
        public string shipping_info_first_name => shipping_info == null ? string.Empty : shipping_info.first_name;

        [FieldSettings("Shipping last name")]
        public string shipping_info_last_name => shipping_info == null ? string.Empty : shipping_info.last_name;

        [FieldSettings("Shipping name")]
        public string shipping_info_name => $"{shipping_info_first_name.Trim()} {shipping_info_last_name.Trim()}".Trim();

        [FieldSettings("Shipping business name")]
        public string shipping_info_business_name => shipping_info == null ? string.Empty : shipping_info.business_name;

        [FieldSettings("Shipping address", FieldTypeId = Connector.FieldTypeIdAddress)]
        public string shipping_info_address => shipping_info?.address == null ? string.Empty : shipping_info.address.address;

        [FieldSettings("Shipping address 1")]
        public string shipping_info_address_line1 => shipping_info?.address == null ? string.Empty : shipping_info.address.line1;

        [FieldSettings("Shipping address 2")]
        public string shipping_info_address_line2 => shipping_info?.address == null ? string.Empty : shipping_info.address.line2;

        [FieldSettings("Shipping city")]
        public string shipping_info_address_city => shipping_info?.address == null ? string.Empty : shipping_info.address.city;

        [FieldSettings("Shipping state")]
        public string shipping_info_address_state => shipping_info?.address == null ? string.Empty : shipping_info.address.state;

        [FieldSettings("Shipping postal code")]
        public string shipping_info_address_postal_code => shipping_info?.address == null ? string.Empty : shipping_info.address.postal_code;

        [FieldSettings("Shipping country code")]
        public string shipping_info_address_country_code => shipping_info?.address == null ? string.Empty : shipping_info.address.country_code;

        [FieldSettings("Shipping phone number")]
        public string shipping_info_address_phone => shipping_info?.address == null ? string.Empty : shipping_info.address.phone;

        [FieldSettings("Billing email", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string billing_info_email => billing_info == null || billing_info.Count == 0 ? string.Empty : billing_info[0].email;

        [FieldSettings("Billing first name")]
        public string billing_info_first_name => billing_info == null || billing_info.Count == 0 ? string.Empty : billing_info[0].first_name;

        [FieldSettings("Billing last name")]
        public string billing_info_last_name => billing_info == null || billing_info.Count == 0 ? string.Empty : billing_info[0].last_name;

        [FieldSettings("Billing name")]
        public string billing_info_name => $"{billing_info_first_name.Trim()} {billing_info_last_name.Trim()}".Trim();

        [FieldSettings("Billing business name")]
        public string billing_info_business_name => billing_info == null || billing_info.Count == 0 ? string.Empty : billing_info[0].business_name;

        [FieldSettings("Billing language")]
        public PaypalBillingInfo.PaypalBillingInfoLanguage billing_info_language => billing_info == null || billing_info.Count == 0 ? PaypalBillingInfo.PaypalBillingInfoLanguage.None : billing_info[0].language;

        [FieldSettings("Billing additional info")]
        public string billing_info_additional_info => billing_info == null || billing_info.Count == 0 ? string.Empty : billing_info[0].additional_info;

        [FieldSettings("Billing address", FieldTypeId = Connector.FieldTypeIdAddress)]
        public string billing_info_address => billing_info == null || billing_info.Count == 0 || billing_info[0].address == null ? string.Empty : billing_info[0].address.address;

        [FieldSettings("Billing address 1")]
        public string billing_info_address_line1 => billing_info == null || billing_info.Count == 0 || billing_info[0].address == null ? string.Empty : billing_info[0].address.line1;

        [FieldSettings("Billing address 2")]
        public string billing_info_address_line2 => billing_info == null || billing_info.Count == 0 || billing_info[0].address == null ? string.Empty : billing_info[0].address.line2;

        [FieldSettings("Billing city")]
        public string billing_info_address_city => billing_info == null || billing_info.Count == 0 || billing_info[0].address == null ? string.Empty : billing_info[0].address.city;

        [FieldSettings("Billing state")]
        public string billing_info_address_state => billing_info == null || billing_info.Count == 0 || billing_info[0].address == null ? string.Empty : billing_info[0].address.state;

        [FieldSettings("Billing postal code")]
        public string billing_info_address_postal_code => billing_info == null || billing_info.Count == 0 || billing_info[0].address == null ? string.Empty : billing_info[0].address.postal_code;

        [FieldSettings("Billing country code")]
        public string billing_info_address_country_code => billing_info == null || billing_info.Count == 0 || billing_info[0].address == null ? string.Empty : billing_info[0].address.country_code;

        [FieldSettings("Billing phone number")]
        public string billing_info_address_phone => billing_info == null || billing_info.Count == 0 || billing_info[0].address == null ? string.Empty : billing_info[0].address.phone;

        #endregion

    }
}