using System;
using System.Collections.Generic;
using System.Linq;

namespace eOne.Common.Connectors.Facebook.Models
{
    public class FacebookPost
    {

        public enum FacebookPostStatusType
        {
            mobile_status_update,
            created_note,
            added_photos,
            added_video,
            shared_story,
            created_group,
            created_event,
            wall_post,
            app_created_story,
            published_story,
            tagged_in_photo,
            approved_friend
        }
        public enum FacebookPostType
        {
            link,
            status,
            photo,
            video,
            offer
        }

        public string message { get; set; }
        public DateTime created_time { get; set; }
        public DateTime updated_time { get; set; }
        public string id { get; set; }
        public bool is_hidden { get; set; }
        public bool is_published { get; set; }
        public FacebookPostShares shares { get; set; }
        public FacebookPostStatusType status_type { get; set; }
        public FacebookPostType type { get; set; }
        public FacebookAdmin admin_creator { get; set; }
        public string caption { get; set; }
        public string description { get; set; }
        public List<FacebookPostProperty> properties { get; set; }
        public string from { get; set; }
        public string icon { get; set; }
        public string link { get; set; }
        public string picture { get; set; }
        public FacebookPostPrivacy privacy { get; set; } 
        public string source { get; set; }
        public string story { get; set; }
        public FacebookLikes likes { get; set; }
        public FacebookComments comments { get; set; }

        public int number_of_shares => shares?.count ?? 0;
        public string created_by => admin_creator == null ? string.Empty : admin_creator.name;
        public string property_length => GetPropertyText("Length");
        public string privacy_description => privacy.description;
        public FacebookPostPrivacy.FacebookPostPrivacyType privacy_type => privacy.value;
        public int number_of_likes
        {
            get
            {
                if (likes == null) return 0;
                return likes.summary?.total_count ?? likes.data.Count;
            }
        }
        public int number_of_comments
        {
            get
            {
                if (comments == null) return 0;
                return comments.summary?.total_count ?? comments.data.Count;
            }
        }

        private string GetPropertyText(string propertyName)
        {
            if (properties == null) return string.Empty;
            foreach (var property in properties.Where(property => property.name == propertyName)) return property.text;
            return string.Empty;
        }
        private string GetPropertyLink(string propertyName)
        {
            if (properties == null) return string.Empty;
            foreach (var property in properties.Where(property => property.name == propertyName)) return property.href;
            return string.Empty;
        }

    }
}