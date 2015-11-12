using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eOne.Common.DataConnectors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace eOne.Common.Connectors.Stripe.Models
{
    class StripeCharges : DataConnectorEntityModel
    {
        #region Enum

        public enum StripeChargesStatus
        {
            [FieldSettings("Paid")]
            paid
            
        }
        #endregion

        #region Default properties
        [FieldSettings("Charge ID", DefaultField = true)]
        public string id { get; set; }

        [FieldSettings("Description", DefaultField = true)]
        public string description { get; set; }

        [FieldSettings("Status", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public StripeChargesStatus status { get; set; }
        #endregion

        #region General properties
        [FieldSettings("Captured", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool captured { get; set; }

        [FieldSettings("Currency")]
        public string currency { get; set; }

        [FieldSettings("Failure code")]
        public string failure_code { get; set; }

        [FieldSettings("Failure message")]
        public string failure_message { get; set; }

        [FieldSettings("Paid")]
        public bool paid { get; set; }

        [FieldSettings("Receipt email")]
        public string receipt_email { get; set; }

        [FieldSettings("Receipt number", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public string receipt_number { get; set; }

        [FieldSettings("Refunded", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool refunded { get; set; }

        [FieldSettings("Customer ID")]
        public string customer { get; set; }
        
        #endregion

        #region Hidden properties
        public decimal amount { get; set; }
        public decimal amount_refunded { get; set; }
        public string application_fee { get; set; }
        public StripeCustomerShipping shipping { get; set; }
        public decimal account_balance { get; set; }
        public long created { get; set; }
        public List<StripeCustomers> customer_list { get; set; }
        public List<StripeChargeRefunds> refunds { get; set; }
        public StripeChargesCollection chargesCollection { get; set; }
        public StripeCustomers customersColl { get; set; }
        #endregion
        
        #region Charges calculations
        [FieldSettings("Amount", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal amount_per
        {
            get
            {
                if (amount != 0) return amount / 100;
                return 0;
            }

        }

        [FieldSettings("Amount refunded", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal amount_ref
        {
            get
            {
                if (amount_refunded != 0) return amount_refunded / 100;
                return 0;
            }

        }

        [FieldSettings("Application fee", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal app_fee
        {
            get
            {
                if (application_fee != null) return Convert.ToDecimal(application_fee) / 100;
                return 0;
            }
        }

        [FieldSettings("Created date", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime created_date
        {
            get
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return epoch.AddSeconds(created);
            }
        }
        #endregion

        #region Customer calculations

        [FieldSettings("Customer email")]
        public string customer_email => customersColl?.email;

        [FieldSettings("Customer description")]
        public string customer_description => customersColl?.description;

        [FieldSettings("Delinquent customer", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool? delinquent => customersColl?.delinquent;

        [FieldSettings("Shipping address line 1")]
        public string address_line1 => customersColl?.address_line1;

        [FieldSettings("Shipping address line 2")]
        public string address_line2 => customersColl?.address_line2;

        [FieldSettings("Shipping address city")]
        public string address_city => customersColl?.address_city;

        [FieldSettings("Shipping address state")]
        public string address_state => customersColl?.address_state;

        [FieldSettings("Shipping address postal code")]
        public string address_postal_code => customersColl?.address_postal_code;

        [FieldSettings("Shipping address name")]
        public string address_name => customersColl?.address_name;

        [FieldSettings("Shipping address country")]
        public string address_country => customersColl?.address_country;

        [FieldSettings("Shipping address phone")]
        public string address_phone => customersColl?.address_phone;

        [FieldSettings("Customer account balance", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? acc_balance => customersColl?.acc_balance;
        #endregion

    }
}
