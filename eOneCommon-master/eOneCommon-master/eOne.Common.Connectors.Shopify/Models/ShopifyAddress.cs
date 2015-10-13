using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Shopify.Models
{
    public class ShopifyAddress : DataConnectorEntityModel
    {

        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string company { get; set; }
        public string country { get; set; }
        public string first_name { get; set; }
        public string id { get; set; }
        public string last_name { get; set; }
        public string phone { get; set; }
        public string province { get; set; }
        public string zip { get; set; }
        public string province_code { get; set; }
        public string country_code { get; set; }
        public string country_name { get; set; }
        public string name { get; set; }

        public string address => BuildAddress(address1, address2, city, province, zip);

    }
}
