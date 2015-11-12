using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eOne.Common.DataConnectors;
using Newtonsoft.Json.Linq;

namespace eOne.Common.Connectors.Stripe.Models
{
    class StripeProductSKUData : DataConnectorEntityModel
    {
        public StripeProductSKUData()
        {
            names = new List<string>();
            values = new List<string>();
        }

        #region Default properties

        [FieldSettings("Product name", DefaultField = true)]
        public string product_name => products?.name;
        #endregion

        #region General properties
        [FieldSettings("Active", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool active { get; set; }

        [FieldSettings("Currency")]
        public string currency { get; set; }

        [FieldSettings("Inventory quantity", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int? inventory_quantity => inventory?.quantity;

        [FieldSettings("Inventory quantity type", FieldTypeId = DataConnector.FieldTypeIdEnum,
            EnumType = typeof(StripeProductSKUInventory.InventoryType))]
        public StripeProductSKUInventory.InventoryType? inventory_type => inventory?.type;

        [FieldSettings("Inventory status", FieldTypeId = DataConnector.FieldTypeIdEnum,
            EnumType = typeof(StripeProductSKUInventory.InventoryValue))]
        public StripeProductSKUInventory.InventoryValue? inventory_status => inventory?.value;

        [FieldSettings("Product active", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool? product_active => products?.active;

        [FieldSettings("Product shippable", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool? product_shippable => products?.shippable;

        [FieldSettings("Product description")]
        public string product_description => products?.description;

        [FieldSettings("Product caption")]
        public string product_caption => products?.caption;

        [FieldSettings("Link")]
        public string url => skus?.url;

        [FieldSettings("Height (inches)", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? height => package_dimensions?.height;

        [FieldSettings("Weight (inches)", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? weight => package_dimensions?.weight;

        [FieldSettings("Length (inches)", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? length => package_dimensions?.length;

        [FieldSettings("Width (inches)", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? width => package_dimensions?.width;
        #endregion

        #region Hidden properties

        //public List<CustomAttribute> Attributes { get; set; }


        public StripeProductSKU skus { get; set; }
        public StripeProducts products { get; set; }
        public StripeProductSKUInventory inventory { get; set; }
        public StripeProductSKUProductDimension package_dimensions { get; set; }
        public decimal price { get; set; }
        public long created { get; set; }
        public long updated { get; set; }
        public List<string> names { get; set; }
        public List<string> values { get; set; }
        public StripeSKUAttributes attributes { get; set; }
        #endregion

        #region Calculations

        [FieldSettings("Price", FieldTypeId = DataConnector.FieldTypeIdQuantity, DefaultField = true)]
        public decimal? sku_price => price / 100;

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
        public DateTime updated_date
        {
            get
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return epoch.AddSeconds(updated);
            }
        }

        [FieldSettings("Attributes", DefaultField = true)]
        public string attribs
        {
            get
            {
                var joined = values.Where(val => val != null).Aggregate("", (current, val) => current + (val + ","));
                joined = joined.TrimEnd(',');
                return joined;
            }
        }

        [FieldSettings("Attribute name 1")]
        public string attrib1_name => names[0];

        [FieldSettings("Attribute value 1")]
        public string attrib1_value => values[0];

        [FieldSettings("Attribute name 2")]
        public string attrib2_name => names[1];

        [FieldSettings("Attribute value 2")]
        public string attrib2_value => values[1];

        //[FieldSettings("Attribute name 3")]
        //public string attrib3_name => names.Count < 3 ? null : names[2];
        [FieldSettings("Attribute name 3")]
        public string attrib3_name => names[2];

        [FieldSettings("Attribute value 3")]
        public string attrib3_value => values[2];

        [FieldSettings("Attribute name 4")]
        public string attrib4_name => names[3];

        [FieldSettings("Attribute value 4")]
        public string attrib4_value => values[3];

        [FieldSettings("Attribute name 5")]
        public string attrib5_name => names[4];

        [FieldSettings("Attribute value 5")]
        public string attrib5_value => values[4];

        [FieldSettings("Height (cm)", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? height_in_cm => height * 254 / 100;

        [FieldSettings("Weight (cm)", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? weight_in_cm => weight * 254 / 100;

        [FieldSettings("Length (cm)", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? length_in_cm => length * 254 / 100;

        [FieldSettings("Width (cm)", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? width_in_cm => width * 254 / 100;
        #endregion
    }


}
