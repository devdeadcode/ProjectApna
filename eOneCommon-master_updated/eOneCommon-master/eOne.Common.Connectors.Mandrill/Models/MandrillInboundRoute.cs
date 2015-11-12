namespace eOne.Common.Connectors.Mandrill.Models
{
    public class MandrillInboundRoute : ConnectorEntityModel
    {

        public string id { get; set; }
        public string pattern { get; set; }
        public string url { get; set; }

    }
}
