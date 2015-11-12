namespace eOne.Common.Connectors.Instagram.Models
{
    public class InstagramComment
    {

        public int created_time { get; set; }
        public string text { get; set; }
        public InstagramUser from { get; set; }
        public string id { get; set; }

    }
}