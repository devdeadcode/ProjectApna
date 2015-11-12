using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace eOne.Common.Connectors.Insightly.Models
{
    public class InsightlyOpportunity : ConnectorEntityModel
    {

        #region Enums

        public enum InsightlyOpportunityState
        {
            [Description("Open")]
            OPEN,
            [Description("Won")]
            WON,
            [Description("Abandoned")]
            ABANDONED,
            [Description("Suspended")]
            SUSPENDED,
            [Description("Lost")]
            LOST
        }

        #endregion

        #region Default properties

        [FieldSettings("Name", DefaultField = true, Description = true)]
        public string OPPORTUNITY_NAME { get; set; }

        [FieldSettings("Details", DefaultField = true)]
        public string OPPORTUNITY_DETAILS { get; set; }

        [FieldSettings("Probability", DefaultField = true)]
        public string PROBABILITY { get; set; }

        [FieldSettings("Status", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(InsightlyOpportunityState), DefaultField = true)]
        public InsightlyOpportunityState? OPPORTUNITY_STATE { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Opportunity ID", KeyNumber = 1, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int OPPORTUNITY_ID { get; set; }

        [FieldSettings("Bid currency")]
        public string BID_CURRENCY { get; set; }

        [FieldSettings("Bid type")]
        public string BID_TYPE { get; set; }

        [FieldSettings("Bid duration", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int? BID_DURATION { get; set; }

        [FieldSettings("Image", FieldTypeId = Connector.FieldTypeIdImage)]
        public string IMAGE_URL { get; set; }

        [FieldSettings("Opportunity field 1")]
        public string OPPORTUNITY_FIELD_1 { get; set; }

        [FieldSettings("Opportunity field 2")]
        public string OPPORTUNITY_FIELD_2 { get; set; }

        [FieldSettings("Opportunity field 3")]
        public string OPPORTUNITY_FIELD_3 { get; set; }

        [FieldSettings("Opportunity field 4")]
        public string OPPORTUNITY_FIELD_4 { get; set; }

        [FieldSettings("Opportunity field 5")]
        public string OPPORTUNITY_FIELD_5 { get; set; }

        [FieldSettings("Opportunity field 6")]
        public string OPPORTUNITY_FIELD_6 { get; set; }

        [FieldSettings("Opportunity field 7")]
        public string OPPORTUNITY_FIELD_7 { get; set; }

        [FieldSettings("Opportunity field 8")]
        public string OPPORTUNITY_FIELD_8 { get; set; }

        [FieldSettings("Opportunity field 9")]
        public string OPPORTUNITY_FIELD_9 { get; set; }

        [FieldSettings("Opportunity field 10")]
        public string OPPORTUNITY_FIELD_10 { get; set; }

        [FieldSettings("Forecast close date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? FORECAST_CLOSE_DATE { get; set; }

        [FieldSettings("Date created", Created = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? DATE_CREATED_UTC { get; set; }

        [FieldSettings("Date updated", Modified = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? DATE_UPDATED_UTC { get; set; }

        #endregion

        #region Hidden properties

        public string BID_AMOUNT { get; set; }
        public List<InsightlyTag> TAGS { get; set; }
        public List<InsightlyLink> LINKS { get; set; }
        public List<InsightlyEmailLink> EMAILLINKS { get; set; }
        public InsightlyOpportunityCategory Category { get; set; }
        public InsightlyPipeline Pipeline { get; set; }
        public InsightlyPipelineStage PipelineStage { get; set; }
        public int? CATEGORY_ID { get; set; }
        public int? PIPELINE_ID { get; set; }
        public int? STAGE_ID { get; set; }
        public int? RESPONSIBLE_USER_ID { get; set; }
        public int? OWNER_USER_ID { get; set; }
        public string VISIBLE_TO { get; set; }
        public int? VISIBLE_TEAM_ID { get; set; }
        public string VISIBLE_USER_IDS { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Category name")]
        public string CategoryName => (Category == null) ? string.Empty : Category.CATEGORY_NAME;

        [FieldSettings("Category active", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool CategoryActive => (Category != null) && Category.ACTIVE;
        
        [FieldSettings("Pipeline name")]
        public string PipelineName => (Pipeline == null) ? string.Empty : Pipeline.PIPELINE_NAME;

        [FieldSettings("Pipeline stage name")]
        public string PipelineStageName => (PipelineStage == null) ? string.Empty : PipelineStage.STAGE_NAME;

        [FieldSettings("Pipeline stage order", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int PipelineStageOrder => (PipelineStage == null) ? 0 : PipelineStage.STAGE_ORDER ?? 0;

        [FieldSettings("Tags")]
        public string TagList => string.Join(", ", TAGS.Select(tag => tag.TAG_NAME).ToList());

        [FieldSettings("Bid amount", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal BidAmount
        {
            get
            {
                decimal amount;
                return decimal.TryParse(BID_AMOUNT, out amount) ? amount : 0;
            }
        }

        #endregion

    }
}
