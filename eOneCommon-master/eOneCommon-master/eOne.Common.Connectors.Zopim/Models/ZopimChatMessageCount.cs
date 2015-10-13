using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Zopim.Models
{
    public class ZopimChatMessageCount : DataConnectorEntityModel
    {

        public int visitor { get; set; }
        public int total { get; set; }
        public int agent { get; set; }

    }
}