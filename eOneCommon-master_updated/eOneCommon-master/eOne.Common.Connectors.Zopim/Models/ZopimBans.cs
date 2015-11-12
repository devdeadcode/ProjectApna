using System.Collections.Generic;

namespace eOne.Common.Connectors.Zopim.Models
{
    public class ZopimBans : ConnectorEntityModel
    {

        public List<ZopimBannedVisitor> visitors { get; set; }
        public List<ZopimBannedIp> ips { get; set; }

    }
}
