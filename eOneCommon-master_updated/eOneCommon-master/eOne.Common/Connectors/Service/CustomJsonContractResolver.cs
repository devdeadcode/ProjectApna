using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Serialization;

namespace eOne.Common.Connectors.Service
{
    public class CustomJsonContractResolver : DefaultContractResolver
    {

        private Dictionary<string, string> PropertyMappings { get; }

        public CustomJsonContractResolver()
        {
            PropertyMappings = new Dictionary<string, string>();
        }

        public CustomJsonContractResolver(ConnectorEntity entity)
        {
            PropertyMappings = new Dictionary<string, string>();
            foreach (var field in entity.Fields.Where(field => !string.IsNullOrWhiteSpace(field.ApiName)))
            {
                PropertyMappings.Add(field.Name, field.ApiName);
            }
        }

        protected override string ResolvePropertyName(string propertyName)
        {
            string resolvedName;
            var resolved = PropertyMappings.TryGetValue(propertyName, out resolvedName);
            return (resolved) ? resolvedName : base.ResolvePropertyName(propertyName);
        }

    }
}
