using System;

namespace eOne.Common.Connectors.HubSpot.Models
{
    public class HubspotIdentity : ConnectorEntityModel
    {

        public enum HubspotIdentityType
        {
            EMAIL,
            LEAD_GUID
        }

        public HubspotIdentityType type { get; set; }
        public string value { get; set; }
        public int timestamp { get; set; }
        public DateTime createdate => FromEpochMilliseconds(timestamp);

    }
}
