using System;

namespace eOne.Common.Connectors.MailChimp.Models.Files
{
    public class MailChimpFile
    {

        public enum MailChimpFileType
        {
            image,
            file
        }

        public int id { get; set; }
        public int folder_id { get; set; }
        public MailChimpFileType type { get; set; }
        public string name { get; set; }
        public string full_size_url { get; set; }
        public string thumbnail_url { get; set; }
        public int size { get; set; }
        public DateTime created_at { get; set; }
        public string created_by { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string file_data { get; set; }

    }
}

