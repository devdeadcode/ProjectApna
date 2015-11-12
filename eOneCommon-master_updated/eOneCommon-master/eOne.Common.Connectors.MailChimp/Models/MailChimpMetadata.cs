namespace eOne.Common.Connectors.MailChimp.Models
{
    public class MailChimpMetadata : ConnectorEntityModel
    {
        
        public string dc { get; set; }
        public string role { get; set; }
        public string accountname { get; set; }
        public string login_url { get; set; }
        public string api_endpoint { get; set; }

    }
}
