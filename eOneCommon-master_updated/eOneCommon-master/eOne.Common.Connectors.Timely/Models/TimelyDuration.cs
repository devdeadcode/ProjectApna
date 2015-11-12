namespace eOne.Common.Connectors.Timely.Models
{
    public class TimelyDuration : ConnectorEntityModel
    {

        public decimal total_minutes { get; set; }
        public decimal total_hours { get; set; }
        public int total_seconds { get; set; }
        public decimal minutes { get; set; }
        public decimal hours { get; set; }
        public int seconds { get; set; }
        public string formatted { get; set; }

    }
}
