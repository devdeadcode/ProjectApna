using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Twitter.Models
{
    public class TwitterUrlList : DataConnectorEntityModel
    {

        public List<TwitterUrl> urls { get; set; }

    }
}
