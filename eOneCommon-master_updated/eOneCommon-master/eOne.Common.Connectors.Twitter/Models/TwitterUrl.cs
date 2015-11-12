using System.Collections.Generic;

namespace eOne.Common.Connectors.Twitter.Models
{
    public class TwitterUrl : ConnectorEntityModel
    {

        public string expanded_url { get; set; }
        public string url { get; set; }
        public List<int> indices { get; set; }

    }
}
