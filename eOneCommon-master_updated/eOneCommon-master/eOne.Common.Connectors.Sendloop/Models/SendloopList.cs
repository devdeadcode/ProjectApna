using System;

namespace eOne.Common.Connectors.Sendloop.Models
{
    public class SendloopList : ConnectorEntityModel
    {

        [FieldSettings("Name", DefaultField = true, Description = true)]
        public string Name { get; set; }

        [FieldSettings("Active subscribers", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int ActiveSubscribers { get; set; }

        [FieldSettings("List ID", FieldTypeId = Connector.FieldTypeIdInteger, KeyNumber = 1)]
        public int ListID { get; set; }

        [FieldSettings("Created date", FieldTypeId = Connector.FieldTypeIdDate, Created = true)]
        public DateTime CreatedOn { get; set; }

        public SendloopListStatistics Statistics { get; set; }

        [FieldSettings("Open performance", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int StatisticsOpenPerformance => Statistics.OpenPerformance;

        [FieldSettings("Open average", FieldTypeId = Connector.FieldTypeIdInteger)]
        public decimal StatisticsOpenAvg => Statistics.OpenAvg;

        [FieldSettings("Open performance rate", FieldTypeId = Connector.FieldTypeIdInteger)]
        public decimal StatisticsOpenPerformanceRate => Statistics.OpenPerformanceRate;

        [FieldSettings("Link performance", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int StatisticsLinkPerformance => Statistics.LinkPerformance;

        [FieldSettings("Link average", FieldTypeId = Connector.FieldTypeIdInteger)]
        public decimal StatisticsLinkAvg => Statistics.LinkAvg;

        [FieldSettings("Link performance rate", FieldTypeId = Connector.FieldTypeIdInteger)]
        public decimal StatisticsLinkPerformanceRate => Statistics.LinkPerformanceRate;

        [FieldSettings("Highest open rate day")]
        public string StatisticsHighestOpenRateDay => Statistics.HighestOpenRateDay;

        [FieldSettings("Highest link rate day")]
        public string StatisticsHighestLinkRateDay => Statistics.HighestLinkRateDay;

        [FieldSettings("Total SPAM complaints", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int StatisticsTotalSPAMComplaints => Statistics.TotalSPAMComplaints;

    }
}