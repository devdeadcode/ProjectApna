using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Stripe.Models
{
    class StripeOrder : DataConnectorEntityModel
    {
        #region Enum

        public enum StripeOrderStatus
        {
            [Description("Created")]
            created,
            [Description("Paid")]
            paid,
            [Description("Fulfilled")]
            fulfilled,
            [Description("Returned")]
            returned,
            [Description("Canceled")]
            canceled
        }
        #endregion

        #region Default properties
        [FieldSettings("Order ID", DefaultField = true)]
        public string id { get; set; }

        [FieldSettings("Email", DefaultField = true)]
        public string email { get; set; }
        #endregion

        #region General properties
        [FieldSettings("Currency")]
        public string currency { get; set; }

        [FieldSettings("Shipping address name")]
        public string shipping_name => shipping?.name;

        [FieldSettings("Shipping address phone")]
        public string shipping_phone => shipping?.phone;

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

        [FieldSettings("Shipping address country")]
        public string address_country => shipping?.address.country;

        [FieldSettings("Status", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(StripeOrderStatus))]
        public StripeOrderStatus status { get; set; }
        #endregion

        #region Hidden properties

        public StripeCustomerShipping shipping { get; set; }
        public decimal? amount { get; set; }
        public decimal? application_fee { get; set; }
        public long created { get; set; }
        public long update { get; set; }
        public List<StripeOrderItem> items { get; set; }
        public List<StripeOrderShippingMethod> shipping_methods { get; set; }
        public string selected_shipping_method { get; set; }
        #endregion

        #region Calculations

        [FieldSettings("Amount", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? amt => amount != null ? amount/100 : 0;

        [FieldSettings("Application fee", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? app_fee => application_fee != null ? application_fee/100 : 0;

        [FieldSettings("Created date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime created_date
        {
            get
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return epoch.AddSeconds(created);
            }
        }

        [FieldSettings("Updated date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime update_date
        {
            get
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return epoch.AddSeconds(update);
            }
        }

        [FieldSettings("Number of items", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int num_of_items => items?.Count ?? 0;

        [FieldSettings("Shipping method")]
        public string ship_method => (from val in shipping_methods where selected_shipping_method == val.id select val.description).FirstOrDefault();

        [FieldSettings("Shipping amount", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? shipping_amount
        {
            get
            {
                decimal? sum = 0;
                if (items == null) return 0;
                sum = items.Where(val => Convert.ToString(val.type) == "shipping").Aggregate(sum, (current, val) => current + val.amount);
                return sum / 100;
            }
        }

        [FieldSettings("Tax amount", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? tax_amount
        {
            get
            {
                decimal? sum = 0;
                if (items == null) return 0;
                sum = items.Where(val => Convert.ToString(val.type) == "tax").Aggregate(sum, (current, val) => current + val.amount);
                return sum / 100;
            }
        }

        [FieldSettings("Subtotal", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? subtotal_amount
        {
            get
            {
                decimal? sum = 0;
                if (items == null) return 0;
                sum = items.Where(val => Convert.ToString(val.type) == "sku").Aggregate(sum, (current, val) => current + val.amount);
                return sum / 100;
            }
        }

        [FieldSettings("Discount amount", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? discount_amount
        {
            get
            {
                decimal? sum = 0;
                if (items == null) return 0;
                sum = items.Where(val => Convert.ToString(val.type) == "discount").Aggregate(sum, (current, val) => current + val.amount);
                return sum / 100;
            }
        }
        #endregion

    }
}
