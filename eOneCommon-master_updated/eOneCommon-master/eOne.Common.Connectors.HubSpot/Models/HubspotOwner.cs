using System;
using System.ComponentModel;

namespace eOne.Common.Connectors.HubSpot.Models
{
    public class HubspotOwner : ConnectorEntityModel
    {

        #region Enums

        public enum HubspotOwnerType
        {
            [Description("Person")]
            PERSON,
            [Description("Queue")]
            QUEUE
        }

        #endregion

        #region Default properties

        [FieldSettings("Type", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(HubspotOwnerType))]
        public HubspotOwnerType type { get; set; }

        [FieldSettings("Name", DefaultField = true, Description = true)]
        public string name => CombineFirstLastName(firstName, lastName);

        [FieldSettings("Email", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEmail)]
        public string email { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Owner ID", FieldTypeId = Connector.FieldTypeIdInteger, KeyNumber = 1)]
        public int ownerId { get; set; }

        [FieldSettings("First name")]
        public string firstName { get; set; }

        [FieldSettings("Last name")]
        public string lastName { get; set; }

        #endregion

        #region Hidden properties

        public long portalId { get; set; }
        public long createdAt { get; set; }
        public long updatedAt { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Created date", FieldTypeId = Connector.FieldTypeIdDate, Created = true)]
        public DateTime? createdAtDate => FromEpochMilliseconds(createdAt);

        [FieldSettings("Created time", FieldTypeId = Connector.FieldTypeIdTime, Created = true)]
        public DateTime? createdAtTime => createdAtDate;

        [FieldSettings("Modified date", FieldTypeId = Connector.FieldTypeIdDate, Modified = true)]
        public DateTime? updatedAtDate => FromEpochMilliseconds(updatedAt);

        [FieldSettings("Modified time", FieldTypeId = Connector.FieldTypeIdTime, Modified = true)]
        public DateTime? updatedAtTime => updatedAtDate;

        #endregion

    }
}


//        "remoteList": [
//            {
//                "portalId": 62515,
//                "ownerId": 64,
//                "remoteId": "137304",
//                "remoteType": "HUBSPOT",
//                "active": true
//            }
//        ]
