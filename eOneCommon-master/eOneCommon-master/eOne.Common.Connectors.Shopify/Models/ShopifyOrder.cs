using System;
using System.Collections.Generic;
using System.ComponentModel;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Shopify.Models
{
    public class ShopifyOrder : DataConnectorEntityModel
    {

        #region Enums

        public enum ShopifyOrderCancelReason
        {
            [Description("Customer")]
            customer,
            [Description("Fraud")]
            fraud,
            [Description("Inventory")]
            inventory,
            [Description("Other")]
            other
        }

        public enum ShopifyOrderFinancialStatus
        {
            [Description("Pending")]
            pending,
            [Description("Authorized")]
            authorized,
            [Description("Partially paid")]
            partially_paid,
            [Description("Paid")]
            paid,
            [Description("Partially refunded")]
            partially_refunded,
            [Description("Refunded")]
            refunded,
            [Description("Voided")]
            voided
        }

        public enum ShopifyOrderFulfillmentStatus
        {
            [Description("Fulfilled")]
            fulfilled,
            [Description("Null")]
            @null,
            [Description("Partial")]
            partial
        } 

        public enum ShopifyOrderProcessingMethod
        {
            [Description("Checkout")]
            checkout,
            [Description("Direct")]
            direct,
            [Description("Manual")]
            manual,
            [Description("Offsite")]
            offsite,
            [Description("Express")]
            express
        }

        #endregion

        #region Default properties

        [FieldSettings("Order number")]
        public string order_number { get; set; }

        [FieldSettings("Name")]
        public string name { get; set; }

        [FieldSettings("Total", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal? total_price { get; set; }

        [FieldSettings("Email address")]
        public string email { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Browser IP")]
        public string browser_ip { get; set; }

        [FieldSettings("Buyer accepts marketing", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool buyer_accepts_marketing { get; set; }

        [FieldSettings("Cart token")]
        public string cart_token { get; set; }

        [FieldSettings("Currency")]
        public string currency { get; set; }

        [FieldSettings("Cancel reason", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(ShopifyOrderCancelReason))]
        public ShopifyOrderCancelReason? cancel_reason { get; set; }

        [FieldSettings("Financial status", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(ShopifyOrderFinancialStatus))]
        public ShopifyOrderFinancialStatus? financial_status { get; set; }

        [FieldSettings("Fulfillment status", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(ShopifyOrderFulfillmentStatus))]
        public ShopifyOrderFulfillmentStatus? fulfillment_status { get; set; }

        [FieldSettings("Processing method", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(ShopifyOrderProcessingMethod))]
        public ShopifyOrderProcessingMethod? processing_method { get; set; }

        [FieldSettings("Tags")]
        public string tags { get; set; }

        [FieldSettings("Id")]
        public string id { get; set; }

        [FieldSettings("Landing site")]
        public string landing_site { get; set; }

        [FieldSettings("Note")]
        public string note { get; set; }

        [FieldSettings("Number", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int number { get; set; }

        [FieldSettings("Referring site")]
        public string referring_site { get; set; }

        [FieldSettings("Source name")]
        public string source_name { get; set; }

        [FieldSettings("Subtotal", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal? subtotal_price { get; set; }

        [FieldSettings("Taxes included", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool taxes_included { get; set; }

        [FieldSettings("Token")]
        public string token { get; set; }

        [FieldSettings("Total discounts", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal? total_discounts { get; set; }

        [FieldSettings("Total line items price", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal? total_line_items_price { get; set; }

        [FieldSettings("Total tax", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal? total_tax { get; set; }

        #endregion

        #region Hidden properties

        public DateTime? cancelled_at { get; set; }
        public DateTime? closed_at { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? processed_at { get; set; }
        public DateTime? updated_at { get; set; }
        public ShopifyAddress billing_address { get; set; }
        public ShopifyAddress shipping_address { get; set; }
        public ShopifyClientDetails client_details { get; set; }
        public ShopifyCustomer customer { get; set; }
        public string refund { get; set; } // todo - create new class
        public List<ShopifyDiscountCode> discount_codes { get; set; }
        public List<ShopifyFulfillment> fulfillments { get; set; }
        public List<ShopifyOrderLine> line_items { get; set; }
        public List<ShopifyNoteAttribute> note_attributes { get; set; }
        public List<ShopifyShippingLine> shipping_lines { get; set; }
        public List<ShopifyTaxLine> tax_lines { get; set; }
        public int total_weight { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Cancelled", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool cancelled => (cancelled_at == null);

        [FieldSettings("Cancelled at date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime cancelled_at_date
        {
            get
            {
                return cancelled_at?.Date ?? DateTime.MinValue;
            }
            set
            {
                cancelled_at = value;
            }
        }

        [FieldSettings("Closed", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool closed => (closed_at == null);

        [FieldSettings("Closed at date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime closed_at_date
        {
            get
            {
                return closed_at?.Date ?? DateTime.MinValue;
            }
            set
            {
                closed_at = value;
            }
        }

        [FieldSettings("Created at date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime created_at_date
        {
            get
            {
                return created_at?.Date ?? DateTime.MinValue;
            }
            set
            {
                created_at = value;
            }
        }

        [FieldSettings("Processed at date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime processed_at_date
        {
            get
            {
                return processed_at?.Date ?? DateTime.MinValue;
            }
            set
            {
                processed_at = value;
            }
        }

        [FieldSettings("Updated at date", Modified = true, FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public DateTime updated_at_date
        {
            get
            {
                return updated_at?.Date ?? DateTime.MinValue;
            }
            set
            {
                updated_at = value;
            }
        }

        [FieldSettings("Total weight in kilograms", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal? total_weight_kilograms
        {
            get
            {
                return total_weight / 1000;
            }
            set
            {
                var @decimal = value * 1000;
                if (@decimal != null) total_weight = (int)@decimal;
            }
        }

        [FieldSettings("Total weight in pounds", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal? total_weight_pounds
        {
            get
            {
                return (decimal)(total_weight / 453.592);
            }
            set
            {
                var @decimal = value * (decimal)453.592;
                if (@decimal != null) total_weight = (int)@decimal;
            }
        }

        #region Billing address

        [FieldSettings("Billing address", FieldTypeId = DataConnector.FieldTypeIdAddress)]
        public string billing_address_address => billing_address.address;

        [FieldSettings("Billing address 1")]
        public string billing_address1
        {
            get
            {
                return billing_address.address1;
            }
            set
            {
                billing_address.address1 = value;
            }
        }

        [FieldSettings("Billing address 2")]
        public string billing_address2
        {
            get
            {
                return billing_address.address2;
            }
            set
            {
                billing_address.address2 = value;
            }
        }

        [FieldSettings("Billing address city")]
        public string billing_address_city
        {
            get
            {
                return billing_address.city;
            }
            set
            {
                billing_address.city = value;
            }
        }

        [FieldSettings("Billing address company")]
        public string billing_address_company
        {
            get
            {
                return billing_address.company;
            }
            set
            {
                billing_address.company = value;
            }
        }

        [FieldSettings("Billing address country")]
        public string billing_address_country
        {
            get
            {
                return billing_address.country;
            }
            set
            {
                billing_address.country = value;
            }
        }

        [FieldSettings("Billing address first name")]
        public string billing_address_first_name
        {
            get
            {
                return billing_address.first_name;
            }
            set
            {
                billing_address.first_name = value;
            }
        }

        [FieldSettings("Billing address Id")]
        public string billing_address_id
        {
            get
            {
                return billing_address.id;
            }
            set
            {
                billing_address.id = value;
            }
        }

        [FieldSettings("Billing address last name")]
        public string billing_address_last_name
        {
            get
            {
                return billing_address.last_name;
            }
            set
            {
                billing_address.last_name = value;
            }
        }

        [FieldSettings("Billing address phone number")]
        public string billing_address_phone
        {
            get
            {
                return billing_address.phone;
            }
            set
            {
                billing_address.phone = value;
            }
        }

        [FieldSettings("Billing address province")]
        public string billing_address_province
        {
            get
            {
                return billing_address.province;
            }
            set
            {
                billing_address.province = value;
            }
        }

        [FieldSettings("Billing address zip")]
        public string billing_address_zip
        {
            get
            {
                return billing_address.zip;
            }
            set
            {
                billing_address.zip = value;
            }
        }

        [FieldSettings("Billing address province code")]
        public string billing_address_province_code
        {
            get
            {
                return billing_address.province_code;
            }
            set
            {
                billing_address.province_code = value;
            }
        }

        [FieldSettings("Billing address country code")]
        public string billing_address_country_code
        {
            get
            {
                return billing_address.country_code;
            }
            set
            {
                billing_address.country_code = value;
            }
        }

        [FieldSettings("Billing address country name")]
        public string billing_address_country_name
        {
            get
            {
                return billing_address.country_name;
            }
            set
            {
                billing_address.country_name = value;
            }
        }

        [FieldSettings("Billing address name")]
        public string billing_address_name
        {
            get
            {
                return billing_address.name;
            }
            set
            {
                billing_address.name = value;
            }
        }

        #endregion

        #region Shipping address

        [FieldSettings("Shipping address", FieldTypeId = DataConnector.FieldTypeIdAddress)]
        public string shipping_address_address => shipping_address.address;

        [FieldSettings("Shipping address 1")]
        public string shipping_address1
        {
            get
            {
                return shipping_address.address1;
            }
            set
            {
                shipping_address.address1 = value;
            }
        }

        [FieldSettings("Shipping address 2")]
        public string shipping_address2
        {
            get
            {
                return shipping_address.address2;
            }
            set
            {
                shipping_address.address2 = value;
            }
        }

        [FieldSettings("Shipping address city")]
        public string shipping_address_city
        {
            get
            {
                return shipping_address.city;
            }
            set
            {
                shipping_address.city = value;
            }
        }

        [FieldSettings("Shipping address company")]
        public string shipping_address_company
        {
            get
            {
                return shipping_address.company;
            }
            set
            {
                shipping_address.company = value;
            }
        }

        [FieldSettings("Shipping address country")]
        public string shipping_address_country
        {
            get
            {
                return shipping_address.country;
            }
            set
            {
                shipping_address.country = value;
            }
        }

        [FieldSettings("Shipping address first name")]
        public string shipping_address_first_name
        {
            get
            {
                return shipping_address.first_name;
            }
            set
            {
                shipping_address.first_name = value;
            }
        }

        [FieldSettings("Shipping address Id")]
        public string shipping_address_id
        {
            get
            {
                return shipping_address.id;
            }
            set
            {
                shipping_address.id = value;
            }
        }

        [FieldSettings("Shipping address last name")]
        public string shipping_address_last_name
        {
            get
            {
                return shipping_address.last_name;
            }
            set
            {
                shipping_address.last_name = value;
            }
        }

        [FieldSettings("Shipping address phone number")]
        public string shipping_address_phone
        {
            get
            {
                return shipping_address.phone;
            }
            set
            {
                shipping_address.phone = value;
            }
        }

        [FieldSettings("Shipping address province")]
        public string shipping_address_province
        {
            get
            {
                return shipping_address.province;
            }
            set
            {
                shipping_address.province = value;
            }
        }

        [FieldSettings("Shipping address zip")]
        public string shipping_address_zip
        {
            get
            {
                return shipping_address.zip;
            }
            set
            {
                shipping_address.zip = value;
            }
        }

        [FieldSettings("Shipping address province code")]
        public string shipping_address_province_code
        {
            get
            {
                return shipping_address.province_code;
            }
            set
            {
                shipping_address.province_code = value;
            }
        }

        [FieldSettings("Shipping address country code")]
        public string shipping_address_country_code
        {
            get
            {
                return shipping_address.country_code;
            }
            set
            {
                shipping_address.country_code = value;
            }
        }

        [FieldSettings("Shipping address country name")]
        public string shipping_address_country_name
        {
            get
            {
                return shipping_address.country_name;
            }
            set
            {
                shipping_address.country_name = value;
            }
        }

        [FieldSettings("Shipping address name")]
        public string shipping_address_name
        {
            get
            {
                return shipping_address.name;
            }
            set
            {
                shipping_address.name = value;
            }
        }

        #endregion

        #region Customer

        [FieldSettings("Customer accepts marketing", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool customer_accepts_marketing
        {
            get
            {
                return customer.accepts_marketing;
            }
            set
            {
                customer.accepts_marketing = value;
            }
        }

        [FieldSettings("Customer created at date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime customer_created_at_date
        {
            get
            {
                return customer.created_at_date;
            }
            set
            {
                customer.created_at_date = value;
            }
        }

        [FieldSettings("Customer first name")]
        public string customer_first_name
        {
            get
            {
                return customer.first_name;
            }
            set
            {
                customer.first_name = value;
            }
        }

        [FieldSettings("Customer Id")]
        public string customer_id
        {
            get
            {
                return customer.id;
            }
            set
            {
                customer.id = value;
            }
        }

        [FieldSettings("Customer last name")]
        public string customer_last_name
        {
            get
            {
                return customer.last_name;
            }
            set
            {
                customer.last_name = value;
            }
        }

        [FieldSettings("Number of orders from customer", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int? customer_orders_count
        {
            get
            {
                return customer.orders_count;
            }
            set
            {
                customer.orders_count = value;
            }
        }

        [FieldSettings("Customer status", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(ShopifyCustomer.ShopifyCustomerState))]
        public ShopifyCustomer.ShopifyCustomerState? customer_state
        {
            get
            {
                return customer.state;
            }
            set
            {
                customer.state = value;
            }
        }

        [FieldSettings("Customer tags")]
        public string customer_tags
        {
            get
            {
                return customer.tags;
            }
            set
            {
                customer.tags = value;
            }
        }

        [FieldSettings("Total spent by customer", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal customer_total_spent
        {
            get
            {
                return customer.total_spent ?? 0;
            }
            set
            {
                customer.total_spent = value;
            }
        }

        [FieldSettings("Customer updated at date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime customer_updated_at_date
        {
            get
            {
                return customer.updated_at_date;
            }
            set
            {
                customer.updated_at_date = value;
            }
        }

        #endregion

        #endregion

    }
}
