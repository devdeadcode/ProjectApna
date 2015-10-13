using System;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Twitter.Models
{
    public class TwitterTweet : DataConnectorEntityModel
    {

        public enum TwitterTweetSource 
        {
            // todo - determine other sources
            web
        }

        public TwitterCoordinates coordinates { get; set; }
        public bool favorited { get; set; }
        public bool truncated { get; set; }
        public DateTime created_at { get; set; }
        public string id_str { get; set; }
        public TwitterUser user { get; set; }
        public TwitterEntities entities { get; set; }
        public string in_reply_to_screen_name { get; set; }
        public int in_reply_to_status_id { get; set; }
        public string in_reply_to_status_id_str { get; set; }
        public int in_reply_to_user_id { get; set; }
        public string text { get; set; }
        public bool retweeted { get; set; }
        public int retweet_count { get; set; }
        public int id { get; set; }
        public TwitterTweetSource source { get; set; }
    

    }
}

