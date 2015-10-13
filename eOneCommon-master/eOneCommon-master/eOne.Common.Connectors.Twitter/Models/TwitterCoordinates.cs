using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Twitter.Models
{
    public class TwitterCoordinates : DataConnectorEntityModel
    {

        public List<double> coordinates { get; set; }
        // todo - create as enum
        public string type { get; set; }

    }
}
