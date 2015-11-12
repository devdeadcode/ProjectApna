using System.Collections.Generic;

namespace eOne.Common.Connectors.Twitter.Models
{
    public class TwitterEntities : ConnectorEntityModel
    {

        public TwitterUrlList url { get; set; }
        public TwitterUrlList description { get; set; }
        public List<TwitterHashtag> hashtags { get; set; }
        public List<TwitterUserMention> user_mentions { get; set; }

    }
}
