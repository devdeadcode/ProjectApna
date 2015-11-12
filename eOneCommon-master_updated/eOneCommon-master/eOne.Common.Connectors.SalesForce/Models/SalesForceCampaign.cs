using System;

namespace eOne.Common.Connectors.SalesForce.Models
{
    public class SalesForceCampaign : SalesForceEntity
    {

        [FieldSettings("Name", DefaultField = true, Description = true)]
        public string Name { get; set; }

        [FieldSettings("Type", DefaultField = true)]
        public string Type { get; set; }

        [FieldSettings("Status", DefaultField = true)]
        public string Status { get; set; }

        [FieldSettings("Campaign ID", KeyNumber = 1)]
        public string Id { get; set; }

        [FieldSettings("Active", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool IsActive { get; set; }

        [FieldSettings("Description")]
        public string Description { get; set; }

        [FieldSettings("Expected revenue", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal? ExpectedRevenue { get; set; }

        [FieldSettings("Budgeted cost", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal? BudgetedCost { get; set; }

        [FieldSettings("Actual cost", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal? ActualCost { get; set; }

        [FieldSettings("Amount of all opportunities", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public int? AmountAllOpportunities { get; set; }

        [FieldSettings("Amount of won opportunities", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public int? AmountWonOpportunities { get; set; }

        [FieldSettings("Expected response", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int? ExpectedResponse { get; set; }

        [FieldSettings("Number sent", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int? NumberSent { get; set; }

        [FieldSettings("Number of leads", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int? NumberOfLeads { get; set; }

        [FieldSettings("Number of converted leads", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int? NumberOfConvertedLeads { get; set; }

        [FieldSettings("Number of contacts", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int? NumberOfContacts { get; set; }

        [FieldSettings("Number of responses", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int? NumberOfResponses { get; set; }

        [FieldSettings("Number of opportunities", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int? NumberOfOpportunities { get; set; }

        [FieldSettings("Number of won opportunities", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int? NumberOfWonOpportunities { get; set; }
        
        public string CampaignMemberRecordTypeId { get; set; }
        public string ParentId { get; set; }

        [FieldSettings("Start date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? StartDate { get; set; }

        [FieldSettings("End date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? EndDate { get; set; }

        [FieldSettings("Last activity date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? LastActivityDate { get; set; }

        [FieldSettings("Last viewed date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? LastViewedDate { get; set; }

        [FieldSettings("Last referenced date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? LastReferencedDate { get; set; }

    }
}