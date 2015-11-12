using System.Collections.Generic;

namespace eOne.Common.Connectors.Twitter.Models
{
    public class TwitterHashtag : ConnectorEntityModel
    {

        public string text { get; set; }
        public List<int> indices { get; set; }

    }
}
