namespace eOne.Common.Connectors.SalesForce.Models
{
    public class SalesForceCustomEntity : SalesForceEntity
    {

        #region Default properties

        [FieldSettings("Name", DefaultField = true)]
        public string Name { get; set; }

        #endregion

        #region Properties

        [FieldSettings("ID", KeyNumber = 1)]
        public string Id { get; set; }

        #endregion

    }
}