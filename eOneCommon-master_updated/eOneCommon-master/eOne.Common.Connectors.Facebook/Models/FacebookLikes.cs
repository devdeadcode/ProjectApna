using System.Collections.Generic;

namespace eOne.Common.Connectors.Facebook.Models
{
    public class FacebookLikes : FacebookCore
    {

        public List<FacebookLike> data { get; set; }
        public FacebookLikeSummary summary { get; set; }

    }
}
