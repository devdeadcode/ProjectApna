using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Timely.Models
{
    public class TimelyClient : DataConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Name", DefaultField = true)]
        public string name { get; set; }

        [FieldSettings("Archived", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool archived { get; set; }

        #endregion

        #region Properties

        [FieldSettings("ID", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int id { get; set; }

        #endregion
        
    }
}
