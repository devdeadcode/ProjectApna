using System;
using System.Collections.Generic;
using System.Linq;

namespace eOne.Common.Connectors.Timely.Models
{
    public class TimelyUserEventSummary : ConnectorEntityModel
    {

        public TimelyUserEventSummary()
        {
            events = new List<TimelyEvent>();
        }

        #region Default properties

        [FieldSettings("User name", DefaultField = true)]
        public string user_name => user.name;

        [FieldSettings("Total cost", DefaultField = true, FieldTypeId = Connector.FieldTypeIdQuantity)]
        public decimal total_cost
        {
            get
            {
                return events.Sum(item => item.cost);
            }
        }

        [FieldSettings("Total duration", DefaultField = true, FieldTypeId = Connector.FieldTypeIdQuantity)]
        public string total_duration
        {
            get
            {
                var time_span = TimeSpan.FromSeconds(events.Sum(item => item.duration.seconds));
                return time_span.ToString(@"hh\:mm\:ss");
            }
        }

        #endregion

        #region Properties

        [FieldSettings("Total estimated cost", FieldTypeId = Connector.FieldTypeIdQuantity)]
        public decimal total_estimated_cost
        {
            get
            {
                return events.Sum(item => item.estimated_cost);
            }
        }

        [FieldSettings("Total number of days", FieldTypeId = Connector.FieldTypeIdQuantity)]
        public decimal total_days_count
        {
            get
            {
                return events.Where(item => item.days_count != null).Sum(item => (decimal)item.days_count);
            }
        }

        [FieldSettings("Number of events", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int event_count => events.Count;

        [FieldSettings("Total estimated duration", FieldTypeId = Connector.FieldTypeIdQuantity)]
        public string total_estimated_duration
        {
            get
            {
                var time_span = TimeSpan.FromSeconds(events.Sum(item => item.estimated_duration.seconds));
                return time_span.ToString(@"hh\:mm\:ss");
            }
        }

        [FieldSettings("Email", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEmail)]
        public string user_email => user.email;

        [FieldSettings("User level", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(TimelyUser.TimelyUserLevel))]
        public TimelyUser.TimelyUserLevel user_level => user.user_level;

        [FieldSettings("Active", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool user_active => user.active;

        [FieldSettings("Deleted", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool user_deleted => user.deleted;

        #endregion

        #region Hidden parameters

        public List<TimelyEvent> events { get; set; }
        public TimelyUser user { get; set; }

        #endregion

    }
}
