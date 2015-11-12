using System;

namespace eOne.Common.Connectors.Shopify.Models
{
    public class ShopifyCustomerAddress : ConnectorEntityModel
    {

        #region Hidden properties

        public ShopifyAddress Address { get; set; }
        public ShopifyCustomer Customer { get; set; }

        #endregion

        #region Calculations

        #region Customer

        [FieldSettings("First name", DefaultField = true)]
        public string CustomerFirstName => Customer.first_name;

        [FieldSettings("Last name", DefaultField = true)]
        public string CustomerLastName => Customer.last_name;

        [FieldSettings("Email", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEmail)]
        public string CustomerEmail => Customer.email;

        [FieldSettings("Customer status", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(ShopifyCustomer.ShopifyCustomerState))]
        public ShopifyCustomer.ShopifyCustomerState CustomerStatus => Customer.state ?? ShopifyCustomer.ShopifyCustomerState.disabled;

        [FieldSettings("Customer accepts marketing", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool CustomerAcceptsMarketing => Customer.accepts_marketing;

        [FieldSettings("Customer ID", KeyNumber = 1)]
        public string CustomerId => Customer.id;

        [FieldSettings("Last order ID")]
        public string CustomerLastOrderId => Customer.last_order_id;

        [FieldSettings("Last order name")]
        public string CustomerLastOrderName => Customer.last_order_name;

        [FieldSettings("Customer note")]
        public string CustomerNote => Customer.note;

        [FieldSettings("Number of orders", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int CustomerOrdersCount => Customer.orders_count ?? 0;

        [FieldSettings("Customer tags")]
        public string CustomerTags => Customer.tags;

        [FieldSettings("Total spent", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal CustomerTotalSpent => Customer.total_spent ?? 0;

        [FieldSettings("Customer email verified", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool CustomerVerifiedEmail => Customer.verified_email;

        [FieldSettings("Customer created at date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? CustomerCreatedAtDate => Customer.created_at;

        [FieldSettings("Customer updated at date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? CustomerUpdatedAtDate => Customer.updated_at;

        #endregion

        #region Address

        [FieldSettings("Address", FieldTypeId = Connector.FieldTypeIdAddress)]
        public string address_address => Address.address;

        [FieldSettings("Address 1", DefaultField = true)]
        public string address1 { get; set; }

        [FieldSettings("Address 2", DefaultField = true)]
        public string address2 { get; set; }

        [FieldSettings("City", DefaultField = true)]
        public string city { get; set; }

        [FieldSettings("State", DefaultField = true)]
        public string province { get; set; }

        [FieldSettings("Zip", DefaultField = true)]
        public string zip { get; set; }

        [FieldSettings("Company")]
        public string company { get; set; }

        [FieldSettings("Country")]
        public string country { get; set; }

        [FieldSettings("Address first name")]
        public string first_name { get; set; }

        [FieldSettings("Address ID", KeyNumber = 2)]
        public string id { get; set; }

        [FieldSettings("Address last name")]
        public string last_name { get; set; }

        [FieldSettings("Phone number", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string phone { get; set; }

        [FieldSettings("State code")]
        public string province_code { get; set; }

        [FieldSettings("Country code")]
        public string country_code { get; set; }

        [FieldSettings("Country name")]
        public string country_name { get; set; }

        [FieldSettings("Address name")]
        public string name { get; set; }

        #endregion

        #endregion

    }
}
