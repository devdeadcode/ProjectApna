using System;
using System.Collections.Generic;

namespace eOne.Common.Connectors.BaseCamp.Models
{
    public class BaseCampMessage
    {

        public int id { get; set; }
        public string subject { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string content { get; set; }
        public bool @private { get; set; }
        public bool trashed { get; set; }
        public BaseCampPerson creator { get; set; }
        public List<BaseCampComment> comments { get; set; }
        public List<BaseCampPerson> subscribers { get; set; }

        public int creator_id => creator.id;

        public string creator_name => creator.name;
        public string creator_url => creator.url;

        public string creator_fullsize_avatar_url => creator.fullsize_avatar_url;

        public int number_of_comments => comments?.Count ?? 0;

        public bool has_comments => number_of_comments > 0;

        public int number_of_subscribers => subscribers?.Count ?? 0;
        public bool has_subscribers => number_of_subscribers > 0;
    }
}