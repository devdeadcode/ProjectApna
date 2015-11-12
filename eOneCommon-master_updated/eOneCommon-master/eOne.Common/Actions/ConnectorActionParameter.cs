using eOne.Common.Connectors;
using eOne.Common.Query;

namespace eOne.Common.Actions
{
    public class ConnectorActionParameter
    {

        public ConnectorActionParameter()
        {
            Required = true;
        }

        public enum ConnectorActionParameterType
        {
            Field,
            Value
        }

        public string Name { get; set; }
        public ConnectorActionParameterType Type { get; set; }
        public ConnectorField Field { get; set; }
        public ConnectorFieldType FieldType { get; set; }
        public bool Required { get; set; }
        public string HelpText { get; set; }
        public ConnectorValue DefaultValue { get; set; }

    }
}
