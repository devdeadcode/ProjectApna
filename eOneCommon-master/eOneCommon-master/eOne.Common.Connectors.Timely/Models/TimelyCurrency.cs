using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Timely.Models
{
    public class TimelyCurrency : DataConnectorEntityModel
    {

        public string id { get; set; }
        public int priority { get; set; }
        public string iso_code { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public List<string> alternate_symbols { get; set; }
        public string subunit { get; set; }
        public int subunit_to_unit { get; set; }
        public bool symbol_first { get; set; }
        public string html_entity { get; set; }
        public string decimal_mark { get; set; }
        public string thousands_separator { get; set; }
        public int iso_numeric { get; set; }

    }
}