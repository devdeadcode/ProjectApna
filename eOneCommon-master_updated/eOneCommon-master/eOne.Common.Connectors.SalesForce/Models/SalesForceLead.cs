using System;

namespace eOne.Common.Connectors.SalesForce.Models
{
    public class SalesForceLead : SalesForceEntity
    {

        #region Default properties

        [FieldSettings("Name", DefaultField = true, Description = true)]
        public string Name { get; set; }

        [FieldSettings("Phone", DefaultField = true, FieldTypeId = Connector.FieldTypeIdPhone)]
        public string Phone { get; set; }

        [FieldSettings("Email", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEmail)]
        public string Email { get; set; }

        [FieldSettings("Rating", DefaultField = true)]
        public string Rating { get; set; }

        [FieldSettings("Lead source", DefaultField = true)]
        public string LeadSource { get; set; }

        [FieldSettings("Status", DefaultField = true)]
        public string Status { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Lead ID", KeyNumber = 1)]
        public string Id { get; set; }

        [FieldSettings("Master record ID")]
        public string MasterRecordId { get; set; }

        [FieldSettings("Last name")]
        public string LastName { get; set; }

        [FieldSettings("First name")]
        public string FirstName { get; set; }

        [FieldSettings("Salutation")]
        public string Salutation { get; set; }

        [FieldSettings("Title")]
        public string Title { get; set; }

        [FieldSettings("Company")]
        public string Company { get; set; }

        [FieldSettings("Street")]
        public string Street { get; set; }

        [FieldSettings("City")]
        public string City { get; set; }

        [FieldSettings("State")]
        public string State { get; set; }

        [FieldSettings("Postal code")]
        public string PostalCode { get; set; }

        [FieldSettings("Country")]
        public string Country { get; set; }

        [FieldSettings("Mobile phone", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string MobilePhone { get; set; }

        [FieldSettings("Fax")]
        public string Fax { get; set; }

        [FieldSettings("Website", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string Website { get; set; }

        [FieldSettings("Description")]
        public string Description { get; set; }

        [FieldSettings("Industry")]
        public string Industry { get; set; }

        [FieldSettings("Annual revenue", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal? AnnualRevenue { get; set; }

        [FieldSettings("Number of employees", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int NumberOfEmployees { get; set; }

        [FieldSettings("Converted", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool IsConverted { get; set; }

        [FieldSettings("Unread by owner", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool IsUnreadByOwner { get; set; }

        [FieldSettings("Last activity date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? LastActivityDate { get; set; }

        [FieldSettings("Email bounce reason")]
        public string EmailBouncedReason { get; set; }

        [FieldSettings("Email bounce date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? EmailBouncedDate { get; set; }

        [FieldSettings("Converted date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? ConvertedDate { get; set; }

        #endregion

        #region Hidden properties

        public string ConvertedAccountId { get; set; }
        public string ConvertedContactId { get; set; }
        public string ConvertedOpportunityId { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Address", FieldTypeId = Connector.FieldTypeIdAddress, FieldsRequiredForCalculation = "Street, City, State, PostalCode")]
        public string Address => BuildAddress(Street, City, State, PostalCode);

        #endregion

    }
}
