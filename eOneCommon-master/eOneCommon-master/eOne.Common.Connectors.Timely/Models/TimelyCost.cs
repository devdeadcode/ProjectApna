using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Timely.Models
{
    public class TimelyCost : DataConnectorEntityModel
    {

        public TimelyDuration duration { get; set; }
        public double cost { get; set; }
        public string time => duration.formatted;
    }
}
