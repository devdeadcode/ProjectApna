using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Security.Principal;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Stripe.Models
{
    class StripeCustomers : DataConnectorEntityModel
    {
        #region Default properties
        [FieldSettings("Customer ID", DefaultField = true)]
        public string id { get; set; }

        [FieldSettings("Email", DefaultField = true)]
        public string email { get; set; }

        [FieldSettings("Description", DefaultField = true)]
        public string description { get; set; }
        #endregion

        #region General properties
        [FieldSettings("Currency")]
        public string currency { get; set; }

        [FieldSettings("Delinquent")]
        public bool delinquent { get; set; }

        [FieldSettings("Discount percent")]
        public decimal? discount_percent => discount?.coupon.percent_off;

        [FieldSettings("Discount coupon ID")]
        public string discount_coupon_id => discount?.coupon.id;

        [FieldSettings("Shipping address line 1")]
        public string address_line1 => shipping?.address.line1;

        [FieldSettings("Shipping address line 2")]
        public string address_line2 => shipping?.address.line2;

        [FieldSettings("Shipping address city")]
        public string address_city => shipping?.address.city;

        [FieldSettings("Shipping address state")]
        public string address_state => shipping?.address.state;

        [FieldSettings("Shipping address postal code")]
        public string address_postal_code => shipping?.address.postal_code;

        [FieldSettings("Shipping address name")]
        public string address_name => shipping?.name;

        [FieldSettings("Shipping address country")]
        public string address_country => shipping?.address.country;

        [FieldSettings("Shipping address phone")]
        public string address_phone => shipping?.phone;
        #endregion

        #region Hidden Properties

        public StripeCustomerDiscount discount { get; set; }

        public StripeCustomerShipping shipping { get; set; }

        public long created { get; set; }

        public decimal account_balance { get; set; }

        public StripeCustomerCollections customerColl { get; set; }

        public StripeSubscriptions subscriptions { get; set; }
        #endregion

        #region Calculations

        [FieldSettings("Created date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime created_date
        {
            get
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return epoch.AddSeconds(created);
            }
        }

        [FieldSettings("Account balance")]
        public decimal acc_balance
        {
            get
            {
                if (account_balance != 0)
                {
                    return account_balance/100;
                }
                return 0;
            }
        }

        [FieldSettings("Discount amount")]
        public decimal? disc_amount
        {
            get
            {
                if (discount?.coupon == null) return 0;
                if (discount.coupon.amount_off != 0)
                    return discount.coupon.amount_off/100;
                return 0;
            }
        }
        #endregion
    }
}
