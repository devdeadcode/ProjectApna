using System;

namespace eOne.Common.Connectors.BaseCamp.Models
{
    public class BaseCampAccess
    {

        public int id { get; set; }
        public int identity_id { get; set; }
        public string name { get; set; }
        public string email_address { get; set; }
        public bool admin { get; set; }
        public bool is_client { get; set; }
        public bool trashed { get; set; }
        public string avatar_url { get; set; }
        public string fullsize_avatar_url { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string url { get; set; }
        public string app_url { get; set; }

    }
}