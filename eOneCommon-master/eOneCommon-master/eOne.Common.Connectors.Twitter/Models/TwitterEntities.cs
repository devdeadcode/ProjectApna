using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Twitter.Models
{
    public class TwitterEntities : DataConnectorEntityModel
    {

        public TwitterUrlList url { get; set; }
        public TwitterUrlList description { get; set; }
        public List<TwitterHashtag> hashtags { get; set; }
        public List<TwitterUserMention> user_mentions { get; set; }

    }
}
