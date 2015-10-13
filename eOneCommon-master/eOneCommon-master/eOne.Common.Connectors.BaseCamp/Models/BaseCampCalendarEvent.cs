using System;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.BaseCamp.Models
{
    public class BaseCampCalendarEvent : DataConnectorEntityModel
    {

        [FieldSettings("Summary", DefaultField = true)]
        public string summary { get; set; }

        [FieldSettings("Creator name", DefaultField = true)]
        public string creator_name => creator.name;

        [FieldSettings("Starts at date", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime starts_at_date => starts_at.Date;

        [FieldSettings("Starts at time", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime starts_at_time => Time(starts_at);

        [FieldSettings("Ends at date", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime ends_at_date => ends_at.Date;

        [FieldSettings("Ends at time", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime ends_at_time => Time(ends_at);

        [FieldSettings("All day event", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool all_day { get; set; }

        [FieldSettings("Event ID", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int id { get; set; }

        [FieldSettings("Description")]
        public string description { get; set; }

        [FieldSettings("Number of comments", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int comments_count { get; set; }

        [FieldSettings("Private", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool @private { get; set; }

        [FieldSettings("Trashed", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool trashed { get; set; }

        [FieldSettings("Event URL", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string url { get; set; }

        [FieldSettings("Event Application URL", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string app_url { get; set; }

        [FieldSettings("Creator ID", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int creator_id => creator.id;

        [FieldSettings("Creator URL", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string creator_url => creator.url;

        [FieldSettings("Creator avatar", FieldTypeId = DataConnector.FieldTypeIdImage)]
        public string creator_fullsize_avatar_url => creator.fullsize_avatar_url;

        [FieldSettings("Created at date", FieldTypeId = DataConnector.FieldTypeIdDate, Created = true)]
        public DateTime created_at_date => created_at.Date;

        [FieldSettings("Created at time", FieldTypeId = DataConnector.FieldTypeIdTime, Created = true)]
        public DateTime created_at_time => Time(created_at);

        [FieldSettings("Updated at date", FieldTypeId = DataConnector.FieldTypeIdDate, Modified = true)]
        public DateTime updated_at_date => updated_at.Date;

        [FieldSettings("Updated at time", FieldTypeId = DataConnector.FieldTypeIdTime, Modified = true)]
        public DateTime updated_at_time => Time(updated_at);

        #region Hidden properties

        public BaseCampPerson creator { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime starts_at { get; set; }
        public DateTime ends_at { get; set; }

        #endregion

    }
}
