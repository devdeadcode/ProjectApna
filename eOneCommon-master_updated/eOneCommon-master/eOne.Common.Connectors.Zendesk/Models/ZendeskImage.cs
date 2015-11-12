namespace eOne.Common.Connectors.Zendesk.Models
{
    public class ZendeskImage : ConnectorEntityModel
    {

        public int id { get; set; }
        public string name { get; set; }
        public string file_name { get; set; }
        public string content_url { get; set; }
        public string content_type { get; set; }
        public int size { get; set; }
        public string url { get; set; }
        public string mapped_content_url { get; set; }

    }
}