namespace eOne.Common.Connectors.BaseCamp.Models
{
    public class BaseCampBucket
    {

        public enum BaseCampBucketType
        {
            Project
        }

        public BaseCampBucketType type { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string color { get; set; }
        public string url { get; set; }
        public string app_url { get; set; }

    }
}