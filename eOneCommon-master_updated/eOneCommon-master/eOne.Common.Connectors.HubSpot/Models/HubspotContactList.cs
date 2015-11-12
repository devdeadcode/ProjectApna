using System;
using System.ComponentModel;

namespace eOne.Common.Connectors.HubSpot.Models
{
    public class HubspotContactList : ConnectorEntityModel
    {

        public enum HubspotContactListType
        {
            [Description("Static")]
            STATIC,
            [Description("Smart")]
            DYNAMIC
        }

        [FieldSettings("Name", DefaultField = true, Description = true)]
        public string name { get; set; }

        [FieldSettings("Type", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(HubspotContactListType))]
        public HubspotContactListType listType { get; set; }

        [FieldSettings("Size", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int metaData_size => metaData?.size ?? 0;

        [FieldSettings("Deleteable", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool deleteable { get; set; }

        [FieldSettings("List ID", FieldTypeId = Connector.FieldTypeIdInteger, KeyNumber = 1)]
        public int listId { get; set; }

        public int internalListId { get; set; }
        public long createdAt { get; set; }
        public long updatedAt { get; set; }
        public HubspotContactListMetadata metaData { get; set; }

        [FieldSettings("Created date", FieldTypeId = Connector.FieldTypeIdDate, Created = true)]
        public DateTime createdAtDate => FromEpochMilliseconds(createdAt);

        [FieldSettings("Modified date", FieldTypeId = Connector.FieldTypeIdDate, Modified = true)]
        public DateTime updatedAtDate => FromEpochMilliseconds(updatedAt);

    }
}
