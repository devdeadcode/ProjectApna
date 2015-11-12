using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Stripe.Models
{
    class StripeProducts : DataConnectorEntityModel
    {
        #region Default properties
        [FieldSettings("Name", DefaultField = true)]
        public string name { get; set; }

        [FieldSettings("Description", DefaultField = true)]
        public string description { get; set; }
        #endregion

        #region General properties
        [FieldSettings("Active", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool active { get; set; }

        [FieldSettings("Caption")]
        public string caption { get; set; }

        [FieldSettings("Shippable", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool shippable { get; set; }

        [FieldSettings("Link")]
        public string url { get; set; }

        [FieldSettings("NUmber of SKUs")]
        public int? total_count => skus?.count;

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

        public StripeProductSKU skus { get; set; }
        public StripeProductPackageDimensions package_dimensions { get; set; }
        public long created { get; set; }
        public long updated { get; set; }
        public List<string> attributes { get; set; }
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

        [FieldSettings("Updated date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime updated_date
        {
            get
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return epoch.AddSeconds(updated);
            }
        }

        [FieldSettings("Has SKUs", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool has_sku => skus?.count > 0;

        [FieldSettings("Height (cm)", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? height_in_cm => height*254/100;

        [FieldSettings("Weight (cm)", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? weight_in_cm => weight * 254 / 100;

        [FieldSettings("Length (cm)", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? length_in_cm=> length * 254 / 100;

        [FieldSettings("Width (cm)", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? width_in_cm => width * 254 / 100;
        #endregion

    }
}
