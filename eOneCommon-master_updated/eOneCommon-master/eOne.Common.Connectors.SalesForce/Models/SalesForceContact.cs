using System;

namespace eOne.Common.Connectors.SalesForce.Models
{
    public class SalesForceContact : SalesForceEntity
    {

        #region Default properties

        [FieldSettings("Name", DefaultField = true, Description = true)]
        public string Name { get; set; }

        [FieldSettings("Phone", DefaultField = true, FieldTypeId = Connector.FieldTypeIdPhone)]
        public string Phone { get; set; }

        [FieldSettings("Email", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEmail)]
        public string Email { get; set; }

        [FieldSettings("Account name", FieldsRequiredForCalculation = "AccountId")]
        public string AccountName => (Account == null) ? string.Empty : Account.Name;

        #endregion

        #region Properties

        [FieldSettings("Contact ID", KeyNumber = 1)]
        public string Id { get; set; }

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

        [FieldSettings("Mobile phone", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string MobilePhone { get; set; }

        [FieldSettings("Home phone", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string HomePhone { get; set; }

        [FieldSettings("Other phone", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string OtherPhone { get; set; }

        [FieldSettings("Assistant phone", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string AssistantPhone { get; set; }

        [FieldSettings("Assistant name")]
        public string AssistantName { get; set; }

        [FieldSettings("Title")]
        public string Title { get; set; }

        [FieldSettings("Department")]
        public string Department { get; set; }

        [FieldSettings("Lead source")]
        public string LeadSource { get; set; }

        [FieldSettings("Birth date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime Birthdate { get; set; }

        [FieldSettings("Description")]
        public string Description { get; set; }

        [FieldSettings("Email bounce date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? EmailBouncedDate { get; set; }

        [FieldSettings("Email bounce reason")]
        public string EmailBouncedReason { get; set; }

        [FieldSettings("Last activity date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? LastActivityDate { get; set; }

        [FieldSettings("Last stay-in-touch request date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? LastCURequestDate { get; set; }

        [FieldSettings("Last stay-in-touch update date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? LastCUUpdateDate { get; set; }

        #endregion

        #region Hidden properties

        public string ReportsToId { get; set; }
        public SalesForceAccount Account { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Account owner", FieldsRequiredForCalculation = "AccountId")]
        public string AccountOwnerName => (Account == null) ? string.Empty : Account.OwnerName;

        [FieldSettings("Account type", FieldsRequiredForCalculation = "AccountId")]
        public string AccountType => (Account == null) ? string.Empty : Account.Type;

        [FieldSettings("Mailing address", FieldTypeId = Connector.FieldTypeIdAddress, FieldsRequiredForCalculation = "MailingStreet, MailingCity, MailingState, MailingPostalCode")]
        public string BillingAddress => BuildAddress(MailingStreet, MailingCity, MailingState, MailingPostalCode);

        [FieldSettings("Other address", FieldTypeId = Connector.FieldTypeIdAddress, FieldsRequiredForCalculation = "OtherStreet, OtherCity, OtherState, OtherPostalCode")]
        public string ShippingAddress => BuildAddress(OtherStreet, OtherCity, OtherState, OtherPostalCode);

        #endregion

    }
}