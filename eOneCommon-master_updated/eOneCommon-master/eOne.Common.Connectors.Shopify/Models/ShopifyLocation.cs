using System;

namespace eOne.Common.Connectors.Shopify.Models
{
    public class ShopifyLocation : ConnectorEntityModel
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

        [FieldSettings("ID")]
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

        [FieldSettings("Created at date", Created = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? created_at { get; set; }

        [FieldSettings("Updated at date", Modified = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? updated_at { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Address", FieldTypeId = Connector.FieldTypeIdAddress)]
        public string address => BuildAddress(address1, address2, city, province, zip);

        #endregion

    }
}
