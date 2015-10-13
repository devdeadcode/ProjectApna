using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Twitter.Models
{
    public class TwitterUserMention : DataConnectorEntityModel
    {

        public string name { get; set; }
        public string id_str { get; set; }
        public int id { get; set; }
        public List<int> indices { get; set; }
        public string screen_name { get; set; }

    }
}
