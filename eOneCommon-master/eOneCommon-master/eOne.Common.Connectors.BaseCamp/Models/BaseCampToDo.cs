using System;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.BaseCamp.Models
{
    public class BaseCampToDo : DataConnectorEntityModel
    {

        [FieldSettings("To do list name", DefaultField = true)]
        public string todolist_name => todolist.name;

        [FieldSettings("Assigned to", DefaultField = true)]
        public string assignee_name => assignee.name;

        [FieldSettings("Description", DefaultField = true)]
        public string content { get; set; }

        [FieldSettings("Due date", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime due_at_date => due_at?.Date ?? DateTime.MinValue;

        [FieldSettings("Completed", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool completed { get; set; }

        [FieldSettings("Item ID", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int id { get; set; }

        [FieldSettings("To do list ID", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int todolist_id { get; set; }

        [FieldSettings("Position", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int position { get; set; }

        [FieldSettings("Number of comments", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int comments_count { get; set; }

        [FieldSettings("Private", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool @private { get; set; }

        [FieldSettings("Trashed", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool trashed { get; set; }

        [FieldSettings("Item URL", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string url { get; set; }

        [FieldSettings("Item application URL", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string app_url { get; set; }

        [FieldSettings("Creator ID", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int creator_id => creator.id;

        [FieldSettings("Creator name")]
        public string creator_name => creator.name;

        [FieldSettings("Creator URL", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string creator_url => creator.url;

        [FieldSettings("Creator avatar", FieldTypeId = DataConnector.FieldTypeIdImage)]
        public string creator_fullsize_avatar_url => creator.fullsize_avatar_url;

        [FieldSettings("To do list creator ID", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int todolist_creator_id => todolist.creator.id;

        [FieldSettings("To do list creator name")]
        public string todolist_creator_name => todolist.creator.name;

        [FieldSettings("To do list creator URL", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string todolist_creator_url => todolist.creator.url;

        [FieldSettings("To do list creator avatar", FieldTypeId = DataConnector.FieldTypeIdImage)]
        public string todolist_creator_fullsize_avatar_url => todolist.creator.fullsize_avatar_url;

        [FieldSettings("To do list description")]
        public string todolist_description => todolist.description;

        [FieldSettings("To do list URL", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string todolist_url => todolist.url;

        [FieldSettings("To do list application URL", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string todolist_app_url => todolist.app_url;

        [FieldSettings("To do list completed", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool todolist_completed => todolist.completed;

        [FieldSettings("To do list position", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int todolist_position => todolist.position;

        [FieldSettings("To do list private", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool todolist_private => todolist.@private;

        [FieldSettings("To do list trashed", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool todolist_trashed => todolist.trashed;

        [FieldSettings("Number of completed items in to do list", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int todolist_completed_count => todolist.completed_count;

        [FieldSettings("Number of remaining items in to do list", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int todolist_remaining_count => todolist.remaining_count;

        [FieldSettings("Assignee ID", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int assignee_id => assignee.id;

        [FieldSettings("Assignee type", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(BaseCampAssignee.BaseCampAssigneeType))]
        public BaseCampAssignee.BaseCampAssigneeType assignee_type => assignee.type;

        [FieldSettings("Due at time", FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime due_at_time => due_at.HasValue ? Time(due_at.Value) : DateTime.MinValue;

        [FieldSettings("Due on date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime due_on_date => due_on?.Date ?? DateTime.MinValue;

        [FieldSettings("Due on time", FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime due_on_time => due_on.HasValue ? Time(due_on.Value) : DateTime.MinValue;

        [FieldSettings("Completed at date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime completed_at_date => completed_at?.Date ?? DateTime.MinValue;

        [FieldSettings("Completed at time", FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime completed_at_time => completed_at.HasValue ? Time(completed_at.Value) : DateTime.MinValue;

        [FieldSettings("Created at date", FieldTypeId = DataConnector.FieldTypeIdDate, Created = true)]
        public DateTime created_at_date => created_at.Date;

        [FieldSettings("Created at time", FieldTypeId = DataConnector.FieldTypeIdTime, Created = true)]
        public DateTime created_at_time => Time(created_at);

        [FieldSettings("Updated at date", FieldTypeId = DataConnector.FieldTypeIdDate, Modified = true)]
        public DateTime updated_at_date => updated_at.Date;

        [FieldSettings("Updated at time", FieldTypeId = DataConnector.FieldTypeIdTime, Modified = true)]
        public DateTime updated_at_time => Time(updated_at);

        [FieldSettings("To do list created at date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime todolist_created_at_date => todolist.created_at_date;

        [FieldSettings("To do list updated at date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime todolist_updated_at_date => todolist.updated_at_date;

        [FieldSettings("To do list created at time", FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime todolist_created_at_time => todolist.created_at_time;

        [FieldSettings("To do list updated at time", FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime todolist_updated_at_time => todolist.updated_at_time;

        #region Hidden properties

        public BaseCampPerson creator { get; set; }
        public BaseCampToDoList todolist { get; set; }
        public BaseCampAssignee assignee { get; set; }
        public DateTime? due_at { get; set; }
        public DateTime? due_on { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime? completed_at { get; set; }

        #endregion

    }
}
