using System;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Shopify.Models
{
    public class ShopifyArticle : DataConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Author", DefaultField = true)]
        public string author { get; set; }

        [FieldSettings("Title", DefaultField = true)]
        public string title { get; set; }

        [FieldSettings("Article summary", DefaultField = true)]
        public string summary_html { get; set; }

        [FieldSettings("Published", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool published { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Id")]
        public string id { get; set; }

        [FieldSettings("Article body")]
        public string body_html { get; set; }

        [FieldSettings("Tags")]
        public string tags { get; set; }

        [FieldSettings("Template suffix")]
        public string template_suffix { get; set; }

        #endregion

        #region Hidden properties

        public string blog_id { get; set; }
        public DateTime? updated_at { get; set; }
        public DateTime? published_at { get; set; }
        public DateTime? created_at { get; set; }
        public ShopifyMetafield metafield { get; set; }
        public string user_id { get; set; }

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

        #endregion

    }
}
