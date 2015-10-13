using System;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.BaseCamp.Models
{
    public class BaseCampToDoList : DataConnectorEntityModel
    {

        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string url { get; set; }
        public string app_url { get; set; }
        public bool completed { get; set; }
        public int position { get; set; }
        public bool @private { get; set; }
        public bool trashed { get; set; }
        public int completed_count { get; set; }
        public int remaining_count { get; set; }
        public BaseCampPerson creator { get; set; }

        public int creator_id => creator.id;

        public string creator_name => creator.name;

        public string creator_url => creator.url;
        public string creator_fullsize_avatar_url => creator.fullsize_avatar_url;

        public BaseCampBucket bucket { get; set; }

        public BaseCampBucket.BaseCampBucketType bucket_type => bucket.type;

        public int bucket_id => bucket.id;

        public string bucket_name => bucket.name;

        public string bucket_url => bucket.url;

        public string bucket_app_url => bucket.app_url;

        public DateTime created_at_date => created_at.Date;

        public DateTime created_at_time => Time(created_at);
        public DateTime updated_at_date => updated_at.Date;
        public DateTime updated_at_time => Time(updated_at);
    }
}