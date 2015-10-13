using System;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.SalesForce.Models
{
    public class SalesForceCustomEntity : SalesForceEntity
    {

        #region Default properties

        [FieldSettings("Name", DefaultField = true)]
        public string Name { get; set; }

        [FieldSettings("Owner name", DefaultField = true, FieldsRequiredForCalculation = "OwnerId")]
        public string OwnerName => Owner == null ? string.Empty : Owner.Name;

        #endregion

        #region Properties

        [FieldSettings("ID")]
        public string Id { get; set; }

        [FieldSettings("Deleted", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool IsDeleted { get; set; }

        #endregion

        #region Hidden properties

        public string OwnerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedById { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedById { get; set; }

        public SalesForceUser Owner { get; set; }
        public SalesForceUser CreatedBy { get; set; }
        public SalesForceUser LastModifiedBy { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Created by user name")]
        public string CreatedByName => CreatedBy.Name;

        [FieldSettings("Last modified by user name")]
        public string LastModifiedByName => LastModifiedBy.Name;

        [FieldSettings("Last modified by date", FieldTypeId = DataConnector.FieldTypeIdDate, Modified = true)]
        public DateTime LastModifiedDateOnly => LastModifiedDate.Date;

        [FieldSettings("Created by date", FieldTypeId = DataConnector.FieldTypeIdDate, Created = true)]
        public DateTime CreatedDateOnly => CreatedDate.Date;

        [FieldSettings("Last modified by time", FieldTypeId = DataConnector.FieldTypeIdTime, Modified = true)]
        public DateTime LastModifiedTime => Time(LastModifiedDate);

        [FieldSettings("Created by time", FieldTypeId = DataConnector.FieldTypeIdTime, Created = true)]
        public DateTime CreatedTime => Time(CreatedDate);

        #endregion

    }
}