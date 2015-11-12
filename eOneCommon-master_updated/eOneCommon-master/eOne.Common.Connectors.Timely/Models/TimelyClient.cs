namespace eOne.Common.Connectors.Timely.Models
{
    public class TimelyClient : ConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Name", DefaultField = true, Description = true)]
        public string name { get; set; }

        [FieldSettings("Archived", DefaultField = true, FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool archived { get; set; }

        #endregion

        #region Properties

        [FieldSettings("ID", FieldTypeId = Connector.FieldTypeIdInteger, KeyNumber = 1)]
        public int id { get; set; }

        #endregion
        
    }
}
