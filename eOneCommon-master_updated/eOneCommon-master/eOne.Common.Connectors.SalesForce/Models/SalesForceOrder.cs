using System;

namespace eOne.Common.Connectors.SalesForce.Models
{
    public class SalesForceOrder : SalesForceEntity
    {

        #region Default properties

        [FieldSettings("Order number", DefaultField = true, Description = true)]
        public string OrderNumber { get; set; }

        [FieldSettings("Order amount", DefaultField = true, FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal? TotalAmount { get; set; }

        [FieldSettings("Status", DefaultField = true)]
        public string Status { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Order ID", KeyNumber = 1)]
        public string Id { get; set; }

        [FieldSettings("Contract ID")]
        public string ContractId { get; set; }

        [FieldSettings("Account ID")]
        public string AccountId { get; set; }

        [FieldSettings("Effective date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? EffectiveDate { get; set; }

        [FieldSettings("End date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? EndDate { get; set; }

        [FieldSettings("Last viewed date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? LastViewedDate { get; set; }

        [FieldSettings("Last referenced date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? LastReferencedDate { get; set; }

        [FieldSettings("Description")]
        public string Description { get; set; }

        [FieldSettings("Billing street")]
        public string BillingStreet { get; set; }

        [FieldSettings("Billing city")]
        public string BillingCity { get; set; }

        [FieldSettings("Billing state")]
        public string BillingState { get; set; }

        [FieldSettings("Billing postal code")]
        public string BillingPostalCode { get; set; }

        [FieldSettings("Billing country")]
        public string BillingCountry { get; set; }

        [FieldSettings("Shipping street")]
        public string ShippingStreet { get; set; }

        [FieldSettings("Shipping city")]
        public string ShippingCity { get; set; }

        [FieldSettings("Shipping state")]
        public string ShippingState { get; set; }

        [FieldSettings("Shipping postal code")]
        public string ShippingPostalCode { get; set; }

        [FieldSettings("Shipping country")]
        public string ShippingCountry { get; set; }

        [FieldSettings("Order type")]
        public string Type { get; set; }
        
        public string CustomerAuthorizedById { get; set; }
        public string CompanyAuthorizedById { get; set; }
        
        public string BillToContactId { get; set; }
        public string ShipToContactId { get; set; }
        public DateTime? CustomerAuthorizedDate { get; set; }
        public DateTime? CompanyAuthorizedDate { get; set; }
        public DateTime? ActivatedDate { get; set; }
        public DateTime? ActivatedById { get; set; }

        #endregion

        #region Hidden properties

        public string Pricebook2Id { get; set; }
        public bool IsReductionOrder { get; set; }
        public string Name { get; set; }
        public string OriginalOrderId { get; set; }
        public string PoNumber { get; set; }
        public string OrderReferenceNumber { get; set; }
        public DateTime? PoDate { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Billing address", FieldsRequiredForCalculation = "BillingStreet, BillingCity, BillingState, BillingPostalCode")]
        public string BillingAddress => BuildAddress(BillingStreet, BillingCity, BillingState, BillingPostalCode);

        [FieldSettings("Shipping address", FieldsRequiredForCalculation = "ShippingStreet, ShippingCity, ShippingState, ShippingPostalCode")]
        public string ShippingAddress => BuildAddress(ShippingStreet, ShippingCity, ShippingState, ShippingPostalCode);

        #endregion

    }
}


