using System;
using System.Collections.Generic;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common
{
    public class ConnectorSearch
    {

        public DataConnector Connector { get; set; }
        public DataConnectorEntity Entity { get; set; }
        public List<DataConnectorCompany> Companies { get; set; }
        public string Query { get; set; }
        public List<string> Terms
        {
            get
            {
                var quotes = Query.Split('"');
                var splitChars = new[] { ' ', ',', ';' };
                var terms = new List<string>();
                foreach (var quote in quotes) terms.AddRange(quote.Split(splitChars, StringSplitOptions.RemoveEmptyEntries).ToList());
                return terms.Distinct().ToList();
            }
        }
        public List<string> NumericTerms
        {
            get
            {
                double value;
                return Terms.Where(term => double.TryParse(term, out value)).ToList();
            }
        }

    }
}
