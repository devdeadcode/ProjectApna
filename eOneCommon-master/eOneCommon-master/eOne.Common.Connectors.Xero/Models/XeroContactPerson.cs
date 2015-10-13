using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Xero.Models
{
    public class XeroContactPerson : DataConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Person", DefaultField = true)]
        public string Name => $"{FirstName.Trim()} {LastName.Trim()}";

        [FieldSettings("Contact", DefaultField = true)]
        public string ContactName => Contact == null ? string.Empty : Contact.Name;

        [FieldSettings("Email address", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string EmailAddress { get; set; }

        #endregion

        #region Properties

        [FieldSettings("First name")]
        public string FirstName { get; set; }

        [FieldSettings("Last name")]
        public string LastName { get; set; }

        [FieldSettings("Include in emails", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool IncludeInEmails { get; set; }

        #endregion

        #region Hidden properties

        public XeroContact Contact { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Supplier", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool ContactIsSupplier => Contact != null && Contact.IsSupplier;

        [FieldSettings("Customer", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool ContactIsCustomer => Contact != null && Contact.IsCustomer;

        [FieldSettings("Contact status", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(XeroContact.XeroContactStatus))]
        public XeroContact.XeroContactStatus ContactStatus => Contact?.ContactStatus ?? XeroContact.XeroContactStatus.ACTIVE;

        [FieldSettings("Contact ID")]
        public string ContactID => Contact.ContactID;

        #endregion

    }
}