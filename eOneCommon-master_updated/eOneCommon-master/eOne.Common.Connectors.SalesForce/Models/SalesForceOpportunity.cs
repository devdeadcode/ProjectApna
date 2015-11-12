using System;

namespace eOne.Common.Connectors.SalesForce.Models
{
    public class SalesForceOpportunity : SalesForceEntity
    {

        [FieldSettings("Name", DefaultField = true, Description = true)]
        public string Name { get; set; }

        [FieldSettings("Stage", DefaultField = true)]
        public string StageName { get; set; }

        [FieldSettings("Amount", DefaultField = true, FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal? Amount { get; set; }

        [FieldSettings("Probability", DefaultField = true, FieldTypeId = Connector.FieldTypeIdPercentage)]
        public decimal? Probability { get; set; }

        [FieldSettings("Opportunity ID", KeyNumber = 1)]
        public string Id { get; set; }

        [FieldSettings("Private", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool IsPrivate { get; set; }

        [FieldSettings("Closed", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool IsClosed { get; set; }

        [FieldSettings("Won", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool IsWon { get; set; }

        [FieldSettings("Expected revenue", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal? ExpectedRevenue { get; set; }

        [FieldSettings("Total opportunity quantity", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal? TotalOpportunityQuantity { get; set; }

        [FieldSettings("Fiscal quarter", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public int? FiscalQuarter { get; set; }

        [FieldSettings("Fiscal year", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public int? FiscalYear { get; set; }

        [FieldSettings("Description")]
        public string Description { get; set; }

        [FieldSettings("Type")]
        public string Type { get; set; }

        [FieldSettings("Next step")]
        public string NextStep { get; set; }

        [FieldSettings("Lead source")]
        public string LeadSource { get; set; }

        [FieldSettings("Forecast category")]
        public string ForecastCategoryName { get; set; }

        [FieldSettings("Campaign ID")]
        public string CampaignId { get; set; }

        [FieldSettings("Account ID")]
        public string AccountId { get; set; }

        public string Pricebook2Id { get; set; }

        [FieldSettings("Close date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? CloseDate { get; set; }

        [FieldSettings("Last activity date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? LastActivityDate { get; set; }

        [FieldSettings("Last viewed date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? LastViewedDate { get; set; }

        [FieldSettings("Last referenced date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? LastReferencedDate { get; set; }

    }
}
