using System;

namespace eOne.Common.Connectors.BaseCamp.Models
{
    public class BaseCampCalendarEvent : ConnectorEntityModel
    {

        [FieldSettings("Summary", DefaultField = true)]
        public string summary { get; set; }

        [FieldSettings("Creator name", DefaultField = true)]
        public string creator_name => creator.name;

        [FieldSettings("Starts at date", DefaultField = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime starts_at { get; set; }

        [FieldSettings("Starts at time", DefaultField = true, FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime starts_at_time => starts_at;

        [FieldSettings("Ends at date", DefaultField = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime ends_at { get; set; }

        [FieldSettings("Ends at time", DefaultField = true, FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime ends_at_time => ends_at;

        [FieldSettings("All day event", DefaultField = true, FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool all_day { get; set; }

        [FieldSettings("Event ID", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int id { get; set; }

        [FieldSettings("Description")]
        public string description { get; set; }

        [FieldSettings("Number of comments", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int comments_count { get; set; }

        [FieldSettings("Private", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool @private { get; set; }

        [FieldSettings("Trashed", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool trashed { get; set; }

        [FieldSettings("Event URL", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string url { get; set; }

        [FieldSettings("Event Application URL", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string app_url { get; set; }

        [FieldSettings("Creator ID", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int creator_id => creator.id;

        [FieldSettings("Creator URL", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string creator_url => creator.url;

        [FieldSettings("Creator avatar", FieldTypeId = Connector.FieldTypeIdImage)]
        public string creator_fullsize_avatar_url => creator.fullsize_avatar_url;

        [FieldSettings("Created at date", FieldTypeId = Connector.FieldTypeIdDate, Created = true)]
        public DateTime created_at { get; set; }

        [FieldSettings("Created at time", FieldTypeId = Connector.FieldTypeIdTime, Created = true)]
        public DateTime created_at_time => created_at;

        [FieldSettings("Updated at date", FieldTypeId = Connector.FieldTypeIdDate, Modified = true)]
        public DateTime updated_at { get; set; }

        [FieldSettings("Updated at time", FieldTypeId = Connector.FieldTypeIdTime, Modified = true)]
        public DateTime updated_at_time => updated_at;

        #region Hidden properties

        public BaseCampPerson creator { get; set; }

        #endregion

    }
}
