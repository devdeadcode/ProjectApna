using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Serialization;

namespace eOne.Common.Connectors.HubSpot.Models.ContractResolver
{
    class HubspotCustomContractResolver : DefaultContractResolver
    {

        private Dictionary<string, string> PropertyMappings { get; }

        public HubspotCustomContractResolver()
        {
            PropertyMappings = new Dictionary<string, string>();
        }

        public HubspotCustomContractResolver(ConnectorEntity entity)
        {
            PropertyMappings = new Dictionary<string, string>();
            switch (entity.Id)
            {
                case HubspotConnector.EntityIdContact:
                    PropertyMappings.Add("identity-profiles", "identity_profiles");
                    break;
            }
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
