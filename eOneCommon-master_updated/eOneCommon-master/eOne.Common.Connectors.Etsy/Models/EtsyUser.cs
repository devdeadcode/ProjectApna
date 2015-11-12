using System.Collections.Generic;

namespace eOne.Common.Connectors.Etsy.Models
{
    public class EtsyUser : ConnectorEntityModel
    {

        public int user_id { get; set; }
        public string login_name { get; set; }
        public string primary_email { get; set; }
        public float creation_tsz { get; set; }
        public string user_pub_key { get; set; }
        public int referred_by_user_id { get; set; }
        public int awaiting_feedback_count { get; set; }
        public List<EtsyFeedbackInfo> feedback_info { get; set; }

    }
}