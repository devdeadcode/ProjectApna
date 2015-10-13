using System;

namespace eOne.Common.Connectors.BaseCamp.Models
{
    public class BaseCampDocument
    {

        public int id { get; set; }
        public string title { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string url { get; set; }
        public string app_url { get; set; }
        public bool @private { get; set; }
        public bool trashed { get; set; }
        public BaseCampBucket bucket { get; set; }

        public BaseCampBucket.BaseCampBucketType bucket_type => bucket.type;

        public int bucket_id => bucket.id;
        public string bucket_name => bucket.name;
        public string bucket_color => bucket.color;

        public string bucket_url => bucket.url;
        public string bucket_app_url => bucket.app_url;
    }
}
