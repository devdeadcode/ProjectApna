namespace eOne.Common.Connectors.Timely.Models
{
    public class TimelyCost : ConnectorEntityModel
    {

        public TimelyDuration duration { get; set; }
        public double cost { get; set; }
        public string time => duration.formatted;
    }
}
