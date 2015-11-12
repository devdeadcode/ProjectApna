using System;
using System.Collections.Generic;

namespace eOne.Common.Connectors.Insightly.Models
{
    public class InsightlyEvent : ConnectorEntityModel
    {

        public int? EVENT_ID { get; set; }
        public string TITLE { get; set; }
        public string LOCATION { get; set; }
        public string DETAILS { get; set; }
        public DateTime START_DATE_UTC { get; set; }
        public DateTime END_DATE_UTC { get; set; }
        public bool ALL_DAY { get; set; }
        public bool PUBLICLY_VISIBLE { get; set; }
        public DateTime REMINDER_DATE_UTC { get; set; }
        public bool REMINDER_SENT { get; set; }
        public List<InsightlyEventLink> EVENTLINKS { get; set; }
        public int? OWNER_USER_ID { get; set; }
        public DateTime DATE_CREATED_UTC { get; set; }
        public DateTime DATE_UPDATED_UTC { get; set; }

    }
}
