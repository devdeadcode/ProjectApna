using System;
using System.ComponentModel;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Timely.Models
{
    public class TimelyEvent : DataConnectorEntityModel
    {

        #region Enums

        public enum TimelyEventTimerState
        {
            [Description("Default")]
            DEFAULT
        }
        public enum TimelyEventFrom
        {
            [Description("Web")]
            Web,
            [Description("iCal")]
            iCal
        }

        #endregion

        #region Default parameters

        [FieldSettings("Date", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime day { get; set; }

        [FieldSettings("Note", DefaultField = true)]
        public string note { get; set; }

        [FieldSettings("Cost", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal cost { get; set; }

        #endregion

        #region Parameters

        [FieldSettings("Estimated cost", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal estimated_cost { get; set; }

        [FieldSettings("Number of days", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? days_count { get; set; }

        [FieldSettings("Hourly rate", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal hour_rate { get; set; }

        [FieldSettings("Estimated", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool estimated { get; set; }

        [FieldSettings("Timer started on", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal timer_started_on { get; set; }

        [FieldSettings("Timer stopped on", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal timer_stopped_on { get; set; }

        [FieldSettings("Created from", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public TimelyEventFrom created_from { get; set; }

        [FieldSettings("Updated from", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public TimelyEventFrom updated_from { get; set; }

        [FieldSettings("Sequence", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int sequence { get; set; }

        [FieldSettings("Event ID", KeyNumber = 1, FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int id { get; set; }

        #endregion

        #region Hidden parameters

        public TimelyUser user { get; set; }
        public TimelyProject project { get; set; }
        public TimelyDuration duration { get; set; }
        public TimelyDuration estimated_duration { get; set; }
        public int source { get; set; }
        public int updated_at { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("User name", DefaultField = true)]
        public string user_name => user == null ? string.Empty : user.name;

        [FieldSettings("Project name", DefaultField = true)]
        public string project_name => project == null ? string.Empty : project.name;

        [FieldSettings("Client name")]
        public string client_name => project == null ? string.Empty : project.client_name;

        [FieldSettings("Duration")]
        public string duration_time => duration.formatted;

        [FieldSettings("Estimated duration")]
        public string estimated_duration_time => estimated_duration.formatted;

        [FieldSettings("Updated date", Modified = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime updated_at_date => DateTime.FromOADate(updated_at);

        public int project_id => project.id;
        public int user_id => user.id;
        public int client_id => project.client_id;

        #endregion

    }
}