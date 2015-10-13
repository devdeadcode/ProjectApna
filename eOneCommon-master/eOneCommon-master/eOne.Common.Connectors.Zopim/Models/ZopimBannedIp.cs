using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Zopim.Models
{
    public class ZopimBannedIp : DataConnectorEntityModel
    {

        public string ip_address { get; set; }
        public string reason { get; set; }
        public int id { get; set; }

    }
}
