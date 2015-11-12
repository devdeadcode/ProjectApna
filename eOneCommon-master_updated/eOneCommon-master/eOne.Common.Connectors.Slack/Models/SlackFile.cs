using System.Collections.Generic;

namespace eOne.Common.Connectors.Slack.Models
{
    public class SlackFile : ConnectorEntityModel
    {

        [FieldSettings("Title", DefaultField = true, Description = true)]
        public string title { get; set; }

        [FieldSettings("Filename", DefaultField = true)]
        public string name { get; set; }

        [FieldSettings("File ID", KeyNumber = 1)]
        public string id { get; set; }

        [FieldSettings("MIME type")]
        public string mimetype { get; set; }

        [FieldSettings("Extension")]
        public string filetype { get; set; }

        [FieldSettings("Type")]
        public string pretty_type { get; set; }

        public string user { get; set; }
        public bool editable { get; set; }
        public int size { get; set; }
        public string mode { get; set; }
        public bool is_external { get; set; }
        public string external_type { get; set; }
        public bool is_public { get; set; }
        public bool public_url_shared { get; set; }
        public bool display_as_bot { get; set; }
        public string username { get; set; }
        public string url { get; set; }
        public string url_private { get; set; }
        public string permalink { get; set; }
        public int comments_count { get; set; }
        public int num_stars { get; set; }
        public bool is_starred { get; set; }

        public List<string> channels { get; set; }
        public List<string> groups { get; set; }
        public List<string> ims { get; set; }
        public List<string> pinned_to { get; set; }
        public long created { get; set; }
        public long timestamp { get; set; }

        public SlackUser user_object { get; set; }

    }
}