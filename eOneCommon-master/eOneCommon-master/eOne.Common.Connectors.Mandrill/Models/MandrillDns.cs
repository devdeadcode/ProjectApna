using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Mandrill.Models
{
    public class MandrillDns : DataConnectorEntityModel
    {

        public bool enabled { get; set; }
        public bool valid { get; set; }
        public string error { get; set; }

    }
}
