using System.Collections.Generic;

namespace eOne.Common.Connectors.Facebook.Models
{
    public class FacebookComments : FacebookCore
    {

        public List<FacebookComment> data { get; set; }
        public FacebookCommentSummary summary { get; set; }

    }
}
