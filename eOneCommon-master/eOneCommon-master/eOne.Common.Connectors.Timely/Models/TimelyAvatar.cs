using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Timely.Models
{
    public class TimelyAvatar : DataConnectorEntityModel
    {

        public string large_retina { get; set; }
        public string large { get; set; }
        public string medium_retina { get; set; }
        public string medium { get; set; }
        public string timeline { get; set; }

    }
}
