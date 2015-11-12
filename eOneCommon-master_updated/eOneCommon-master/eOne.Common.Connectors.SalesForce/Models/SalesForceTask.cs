using System;

namespace eOne.Common.Connectors.SalesForce.Models
{
    public class SalesForceTask : SalesForceActivity
    {

        #region Default properties

        [FieldSettings("Status", DefaultField = true)]
        public string Status { get; set; }

        [FieldSettings("Priority", DefaultField = true)]
        public string Priority { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Task ID", KeyNumber = 1)]
        public string Id { get; set; }

        [FieldSettings("Closed", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool IsClosed { get; set; }

        #endregion

    }
}


//"CallDurationInSeconds": null,
//"CallType": null,
//"CallDisposition": null,
//"CallObject": null,