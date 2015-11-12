using System;

namespace eOne.Common.Connectors.Insightly.Models
{
    public class InsightlyDate : ConnectorEntityModel
    {

        public int? DATE_ID { get; set; }
        public string OCCASION_NAME { get; set; }
        public DateTime OCCASION_DATE { get; set; }
        public bool REPEAT_YEARLY { get; set; }
        public bool CREATE_TASK_YEARLY { get; set; }

    }
}
