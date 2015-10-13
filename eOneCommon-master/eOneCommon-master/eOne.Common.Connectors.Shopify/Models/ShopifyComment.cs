using System;
using System.ComponentModel;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Shopify.Models
{
    public class ShopifyComment : DataConnectorEntityModel
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

        [FieldSettings("Email", DefaultField = true)]
        public string email { get; set; }

        [FieldSettings("Comment", DefaultField = true)]
        public string body { get; set; }

        [FieldSettings("Status", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(ShopifyCommentStatus))]
        public ShopifyCommentStatus status { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Html comment")]
        public string body_html { get; set; }

        [FieldSettings("Id")]
        public string id { get; set; }

        [FieldSettings("IP Address")]
        public string ip { get; set; }

        [FieldSettings("Browser")]
        public string user_agent { get; set; }

        #endregion

        #region Hidden properties

        public string article_id { get; set; }
        public string blog_id { get; set; }
        public DateTime? created_at { get; set; }
        public string published_at { get; set; }
        public DateTime? updated_at { get; set; }

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
