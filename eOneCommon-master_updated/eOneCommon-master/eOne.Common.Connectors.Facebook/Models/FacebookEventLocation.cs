namespace eOne.Common.Connectors.Facebook.Models
{
    public class FacebookEventLocation: ConnectorEntityModel
    {

        public string city { get; set; }
        public string country { get; set; }
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
        public string state { get; set; }
        public string street { get; set; }
        public string zip { get; set; }

        public string address => BuildAddress(street, city, state, zip, country);

    }
}
