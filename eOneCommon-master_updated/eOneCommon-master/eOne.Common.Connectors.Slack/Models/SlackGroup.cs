using System.Collections.Generic;

namespace eOne.Common.Connectors.Slack.Models
{
    public class SlackGroup : ConnectorEntityModel
    {

        [FieldSettings("Name", DefaultField = true, Description = true)]
        public string name { get; set; }

        [FieldSettings("Number of members", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int number_of_members => members?.Count ?? 0;

        [FieldSettings("Group ID", KeyNumber = 1)]
        public string id { get; set; }

        [FieldSettings("Archived", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool is_archived { get; set; }

        public string creator { get; set; }

        public List<string> members { get; set; }
        public long created { get; set; }
        public SlackDescription topic { get; set; }
        public SlackDescription purpose { get; set; }

        [FieldSettings("Topic")]
        public string topic_value => topic == null ? string.Empty : topic.value;

        [FieldSettings("Purpose")]
        public string purpose_value => purpose == null ? string.Empty : purpose.value;

        

    }
}
