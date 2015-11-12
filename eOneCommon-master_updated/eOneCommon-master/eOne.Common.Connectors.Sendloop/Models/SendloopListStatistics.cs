namespace eOne.Common.Connectors.Sendloop.Models
{
    public class SendloopListStatistics
    {

        public int OpenPerformance { get; set; }
        public int OpenAvg { get; set; }
        public int OpenPerformanceRate { get; set; }
        public int LinkPerformance { get; set; }
        public int LinkAvg { get; set; }
        public int LinkPerformanceRate { get; set; }
        public string HighestOpenRateDay { get; set; }
        public string HighestLinkRateDay { get; set; }
        public int TotalSPAMComplaints { get; set; }

    }
}
