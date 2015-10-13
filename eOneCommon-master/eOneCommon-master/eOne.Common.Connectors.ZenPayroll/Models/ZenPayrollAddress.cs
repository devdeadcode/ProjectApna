using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.ZenPayroll.Models
{
    public class ZenPayrollAddress : DataConnectorEntityModel
    {

        public int id { get; set; }
        public string version { get; set; }
        public int employee_id { get; set; }
        public string street_1 { get; set; }
        public string street_2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string country { get; set; }
        public bool active { get; set; }

        public string address => BuildAddress(street_1, street_2, city, state, zip);

    }
}