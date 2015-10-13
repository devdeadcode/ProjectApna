using System;
using System.Collections.Generic;

namespace eOne.Common.Connectors.BaseCamp.Models
{
    public class BaseCampComment
    {

        public int id { get; set; }
        public string content { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public bool @private { get; set; }
        public bool trashed { get; set; }
        public BaseCampPerson creator { get; set; }
        public List<BaseCampAttachment> attachments { get; set; }

        public int creator_id => creator.id;

        public string creator_name => creator.name;
        public string creator_url => creator.url;

        public string creator_fullsize_avatar_url => creator.fullsize_avatar_url;

        public int number_of_attachments => attachments?.Count ?? 0;
        public bool has_attachments => number_of_attachments > 0;
    }
}
