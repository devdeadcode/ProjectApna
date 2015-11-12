using System;

namespace eOne.Common.Connectors.Facebook.Models
{
    public class FacebookComment
    {

        public string id { get; set; }
        public string message { get; set; }
        public DateTime created_time { get; set; }
        public FacebookCommentFrom from { get; set; }

        public string from_name => from.name;

    }
}