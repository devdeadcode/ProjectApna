using System;

namespace eOne.Common.Connectors.Infusionsoft.Models
{
    public class InfusionsoftContact : ConnectorEntityModel
    {

        [FieldSettings("Name", DefaultField = true, FieldsRequiredForCalculation = "FirstName,LastName")]
        public string Name => CombineFirstLastName(FirstName, LastName);

        [FieldSettings("Company", DefaultField = true)]
        public string Company { get; set; }

        [FieldSettings("Email", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEmail)]
        public string Email { get; set; }

        [FieldSettings("First name")]
        public string FirstName { get; set; }

        [FieldSettings("Last name")]
        public string LastName { get; set; }

        [FieldSettings("Created date", DefaultField = true, FieldTypeId = Connector.FieldTypeIdDate, Created = true)]
        public DateTime? DateCreated { get; set; }

        public string Groups { get; set; }

        public int? AccountId { get; set; }
        public string Address1Type { get; set; }
        public string Address2Street1 { get; set; }
        public string Address2Street2 { get; set; }
        public string Address2Type { get; set; }
        public string Address3Street1 { get; set; }
        public string Address3Street2 { get; set; }
        public string Address3Type { get; set; }
        public DateTime? Anniversary { get; set; }
        public string AssistantName { get; set; }
        public string AssistantPhone { get; set; }
        public string BillingInformation { get; set; }
        public DateTime? Birthday { get; set; }
        public string City { get; set; }
        public string City2 { get; set; }
        public string City3 { get; set; }
        public int? CompanyID { get; set; }
        public string ContactNotes { get; set; }
        public string ContactType { get; set; }
        public string Country { get; set; }
        public string Country2 { get; set; }
        public string Country3 { get; set; }
        public int? CreatedBy { get; set; }
        
        public string EmailAddress2 { get; set; }
        public string EmailAddress3 { get; set; }
        public string Fax1 { get; set; }
        public string Fax1Type { get; set; }
        public string Fax2 { get; set; }
        public string Fax2Type { get; set; }
        
        public int? Id { get; set; }
        public string JobTitle { get; set; }
        public DateTime? LastUpdated { get; set; }
        public int? LastUpdatedBy { get; set; }
        public int? LeadSourceId { get; set; }
        public string Leadsource { get; set; }
        public string MiddleName { get; set; }
        public string Nickname { get; set; }
        public int? OwnerID { get; set; }
        public string Password { get; set; }
        public string Phone1 { get; set; }
        public string Phone1Ext { get; set; }
        public string Phone1Type { get; set; }
        public string Phone2 { get; set; }
        public string Phone2Ext { get; set; }
        public string Phone2Type { get; set; }
        public string Phone3 { get; set; }
        public string Phone3Ext { get; set; }
        public string Phone3Type { get; set; }
        public string Phone4 { get; set; }
        public string Phone4Ext { get; set; }
        public string Phone4Type { get; set; }
        public string Phone5 { get; set; }
        public string Phone5Ext { get; set; }
        public string Phone5Type { get; set; }
        public string PostalCode { get; set; }
        public string PostalCode2 { get; set; }
        public string PostalCode3 { get; set; }
        public string ReferralCode { get; set; }
        public string SpouseName { get; set; }
        public string State { get; set; }
        public string State2 { get; set; }
        public string State3 { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public string Suffix { get; set; }
        public string Title { get; set; }
        public string Username { get; set; }
        public string Validated { get; set; }
        public string Website { get; set; }
        public string ZipFour1 { get; set; }
        public string ZipFour2 { get; set; }
        public string ZipFour3 { get; set; }

    }
}
