using System;
using System.ComponentModel;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Shopify.Models
{
    public class ShopifyBlog : DataConnectorEntityModel
    {

        #region Enums

        public enum ShopifyBlogCommentable
        {
            [Description("Disabled")]
            no,
            [Description("Moderated")]
            moderate,
            [Description("Enabled")]
            yes
        }

        #endregion

        #region Default properties

        [FieldSettings("Title", DefaultField = true)]
        public string title { get; set; }

        [FieldSettings("Comment policy", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(ShopifyBlogCommentable))]
        public ShopifyBlogCommentable commentable { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Feedburner")]
        public string feedburner { get; set; }

        [FieldSettings("Feedburner location")]
        public string feedburner_location { get; set; }

        [FieldSettings("Handle")]
        public string handle { get; set; }

        [FieldSettings("Id")]
        public string id { get; set; }

        [FieldSettings("Tags")]
        public string tags { get; set; }

        [FieldSettings("Template suffix")]
        public string template_suffix { get; set; }

        #endregion

        #region Hidden properties

        public ShopifyMetafield metafield { get; set; }
        public DateTime? updated_at { get; set; }
        public DateTime? created_at { get; set; }

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

        #endregion

    }
}
