using System;
using System.Collections.Generic;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Shopify.Models
{
    public class ShopifyProduct : DataConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Title")]
        public string title { get; set; }

        [FieldSettings("Description")]
        public string body_html { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Handle")]
        public string handle { get; set; }

        [FieldSettings("Id")]
        public string id { get; set; }

        [FieldSettings("Product type")]
        public string product_type { get; set; }

        [FieldSettings("Published scope")]
        public string published_scope { get; set; } // enum ?

        [FieldSettings("Tags")]
        public string tags { get; set; }

        [FieldSettings("Template suffix")]
        public string template_suffix { get; set; }

        #endregion

        #region Hidden properties

        public DateTime? created_at { get; set; }
        public DateTime? published_at { get; set; }
        public DateTime? updated_at { get; set; }
        public List<ShopifyProductOption> options { get; set; }
        public List<ShopifyProductVariants> variants { get; set; }
        public string site_prefix { get; set; }

        #endregion

        #region Private dummy properties

        private string url_dummy { get; set; }

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

        [FieldSettings("Published at date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime published_at_date
        {
            get
            {
                return published_at?.Date ?? DateTime.MinValue;
            }
            set
            {
                published_at = value;
            }
        }

        [FieldSettings("Published", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool published
        {
            get
            {
                return (published_at != null);
            }
            set
            {
                if (value) published_at = DateTime.Today; else published_at = null;
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

        [FieldSettings("Option 1")]
        public string option1
        {
            get
            {
                foreach (var option in options.Where(option => option.position == 1)) return option.name;
                return string.Empty;
            }
            set
            {
                foreach (var option in options.Where(option => option.position == 1)) option.name = value;
            }
        }

        [FieldSettings("Option 2")]
        public string option2
        {
            get
            {
                foreach (var option in options.Where(option => option.position == 2)) return option.name;
                return string.Empty;
            }
            set
            {
                foreach (var option in options.Where(option => option.position == 2)) option.name = value;
            }
        }

        [FieldSettings("Option 3")]
        public string option3
        {
            get
            {
                foreach (var option in options.Where(option => option.position == 3)) return option.name;
                return string.Empty;
            }
            set
            {
                foreach (var option in options.Where(option => option.position == 3)) option.name = value;
            }
        }

        [FieldSettings("URL")]
        public string url
        {
            get
            {
                return $"http://{site_prefix}.myshopify.com/products/{handle}";
            }
            set
            {
                url_dummy = value;
            }
        }

        #endregion

    }
}
