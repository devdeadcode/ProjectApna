using System;
using System.ComponentModel;

namespace eOne.Common.Connectors.Shopify.Models
{
    public class ShopifyComment : ConnectorEntityModel
    {

        #region Enums

        public enum ShopifyCommentStatus
        {
            [Description("Unapproved")]
            unapproved,
            [Description("Published")]
            published,
            [Description("Spam")]
            spam,
            [Description("Removed")]
            removed
        }

        #endregion

        #region Default properties

        [FieldSettings("Author", DefaultField = true)]
        public string author { get; set; }

        [FieldSettings("Email", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEmail)]
        public string email { get; set; }

        [FieldSettings("Comment", DefaultField = true)]
        public string body { get; set; }

        [FieldSettings("Status", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(ShopifyCommentStatus))]
        public ShopifyCommentStatus status { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Html comment")]
        public string body_html { get; set; }

        [FieldSettings("ID")]
        public string id { get; set; }

        [FieldSettings("IP address")]
        public string ip { get; set; }

        [FieldSettings("Browser")]
        public string user_agent { get; set; }

        [FieldSettings("Created at date", Created = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? created_at { get; set; }

        [FieldSettings("Updated at date", Modified = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? updated_at { get; set; }

        [FieldSettings("Published at date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? published_at { get; set; }

        #endregion

        #region Hidden properties

        public string article_id { get; set; }
        public string blog_id { get; set; }

        #endregion

    }
}
