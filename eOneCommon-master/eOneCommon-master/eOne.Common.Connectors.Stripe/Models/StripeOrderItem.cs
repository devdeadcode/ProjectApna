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
    class StripeOrderItem : DataConnectorEntityModel
    {
        #region Enum

        public enum StripeOrderItemType
        {
            [Description("Tax")]
            tax,
            [Description("Shipping")]
            shipping,
            [Description("Product")]
            sku,
            [Description("Discount")]
            discount
        }
        #endregion

        #region Default properties

        [FieldSettings("Order ID", DefaultField = true)]
        public string order_id => order.id;

        [FieldSettings("Email", DefaultField = true)]
        public string email => order.email;

        [FieldSettings("Description", DefaultField = true)]
        public string description { get; set; }

        [FieldSettings("Quantity")]
        public int? quantity { get; set; }
        #endregion

        #region General properties
        [FieldSettings("Currency")]
        public string currency { get; set; }

        [FieldSettings("Type", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(StripeOrderItemType))]
        public StripeOrderItemType type { get; set; }

        [FieldSettings("Shipping address name")]
        public string shipping_name => order?.shipping_name;

        [FieldSettings("Shipping address phone")]
        public string shipping_phone => order?.shipping_phone;

        [FieldSettings("Shipping address line 1")]
        public string address_line_1 => order?.address_line1;

        [FieldSettings("Shipping address line 2")]
        public string address_line_2 => order?.address_line2;

        [FieldSettings("Shipping address city")]
        public string address_city => order?.address_city;

        [FieldSettings("Shipping address state")]
        public string address_state => order?.address_state;

        [FieldSettings("Shipping address postal code")]
        public string postal_code => order?.address_postal_code;

        [FieldSettings("Shipping address country")]
        public string address_country => order?.address_country;

        [FieldSettings("Status")]
        public StripeOrder.StripeOrderStatus status_type => order.status;
        #endregion

        #region Hidden properties
        public StripeOrder order { get; set; }
        public decimal? amount { get; set; }
        #endregion

        #region Calculations
        [FieldSettings("Amount", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? amt => amount != null ? amount / 100 : 0;

        [FieldSettings("Created date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime? created_date => order?.created_date;

        #endregion


    }
}
