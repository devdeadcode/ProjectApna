using System;
using System.Text;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.SalesForce.Models
{
    public class SalesForceAccount : SalesForceEntity
    {

        #region Default properties

        [FieldSettings("Name", DefaultField = true)]
        public string Name { get; set; }

        [FieldSettings("Type", DefaultField = true)]
        public string Type { get; set; }

        [FieldSettings("Name", FieldTypeId = DataConnector.FieldTypeIdPhone, DefaultField = true)]
        public string Phone { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Account ID")]
        public string Id { get; set; }

        [FieldSettings("Deleted", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool IsDeleted { get; set; }

        [FieldSettings("Master record ID")]
        public string MasterRecordId { get; set; }

        [FieldSettings("Parent ID")]
        public string ParentId { get; set; }

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

        [FieldSettings("Fax")]
        public string Fax { get; set; }

        [FieldSettings("Account number")]
        public string AccountNumber { get; set; }

        [FieldSettings("Website")]
        public string Website { get; set; }

        [FieldSettings("SIC")]
        public string Sic { get; set; }

        [FieldSettings("Industry")]
        public string Industry { get; set; }

        [FieldSettings("Annual revenue", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal AnnualRevenue { get; set; }

        [FieldSettings("Number of employees", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int NumberOfEmployees { get; set; }

        [FieldSettings("Ownership")]
        public string Ownership { get; set; }

        [FieldSettings("Ticker symbol")]
        public string TickerSymbol { get; set; }

        [FieldSettings("Description")]
        public string Description { get; set; }

        [FieldSettings("Rating")]
        public string Rating { get; set; }

        [FieldSettings("Site")]
        public string Site { get; set; }

        #endregion

        #region Hidden properties

        public string OwnerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedById { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedById { get; set; }
        public DateTime SystemModstamp { get; set; }
        public DateTime LastActivityDate { get; set; }

        public SalesForceUser Owner { get; set; }
        public SalesForceUser CreatedBy { get; set; }
        public SalesForceUser LastModifiedBy { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Owner", FieldsRequiredForCalculation = "OwnerId")]
        public string OwnerName => Owner.Name;

        [FieldSettings("Created by", FieldsRequiredForCalculation = "CreatedById")]
        public string CreatedByName => CreatedBy.Name;

        [FieldSettings("Last modified by", FieldsRequiredForCalculation = "LastModifiedById")]
        public string LastModifiedByName => LastModifiedBy.Name;

        [FieldSettings("Billing address", FieldTypeId = DataConnector.FieldTypeIdAddress, FieldsRequiredForCalculation = "BillingStreet, BillingCity, BillingState, BillingPostalCode")]
        public string BillingAddress => BuildAddress(BillingStreet, BillingCity, BillingState, BillingPostalCode);

        [FieldSettings("Shipping address", FieldTypeId = DataConnector.FieldTypeIdAddress, FieldsRequiredForCalculation = "ShippingStreet, ShippingCity, ShippingState, ShippingPostalCode")]
        public string ShippingAddress => BuildAddress(ShippingStreet, ShippingCity, ShippingState, ShippingPostalCode);

        #endregion

    }
}
