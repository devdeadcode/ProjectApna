using System;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Insightly.Models
{
    public class InsightlyDate : DataConnectorEntityModel
    {

        public int? DATE_ID { get; set; }
        public string OCCASION_NAME { get; set; }
        public DateTime OCCASION_DATE { get; set; }
        public bool REPEAT_YEARLY { get; set; }
        public bool CREATE_TASK_YEARLY { get; set; }

    }
}
