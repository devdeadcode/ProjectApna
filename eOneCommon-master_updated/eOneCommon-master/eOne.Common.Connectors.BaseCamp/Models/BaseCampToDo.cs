using System;

namespace eOne.Common.Connectors.BaseCamp.Models
{
    public class BaseCampToDo : ConnectorEntityModel
    {

        [FieldSettings("To do list name", DefaultField = true)]
        public string todolist_name => todolist.name;

        [FieldSettings("Assigned to", DefaultField = true)]
        public string assignee_name => assignee.name;

        [FieldSettings("Description", DefaultField = true)]
        public string content { get; set; }

        [FieldSettings("Due date", DefaultField = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? due_at { get; set; }

        [FieldSettings("Completed", DefaultField = true, FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool completed { get; set; }

        [FieldSettings("Item ID", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int id { get; set; }

        [FieldSettings("To do list ID", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int todolist_id { get; set; }

        [FieldSettings("Position", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int position { get; set; }

        [FieldSettings("Number of comments", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int comments_count { get; set; }

        [FieldSettings("Private", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool @private { get; set; }

        [FieldSettings("Trashed", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool trashed { get; set; }

        [FieldSettings("Item URL", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string url { get; set; }

        [FieldSettings("Item application URL", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string app_url { get; set; }

        [FieldSettings("Creator ID", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int creator_id => creator.id;

        [FieldSettings("Creator name")]
        public string creator_name => creator.name;

        [FieldSettings("Creator URL", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string creator_url => creator.url;

        [FieldSettings("Creator avatar", FieldTypeId = Connector.FieldTypeIdImage)]
        public string creator_fullsize_avatar_url => creator.fullsize_avatar_url;

        [FieldSettings("To do list creator ID", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int todolist_creator_id => todolist.creator.id;

        [FieldSettings("To do list creator name")]
        public string todolist_creator_name => todolist.creator.name;

        [FieldSettings("To do list creator URL", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string todolist_creator_url => todolist.creator.url;

        [FieldSettings("To do list creator avatar", FieldTypeId = Connector.FieldTypeIdImage)]
        public string todolist_creator_fullsize_avatar_url => todolist.creator.fullsize_avatar_url;

        [FieldSettings("To do list description")]
        public string todolist_description => todolist.description;

        [FieldSettings("To do list URL", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string todolist_url => todolist.url;

        [FieldSettings("To do list application URL", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string todolist_app_url => todolist.app_url;

        [FieldSettings("To do list completed", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool todolist_completed => todolist.completed;

        [FieldSettings("To do list position", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int todolist_position => todolist.position;

        [FieldSettings("To do list private", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool todolist_private => todolist.@private;

        [FieldSettings("To do list trashed", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool todolist_trashed => todolist.trashed;

        [FieldSettings("Number of completed items in to do list", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int todolist_completed_count => todolist.completed_count;

        [FieldSettings("Number of remaining items in to do list", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int todolist_remaining_count => todolist.remaining_count;

        [FieldSettings("Assignee ID", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int assignee_id => assignee.id;

        [FieldSettings("Assignee type", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(BaseCampAssignee.BaseCampAssigneeType))]
        public BaseCampAssignee.BaseCampAssigneeType assignee_type => assignee.type;

        [FieldSettings("Due at time", FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime due_at_time => due_at ?? DateTime.MinValue;

        [FieldSettings("Due on time", FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime due_on_time => due_on ?? DateTime.MinValue;

        [FieldSettings("Completed at time", FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime completed_at_time => completed_at ?? DateTime.MinValue;

        [FieldSettings("Created at time", FieldTypeId = Connector.FieldTypeIdTime, Created = true)]
        public DateTime created_at_time => created_at;

        [FieldSettings("Updated at time", FieldTypeId = Connector.FieldTypeIdTime, Modified = true)]
        public DateTime updated_at_time => updated_at;

        [FieldSettings("To do list created at date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime todolist_created_at_date => todolist.created_at;

        [FieldSettings("To do list updated at date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime todolist_updated_at_date => todolist.updated_at;

        [FieldSettings("To do list created at time", FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime todolist_created_at_time => todolist.created_at_time;

        [FieldSettings("To do list updated at time", FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime todolist_updated_at_time => todolist.updated_at_time;

        [FieldSettings("Due on date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? due_on { get; set; }

        [FieldSettings("Created at date", FieldTypeId = Connector.FieldTypeIdDate, Created = true)]
        public DateTime created_at { get; set; }

        [FieldSettings("Updated at date", FieldTypeId = Connector.FieldTypeIdDate, Modified = true)]
        public DateTime updated_at { get; set; }

        [FieldSettings("Completed at date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? completed_at { get; set; }

        #region Hidden properties

        public BaseCampPerson creator { get; set; }
        public BaseCampToDoList todolist { get; set; }
        public BaseCampAssignee assignee { get; set; }

        #endregion

    }
}
