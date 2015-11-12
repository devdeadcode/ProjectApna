namespace eOne.Common.Connectors.Sendloop.Models
{
    public class SendloopCampaignStatistics : ConnectorEntityModel
    {

        public string HighestOpenRateDay { get; set; }
        public string HighestLinkRateDay { get; set; }
        public int TotalRecipients { get; set; }
        public int TotalDelivered { get; set; }
        public decimal TotalDeliveredRatio { get; set; }
        public int UniqueOpens { get; set; }
        public decimal UniqueOpensRatio { get; set; }
        public int UniqueClicks { get; set; }
        public decimal UniqueClicksRatio { get; set; }
        public decimal LinkAvg { get; set; }
        public decimal OpenAvg { get; set; }
        public decimal OpenPerformanceRate { get; set; }
        public decimal LinkPerformanceRate { get; set; }
        public decimal DeliveredAvg { get; set; }
        public decimal DeliveredRate { get; set; }
        public int TotalSPAMComplaints { get; set; }

    }
}
