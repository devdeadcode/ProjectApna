using System;
using System.Collections.Generic;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Insightly.Models
{
    public class InsightlyProject : DataConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Name", DefaultField = true)]
        public string PROJECT_NAME { get; set; }

        [FieldSettings("Details", DefaultField = true)]
        public string PROJECT_DETAILS { get; set; }

        [FieldSettings("Status", DefaultField = true)]
        public string STATUS { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Project Id", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int PROJECT_ID { get; set; }

        [FieldSettings("Image", FieldTypeId = DataConnector.FieldTypeIdImage)]
        public string IMAGE_URL { get; set; }

        [FieldSettings("Project Field 1")]
        public string PROJECT_FIELD_1 { get; set; }

        [FieldSettings("Project Field 2")]
        public string PROJECT_FIELD_2 { get; set; }

        [FieldSettings("Project Field 3")]
        public string PROJECT_FIELD_3 { get; set; }

        [FieldSettings("Project Field 4")]
        public string PROJECT_FIELD_4 { get; set; }

        [FieldSettings("Project Field 5")]
        public string PROJECT_FIELD_5 { get; set; }

        [FieldSettings("Project Field 6")]
        public string PROJECT_FIELD_6 { get; set; }

        [FieldSettings("Project Field 7")]
        public string PROJECT_FIELD_7 { get; set; }

        [FieldSettings("Project Field 8")]
        public string PROJECT_FIELD_8 { get; set; }

        [FieldSettings("Project Field 9")]
        public string PROJECT_FIELD_9 { get; set; }

        [FieldSettings("Project Field 10")]
        public string PROJECT_FIELD_10 { get; set; }

        #endregion

        #region Hidden properties

        public int? OPPORTUNITY_ID { get; set; }
        public DateTime? STARTED_DATE { get; set; }
        public DateTime? COMPLETED_DATE { get; set; }
        public int? RESPONSIBLE_USER_ID { get; set; }
        public int? OWNER_USER_ID { get; set; }
        public DateTime? DATE_CREATED_UTC { get; set; }
        public DateTime? DATE_UPDATED_UTC { get; set; }
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

        #region Private dummy properties

        private string TagListDummy { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Tags")]
        public string TagList
        {
            get
            {
                return CommaSeparatedValues(TAGS.Select(tag => tag.TAG_NAME).ToList());
            }
            set
            {
                TagListDummy = value;
            }
        }

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

        [FieldSettings("Date started", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime StartedDate
        {
            get
            {
                return STARTED_DATE ?? DateTime.MinValue;
            }
            set
            {
                STARTED_DATE = value;
            }
        }

        [FieldSettings("Date completed", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime CompletedDate
        {
            get
            {
                return COMPLETED_DATE ?? DateTime.MinValue;
            }
            set
            {
                COMPLETED_DATE = value;
            }
        }

        #endregion

    }
}
