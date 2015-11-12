using System.ComponentModel;

namespace eOne.Common.Connectors.Xero.Models
{
    public class XeroEmployee : ConnectorEntityModel
    {

        #region Enums

        public enum XeroEmployeeStatus
        {
            [Description("Active")]
            ACTIVE,
            [Description("Archived")]
            ARCHIVED
        }

        #endregion

        #region Default properties

        [FieldSettings("Name", DefaultField = true)]
        public string Name => $"{FirstName.Trim()} {LastName.Trim()}";

        [FieldSettings("Status", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(XeroEmployeeStatus))]
        public XeroEmployeeStatus Status { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Employee ID")]
        public string EmployeeID { get; set; }

        [FieldSettings("First name")]
        public string FirstName { get; set; }

        [FieldSettings("Last name")]
        public string LastName { get; set; }

        [FieldSettings("Link")]
        public string DisplayUrl => ExternalLink == null ? string.Empty : ExternalLink.DisplayUrl;

        #endregion

        #region Hidden properties

        public XeroExternalLink ExternalLink { get; set; }

        #endregion

    }
}
