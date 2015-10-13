using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Insightly.Models
{
    public class InsightlyOpportunity : DataConnectorEntityModel
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

        [FieldSettings("Name", DefaultField = true)]
        public string OPPORTUNITY_NAME { get; set; }

        [FieldSettings("Details", DefaultField = true)]
        public string OPPORTUNITY_DETAILS { get; set; }

        [FieldSettings("Probability", DefaultField = true)]
        public string PROBABILITY { get; set; }

        [FieldSettings("Status", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(InsightlyOpportunityState), DefaultField = true)]
        public InsightlyOpportunityState? OPPORTUNITY_STATE { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Opportunity Id", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int OPPORTUNITY_ID { get; set; }

        [FieldSettings("Bid currency")]
        public string BID_CURRENCY { get; set; }

        [FieldSettings("Bid type")]
        public string BID_TYPE { get; set; }

        [FieldSettings("Bid duration", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int? BID_DURATION { get; set; }

        [FieldSettings("Image", FieldTypeId = DataConnector.FieldTypeIdImage)]
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

        #endregion

        #region Hidden properties

        public string BID_AMOUNT { get; set; }
        public List<InsightlyTag> TAGS { get; set; }
        public List<InsightlyLink> LINKS { get; set; }
        public List<InsightlyEmailLink> EMAILLINKS { get; set; }
        public InsightlyOpportunityCategory Category { get; set; }
        public InsightlyPipeline Pipeline { get; set; }
        public InsightlyPipelineStage PipelineStage { get; set; }
        public DateTime? FORECAST_CLOSE_DATE { get; set; }
        public int? CATEGORY_ID { get; set; }
        public int? PIPELINE_ID { get; set; }
        public int? STAGE_ID { get; set; }
        public int? RESPONSIBLE_USER_ID { get; set; }
        public int? OWNER_USER_ID { get; set; }
        public DateTime? DATE_CREATED_UTC { get; set; }
        public DateTime? DATE_UPDATED_UTC { get; set; }
        public string VISIBLE_TO { get; set; }
        public int? VISIBLE_TEAM_ID { get; set; }
        public string VISIBLE_USER_IDS { get; set; }

        #endregion

        #region Private dummy properties

        private string TagListDummy { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Category name")]
        public string CategoryName
        {
            get
            {
                return (Category == null) ? string.Empty : Category.CATEGORY_NAME;
            }
            set
            {
                Category.CATEGORY_NAME = value;
            }
        }

        [FieldSettings("Category active", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool CategoryActive
        {
            get
            {
                return (Category != null) && Category.ACTIVE;
            }
            set
            {
                Category.ACTIVE = value;
            }
        }

        [FieldSettings("Pipeline name")]
        public string PipelineName
        {
            get
            {
                return (Pipeline == null) ? string.Empty : Pipeline.PIPELINE_NAME;
            }
            set
            {
                Pipeline.PIPELINE_NAME = value;
            }
        }

        [FieldSettings("Pipeline stage name")]
        public string PipelineStageName
        {
            get
            {
                return (PipelineStage == null) ? string.Empty : PipelineStage.STAGE_NAME;
            }
            set
            {
                PipelineStage.STAGE_NAME = value;
            }
        }

        [FieldSettings("Pipeline stage order", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int PipelineStageOrder
        {
            get
            {
                return (PipelineStage == null) ? 0 : PipelineStage.STAGE_ORDER ?? 0;
            }
            set
            {
                PipelineStage.STAGE_ORDER = value;
            }
        }

        [FieldSettings("Tags")]
        public string TagList
        {
            get
            {
                return string.Join(", ", TAGS.Select(tag => tag.TAG_NAME).ToList());
            }
            set
            {
                TagListDummy = value;
            }
        }

        [FieldSettings("Bid amount", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal BidAmount
        {
            get
            {
                decimal amount;
                return decimal.TryParse(BID_AMOUNT, out amount) ? amount : 0;
            }
            set
            {
                BID_AMOUNT = value.ToString(CultureInfo.InvariantCulture);
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

        [FieldSettings("Forecast close date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime ForecastCloseDate
        {
            get
            {
                return FORECAST_CLOSE_DATE ?? DateTime.MinValue;
            }
            set
            {
                FORECAST_CLOSE_DATE = value;
            }
        }

        #endregion

    }
}
