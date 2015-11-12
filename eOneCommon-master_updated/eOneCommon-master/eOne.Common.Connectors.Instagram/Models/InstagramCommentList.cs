using System;

namespace eOne.Common.Connectors.Instagram.Models
{
    public class InstagramCommentList : ConnectorEntityModel
    {

        public InstagramCommentList(InstagramComment comment, InstagramPost post)
        {
            Comment = comment;
            Post = post;
        }

        #region Default properties

        [FieldSettings("Post caption", DefaultField = true, SearchPriority = 2)]
        public string post_caption => Post.caption_text;

        [FieldSettings("Comment text", DefaultField = true, SearchPriority = 4)]
        public string comment_text => Comment.text;

        [FieldSettings("Name", DefaultField = true, SearchPriority = 3)]
        public string comment_full_name => Comment.from.full_name;

        [FieldSettings("Date", DefaultField = true, FieldTypeId = Connector.FieldTypeIdDate, Created = true)]
        public DateTime comment_created_date => FromEpochSeconds(Comment.created_time);

        [FieldSettings("Time", DefaultField = true, FieldTypeId = Connector.FieldTypeIdTime, Created = true)]
        public DateTime comment_created_time => comment_created_date;

        #endregion

        #region Hidden properties

        public InstagramComment Comment { get; set; }
        public InstagramPost Post { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Username", SearchPriority = 3)]
        public string comment_username => Comment.from.username;

        [FieldSettings("Comment ID", KeyNumber = 2, SearchPriority = 0)]
        public string comment_id => Comment.id;

        [FieldSettings("Post type", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(InstagramPost.InstagramPostType))]
        public InstagramPost.InstagramPostType post_type => Post.type;

        [FieldSettings("Post date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime post_date => Post.post_date;

        [FieldSettings("Post time", FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime post_time => Post.post_time;

        [FieldSettings("Post link", FieldTypeId = Connector.FieldTypeIdUrl, SearchPriority = 0)]
        public string post_link => Post.link;

        [FieldSettings("Filter")]
        public string post_filter => Post.filter;

        [FieldSettings("Post ID", KeyNumber = 1, SearchPriority = 0)]
        public string post_id => Post.id;

        [FieldSettings("Post location name")]
        public string post_location_name => Post.location_name;

        [FieldSettings("Post tags", SearchPriority = 0)]
        public string post_tag_list => Post.tag_list;

        [FieldSettings("Users in photo", SearchPriority = 0)]
        public string post_users_in_photo_list => Post.users_in_photo_list;

        [FieldSettings("Thumbnail image", FieldTypeId = Connector.FieldTypeIdImage, SearchPriority = 0)]
        public string post_thumbnail_image => Post.thumbnail_image;

        [FieldSettings("Image", FieldTypeId = Connector.FieldTypeIdImage, SearchPriority = 0)]
        public string post_image => Post.image;

        [FieldSettings("Low-resolution image", FieldTypeId = Connector.FieldTypeIdImage, SearchPriority = 0)]
        public string post_low_resolution_image => Post.low_resolution_image;

        [FieldSettings("Video", FieldTypeId = Connector.FieldTypeIdUrl, SearchPriority = 0)]
        public string post_video => Post.video;

        [FieldSettings("Low-resolution video", FieldTypeId = Connector.FieldTypeIdUrl, SearchPriority = 0)]
        public string post_low_resolution_video => Post.low_resolution_video;

        [FieldSettings("Height", FieldTypeId = Connector.FieldTypeIdInteger, SearchPriority = 0)]
        public int post_height => Post.height;

        [FieldSettings("Width", FieldTypeId = Connector.FieldTypeIdInteger, SearchPriority = 0)]
        public int post_width => Post.width;

        #endregion

    }
}
