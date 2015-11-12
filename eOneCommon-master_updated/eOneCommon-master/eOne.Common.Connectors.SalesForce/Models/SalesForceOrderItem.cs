using System;

namespace eOne.Common.Connectors.SalesForce.Models
{
    public class SalesForceOrderItem : SalesForceEntity
    {

        [FieldSettings("Item number", DefaultField = true)]
        public string OrderItemNumber { get; set; }

        [FieldSettings("Quantity", DefaultField = true, FieldTypeId = Connector.FieldTypeIdQuantity)]
        public decimal? Quantity { get; set; }

        [FieldSettings("Unit price", DefaultField = true, FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal? UnitPrice { get; set; }

        [FieldSettings("Total price", DefaultField = true, FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal? TotalPrice => Quantity * UnitPrice;

        public string Id { get; set; }
        public string OrderId { get; set; }
        public string PricebookEntryId { get; set; }
        public string OriginalOrderItemId { get; set; }
        public string Description { get; set; }
        public decimal? AvailableQuantity { get; set; }
        public decimal? ListPrice { get; set; }

        public DateTime? ServiceDate { get; set; }
        public DateTime? EndDate { get; set; }

        
    }
}