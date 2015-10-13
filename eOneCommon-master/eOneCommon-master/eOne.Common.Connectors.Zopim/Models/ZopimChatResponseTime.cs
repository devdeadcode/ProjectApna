using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Zopim.Models
{
    public class ZopimChatResponseTime : DataConnectorEntityModel
    {

        public int max { get; set; }
        public int avg { get; set; }
        public int first { get; set; }

    }
}