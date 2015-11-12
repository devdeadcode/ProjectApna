using System;

namespace eOne.Common.Connectors.Shopify.Models
{
    public class ShopifyArticle : ConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Author", DefaultField = true)]
        public string author { get; set; }

        [FieldSettings("Title", DefaultField = true, Description = true)]
        public string title { get; set; }

        [FieldSettings("Article summary", DefaultField = true)]
        public string summary_html { get; set; }

        [FieldSettings("Published", DefaultField = true, FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool published { get; set; }

        #endregion

        #region Properties

        [FieldSettings("ID", KeyNumber = 1)]
        public string id { get; set; }

        [FieldSettings("Article body")]
        public string body_html { get; set; }

        [FieldSettings("Tags")]
        public string tags { get; set; }

        [FieldSettings("Template suffix")]
        public string template_suffix { get; set; }

        [FieldSettings("Updated at date", Modified = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? updated_at { get; set; }

        [FieldSettings("Published at date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? published_at { get; set; }

        [FieldSettings("Created at date", Created = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? created_at { get; set; }

        #endregion

        #region Hidden properties

        public string blog_id { get; set; }
        public ShopifyBlog blog { get; set; }
        
        public ShopifyMetafield metafield { get; set; }
        public string user_id { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Blog name", DefaultField = true)]
        public string blog_title => blog.title;

        [FieldSettings("Blog comment policy", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(ShopifyBlog.ShopifyBlogCommentable))]
        public ShopifyBlog.ShopifyBlogCommentable blog_commentable => blog.commentable;

        #endregion

    }
}
