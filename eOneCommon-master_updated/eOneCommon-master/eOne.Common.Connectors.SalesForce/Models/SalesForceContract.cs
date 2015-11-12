using System;

namespace eOne.Common.Connectors.SalesForce.Models
{
    public class SalesForceContract : SalesForceEntity
    {

        #region Default properties

        [FieldSettings("Contract number", DefaultField = true, Description = true)]
        public string ContractNumber { get; set; }

        [FieldSettings("Start date", DefaultField = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? StartDate { get; set; }

        [FieldSettings("End date", DefaultField = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? EndDate { get; set; }

        [FieldSettings("Status", DefaultField = true)]
        public string Status { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Description")]
        public string Description { get; set; }

        [FieldSettings("Contract ID", KeyNumber = 1)]
        public string Id { get; set; }

        [FieldSettings("Account ID")]
        public string AccountId { get; set; }

        [FieldSettings("Contract term (months)", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int? ContractTerm { get; set; }

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

        [FieldSettings("Activated date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? ActivatedDate { get; set; }

        [FieldSettings("Last approved date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? LastApprovedDate { get; set; }

        [FieldSettings("Last activity date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? LastActivityDate { get; set; }

        [FieldSettings("Last viewed date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? LastViewedDate { get; set; }

        [FieldSettings("Last referenced date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? LastReferencedDate { get; set; }

        [FieldSettings("Company signed date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? CompanySignedDate { get; set; }

        [FieldSettings("Customer signed date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? CustomerSignedDate { get; set; }

        [FieldSettings("Special terms")]
        public string SpecialTerms { get; set; }

        [FieldSettings("Customer signed title")]
        public string CustomerSignedTitle { get; set; }

        #endregion

        #region Hidden properties

        public string OwnerExpirationNotice { get; set; }
        public string CompanySignedId { get; set; }
        public string CustomerSignedId { get; set; }
        public string ActivatedById { get; set; }
        public string Pricebook2Id { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Owner expiration notice", FieldsRequiredForCalculation = "OwnerExpirationNotice")]
        public string OwnerExpirationNoticeDays => OwnerExpirationNotice == null ? "None" : $"{OwnerExpirationNotice} days";

        [FieldSettings("Billing address", FieldsRequiredForCalculation = "BillingStreet, BillingCity, BillingState, BillingPostalCode")]
        public string BillingAddress => BuildAddress(BillingStreet, BillingCity, BillingState, BillingPostalCode);

        #endregion

    }
}