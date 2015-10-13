using System;
using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Insightly.Models
{
    public class InsightlyTask : DataConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Title", DefaultField = true)]
        public string Title { get; set; }

        [FieldSettings("Details", DefaultField = true)]
        public string DETAILS { get; set; }

        [FieldSettings("Priority", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int Priority => PRIORITY ?? 0;

        [FieldSettings("Status", DefaultField = true)]
        public string STATUS { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Task Id", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int TASK_ID { get; set; }

        [FieldSettings("Publicly visible", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool PUBLICLY_VISIBLE { get; set; }

        [FieldSettings("Completed", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool COMPLETED { get; set; }

        [FieldSettings("Percent complete", FieldTypeId = DataConnector.FieldTypeIdPercentage)]
        public decimal PERCENT_COMPLETE { get; set; }

        #endregion

        #region Hidden properties

        public int? PRIORITY { get; set; }
        public int? CATEGORY_ID { get; set; }
        public DateTime? DUE_DATE { get; set; }
        public DateTime? COMPLETED_DATE_UTC { get; set; }
        public int? PROJECT_ID { get; set; }
        public DateTime? START_DATE { get; set; }
        public int? ASSIGNED_BY_USER_ID { get; set; }
        public int? PARENT_TASK_ID { get; set; }
        public int? RESPONSIBLE_USER_ID { get; set; }
        public int? OWNER_USER_ID { get; set; }
        public DateTime? DATE_CREATED_UTC { get; set; }
        public DateTime? DATE_UPDATED_UTC { get; set; }
        public List<InsightlyTaskLink> TASKLINKS { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Date created", Created = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime DateCreated
        {
            get
            {
                return DATE_CREATED_UTC ?? DateTime.MinValue;
            }
            set
            {
                DATE_CREATED_UTC = value;
            }
        }

        [FieldSettings("Date updated", Modified = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime DateUpdated
        {
            get
            {
                return DATE_UPDATED_UTC ?? DateTime.MinValue;
            }
            set
            {
                DATE_UPDATED_UTC = value;
            }
        }

        [FieldSettings("Due date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime DueDate
        {
            get
            {
                return DUE_DATE ?? DateTime.MinValue;
            }
            set
            {
                DUE_DATE = value;
            }
        }

        [FieldSettings("Completed date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime CompletedDate
        {
            get
            {
                return COMPLETED_DATE_UTC ?? DateTime.MinValue;
            }
            set
            {
                COMPLETED_DATE_UTC = value;
            }
        }

        [FieldSettings("Start date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime StartDate
        {
            get
            {
                return START_DATE ?? DateTime.MinValue;
            }
            set
            {
                START_DATE = value;
            }
        }

        #endregion

    }
}
