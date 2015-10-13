using System.Collections.Generic;
using System.Linq;
using eOne.Common.DataConnectors;
using Newtonsoft.Json.Serialization;

namespace eOne.Common.Connectors.SalesForce.Models.ContractResolver
{
    public class SalesForceCustomContractResolver : DefaultContractResolver
    {

        private Dictionary<string, string> PropertyMappings { get; set; }

        public SalesForceCustomContractResolver()
        {
            PropertyMappings = new Dictionary<string, string>();
        }

        public SalesForceCustomContractResolver(DataConnectorEntity entity)
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
