using System;

namespace eOne.Common.Connectors.Instagram.Models
{
    public class InstagramLikeList : ConnectorEntityModel
    {

        public InstagramLikeList(InstagramUser user, InstagramPost post)
        {
            User = user;
            Post = post;
        }

        #region Default properties

        [FieldSettings("Post caption", DefaultField = true)]
        public string post_caption => Post.caption_text;

        [FieldSettings("Username", DefaultField = true)]
        public string comment_username => User.username;

        [FieldSettings("Name", DefaultField = true)]
        public string comment_full_name => User.full_name;

        #endregion

        #region Hidden properties

        public InstagramUser User { get; set; }
        public InstagramPost Post { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Post type", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(InstagramPost.InstagramPostType))]
        public InstagramPost.InstagramPostType post_type => Post.type;

        [FieldSettings("Post date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime post_date => Post.post_date;

        [FieldSettings("Post time", FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime post_time => Post.post_time;

        [FieldSettings("Post link", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string post_link => Post.link;

        [FieldSettings("Filter")]
        public string post_filter => Post.filter;

        [FieldSettings("Post ID", KeyNumber = 1)]
        public string post_id => Post.id;

        [FieldSettings("Post location name")]
        public string post_location_name => Post.location_name;

        [FieldSettings("Post tags")]
        public string post_tag_list => Post.tag_list;

        [FieldSettings("Users in photo")]
        public string post_users_in_photo_list => Post.users_in_photo_list;

        [FieldSettings("Thumbnail image", FieldTypeId = Connector.FieldTypeIdImage)]
        public string post_thumbnail_image => Post.thumbnail_image;

        [FieldSettings("Image", FieldTypeId = Connector.FieldTypeIdImage)]
        public string post_image => Post.image;

        [FieldSettings("Low-resolution image", FieldTypeId = Connector.FieldTypeIdImage)]
        public string post_low_resolution_image => Post.low_resolution_image;

        [FieldSettings("Video", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string post_video => Post.video;

        [FieldSettings("Low-resolution video", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string post_low_resolution_video => Post.low_resolution_video;

        [FieldSettings("Height", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int post_height => Post.height;

        [FieldSettings("Width", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int post_width => Post.width;

        [FieldSettings("User ID", KeyNumber = 2)]
        public string user_id => User.id;

        #endregion

    }
}
