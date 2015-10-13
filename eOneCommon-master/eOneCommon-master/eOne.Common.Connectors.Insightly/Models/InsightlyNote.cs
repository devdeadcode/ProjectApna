using System;
using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Insightly.Models
{
    public class InsightlyNote : DataConnectorEntityModel
    {

        public int? NOTE_ID { get; set; }
        public string TITLE { get; set; }
        public string BODY { get; set; }
        public int? LINK_SUBJECT_ID { get; set; }
        public string LINK_SUBJECT_TYPE { get; set; }
        public int? OWNER_USER_ID { get; set; }
        public DateTime DATE_CREATED_UTC { get; set; }
        public DateTime DATE_UPDATED_UTC { get; set; }
        public string VISIBLE_TO { get; set; }
        public int? VISIBLE_TEAM_ID { get; set; }
        public string VISIBLE_USER_IDS { get; set; }
        public List<InsightlyFileAttachment> FILE_ATTACHMENTS { get; set; }
        public List<InsightlyNoteLink> NOTELINKS { get; set; }

    }
}
