using System;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.SalesForce.Models
{
    public class SalesForceContact : SalesForceEntity
    {

        #region Default properties

        [FieldSettings("Name", DefaultField = true)]
        public string Name { get; set; }

        [FieldSettings("Phone", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdPhone)]
        public string Phone { get; set; }

        [FieldSettings("Email", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string Email { get; set; }

        [FieldSettings("Account name", FieldsRequiredForCalculation = "AccountId")]
        public string AccountName => (Account == null) ? string.Empty : Account.Name;

        #endregion

        #region Properties

        [FieldSettings("ID")]
        public string Id { get; set; }

        [FieldSettings("Deleted", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool IsDeleted { get; set; }

        [FieldSettings("Master record ID")]
        public string MasterRecordId { get; set; }

        [FieldSettings("Account ID")]
        public string AccountId { get; set; }

        [FieldSettings("Last name")]
        public string LastName { get; set; }

        [FieldSettings("First name")]
        public string FirstName { get; set; }

        [FieldSettings("Salutation")]
        public string Salutation { get; set; }

        [FieldSettings("Other street")]
        public string OtherStreet { get; set; }

        [FieldSettings("Other city")]
        public string OtherCity { get; set; }

        [FieldSettings("Other state")]
        public string OtherState { get; set; }

        [FieldSettings("Other postal code")]
        public string OtherPostalCode { get; set; }

        [FieldSettings("Other country")]
        public string OtherCountry { get; set; }

        [FieldSettings("Mailing street")]
        public string MailingStreet { get; set; }

        [FieldSettings("Mailing city")]
        public string MailingCity { get; set; }

        [FieldSettings("Mailing state")]
        public string MailingState { get; set; }

        [FieldSettings("Mailing postal code")]
        public string MailingPostalCode { get; set; }

        [FieldSettings("Mailing country")]
        public string MailingCountry { get; set; }

        [FieldSettings("Fax")]
        public string Fax { get; set; }

        [FieldSettings("Mobile phone", FieldTypeId = DataConnector.FieldTypeIdPhone)]
        public string MobilePhone { get; set; }

        [FieldSettings("Home phone", FieldTypeId = DataConnector.FieldTypeIdPhone)]
        public string HomePhone { get; set; }

        [FieldSettings("Other phone", FieldTypeId = DataConnector.FieldTypeIdPhone)]
        public string OtherPhone { get; set; }

        [FieldSettings("Assistant phone", FieldTypeId = DataConnector.FieldTypeIdPhone)]
        public string AssistantPhone { get; set; }

        [FieldSettings("Assistant name")]
        public string AssistantName { get; set; }

        [FieldSettings("Title")]
        public string Title { get; set; }

        [FieldSettings("Department")]
        public string Department { get; set; }

        [FieldSettings("Lead source")]
        public string LeadSource { get; set; }

        [FieldSettings("Birth date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime Birthdate { get; set; }

        [FieldSettings("Description")]
        public string Description { get; set; }

        [FieldSettings("Email bounce date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime EmailBouncedDate { get; set; }

        [FieldSettings("Email bounce reason")]
        public string EmailBouncedReason { get; set; }

        #endregion

        #region Hidden properties

        public string OwnerId { get; set; }
        public string CreatedById { get; set; }
        public string LastModifiedById { get; set; }
        public string ReportsToId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public DateTime SystemModstamp { get; set; }
        public DateTime LastActivityDate { get; set; }
        public DateTime LastCURequestDate { get; set; }
        public DateTime LastCUUpdateDate { get; set; }
        
        public SalesForceAccount Account { get; set; }
        public SalesForceUser Owner { get; set; }
        public SalesForceUser CreatedByUser { get; set; }
        public SalesForceUser LastModifiedByUser { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Owner name", FieldsRequiredForCalculation = "OwnerId")]
        public string OwnerName => (Owner == null) ? string.Empty : Owner.Name;

        [FieldSettings("Created by user name", FieldsRequiredForCalculation = "CreatedById")]
        public string CreatedByUserName => (CreatedByUser == null) ? string.Empty : CreatedByUser.Name;

        [FieldSettings("Modified by user name", FieldsRequiredForCalculation = "LastModifiedById")]
        public string LastModifiedByUserName => (LastModifiedByUser == null) ? string.Empty : LastModifiedByUser.Name;

        [FieldSettings("Account owner", FieldsRequiredForCalculation = "AccountId")]
        public string AccountOwnerName => (Account == null) ? string.Empty : Account.OwnerName;

        [FieldSettings("Account type", FieldsRequiredForCalculation = "AccountId")]
        public string AccountType => (Account == null) ? string.Empty : Account.Type;

        [FieldSettings("Mailing address", FieldTypeId = DataConnector.FieldTypeIdAddress, FieldsRequiredForCalculation = "MailingStreet, MailingCity, MailingState, MailingPostalCode")]
        public string BillingAddress => BuildAddress(MailingStreet, MailingCity, MailingState, MailingPostalCode);

        [FieldSettings("Other address", FieldTypeId = DataConnector.FieldTypeIdAddress, FieldsRequiredForCalculation = "OtherStreet, OtherCity, OtherState, OtherPostalCode")]
        public string ShippingAddress => BuildAddress(OtherStreet, OtherCity, OtherState, OtherPostalCode);

        #endregion

    }
}