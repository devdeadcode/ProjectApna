using System;
using System.Collections.Generic;
using System.Linq;

namespace eOne.Common.Connectors.Insightly.Models
{
    public class InsightlyProject : ConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Name", DefaultField = true, Description = true)]
        public string PROJECT_NAME { get; set; }

        [FieldSettings("Details", DefaultField = true)]
        public string PROJECT_DETAILS { get; set; }

        [FieldSettings("Status", DefaultField = true)]
        public string STATUS { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Project ID", KeyNumber = 1, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int PROJECT_ID { get; set; }

        [FieldSettings("Image", FieldTypeId = Connector.FieldTypeIdImage)]
        public string IMAGE_URL { get; set; }

        [FieldSettings("Project field 1")]
        public string PROJECT_FIELD_1 { get; set; }

        [FieldSettings("Project field 2")]
        public string PROJECT_FIELD_2 { get; set; }

        [FieldSettings("Project field 3")]
        public string PROJECT_FIELD_3 { get; set; }

        [FieldSettings("Project field 4")]
        public string PROJECT_FIELD_4 { get; set; }

        [FieldSettings("Project field 5")]
        public string PROJECT_FIELD_5 { get; set; }

        [FieldSettings("Project field 6")]
        public string PROJECT_FIELD_6 { get; set; }

        [FieldSettings("Project field 7")]
        public string PROJECT_FIELD_7 { get; set; }

        [FieldSettings("Project field 8")]
        public string PROJECT_FIELD_8 { get; set; }

        [FieldSettings("Project field 9")]
        public string PROJECT_FIELD_9 { get; set; }

        [FieldSettings("Project field 10")]
        public string PROJECT_FIELD_10 { get; set; }

        [FieldSettings("Date started", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? STARTED_DATE { get; set; }

        [FieldSettings("Date completed", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? COMPLETED_DATE { get; set; }

        [FieldSettings("Date created", Created = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? DATE_CREATED_UTC { get; set; }

        [FieldSettings("Date updated", Modified = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? DATE_UPDATED_UTC { get; set; }

        #endregion

        #region Hidden properties

        public int? OPPORTUNITY_ID { get; set; }
        public int? RESPONSIBLE_USER_ID { get; set; }
        public int? OWNER_USER_ID { get; set; }
        public int? CATEGORY_ID { get; set; }
        public int? PIPELINE_ID { get; set; }
        public int? STAGE_ID { get; set; }
        public string VISIBLE_TO { get; set; }
        public int? VISIBLE_TEAM_ID { get; set; }
        public string VISIBLE_USER_IDS { get; set; }
        public List<InsightlyTag> TAGS { get; set; }
        public List<InsightlyLink> LINKS { get; set; }
        public List<InsightlyEmailLink> EMAILLINKS { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Tags")]
        public string TagList => CommaSeparatedValues(TAGS.Select(tag => tag.TAG_NAME).ToList());

        [FieldSettings("Completed", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool Completed => COMPLETED_DATE != null;

        #endregion

    }
}