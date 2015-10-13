using eOne.Common.DataConnectors;

namespace eOne.Common
{
    public class ConnectorActionParameter
    {

        public enum ConnectorActionParameterType
        {
            Field,
            Value
        }

        public string Name { get; set; }
        public DataConnectorField Field { get; set; }
        public DataConnectorFieldType FieldType { get; set; }
        public ConnectorActionParameterType Type { get; set; }

    }
}
