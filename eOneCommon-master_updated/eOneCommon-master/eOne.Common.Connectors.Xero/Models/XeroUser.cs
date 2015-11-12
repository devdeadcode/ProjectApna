using System;
using System.ComponentModel;

namespace eOne.Common.Connectors.Xero.Models
{
    public class XeroUser : ConnectorEntityModel
    {

        #region Enums

        public enum XeroUserRole
        {
            [Description("Read only")]
            READONLY,
            [Description("Invoice only")]
            INVOICEONLY,
            [Description("Standard")]
            STANDARD,
            [Description("Financial adviser")]
            FINANCIALADVISER,
            [Description("Managed client")]
            MANAGEDCLIENT,
            [Description("Cashbook client")]
            CASHBOOKCLIENT
        }

        #endregion

        #region Default properties

        [FieldSettings("Name", DefaultField = true)]
        public string Name => $"{FirstName.Trim()} {LastName.Trim()}";

        [FieldSettings("Email address", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEmail)]
        public string EmailAddress { get; set; }

        [FieldSettings("Role", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(XeroUserRole))]
        public XeroUserRole OrganisationRole { get; set; }

        #endregion

        #region Properties

        [FieldSettings("User ID")]
        public string UserID { get; set; }

        [FieldSettings("First name")]
        public string FirstName { get; set; }

        [FieldSettings("Last name")]
        public string LastName { get; set; }

        [FieldSettings("Subscriber", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool IsSubscriber { get; set; }

        [FieldSettings("Updated date", FieldTypeId = Connector.FieldTypeIdDate, Modified = true)]
        public DateTime UpdatedDateUTC { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Updated time", FieldTypeId = Connector.FieldTypeIdTime, Modified = true)]
        public DateTime UpdatedTime => UpdatedDateUTC;

        #endregion

    }
}