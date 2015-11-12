using System;
using System.Collections.Generic;
using System.Linq;

namespace eOne.Common.Connectors.Shopify.Models
{
    public class ShopifyProduct : ConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Title", DefaultField = true, Description = true)]
        public string title { get; set; }

        [FieldSettings("Description", DefaultField = true)]
        public string body_html { get; set; }

        [FieldSettings("Published", FieldTypeId = Connector.FieldTypeIdYesNo, DefaultField = true)]
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

        #endregion

        #region Properties

        [FieldSettings("Handle")]
        public string handle { get; set; }

        [FieldSettings("ID", KeyNumber = 1)]
        public string id { get; set; }

        [FieldSettings("Product type")]
        public string product_type { get; set; }

        [FieldSettings("Published scope")]
        public string published_scope { get; set; } // enum ?

        [FieldSettings("Tags")]
        public string tags { get; set; }

        [FieldSettings("Template suffix")]
        public string template_suffix { get; set; }

        [FieldSettings("Created at date", Created = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? created_at { get; set; }

        [FieldSettings("Published at date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? published_at { get; set; }

        [FieldSettings("Updated at date", Modified = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? updated_at { get; set; }

        #endregion

        #region Hidden properties

        public List<ShopifyProductOption> options { get; set; }
        public List<ShopifyProductVariants> variants { get; set; }
        public string site_prefix { get; set; }

        #endregion

        #region Calculations

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
        public string url => $"http://{site_prefix}.myshopify.com/products/{handle}";

        #endregion

    }
}
