using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Stripe.Models
{
    class StripeInvoiceLineData : DataConnectorEntityModel
    {
       
        #region Default properties

        [FieldSettings("Invoice ID", DefaultField = true)]
        public string id => invoice.id;

        [FieldSettings("Quantity", DefaultField = true)]
        public int? quantity { get; set; }
        #endregion

        #region General properties

        [FieldSettings("Description")]
        public string desc => invoice.description;

        [FieldSettings("Attempted", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool attempted { get; set; }

        [FieldSettings("Closed", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool closed { get; set; }

        [FieldSettings("Currency")]
        public string currency { get; set; }

        [FieldSettings("Customer ID")]
        public string customer { get; set; }

        [FieldSettings("Invoice description")]
        public string description { get; set; }

        [FieldSettings("Forgiven", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool forgiven { get; set; }

        [FieldSettings("Paid", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool paid { get; set; }

        [FieldSettings("Receipt number", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int? receipt_number { get; set; }
        #endregion

        #region Hidden properties
        public long date { get; set; }
        public long created { get; set; }
        public long next_payment_attempt { get; set; }
        public long period_end { get; set; }
        public long period_start { get; set; }
        public StripeCustomers customerColl { get; set; }
        public StripeInvoice invoice { get; set; }
        public decimal amount { get; set; }
        #endregion

        #region Invoice calculations

        [FieldSettings("Amount", DefaultField = true)]
        public decimal amt => amount/100;

        [FieldSettings("Invoice date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime invoice_date
        {
            get
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return epoch.AddSeconds(date);
            }
        }

        [FieldSettings("Created date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime created_date
        {
            get
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return epoch.AddSeconds(created);
            }
        }

        [FieldSettings("Next payment attempt date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime payment_attemp_date
        {
            get
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return epoch.AddSeconds(next_payment_attempt);
            }
        }

        [FieldSettings("Period end date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime period_end_date
        {
            get
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return epoch.AddSeconds(period_end);
            }
        }

        [FieldSettings("Period start date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime period_start_date
        {
            get
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return epoch.AddSeconds(period_start);
            }
        }
        #endregion

        #region Customer calculations
        [FieldSettings("Customer email")]
        public string customer_email => customerColl?.email;

        [FieldSettings("Customer description")]
        public string Customer_description => customerColl?.description;

        [FieldSettings("Delinquent customer", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool? delinquent => customerColl?.delinquent;

        [FieldSettings("Shipping address line 1")]
        public string address_line1 => customerColl?.address_line1;

        [FieldSettings("Shipping address line 2")]
        public string address_line2 => customerColl?.address_line2;

        [FieldSettings("Shipping address city")]
        public string address_city => customerColl?.address_city;

        [FieldSettings("Shipping address state")]
        public string address_state => customerColl?.address_state;

        [FieldSettings("Shipping address postal code")]
        public string address_postal_code => customerColl?.address_postal_code;

        [FieldSettings("Shipping address name")]
        public string address_name => customerColl?.address_name;

        [FieldSettings("Shipping address country")]
        public string address_country => customerColl?.address_country;

        [FieldSettings("Shipping address phone")]
        public string address_phone => customerColl?.address_phone;

        [FieldSettings("Customer account balance", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? acc_balance => customerColl?.acc_balance;
        #endregion
    }
}
