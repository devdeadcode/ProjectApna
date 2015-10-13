using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.FreshBooks.Models
{
    public class FreshBookContact : DataConnectorEntityModel
    {

        public int contact_id { get; set; }
        public string username { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string phone1 { get; set; }
        public string phone2 { get; set; }

    }
}
