using System.Collections.Generic;
using System.ComponentModel;

namespace eOne.Common.Connectors.Xero.Models
{
    public class XeroContactGroup
    {

        #region Enums

        public enum XeroContactGroupStatus
        {
            [Description("Active")]
            ACTIVE,
            [Description("Deleted")]
            DELETED
        }

        #endregion

        #region Default properties

        [FieldSettings("Name", DefaultField = true)]
        public string Name { get; set; }

        [FieldSettings("Number of contacts", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int NumberOfContacts => Contacts?.Count ?? 0;

        #endregion

        #region Properties

        [FieldSettings("Status", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(XeroContactGroupStatus))]
        public XeroContactGroupStatus Status { get; set; }

        [FieldSettings("Contact group ID")]
        public string ContactGroupID { get; set; }

        #endregion

        #region Hidden properties

        public List<XeroContact> Contacts { get; set; }

        #endregion

    }
}