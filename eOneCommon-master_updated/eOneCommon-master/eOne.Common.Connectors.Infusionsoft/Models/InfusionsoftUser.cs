namespace eOne.Common.Connectors.Infusionsoft.Models
{
    public class InfusionsoftUser : ConnectorEntityModel
    {

        #region Enums

        public enum InfusionsoftUserPhoneType
        {
            Work,
            Home,
            Mobile,
            Other
        }

        #endregion

        #region Default properties

        [FieldSettings("Name", DefaultField = true, FieldsRequiredForCalculation = "FirstName,LastName")]
        public string Name => CombineFirstLastName(FirstName, LastName);

        [FieldSettings("Email address", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEmail)]
        public string Email { get; set; }

        #endregion

        #region Properties

        [FieldSettings("City")]
        public string City { get; set; }

        [FieldSettings("First name")]
        public string FirstName { get; set; }

        [FieldSettings("HTML signature")]
        public string HTMLSignature { get; set; }

        [FieldSettings("ID", FieldTypeId = Connector.FieldTypeIdInteger, KeyNumber = 1)]
        public int Id { get; set; }

        [FieldSettings("Last name")]
        public string LastName { get; set; }

        [FieldSettings("Middle name")]
        public string MiddleName { get; set; }

        [FieldSettings("Nickname")]
        public string Nickname { get; set; }

        [FieldSettings("Postal code")]
        public string PostalCode { get; set; }

        [FieldSettings("Text signature")]
        public string Signature { get; set; }

        [FieldSettings("Spouse name")]
        public string SpouseName { get; set; }

        [FieldSettings("State")]
        public string State { get; set; }

        [FieldSettings("Address 1")]
        public string StreetAddress1 { get; set; }

        [FieldSettings("Address 2")]
        public string StreetAddress2 { get; set; }

        [FieldSettings("Suffix")]
        public string Suffix { get; set; }

        [FieldSettings("Title")]
        public string Title { get; set; }

        [FieldSettings("Email address 2", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEmail)]
        public string EmailAddress2 { get; set; }

        [FieldSettings("Phone 1", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string Phone1 { get; set; }

        [FieldSettings("Phone 1 extension")]
        public string Phone1Ext { get; set; }

        [FieldSettings("Phone 1 type", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(InfusionsoftUserPhoneType))]
        public string Phone1Type { get; set; }

        [FieldSettings("Phone 2", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string Phone2 { get; set; }

        [FieldSettings("Phone 2 extension")]
        public string Phone2Ext { get; set; }

        [FieldSettings("Phone 2 type", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(InfusionsoftUserPhoneType))]
        public string Phone2Type { get; set; }

        #endregion

    }
}
