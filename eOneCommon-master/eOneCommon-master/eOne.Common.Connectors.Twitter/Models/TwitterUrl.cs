using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Twitter.Models
{
    public class TwitterUrl : DataConnectorEntityModel
    {

        public string expanded_url { get; set; }
        public string url { get; set; }
        public List<int> indices { get; set; }

    }
}
