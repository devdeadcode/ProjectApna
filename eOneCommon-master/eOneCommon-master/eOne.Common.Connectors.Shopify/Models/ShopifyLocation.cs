using System;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Shopify.Models
{
    public class ShopifyLocation : DataConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Name", DefaultField = true)]
        public string name { get; set; }

        [FieldSettings("City", DefaultField = true)]
        public string city { get; set; }

        [FieldSettings("Phone number", DefaultField = true)]
        public string phone { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Id")]
        public string id { get; set; }

        [FieldSettings("Location type")]
        public string location_type { get; set; } // posible enum

        [FieldSettings("Address 1")]
        public string address1 { get; set; }

        [FieldSettings("Address 2")]
        public string address2 { get; set; }

        [FieldSettings("Zip")]
        public string zip { get; set; }

        [FieldSettings("Province")]
        public string province { get; set; }

        [FieldSettings("Country")]
        public string country { get; set; }

        #endregion

        #region Hidden properties

        public DateTime? created_at { get; set; }
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
        public string address => BuildAddress(address1, address2, city, province, zip);

        #endregion

    }
}
