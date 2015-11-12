using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eOne.Common.DataConnectors;
using Newtonsoft.Json;

namespace eOne.Common.Connectors.Stripe.Models
{
    class StripeInvoice : DataConnectorEntityModel
    {
        #region Default properties
        [FieldSettings("Invoice ID", DefaultField = true)]
        public string id { get; set; }

        [FieldSettings("Description", DefaultField = true)]
        public string description { get; set; }

        #endregion

        #region General properties
        [FieldSettings("NUmber of attempts", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int attempt_count { get; set; }

        [FieldSettings("Payment attempted", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool attempted { get; set; }

        [FieldSettings("Closed", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool closed { get; set; }

        [FieldSettings("Currency")]
        public string currency { get; set; }

        [FieldSettings("Customer ID")]
        public string customer { get; set; }

        [FieldSettings("Discount percent", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal discount_percent => discount?.coupon?.percent_off ?? 0;

        [FieldSettings("Discount coupon ID")]
        public string discount_coupon_id => discount?.coupon?.id ?? null;

        [FieldSettings("Forgiven", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool forgiven { get; set; }

        [FieldSettings("Paid", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool paid { get; set; }

        [FieldSettings("Receipt number", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int? receipt_number { get; set; }

        [FieldSettings("Tax percent", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? tax_percent { get; set; }
        #endregion

        #region Hidden Properties
        public StripeCustomerDiscount discount { get; set; }
        public decimal total { get; set; }
        public decimal starting_balance { get; set; }
        public decimal? ending_balance { get; set; }
        public decimal? subtotal { get; set; }
        public decimal? tax { get; set; }
        public decimal? amount_due { get; set; }
        public decimal? application_fee { get; set; }
        public long date { get; set; }
        public long created { get; set; }
        public long? next_payment_attempt { get; set; }
        public long period_end { get; set; }
        public long period_start { get; set; }
        public StripeCustomers customerColl { get; set; }
        public StripeInvoiceCollection invoiceColl { get; set; }
        public StripeInvoiceLine lines { get; set; }
            #endregion

        #region Invoice Calculations
        [FieldSettings("Invoice total", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal invoice_total => total != 0 ? total/100 : 0;

        [FieldSettings("Starting balance", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal start_balance => starting_balance/100;

        [FieldSettings("Ending balance", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? end_balance => ending_balance != null ? ending_balance/100 : 0;

        [FieldSettings("Subtotal", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? sub => subtotal != null ? subtotal/100 : 0;

        [FieldSettings("Tax amount", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? tax_amount => tax != null ? tax/100 : 0;

        [FieldSettings("Amount due", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? amt_due => amount_due != null ? amount_due/100 : 0;

        [FieldSettings("Application fee", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? app_fee => application_fee != null ? application_fee/100 : 0;

        [FieldSettings("Discount amount", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? descount_amount => discount.coupon.amount_off != null ? discount.coupon.amount_off/100 : 0;

        [FieldSettings("Invoice date", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
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
        public DateTime? payment_attemp_date
        {
            get
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                if(next_payment_attempt != null) return epoch.AddSeconds(Convert.ToDouble(next_payment_attempt));
                return null;
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
