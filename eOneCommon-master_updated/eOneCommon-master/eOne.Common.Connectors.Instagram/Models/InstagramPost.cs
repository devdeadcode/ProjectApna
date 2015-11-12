using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace eOne.Common.Connectors.Instagram.Models
{
    public class InstagramPost : ConnectorEntityModel
    {

        #region Enums

        public enum InstagramPostType
        {
            [Description("Image")]
            image,
            [Description("Video")]
            video
        }

        #endregion

        #region Default properties

        [FieldSettings("Caption", DefaultField = true)]
        public string caption_text => caption == null ? string.Empty : caption.text;

        [FieldSettings("Type", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(InstagramPostType))]
        public InstagramPostType type { get; set; }

        [FieldSettings("Date", DefaultField = true, FieldTypeId = Connector.FieldTypeIdDate, Created = true)]
        public DateTime post_date => FromEpochSeconds(created_time);

        [FieldSettings("Time", DefaultField = true, FieldTypeId = Connector.FieldTypeIdTime, Created = true)]
        public DateTime post_time => post_date;

        [FieldSettings("Number of comments", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int number_of_comments => comments?.count ?? 0;

        [FieldSettings("Number of likes", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int number_of_likes => likes?.count ?? 0;

        #endregion

        #region Properties

        [FieldSettings("Link", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string link { get; set; }

        [FieldSettings("Filter")]
        public string filter { get; set; }

        [FieldSettings("ID", KeyNumber = 1)]
        public string id { get; set; }

        #endregion

        #region Hidden properties

        public InstagramCaption caption { get; set; }
        public InstagramUser user { get; set; }
        public List<InstagramUser> users_in_photo { get; set; }
        public List<string> tags { get; set; }
        public InstagramVideos videos { get; set; }
        public InstagramLocation location { get; set; }
        public InstagramCommentCollection comments { get; set; }
        public InstagramLikeCollection likes { get; set; }
        public int created_time { get; set; }
        public InstagramImages images { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Location name")]
        public string location_name => location == null ? string.Empty : location.name;

        [FieldSettings("Tags")]
        public string tag_list => CommaSeparatedValues(tags);

        [FieldSettings("Users in photo")]
        public string users_in_photo_list
        {
            get
            {
                return users_in_photo == null ? string.Empty : CommaSeparatedValues(users_in_photo.Select(user_in_photo => user_in_photo.full_name).ToList());
            }
        }

        [FieldSettings("Thumbnail image", FieldTypeId = Connector.FieldTypeIdImage)]
        public string thumbnail_image => images == null ? string.Empty : images.thumbnail.url;

        [FieldSettings("Image", FieldTypeId = Connector.FieldTypeIdImage)]
        public string image => images == null ? string.Empty : images.standard_resolution.url;

        [FieldSettings("Low-resolution image", FieldTypeId = Connector.FieldTypeIdImage)]
        public string low_resolution_image => images == null ? string.Empty : images.low_resolution.url;

        [FieldSettings("Video", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string video => videos == null ? string.Empty : videos.standard_resolution.url;

        [FieldSettings("Low-resolution video", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string low_resolution_video => videos == null ? string.Empty : videos.low_resolution.url;

        [FieldSettings("Height", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int height
        {
            get
            {
                switch (type)
                {
                    case InstagramPostType.image:
                        return images?.standard_resolution.height ?? 0;
                    case InstagramPostType.video:
                        return videos?.standard_resolution.height ?? 0;
                    default:
                        return 0;
                }
            }
        }

        [FieldSettings("Width", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int width
        {
            get
            {
                switch (type)
                {
                    case InstagramPostType.image:
                        return images?.standard_resolution.width ?? 0;
                    case InstagramPostType.video:
                        return videos?.standard_resolution.width ?? 0;
                    default:
                        return 0;
                }
            }
        }

        #endregion

    }
}