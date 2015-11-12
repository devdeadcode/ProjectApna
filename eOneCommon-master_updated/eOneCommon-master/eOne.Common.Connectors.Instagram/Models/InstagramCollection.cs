namespace eOne.Common.Connectors.Instagram.Models
{
    public class InstagramCollection : ConnectorEntityModel
    {

        public InstagramPagination pagination { get; set; }
        public InstagramMeta meta { get; set; }

    }
}
