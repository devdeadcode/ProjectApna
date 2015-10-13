using System;
using System.ComponentModel;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Xero.Models
{
    public class XeroUser : DataConnectorEntityModel
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

        [FieldSettings("Email address", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string EmailAddress { get; set; }

        [FieldSettings("Role", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(XeroUserRole))]
        public XeroUserRole OrganisationRole { get; set; }

        #endregion

        #region Properties

        [FieldSettings("User ID")]
        public string UserID { get; set; }

        [FieldSettings("First name")]
        public string FirstName { get; set; }

        [FieldSettings("Last name")]
        public string LastName { get; set; }

        [FieldSettings("Subscriber", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool IsSubscriber { get; set; }

        #endregion

        #region Hidden properties

        public DateTime UpdatedDateUTC { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Updated date", FieldTypeId = DataConnector.FieldTypeIdDate, Modified = true)]
        public DateTime UpdatedDate => UpdatedDateUTC.Date;

        [FieldSettings("Updated time", FieldTypeId = DataConnector.FieldTypeIdTime, Modified = true)]
        public DateTime UpdatedTime => Time(UpdatedDateUTC);

        #endregion

    }
}