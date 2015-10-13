using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Twitter.Models
{
    public class TwitterHashtag : DataConnectorEntityModel
    {

        public string text { get; set; }
        public List<int> indices { get; set; }

    }
}
