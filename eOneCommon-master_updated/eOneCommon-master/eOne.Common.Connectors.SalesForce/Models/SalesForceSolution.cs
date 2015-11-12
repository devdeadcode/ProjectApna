using System;

namespace eOne.Common.Connectors.SalesForce.Models
{
    public class SalesForceSolution : SalesForceEntity
    {

        #region Default properties

        [FieldSettings("Solution number", DefaultField = true)]
        public string SolutionNumber { get; set; }

        [FieldSettings("Solution name", DefaultField = true, Description = true)]
        public string SolutionName { get; set; }

        [FieldSettings("Status", DefaultField = true)]
        public string Status { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Public", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool IsPublished { get; set; }

        [FieldSettings("Visible in public knowledge base", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool IsPublishedInPublicKb { get; set; }

        [FieldSettings("Reviewed", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool IsReviewed { get; set; }

        [FieldSettings("Solution ID", KeyNumber = 1)]
        public string Id { get; set; }

        [FieldSettings("Solution details")]
        public string SolutionNote { get; set; }

        [FieldSettings("Number of times used", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int TimesUsed { get; set; }

        [FieldSettings("Last viewed date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? LastViewedDate { get; set; }

        [FieldSettings("Last referenced date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? LastReferencedDate { get; set; }

        [FieldSettings("Is HTML", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool IsHtml { get; set; }

        #endregion

    }
}