namespace eOne.Common.Connectors.SalesForce.Models
{
    public class SalesForceNote : SalesForceEntity
    {

        #region Default properties

        [FieldSettings("Title", DefaultField = true, Description = true)]
        public string Title { get; set; }

        [FieldSettings("Body", DefaultField = true)]
        public string Body { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Parent ID")]
        public string ParentId { get; set; }

        [FieldSettings("Private", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool IsPrivate { get; set; }

        [FieldSettings("Note ID", KeyNumber = 1)]
        public string Id { get; set; }

        #endregion

    }
}