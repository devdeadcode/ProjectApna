using System.Collections.Generic;

namespace eOne.Common.Connectors.Twitter.Models
{
    public class TwitterCoordinates : ConnectorEntityModel
    {

        public List<double> coordinates { get; set; }
        // todo - create as enum
        public string type { get; set; }

    }
}
