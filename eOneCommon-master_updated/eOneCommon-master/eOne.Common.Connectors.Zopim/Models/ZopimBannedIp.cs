namespace eOne.Common.Connectors.Zopim.Models
{
    public class ZopimBannedIp : ConnectorEntityModel
    {

        public string ip_address { get; set; }
        public string reason { get; set; }
        public int id { get; set; }

    }
}
