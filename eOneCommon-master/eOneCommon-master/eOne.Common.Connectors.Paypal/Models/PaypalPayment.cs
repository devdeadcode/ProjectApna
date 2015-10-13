using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalPayment : DataConnectorEntityModel
    {

        #region Enums

        public enum PaypalPaymentState
        {
            [Description("Created")]
            created,
            [Description("Approved")]
            approved,
            [Description("Failed")]
            failed,
            [Description("Cancelled")]
            canceled,
            [Description("Expired")]
            expired,
            [Description("Pending")]
            pending
        }
        public enum PaypalPaymentIntent
        {
            [Description("Sale")]
            sale,
            [Description("Authorize")]
            authorize, 
            [Description("Order")]
            order
        }

        #endregion

        #region Default properties

        [FieldSettings("Payment ID", DefaultField = true, KeyNumber = 1)]
        public string id { get; set; }

        [FieldSettings("Payer name", DefaultField = true)]
        public string payer_name => $"{payer_first_name.Trim()} {payer_last_name.Trim()}";

        [FieldSettings("Status", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(PaypalPaymentState))]
        public PaypalPaymentState state { get; set; }

        [FieldSettings("Payment intent", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(PaypalPaymentIntent))]
        public PaypalPaymentIntent intent { get; set; }

        [FieldSettings("Total transaction amount", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal total_transaction_amount
        {
            get
            {
                return transactions.Sum(transaction => transaction.amount.total);
            }
        }

        #endregion

        #region Hidden properties

        public DateTime create_time { get; set; }
        public DateTime update_time { get; set; }
        public List<PaypalTransaction> transactions { get; set; }
        public List<PaypalLink> links { get; set; }
        public PaypalPayer payer { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Created at date", Created = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime createDate => create_time.Date;

        [FieldSettings("Updated at date", Modified = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime updateDate => update_time.Date;

        [FieldSettings("Created at time", Created = true, FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime createTime => Time(create_time);

        [FieldSettings("Update at time", Modified = true, FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime updateTime => Time(update_time);

        [FieldSettings("Number of transactions", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int NumberOfTransactions => transactions.Count;

        [FieldSettings("URL", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string PaymentUrl
        {
            get
            {
                foreach (var link in links.Where(link => link.rel == PaypalLink.PaypalLinkRel.self)) return link.href;
                return string.Empty;
            }
        }

        [FieldSettings("Payment method", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public PaypalPayer.PaypalPayerPaymentMethod payer_payment_method => payer.payment_method;

        [FieldSettings("Payer status", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(PaypalPayer.PaypalPayerStatus))]
        public PaypalPayer.PaypalPayerStatus payer_status => payer.status;

        [FieldSettings("Payer email", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string payer_email => payer.payer_info.email;

        [FieldSettings("Payer salutation")]
        public string payer_salutation => payer.payer_info.salutation;

        [FieldSettings("Payer first name")]
        public string payer_first_name => payer.payer_info.first_name;

        [FieldSettings("Payer middle name")]
        public string payer_middle_name => payer.payer_info.middle_name;

        [FieldSettings("Payer last name")]
        public string payer_last_name => payer.payer_info.last_name;

        [FieldSettings("Payer suffix")]
        public string payer_suffix => payer.payer_info.suffix;

        [FieldSettings("Payer ID")]
        public string payer_payer_id => payer.payer_info.payer_id;

        [FieldSettings("Shipping address recipient name")]
        public string payer_address_recipient_name => payer.payer_info.shipping_address.recipient_name;

        [FieldSettings("Shipping address type", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(PaypalShippingAddress.PaypalAddressType))]
        public PaypalShippingAddress.PaypalAddressType payer_address_type => payer.payer_info.shipping_address.type;

        [FieldSettings("Shipping address", FieldTypeId = DataConnector.FieldTypeIdAddress)]
        public string payer_address => payer.payer_info.shipping_address.address;

        [FieldSettings("Shipping address line 1")]
        public string payer_address_line1 => payer.payer_info.shipping_address.line1;

        [FieldSettings("Shipping address line 2")]
        public string payer_address_line2 => payer.payer_info.shipping_address.line2;

        [FieldSettings("Shipping address city")]
        public string payer_address_city => payer.payer_info.shipping_address.city;

        [FieldSettings("Shipping address state")]
        public string payer_address_state => payer.payer_info.shipping_address.state;

        [FieldSettings("Shipping address postal code")]
        public string payer_address_postal_code => payer.payer_info.shipping_address.postal_code;

        [FieldSettings("Shipping address country code")]
        public string payer_address_country_code => payer.payer_info.shipping_address.country_code;

        [FieldSettings("Shipping address phone number", FieldTypeId = DataConnector.FieldTypeIdPhone)]
        public string payer_address_phone => payer.payer_info.shipping_address.phone;

        [FieldSettings("Payer phone number", FieldTypeId = DataConnector.FieldTypeIdPhone)]
        public string payer_phone => payer.payer_info.phone;

        [FieldSettings("Payer country code")]
        public string payer_country_code => payer.payer_info.country_code;

        [FieldSettings("Payer tax ID type", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(PaypalPayerInfo.PaypalPayerInfoTaxType))]
        public PaypalPayerInfo.PaypalPayerInfoTaxType payer_tax_id_type => payer.payer_info.tax_id_type;

        [FieldSettings("Payer tax ID")]
        public string payer_tax_id => payer.payer_info.tax_id;

        #endregion

    }
}