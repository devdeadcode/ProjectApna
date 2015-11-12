namespace eOne.Common.Connectors.MailChimp.Models.List
{
    public class MailChimpListContact : ConnectorEntityModel
    {

        public string company { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string country { get; set; }
        public string phone { get; set; }

        public string address => BuildAddress(address1, address2, city, state, zip);

    }
}
