namespace eOne.Common.Connectors.HubSpot.Models
{
    public class HubspotContactListMetadata
    {

        public int size { get; set; }
        public string error { get; set; }
        public long lastProcessingStateChangeAt { get; set; }
        public long lastSizeChangeAt { get; set; }
        public string processing { get; set; }

    }
}