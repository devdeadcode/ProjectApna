using System;

namespace eOne.Common.Connectors.Formstack.Models
{
    public class FormstackForm : ConnectorEntityModel
    {

        public int id { get; set; }
        public string name { get; set; }
        public int views { get; set; }
        public int submissions { get; set; }
        public int submissions_unread { get; set; }
        public int last_submission_id { get; set; }
        public string url { get; set; }
        public string data_url { get; set; }
        public string summary_url { get; set; }
        public string rss_url { get; set; }
        public string timezone { get; set; }

        public DateTime created { get; set; }
        public DateTime updated { get; set; }
        public DateTime last_submission_time { get; set; }
        
        public DateTime created_time => created;

        public DateTime updated_time => updated;
        public DateTime last_submission_date => last_submission_time;
    }
}