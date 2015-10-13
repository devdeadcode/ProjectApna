using System;
using System.Collections.Generic;
using System.ComponentModel;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Shopify.Models
{
    public class ShopifyCustomer : DataConnectorEntityModel
    {

        #region Enums

        public enum ShopifyCustomerState
        {
            [Description("Disabled")]
            disabled,
            [Description("Declined")]
            decline,
            [Description("Invited")]
            invited,
            [Description("Enabled")]
            enabled
        }

        #endregion

        #region Default properties

        [FieldSettings("First name", DefaultField = true)]
        public string first_name { get; set; }

        [FieldSettings("Last name", DefaultField = true)]
        public string last_name { get; set; }

        [FieldSettings("Email", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string email { get; set; }

        [FieldSettings("Status", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(ShopifyCustomerState))]
        public ShopifyCustomerState? state { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Accepts marketing", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool accepts_marketing { get; set; }

        [FieldSettings("Id")]
        public string id { get; set; }

        [FieldSettings("Multipass identifier")]
        public string multipass_identifier { get; set; }

        [FieldSettings("Last order Id")]
        public string last_order_id { get; set; }

        [FieldSettings("Last order name")]
        public string last_order_name { get; set; }

        [FieldSettings("Note")]
        public string note { get; set; }

        [FieldSettings("Number of orders", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int? orders_count { get; set; }

        [FieldSettings("Tags")]
        public string tags { get; set; }

        [FieldSettings("Total spent", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal? total_spent { get; set; }

        [FieldSettings("Email verified", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool verified_email { get; set; }

        #endregion

        #region Hidden properties

        public List<ShopifyAddress> addresses { get; set; }
        public ShopifyAddress default_address { get; set; }
        public DateTime? created_at { get; set; }
        public ShopifyMetafield metafield { get; set; }
        public DateTime? updated_at { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Created at date", Created = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
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

        [FieldSettings("Updated at date", Modified = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
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

        [FieldSettings("Address", FieldTypeId = DataConnector.FieldTypeIdAddress)]
        public string default_address_address => default_address.address;

        [FieldSettings("Address 1")]
        public string default_address1
        {
            get
            {
                return default_address.address1;
            }
            set
            {
                default_address.address1 = value;
            }
        }

        [FieldSettings("Address 2")]
        public string default_address2
        {
            get
            {
                return default_address.address2;
            }
            set
            {
                default_address.address2 = value;
            }
        }

        [FieldSettings("City")]
        public string default_address_city
        {
            get
            {
                return default_address.city;
            }
            set
            {
                default_address.city = value;
            }
        }

        [FieldSettings("Company")]
        public string default_address_company
        {
            get
            {
                return default_address.company;
            }
            set
            {
                default_address.company = value;
            }
        }

        [FieldSettings("Country")]
        public string default_address_country
        {
            get
            {
                return default_address.country;
            }
            set
            {
                default_address.country = value;
            }
        }

        [FieldSettings("Phone number")]
        public string default_address_phone
        {
            get
            {
                return default_address.phone;
            }
            set
            {
                default_address.phone = value;
            }
        }

        [FieldSettings("Province")]
        public string default_address_province
        {
            get
            {
                return default_address.province;
            }
            set
            {
                default_address.province = value;
            }
        }

        [FieldSettings("Zip")]
        public string default_address_zip
        {
            get
            {
                return default_address.zip;
            }
            set
            {
                default_address.zip = value;
            }
        }

        [FieldSettings("Province code")]
        public string default_address_province_code
        {
            get
            {
                return default_address.province_code;
            }
            set
            {
                default_address.province_code = value;
            }
        }

        [FieldSettings("Country code")]
        public string default_address_country_code
        {
            get
            {
                return default_address.country_code;
            }
            set
            {
                default_address.country_code = value;
            }
        }

        [FieldSettings("Country name")]
        public string default_address_country_name
        {
            get
            {
                return default_address.country_name;
            }
            set
            {
                default_address.country_name = value;
            }
        }

        #endregion

    }
}
