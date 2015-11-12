using System;

namespace eOne.Common.Connectors.SalesForce.Models
{
    public class SalesForceProduct : SalesForceEntity
    {

        #region Default properties

        [FieldSettings("Name", DefaultField = true, Description = true)]
        public string Name { get; set; }

        [FieldSettings("Product code", DefaultField = true)]
        public string ProductCode { get; set; }

        [FieldSettings("Description", DefaultField = true)]
        public string Description { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Product ID", KeyNumber = 1)]
        public string Id { get; set; }

        [FieldSettings("Product family")]
        public string Family { get; set; }

        [FieldSettings("Active", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool IsActive { get; set; }

        [FieldSettings("Last viewed date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? LastViewedDate { get; set; }

        [FieldSettings("Last referenced date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? LastReferencedDate { get; set; }

        #endregion

    }
}
