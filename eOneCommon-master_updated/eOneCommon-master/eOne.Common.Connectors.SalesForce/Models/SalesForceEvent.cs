using System;

namespace eOne.Common.Connectors.SalesForce.Models
{
    public class SalesForceEvent : SalesForceActivity
    {

        #region Default properties

        [FieldSettings("Location", DefaultField = true)]
        public string Location { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Event ID", KeyNumber = 1)]
        public string Id { get; set; }

        [FieldSettings("Show as")]
        public string ShowAs { get; set; }

        [FieldSettings("Private", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool IsPrivate { get; set; }

        [FieldSettings("Multi-person event", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool IsGroupEvent { get; set; }

        [FieldSettings("All day event", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool IsAllDayEvent { get; set; }

        [FieldSettings("Duration in minutes", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int? DurationInMinutes { get; set; }

        [FieldSettings("Start date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? StartDateTime { get; set; }

        [FieldSettings("End date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? EndDateTime { get; set; }

        #endregion

        public bool IsChild { get; set; }
        public string GroupEventType { get; set; }

        #region Calculations

        [FieldSettings("Start time", FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime? StartTime => StartDateTime;

        [FieldSettings("End time", FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime? EndTime => EndDateTime;

        #endregion

    }
}


