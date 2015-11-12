namespace eOne.Common.Connectors.Zopim.Models
{
    public class ZopimChatMessageCount : ConnectorEntityModel
    {

        public int visitor { get; set; }
        public int total { get; set; }
        public int agent { get; set; }

    }
}