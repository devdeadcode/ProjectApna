using System;

namespace eOne.Common.Connectors.Infusionsoft.Models
{
    public class InfusionsoftOpportunity : ConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Title", DefaultField = true)]
        public string OpportunityTitle { get; set; }

        [FieldSettings("Lead source", DefaultField = true)]
        public string Leadsource { get; set; }

        [FieldSettings("Estimated close date", DefaultField = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? EstimatedCloseDate { get; set; }

        [FieldSettings("Projected revenue - high", DefaultField = true, FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal? ProjectedRevenueHigh { get; set; }

        [FieldSettings("Projected revenue - high", DefaultField = true, FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal? ProjectedRevenueLow { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Next follow-up action")]
        public string NextActionNotes { get; set; }

        [FieldSettings("Notes")]
        public string OpportunityNotes { get; set; }

        [FieldSettings("Created date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? DateCreated { get; set; }

        [FieldSettings("Modified date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? LastUpdated { get; set; }

        [FieldSettings("Next follow-up date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? NextActionDate { get; set; }

        [FieldSettings("ID", FieldTypeId = Connector.FieldTypeIdInteger, KeyNumber = 1)]
        public int? Id { get; set; }

        [FieldSettings("Referral partner ID", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int? AffiliateId { get; set; }

        [FieldSettings("Contact ID", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int? ContactID { get; set; }

        [FieldSettings("Created by user ID", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int? CreatedBy { get; set; }

        [FieldSettings("Modified by user ID", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int? LastUpdatedBy { get; set; }

        [FieldSettings("Stage ID", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int? StageID { get; set; }

        [FieldSettings("Status ID", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int? StatusID { get; set; }

        [FieldSettings("Assigned to user ID", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int? UserID { get; set; }

        #endregion

        #region Hidden fields

        public string Objection { get; set; }
        public InfusionsoftUser AssignedToUser { get; set; }
        public InfusionsoftUser LastUpdatedByUser { get; set; }
        public InfusionsoftUser CreatedByUser { get; set; }
        public InfusionsoftAffiliate Affiliate { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Assigned to user name", FieldsRequiredForCalculation = "UserID")]
        public string AssignedToUserName => AssignedToUser == null ? string.Empty : AssignedToUser.Name;

        [FieldSettings("Modified by user name", FieldsRequiredForCalculation = "LastUpdatedBy")]
        public string LastUpdatedByUserName => LastUpdatedByUser == null ? string.Empty : LastUpdatedByUser.Name;

        [FieldSettings("Created by user name", FieldsRequiredForCalculation = "CreatedBy")]
        public string CreatedByUserName => CreatedByUser == null ? string.Empty : CreatedByUser.Name;

        [FieldSettings("Referral partner name", FieldsRequiredForCalculation = "AffiliateId")]
        public string AffiliateName => Affiliate == null ? string.Empty : Affiliate.AffName;

        #endregion

    }
}
