using System;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalInvoiceItem : DataConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Invoice number", DefaultField = true)]
        public string invoice_number => invoice.number;

        [FieldSettings("Item name", DefaultField = true)]
        public string name { get; set; }

        [FieldSettings("Item description", DefaultField = true)]
        public string description { get; set; }

        [FieldSettings("Unit price", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal unit_price { get; set; }

        [FieldSettings("Quantity", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal quantity { get; set; }

        [FieldSettings("Extended price", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal extended_price => quantity * unit_price;

        #endregion

        #region Hidden properties

        public PaypalInvoice invoice { get; set; }
        public PaypalTax tax { get; set; }
        public string date { get; set; }
        public PaypalCost discount { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Status", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(PaypalInvoice.PaypalInvoiceStatus))]
        public PaypalInvoice.PaypalInvoiceStatus invoice_status => invoice.status;

        [FieldSettings("Tax amount", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal tax_amount
        {
            get
            {
                if (invoice.tax_calculated_after_discount) return (extended_price - discount_amount) * tax.percent / 100;
                return extended_price * tax.percent / 100;
            }
        }

        [FieldSettings("Discount amount", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal discount_amount
        {
            get
            {
                if (discount == null) return 0;
                return extended_price * discount.percent / 100 + discount.amount.value;
            }
        }

        [FieldSettings("Total", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal total
        {
            get
            {
                if (invoice.tax_inclusive) return extended_price - discount_amount;
                return extended_price - discount_amount + tax_amount;
            }
        }

        [FieldSettings("Extended price excluding tax", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal extended_price_ex_tax => invoice.tax_inclusive ? extended_price - tax_amount : extended_price;

        [FieldSettings("Discount percentage", FieldTypeId = DataConnector.FieldTypeIdPercentage)]
        public decimal discount_percent => discount.percent;

        [FieldSettings("Invoice URL", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string invoice_uri => invoice.uri;

        [FieldSettings("Invoice note")]
        public string invoice_note => invoice.note;

        [FieldSettings("Merchant memo")]
        public string invoice_merchant_memo => invoice.merchant_memo;

        [FieldSettings("Logo", FieldTypeId = DataConnector.FieldTypeIdImage)]
        public string invoice_logo_url => invoice.logo_url;

        [FieldSettings("Invoice ID")]
        public string invoice_id => invoice.id;

        [FieldSettings("Terms")]
        public string invoice_terms => invoice.terms;

        [FieldSettings("Tax inclusive", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool invoice_tax_inclusive => invoice.tax_inclusive;

        [FieldSettings("Tax calculated after discount", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool invoice_tax_calculated_after_discount => invoice.tax_calculated_after_discount;

        [FieldSettings("Invoice date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime invoice_date => invoice.InvoiceDate;

        [FieldSettings("Created date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime invoice_metadata_created_date => invoice.metadata_created_date;

        [FieldSettings("Created by", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string invoice_metadata_created_by => invoice.metadata_created_by;

        [FieldSettings("Cancelled date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime invoice_metadata_cancelled_date => invoice.metadata_cancelled_date;

        [FieldSettings("Cancelled by", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string invoice_metadata_cancelled_by => invoice.metadata_cancelled_by;

        [FieldSettings("Updated date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime invoice_metadata_last_updated_date => invoice.metadata_last_updated_date;

        [FieldSettings("Last updated by", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string invoice_metadata_last_updated_by => invoice.metadata_last_updated_by;

        [FieldSettings("First sent date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime invoice_metadata_first_sent_date => invoice.metadata_first_sent_date;

        [FieldSettings("Last sent date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime invoice_metadata_last_sent_date => invoice.metadata_last_sent_date;

        [FieldSettings("Last sent by", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string invoice_metadata_last_sent_by => invoice.metadata_last_sent_by;

        [FieldSettings("Payment terms", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(PaypalPaymentTerm.PaypalPaymentTermType))]
        public PaypalPaymentTerm.PaypalPaymentTermType invoice_payment_term_type => invoice.payment_term_type;

        [FieldSettings("Due date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime invoice_payment_term_due_date => invoice.payment_term_due_date;

        [FieldSettings("Shipping cost amount", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal invoice_shipping_cost_amount => invoice.shipping_cost_amount;

        [FieldSettings("Shipping cost tax amount", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal invoice_shipping_cost_tax => invoice.shipping_cost_tax;

        [FieldSettings("Shipping cost tax percentage", FieldTypeId = DataConnector.FieldTypeIdPercentage)]
        public decimal invoice_shipping_cost_tax_percent => invoice.shipping_cost_tax_percent;

        [FieldSettings("Shipping cost tax name")]
        public string invoice_shipping_cost_tax_name => invoice.shipping_cost_tax_name;

        [FieldSettings("Refund type", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(PaypalRefundDetail.PaypalRefundDetailType))]
        public PaypalRefundDetail.PaypalRefundDetailType invoice_refund_type => invoice.refund_type;

        [FieldSettings("Refund date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime invoice_refund_date => invoice.refund_date;

        [FieldSettings("Refund note")]
        public string invoice_refund_note => invoice.refund_note;

        [FieldSettings("Payment type", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(PaypalPaymentDetail.PaypalPaymentDetailType))]
        public PaypalPaymentDetail.PaypalPaymentDetailType invoice_payment_type => invoice.payment_type;

        [FieldSettings("Payment transaction type", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(PaypalPaymentDetail.PaypalPaymentDetailTransactionType))]
        public PaypalPaymentDetail.PaypalPaymentDetailTransactionType invoice_payment_transaction_type => invoice.payment_transaction_type;

        [FieldSettings("Payment method", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(PaypalPaymentDetail.PaypalPaymentDetailMethod))]
        public PaypalPaymentDetail.PaypalPaymentDetailMethod invoice_payment_method => invoice.payment_method;

        [FieldSettings("Payment ID")]
        public string invoice_payment_transaction_id => invoice.payment_transaction_id;

        [FieldSettings("Payment date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime invoice_payment_date => invoice.payment_date;

        [FieldSettings("Payment note")]
        public string invoice_payment_note => invoice.payment_note;

        [FieldSettings("Merchant email", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string invoice_merchant_info_email => invoice.merchant_info_email;

        [FieldSettings("Merchant first name")]
        public string invoice_merchant_info_first_name => invoice.merchant_info_first_name;

        [FieldSettings("Merchant last name")]
        public string invoice_merchant_info_last_name => invoice.merchant_info_last_name;

        [FieldSettings("Merchant name")]
        public string invoice_merchant_info_name => invoice.merchant_info_name;

        [FieldSettings("Merchant address", FieldTypeId = DataConnector.FieldTypeIdAddress)]
        public string invoice_merchant_info_address => invoice.merchant_info_address;

        [FieldSettings("Merchant address 1")]
        public string invoice_merchant_info_address_line1 => invoice.merchant_info_address_line1;

        [FieldSettings("Merchant address 2")]
        public string invoice_merchant_info_address_line2 => invoice.merchant_info_address_line2;

        [FieldSettings("Merchant city")]
        public string invoice_merchant_info_address_city => invoice.merchant_info_address_city;

        [FieldSettings("Merchant state")]
        public string invoice_merchant_info_address_state => invoice.merchant_info_address_state;

        [FieldSettings("Merchant postal code")]
        public string invoice_merchant_info_address_postal_code => invoice.merchant_info_address_postal_code;

        [FieldSettings("Merchant country code")]
        public string invoice_merchant_info_address_country_code => invoice.merchant_info_address_country_code;

        [FieldSettings("Merchant address phone number")]
        public string invoice_merchant_info_address_phone => invoice.merchant_info_address_phone;

        [FieldSettings("Merchant business name")]
        public string invoice_merchant_info_business_name => invoice.merchant_info_business_name;

        [FieldSettings("Merchant phone number")]
        public string invoice_merchant_info_phone => invoice.merchant_info_phone;

        [FieldSettings("Merchant fax number")]
        public string invoice_merchant_info_fax => invoice.merchant_info_fax;

        [FieldSettings("Merchant website")]
        public string invoice_merchant_info_website => invoice.merchant_info_website;

        [FieldSettings("Merchant tax ID")]
        public string invoice_merchant_info_tax_id => invoice.merchant_info_tax_id;

        [FieldSettings("Merchant additional info")]
        public string invoice_merchant_info_additional_info => invoice.merchant_info_additional_info;

        [FieldSettings("Shipping first name")]
        public string invoice_shipping_info_first_name => invoice.shipping_info_first_name;

        [FieldSettings("Shipping last name")]
        public string invoice_shipping_info_last_name => invoice.shipping_info_last_name;

        [FieldSettings("Shipping name")]
        public string invoice_shipping_info_name => invoice.shipping_info_name;

        [FieldSettings("Shipping business name")]
        public string invoice_shipping_info_business_name => invoice.shipping_info_business_name;

        [FieldSettings("Shipping address", FieldTypeId = DataConnector.FieldTypeIdAddress)]
        public string invoice_shipping_info_address => invoice.shipping_info_address;

        [FieldSettings("Shipping address 1")]
        public string invoice_shipping_info_address_line1 => invoice.shipping_info_address_line1;

        [FieldSettings("Shipping address 2")]
        public string invoice_shipping_info_address_line2 => invoice.shipping_info_address_line2;

        [FieldSettings("Shipping city")]
        public string invoice_shipping_info_address_city => invoice.shipping_info_address_city;

        [FieldSettings("Shipping state")]
        public string invoice_shipping_info_address_state => invoice.shipping_info_address_state;

        [FieldSettings("Shipping postal code")]
        public string invoice_shipping_info_address_postal_code => invoice.shipping_info_address_postal_code;

        [FieldSettings("Shipping country code")]
        public string invoice_shipping_info_address_country_code => invoice.shipping_info_address_country_code;

        [FieldSettings("Shipping phone number")]
        public string invoice_shipping_info_address_phone => invoice.shipping_info_address_phone;

        [FieldSettings("Billing email", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string invoice_billing_info_email => invoice.billing_info_email;

        [FieldSettings("Billing first name")]
        public string invoice_billing_info_first_name => invoice.billing_info_first_name;

        [FieldSettings("Billing last name")]
        public string invoice_billing_info_last_name => invoice.billing_info_last_name;

        [FieldSettings("Billing name")]
        public string invoice_billing_info_name => invoice.billing_info_name;

        [FieldSettings("Billing business name")]
        public string invoice_billing_info_business_name => invoice.billing_info_business_name;

        [FieldSettings("Billing language", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(PaypalBillingInfo.PaypalBillingInfoLanguage))]
        public PaypalBillingInfo.PaypalBillingInfoLanguage invoice_billing_info_language => invoice.billing_info_language;

        [FieldSettings("Billing additional info")]
        public string invoice_billing_info_additional_info => invoice.billing_info_additional_info;

        [FieldSettings("Billing address", FieldTypeId = DataConnector.FieldTypeIdAddress)]
        public string invoice_billing_info_address => invoice.billing_info_address;

        [FieldSettings("Billing address 1")]
        public string invoice_billing_info_address_line1 => invoice.billing_info_address_line1;

        [FieldSettings("Billing address 2")]
        public string invoice_billing_info_address_line2 => invoice.billing_info_address_line2;

        [FieldSettings("Billing city")]
        public string invoice_billing_info_address_city => invoice.billing_info_address_city;

        [FieldSettings("Billing state")]
        public string invoice_billing_info_address_state => invoice.billing_info_address_state;

        [FieldSettings("Billing postal code")]
        public string invoice_billing_info_address_postal_code => invoice.billing_info_address_postal_code;

        [FieldSettings("Billing country code")]
        public string invoice_billing_info_address_country_code => invoice.billing_info_address_country_code;

        [FieldSettings("Billing phone number")]
        public string invoice_billing_info_address_phone => invoice.billing_info_address_phone;

        #endregion

    }
}