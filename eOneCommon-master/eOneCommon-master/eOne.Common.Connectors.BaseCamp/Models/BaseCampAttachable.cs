namespace eOne.Common.Connectors.BaseCamp.Models
{
    public class BaseCampAttachable
    {

        public enum BaseCampAttachableType
        {
            Upload
        }

        public int id { get; set; }
        public BaseCampAttachableType type { get; set; }
        public string url { get; set; }
        public string app_url { get; set; }

    }
}