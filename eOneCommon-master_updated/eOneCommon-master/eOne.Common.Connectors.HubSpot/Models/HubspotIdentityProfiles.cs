using System.Collections.Generic;
using System.Linq;

namespace eOne.Common.Connectors.HubSpot.Models
{
    public class HubspotIdentityProfiles
    {

        public List<HubspotIdentity> identities { get; set; }

        public string email => GetIdentityValue(HubspotIdentity.HubspotIdentityType.EMAIL);

        public string GetIdentityValue(HubspotIdentity.HubspotIdentityType type)
        {
            foreach (var identity in identities.Where(identity => identity.type == type)) return identity.value;
            return string.Empty;
        }
    }
}
