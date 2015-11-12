using System;
using System.Collections.Generic;

namespace eOne.Common.Connectors.Zendesk.Models
{
    public class ZendeskOrganization : ConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Name", DefaultField = true)]
        public string name { get; set; }

        [FieldSettings("URL", DefaultField = true)]
        public string url { get; set; }

        [FieldSettings("Details", DefaultField = true)]
        public string details { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Notes")]
        public string notes { get; set; }

        [FieldSettings("Shared tickets", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool shared_tickets { get; set; }

        [FieldSettings("Shared comments", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool shared_comments { get; set; }

        [FieldSettings("Created at date", Created = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime created_at { get; set; }

        [FieldSettings("Updated at date", Modified = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime updated_at { get; set; }

        #endregion

        #region Hidden properties

        public int? group_id { get; set; }
        public int id { get; set; }
        public string external_id { get; set; }
        public List<string> domain_names { get; set; }
        public List<string> tags { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Created at time", Created = true, FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime created_at_time => created_at;

        [FieldSettings("Updated at time", Modified = true, FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime updated_at_time => updated_at;

        [FieldSettings("Domain names")]
        public string domain_name_list => CommaSeparatedValues(domain_names);

        [FieldSettings("Tags")]
        public string tag_list => CommaSeparatedValues(tags);

        #endregion

    }
}
