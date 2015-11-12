namespace eOne.Common.Connectors.Instagram.Models
{
    public class InstagramCaption : ConnectorEntityModel
    {

        public int created_time { get; set; }
        public string text { get; set; }
        public string id { get; set; }
        public InstagramUser from { get; set; }

    }
}