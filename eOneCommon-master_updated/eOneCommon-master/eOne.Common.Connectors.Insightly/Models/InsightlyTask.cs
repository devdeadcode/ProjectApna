using System;
using System.Collections.Generic;

namespace eOne.Common.Connectors.Insightly.Models
{
    public class InsightlyTask : ConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Title", DefaultField = true, Description = true)]
        public string Title { get; set; }

        [FieldSettings("Details", DefaultField = true)]
        public string DETAILS { get; set; }

        [FieldSettings("Priority", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int Priority => PRIORITY ?? 0;

        [FieldSettings("Status", DefaultField = true)]
        public string STATUS { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Task ID", FieldTypeId = Connector.FieldTypeIdInteger, KeyNumber = 1)]
        public int TASK_ID { get; set; }

        [FieldSettings("Publicly visible", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool PUBLICLY_VISIBLE { get; set; }

        [FieldSettings("Completed", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool COMPLETED { get; set; }

        [FieldSettings("Percent complete", FieldTypeId = Connector.FieldTypeIdPercentage)]
        public decimal PERCENT_COMPLETE { get; set; }

        [FieldSettings("Due date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? DUE_DATE { get; set; }

        [FieldSettings("Completed date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? COMPLETED_DATE_UTC { get; set; }

        [FieldSettings("Start date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? START_DATE { get; set; }

        [FieldSettings("Date created", Created = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? DATE_CREATED_UTC { get; set; }

        [FieldSettings("Date updated", Modified = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? DATE_UPDATED_UTC { get; set; }

        #endregion

        #region Hidden properties

        public int? PRIORITY { get; set; }
        public int? CATEGORY_ID { get; set; }
        public int? PROJECT_ID { get; set; }
        public int? ASSIGNED_BY_USER_ID { get; set; }
        public int? PARENT_TASK_ID { get; set; }
        public int? RESPONSIBLE_USER_ID { get; set; }
        public int? OWNER_USER_ID { get; set; }
        public List<InsightlyTaskLink> TASKLINKS { get; set; }

        #endregion

    }
}
