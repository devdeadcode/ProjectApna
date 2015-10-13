using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Zopim.Models
{
    public class ZopimBannedVisitor : DataConnectorEntityModel
    {

        public int visitor_id { get; set; }
        public string reason { get; set; }
        public string visitor_name { get; set; }
        public int id { get; set; }

    }
}
