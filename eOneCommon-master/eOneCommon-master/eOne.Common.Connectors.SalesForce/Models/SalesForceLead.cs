using System;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.SalesForce.Models
{
    public class SalesForceLead : SalesForceEntity
    {

        #region Default properties

        [FieldSettings("Name", DefaultField = true)]
        public string Name { get; set; }

        [FieldSettings("Phone", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdPhone)]
        public string Phone { get; set; }

        [FieldSettings("Email", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string Email { get; set; }

        [FieldSettings("Rating", DefaultField = true)]
        public string Rating { get; set; }

        [FieldSettings("Lead source", DefaultField = true)]
        public string LeadSource { get; set; }

        [FieldSettings("Status", DefaultField = true)]
        public string Status { get; set; }

        #endregion

        #region Properties

        [FieldSettings("ID")]
        public string Id { get; set; }

        [FieldSettings("Deleted", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool IsDeleted { get; set; }

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

        [FieldSettings("Mobile phone", FieldTypeId = DataConnector.FieldTypeIdPhone)]
        public string MobilePhone { get; set; }

        [FieldSettings("Fax")]
        public string Fax { get; set; }

        [FieldSettings("Website", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string Website { get; set; }

        [FieldSettings("Description")]
        public string Description { get; set; }

        [FieldSettings("Industry")]
        public string Industry { get; set; }

        [FieldSettings("Annual revenue", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal AnnualRevenue { get; set; }

        [FieldSettings("Number of employees", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int NumberOfEmployees { get; set; }

        [FieldSettings("Converted", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool IsConverted { get; set; }

        [FieldSettings("Unread by owner", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool IsUnreadByOwner { get; set; }

        [FieldSettings("Last activity date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime LastActivityDate { get; set; }

        [FieldSettings("Email bounce reason")]
        public string EmailBouncedReason { get; set; }

        [FieldSettings("Email bounce date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime EmailBouncedDate { get; set; }

        #endregion

        #region Hidden properties

        public string OwnerId { get; set; }
        public DateTime ConvertedDate { get; set; }
        public string ConvertedAccountId { get; set; }
        public string ConvertedContactId { get; set; }
        public string ConvertedOpportunityId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedById { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedById { get; set; }
        public DateTime SystemModstamp { get; set; }

        public SalesForceUser Owner { get; set; }
        public SalesForceUser CreatedByUser { get; set; }
        public SalesForceUser LastModifiedByUser { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Owner name", FieldsRequiredForCalculation = "OwnerId")]
        public string OwnerName => Owner.Name;

        [FieldSettings("Created by user name", FieldsRequiredForCalculation = "CreatedByUserId")]
        public string CreatedByUserName => CreatedByUser.Name;

        [FieldSettings("Last modified by user name", FieldsRequiredForCalculation = "LastModifiedByUserId")]
        public string LastModifiedByUserName => LastModifiedByUser.Name;

        [FieldSettings("Address", FieldTypeId = DataConnector.FieldTypeIdAddress, FieldsRequiredForCalculation = "Street, City, State, PostalCode")]
        public string Address => BuildAddress(Street, City, State, PostalCode);

        #endregion

    }
}
