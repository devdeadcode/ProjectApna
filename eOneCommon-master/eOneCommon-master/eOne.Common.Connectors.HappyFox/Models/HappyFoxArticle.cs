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

        public string last_updated_date { get; set; }

        public string last_updated_time { get; set; }

        public List<string> related_articles { get; set; }

        public string name { get; set; }

        public int id { get; set; }

        public string contents { get; set; }
    }
}
