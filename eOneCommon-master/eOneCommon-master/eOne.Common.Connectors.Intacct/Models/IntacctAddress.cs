using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Intacct.Models
{
    public class IntacctAddress : DataConnectorEntityModel
    {

        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string country { get; set; }

        public string address => BuildAddress(address1, address2, city, state, zip);

    }
}