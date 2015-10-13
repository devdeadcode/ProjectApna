using System.Collections.Generic;

namespace eOne.Common
{
    public class ConnectorEndpoint
    {

        private readonly string _base;
        private readonly List<string> _parameters;

        public ConnectorEndpoint(string baseEndpoint)
        {
            _parameters = new List<string>();
            _base = baseEndpoint;
        }

        public void AddParameter(string parameter, string value)
        {
            _parameters.Add($"{parameter}={value}");
        }

        public override string ToString()
        {
            return $"{_base}{Parameters}";
        }

        private string Parameters => _parameters.Count == 0 ? string.Empty : $"?{string.Join("&", _parameters)}";
    }
}
