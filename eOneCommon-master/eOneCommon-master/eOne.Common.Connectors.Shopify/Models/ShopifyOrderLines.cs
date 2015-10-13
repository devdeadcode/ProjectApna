using System;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Shopify.Models
{
    public class ShopifyOrderLines : DataConnectorEntityModel
    {

        #region Hidden properties

        public ShopifyOrder Order { get; set; }
        public ShopifyOrderLine Line { get; set; }
        public ShopifyProduct Product { get; set; }

        #endregion

        #region Default properties

        [FieldSettings("Order number", DefaultField = true)]
        public string OrderNumber => Order.order_number;

        [FieldSettings("Order name", DefaultField = true)]
        public string OrderName => Order.name;

        [FieldSettings("Product title")]
        public string ProductTitle => Product.title;

        [FieldSettings("Quantity", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int LineQuantity => Line.quantity;

        [FieldSettings("Price", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal LinePrice => Line.price;

        #endregion

        #region Calculations

        #region Order

        [FieldSettings("Total", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal OrderTotal => Order.total_price ?? 0;

        [FieldSettings("Email address")]
        public string OrderEmail => Order.email;

        [FieldSettings("Browser IP")]
        public string OrderBrowserIp => Order.browser_ip;

        [FieldSettings("Buyer accepts marketing", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool OrderBuyerAcceptsMarketing => Order.buyer_accepts_marketing;

        [FieldSettings("Cart token")]
        public string OrderCartToken => Order.cart_token;

        [FieldSettings("Currency")]
        public string OrderCurrency => Order.currency;

        [FieldSettings("Cancel reason", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(ShopifyOrder.ShopifyOrderCancelReason))]
        public ShopifyOrder.ShopifyOrderCancelReason OrderCancelReason => Order.cancel_reason ?? ShopifyOrder.ShopifyOrderCancelReason.other;

        [FieldSettings("Financial status", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(ShopifyOrder.ShopifyOrderFinancialStatus))]
        public ShopifyOrder.ShopifyOrderFinancialStatus OrderFinancialStatus => Order.financial_status ?? ShopifyOrder.ShopifyOrderFinancialStatus.pending;

        [FieldSettings("Fulfillment status", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(ShopifyOrder.ShopifyOrderFulfillmentStatus))]
        public ShopifyOrder.ShopifyOrderFulfillmentStatus OrderFulfillmentStatus => Order.fulfillment_status ?? ShopifyOrder.ShopifyOrderFulfillmentStatus.@null;

        [FieldSettings("Processing method", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(ShopifyOrder.ShopifyOrderProcessingMethod))]
        public ShopifyOrder.ShopifyOrderProcessingMethod OrderProcessingMethod => Order.processing_method ?? ShopifyOrder.ShopifyOrderProcessingMethod.direct;

        [FieldSettings("Tags")]
        public string OrderTags => Order.tags;

        [FieldSettings("Id")]
        public string OrderId => Order.id;

        [FieldSettings("Landing site")]
        public string OrderLandingSite => Order.landing_site;

        [FieldSettings("Note")]
        public string OrderNote => Order.note;

        [FieldSettings("Referring site")]
        public string OrderReferringSite => Order.referring_site;

        [FieldSettings("Source name")]
        public string OrderSourceName => Order.source_name;

        [FieldSettings("Taxes included", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool OrderTaxesIncluded => Order.taxes_included;

        [FieldSettings("Token")]
        public string OrderToken => Order.token;

        [FieldSettings("Cancelled at date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime OrderCancelledAtDate => Order.cancelled_at_date;

        [FieldSettings("Closed at date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime OrderClosedAtDate => Order.closed_at_date;

        [FieldSettings("Created at date", Created = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime OrderCreatedAtDate => Order.created_at_date;

        [FieldSettings("Processed at date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime OrderProcessedAtDate => Order.processed_at_date;

        [FieldSettings("Updated at date", Modified = true, FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public DateTime OrderUpdatedAtDate => Order.updated_at_date;

        #region Billing address

        [FieldSettings("Billing address", FieldTypeId = DataConnector.FieldTypeIdAddress)]
        public string OrderBillingAddress => Order.billing_address_address;

        [FieldSettings("Billing address 1")]
        public string OrderBillingAddress1 => Order.billing_address1;

        [FieldSettings("Billing address 2")]
        public string OrderBillingAddress2 => Order.billing_address2;

        [FieldSettings("Billing address city")]
        public string OrderBillingAddressCity => Order.billing_address_city;

        [FieldSettings("Billing address company")]
        public string OrderBillingAddressCompany => Order.billing_address_company;

        [FieldSettings("Billing address country")]
        public string OrderBillingAddressCountry => Order.billing_address_country;

        [FieldSettings("Billing address first name")]
        public string OrderBillingAddressFirstName => Order.billing_address_first_name;

        [FieldSettings("Billing address Id")]
        public string OrderBillingAddressId => Order.billing_address_id;

        [FieldSettings("Billing address last name")]
        public string OrderBillingAddressLastName => Order.billing_address_last_name;

        [FieldSettings("Billing address phone number")]
        public string OrderBillingAddressPhone => Order.billing_address_phone;

        [FieldSettings("Billing address state")]
        public string OrderBillingAddressProvince => Order.billing_address_province;

        [FieldSettings("Billing address zip")]
        public string OrderBillingAddressZip => Order.billing_address_zip;

        [FieldSettings("Billing address state code")]
        public string OrderBillingAddressProvinceCode => Order.billing_address_province_code;

        [FieldSettings("Billing address country code")]
        public string OrderBillingAddressCountryCode => Order.billing_address_country_code;

        [FieldSettings("Billing address country name")]
        public string OrderBillingAddressCountryName => Order.billing_address_country_name;

        [FieldSettings("Billing address name")]
        public string OrderBillingAddressName => Order.billing_address_name;

        #endregion

        #region Shipping address

        [FieldSettings("Shipping address", FieldTypeId = DataConnector.FieldTypeIdAddress)]
        public string OrderShippingAddress => Order.shipping_address_address;

        [FieldSettings("Shipping address 1")]
        public string OrderShippingAddress1 => Order.shipping_address1;

        [FieldSettings("Shipping address 2")]
        public string OrderShippingAddress2 => Order.shipping_address2;

        [FieldSettings("Shipping address city")]
        public string OrderShippingAddressCity => Order.shipping_address_city;

        [FieldSettings("Shipping address company")]
        public string OrderShippingAddressCompany => Order.shipping_address_company;

        [FieldSettings("Shipping address country")]
        public string OrderShippingAddressCountry => Order.shipping_address_country;

        [FieldSettings("Shipping address first name")]
        public string OrderShippingAddressFirstName => Order.shipping_address_first_name;

        [FieldSettings("Shipping address Id")]
        public string OrderShippingAddressId => Order.shipping_address_id;

        [FieldSettings("Shipping address last name")]
        public string OrderShippingAddressLastName => Order.shipping_address_last_name;

        [FieldSettings("Shipping address phone number")]
        public string OrderShippingAddressPhone => Order.shipping_address_phone;

        [FieldSettings("Shipping address state")]
        public string OrderShippingAddressProvince => Order.shipping_address_province;

        [FieldSettings("Shipping address zip")]
        public string OrderShippingAddressZip => Order.shipping_address_zip;

        [FieldSettings("Shipping address state code")]
        public string OrderShippingAddressProvinceCode => Order.shipping_address_province_code;

        [FieldSettings("Shipping address country code")]
        public string OrderShippingAddressCountryCode => Order.shipping_address_country_code;

        [FieldSettings("Shipping address country name")]
        public string OrderShippingAddressCountryName => Order.shipping_address_country_name;

        [FieldSettings("Shipping address name")]
        public string OrderShippingAddressName => Order.shipping_address_name;

        #endregion

        #region Customer

        [FieldSettings("Customer accepts marketing", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool OrderCustomerAcceptsMarketing => Order.customer_accepts_marketing;

        [FieldSettings("Customer created at date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime OrderCustomerCreatedAtDate => Order.customer_created_at_date;

        [FieldSettings("Customer first name")]
        public string OrderCustomerFirstName => Order.customer_first_name;

        [FieldSettings("Customer Id")]
        public string OrderCustomerId => Order.customer_id;

        [FieldSettings("Customer last name")]
        public string OrderCustomerLastName => Order.customer_last_name;

        [FieldSettings("Number of orders from customer", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int OrderCustomerOrdersCount => Order.customer_orders_count ?? 0;

        [FieldSettings("Customer status", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(ShopifyCustomer.ShopifyCustomerState))]
        public ShopifyCustomer.ShopifyCustomerState OrderCustomerState => Order.customer_state ?? ShopifyCustomer.ShopifyCustomerState.disabled;

        [FieldSettings("Customer tags")]
        public string OrderCustomerTags => Order.customer_tags;

        [FieldSettings("Total spent by customer", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal OrderCustomerTotalSpent => Order.customer_total_spent;

        [FieldSettings("Customer updated at date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime OrderCustomerUpdatedAtDate => Order.customer_updated_at_date;

        #endregion

        #endregion

        #region Line

        [FieldSettings("Fulfillable quantity", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int LineFulfillableQuantity => Line.fulfillable_quantity;

        [FieldSettings("Fulfillment service")]
        public string LineFulfillmentService => Line.fulfillment_service;

        [FieldSettings("Fulfillment status")]
        public string LineFulfillmentStatus => Line.fulfillment_status;

        [FieldSettings("Weight in grams", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int LineGrams => Line.grams;

        [FieldSettings("Requires shipping", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool LineRequiresShipping => Line.requires_shipping;

        [FieldSettings("SKU")]
        public string LineSku => Line.sku;

        [FieldSettings("Title")]
        public string LineTitle => Line.title;

        [FieldSettings("Variant Id")]
        public string LineVariantId => Line.variant_id;

        [FieldSettings("Variant title")]
        public string LineVariantTitle => Line.variant_title;

        [FieldSettings("Vendor")]
        public string LineVendor => Line.vendor;

        [FieldSettings("Name")]
        public string LineName => Line.name;

        [FieldSettings("Gift card", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool LineGiftCard => Line.gift_card;

        [FieldSettings("Taxable", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool LineTaxable => Line.taxable;

        #endregion

        #region Product

        [FieldSettings("Product description")]
        public string ProductDescription => Product.body_html;

        [FieldSettings("Product Id")]
        public string ProductId => Product.id;

        [FieldSettings("Product type")]
        public string ProductType => Product.product_type;

        [FieldSettings("Product tags")]
        public string ProductTags => Product.tags;

        [FieldSettings("Product URL")]
        public string ProductUrl => Product.url;

        #endregion

        [FieldSettings("Unit price", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal UnitPrice => LinePrice / LineQuantity;

        #endregion

    }
}
