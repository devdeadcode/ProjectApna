using System;
using System.ComponentModel;

namespace eOne.Common.Connectors.Emma.Models
{
    public class EmmaMember : ConnectorEntityModel
    {

        public enum EmmaMemberStatus
        {
            [Description("Active")]
            active,
            [Description("Opted out")]
            opt_out
        }

        public string email { get; set; }
        public EmmaMemberStatus status { get; set; }

        public bool? confirmed_opt_in { get; set; }
        public int account_id { get; set; }
        public int member_id { get; set; }
        public DateTime? last_modified_at { get; set; }
        public string member_status_id { get; set; }
        public string plaintext_preferred { get; set; }
        public string email_error { get; set; }
        public DateTime? member_since { get; set; }
        public int bounce_count { get; set; }
        public DateTime? deleted_at { get; set; }
        
        public EmmaMemberFields fields { get; set; }
        public bool deleted => deleted_at != null;

    }
}