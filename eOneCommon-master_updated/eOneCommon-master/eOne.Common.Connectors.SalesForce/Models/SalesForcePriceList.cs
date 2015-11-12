using System;

namespace eOne.Common.Connectors.SalesForce.Models
{
    public class SalesForcePriceList : SalesForceEntity
    {

        [FieldSettings("Name", DefaultField = true, Description = true)]
        public string Name { get; set; }

        [FieldSettings("Description", DefaultField = true)]
        public string Description { get; set; }

        [FieldSettings("Pricebook ID", KeyNumber = 1)]
        public string Id { get; set; }

        [FieldSettings("Last viewed date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? LastViewedDate { get; set; }

        [FieldSettings("Last referenced date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? LastReferencedDate { get; set; }

        [FieldSettings("Active", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool IsActive { get; set; }

        [FieldSettings("Standard", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool IsStandard { get; set; }

    }
}
