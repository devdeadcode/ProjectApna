using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalAddress : DataConnectorEntityModel
    {

        public string line1 { get; set; }
        public string line2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postal_code { get; set; }
        public string country_code { get; set; }
        public string phone { get; set; }

        public string address => BuildAddress(line1, line2, city, state, postal_code);

    }
}

