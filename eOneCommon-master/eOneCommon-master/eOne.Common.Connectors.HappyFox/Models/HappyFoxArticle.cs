using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.HappyFox.Models
{
    class HappyFoxArticle : DataConnectorEntityModel
    {
        public List<string> attachemtns { get; set; }

        public int views { get; set; }

        public string last_updated_at { get; set; }

        public string last_updated_date => last_updated_at?.Substring(0, 10) ?? string.Empty;

        public string last_updated_time => last_updated_at?.Substring(11) ?? string.Empty;

        public string section_name { get; set; }

        public List<string> related_articles { get; set; }

        public int id { get; set; }

        public string contents { get; set; }
    }
}
