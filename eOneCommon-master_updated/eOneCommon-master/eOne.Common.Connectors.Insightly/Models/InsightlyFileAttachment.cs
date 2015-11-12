using System;

namespace eOne.Common.Connectors.Insightly.Models
{
    public class InsightlyFileAttachment : ConnectorEntityModel
    {

        public int? FILE_ID { get; set; }
        public string FILE_NAME { get; set; }
        public string CONTENT_TYPE { get; set; }
        public int? FILE_SIZE { get; set; }
        public int? FILE_CATEGORY_ID { get; set; }
        public int? OWNER_USER_ID { get; set; }
        public DateTime DATE_CREATED_UTC { get; set; }
        public DateTime DATE_UPDATED_UTC { get; set; }
        public string URL { get; set; }

    }
}
