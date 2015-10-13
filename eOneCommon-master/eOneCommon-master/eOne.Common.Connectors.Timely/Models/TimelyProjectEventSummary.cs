using System;
using System.Collections.Generic;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Timely.Models
{
    public class TimelyProjectEventSummary : DataConnectorEntityModel
    {

        public TimelyProjectEventSummary()
        {
            events = new List<TimelyEvent>();
        }

        #region Default properties

        [FieldSettings("Project name", DefaultField = true)]
        public string project_name => project.name;

        [FieldSettings("Client name", DefaultField = true)]
        public string client_name => project.client_name;

        [FieldSettings("Total cost", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal total_cost
        {
            get
            {
                return events.Sum(item => item.cost);
            }
        }

        [FieldSettings("Total duration", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdQuantity)]
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

        [FieldSettings("Total estimated cost", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal total_estimated_cost
        {
            get
            {
                return events.Sum(item => item.estimated_cost);
            }
        }

        [FieldSettings("Total number of days", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal total_days_count
        {
            get
            {
                return events.Where(item => item.days_count != null).Sum(item => (decimal)item.days_count);
            }
        }

        [FieldSettings("Number of events", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int event_count => events.Count;

        [FieldSettings("Total estimated duration", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public string total_estimated_duration
        {
            get
            {
                var time_span = TimeSpan.FromSeconds(events.Sum(item => item.estimated_duration.seconds));
                return time_span.ToString(@"hh\:mm\:ss");
            }
        }

        [FieldSettings("Project budget", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal project_budget => project.budget ?? 0;

        [FieldSettings("Project budget type", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public TimelyProject.TimelyProjectBudgetType? project_budget_type => project.budget_type;

        [FieldSettings("Budget variance", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal budget_variance
        {
            get
            {
                if (project_budget_type != null)
                {
                    switch (project_budget_type)
                    {
                        case TimelyProject.TimelyProjectBudgetType.H:
                            var totalHours = events.Sum(item => item.duration.hours);
                            return project_budget - totalHours;
                        case TimelyProject.TimelyProjectBudgetType.M:
                            return project_budget - total_cost;
                    }
                }
                return 0;
            }
        }

        [FieldSettings("Project link", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string project_url => $"https://timelyapp.com/{account_id}/projects/{project_id}";

        #endregion

        #region Hidden parameters

        public List<TimelyEvent> events { get; set; }
        public TimelyProject project { get; set; }

        #endregion

        public int project_id => project.id;
        public int account_id { get; set; }

    }
}
