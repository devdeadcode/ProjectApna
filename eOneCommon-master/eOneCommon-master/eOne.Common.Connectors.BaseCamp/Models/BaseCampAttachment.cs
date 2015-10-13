using System;
using System.Collections.Generic;

namespace eOne.Common.Connectors.BaseCamp.Models
{
    public class BaseCampAttachment
    {

        public int id { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public int byte_size { get; set; }
        public string content_type { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string url { get; set; }
        public string app_url { get; set; }
        public string thumbnail_url { get; set; }
        public bool @private { get; set; }
        public bool trashed { get; set; }
        public List<string> tags { get; set; }
        public BaseCampPerson creator { get; set; }
        public BaseCampAttachable attachable { get; set; }

        public int creator_id => creator.id;
        public string creator_name => creator.name;

        public string creator_url => creator.url;
        public string creator_fullsize_avatar_url => creator.fullsize_avatar_url;

        public int attachable_id => attachable.id;
        public BaseCampAttachable.BaseCampAttachableType attachable_type => attachable.type;
        public string attachable_url => attachable.url;
        public string attachable_app_url => attachable.app_url;
    }
}