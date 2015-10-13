using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Zopim.Models
{
    public class ZopimBans : DataConnectorEntityModel
    {

        public List<ZopimBannedVisitor> visitors { get; set; }
        public List<ZopimBannedIp> ips { get; set; }

    }
}
