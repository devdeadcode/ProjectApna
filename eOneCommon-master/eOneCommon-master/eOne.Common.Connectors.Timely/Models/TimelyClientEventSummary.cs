using System;
using System.Collections.Generic;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Timely.Models
{
    public class TimelyClientEventSummary : DataConnectorEntityModel
    {

        public TimelyClientEventSummary()
        {
            events = new List<TimelyEvent>();
        }

        #region Default properties

        [FieldSettings("Client name", DefaultField = true)]
        public string client_name => client.name;

        [FieldSettings("Total cost", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal total_cost
        {
            get
            {
                return events.Sum(user => user.cost);
            }
        }

        [FieldSettings("Total duration", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public string total_duration
        {
            get
            {
                var timeSpan = TimeSpan.FromSeconds(events.Sum(user => user.duration.seconds));
                return timeSpan.ToString(@"hh\:mm\:ss");
            }
        }

        #endregion

        #region Calculations

        [FieldSettings("Total estimated cost", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal total_estimated_cost
        {
            get
            {
                return events.Sum(user => user.estimated_cost);
            }
        }

        [FieldSettings("Total number of days", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal total_days_count
        {
            get
            {
                return events.Where(user => user.days_count != null).Sum(user => (decimal) user.days_count);
            }
        }

        [FieldSettings("Number of events", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int event_count => events.Count;

        [FieldSettings("Total estimated duration", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public string total_estimated_duration
        {
            get
            {
                var timeSpan = TimeSpan.FromSeconds(events.Sum(user => user.estimated_duration.seconds));
                return timeSpan.ToString(@"hh\:mm\:ss");
            }
        }

        [FieldSettings("Archived", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool client_archived => client.archived;

        #endregion

        #region Hidden parameters

        public List<TimelyEvent> events { get; set; }
        public TimelyClient client { get; set; }

        #endregion

        public int client_id => client.id;

    }
}
