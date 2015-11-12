using System;
using System.ComponentModel;

namespace eOne.Common.Connectors.Shopify.Models
{
    public class ShopifyBlog : ConnectorEntityModel
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

        [FieldSettings("Comment policy", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(ShopifyBlogCommentable))]
        public ShopifyBlogCommentable commentable { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Feedburner")]
        public string feedburner { get; set; }

        [FieldSettings("Feedburner location")]
        public string feedburner_location { get; set; }

        [FieldSettings("Handle")]
        public string handle { get; set; }

        [FieldSettings("ID")]
        public string id { get; set; }

        [FieldSettings("Tags")]
        public string tags { get; set; }

        [FieldSettings("Template suffix")]
        public string template_suffix { get; set; }

        [FieldSettings("Created at date", Created = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? updated_at { get; set; }

        [FieldSettings("Updated at date", Modified = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? created_at { get; set; }

        #endregion

        #region Hidden properties

        public ShopifyMetafield metafield { get; set; }

        #endregion

    }
}
