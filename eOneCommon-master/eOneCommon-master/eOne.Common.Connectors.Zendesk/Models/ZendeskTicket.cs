using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Zendesk.Models
{
    public class ZendeskTicket : DataConnectorEntityModel
    {

        #region Enums

        public enum ZendeskTicketType
        {
            [Description("Incident")]
            incident,
            [Description("Problem")]
            problem,
            [Description("Question")]
            question,
            [Description("Task")]
            task
        }
        public enum ZendeskTicketPriority
        {
            [Description("Urgent")]
            urgent,
            [Description("High")]
            high,
            [Description("Normal")]
            normal,
            [Description("Low")]
            low
        }
        public enum ZendeskTicketStatus
        {
            [Description("New")]
            @new,
            [Description("Open")]
            open,
            [Description("Pending")]
            pending,
            [Description("Hold")]
            hold,
            [Description("Solved")]
            solved,
            [Description("Closed")]
            closed
        }

        #endregion

        #region Default properties

        [FieldSettings("Ticket ID", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int id { get; set; }

        [FieldSettings("Type", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(ZendeskTicketType))]
        public ZendeskTicketType type { get; set; }

        [FieldSettings("Priority", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(ZendeskTicketPriority))]
        public ZendeskTicketPriority priority { get; set; }

        [FieldSettings("Status", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(ZendeskTicketStatus))]
        public ZendeskTicketStatus status { get; set; }

        [FieldSettings("Recipient", DefaultField = true, SearchPriority = 3)]
        public string recipient { get; set; }

        [FieldSettings("Subject", DefaultField = true, SearchPriority = 4)]
        public string subject { get; set; }

        [FieldSettings("Description", DefaultField = true, SearchPriority = 4)]
        public string description { get; set; }

        [FieldSettings("Url", DefaultField = true, SearchPriority = 0, FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string url { get; set; }

        #endregion

        #region Properties

        [FieldSettings("External Id")]
        public string external_id { get; set; }

        [FieldSettings("Raw subject")]
        public string raw_subject { get; set; }

        [FieldSettings("Has incidents", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool has_incidents { get; set; }

        #endregion

        #region Hidden properties

        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public List<string> tags { get; set; }
        public DateTime? due_at { get; set; }
        public ZendeskTicketSatisfactionRating satisfaction_rating { get; set; }
        public List<ZendeskTicketCustomFieldValue> custom_fields { get; set; }
        public int requester_id { get; set; }
        public int submitter_id { get; set; }
        public int assignee_id { get; set; }
        public int organization_id { get; set; }
        public int group_id { get; set; }
        public int forum_topic_id { get; set; }
        public int problem_id { get; set; }
        public List<int> collaborator_ids { get; set; }
        public List<int> sharing_agreement_ids { get; set; }
        public ZendeskTicketVia via { get; set; }
        public ZendeskGroup group { get; set; }
        public ZendeskUser requester_user { get; set; }
        public ZendeskUser submitter_user { get; set; }
        public ZendeskUser assignee_user { get; set; }
        public List<ZendeskUser> collaborator_users { get; set; }
        public ZendeskOrganization organization { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Tags")]
        public string tag_list => CommaSeparatedValues(tags);

        [FieldSettings("Satisfaction rating comment")]
        public string satisfaction_rating_comment => satisfaction_rating.comment;

        [FieldSettings("Satisfaction rating score", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(ZendeskTicketSatisfactionRating.ZendeskTicketSatisfactionRatingScore))]
        public ZendeskTicketSatisfactionRating.ZendeskTicketSatisfactionRatingScore satisfaction_rating_score => satisfaction_rating.score;

        [FieldSettings("Group name")]
        public string group_name => @group == null ? string.Empty : @group.name;

        [FieldSettings("Requester name", SearchPriority = 5)]
        public string requester_user_name => requester_user == null ? string.Empty : requester_user.name;

        [FieldSettings("Requester email", SearchPriority = 3)]
        public string requester_user_email => requester_user == null ? string.Empty : requester_user.email;

        [FieldSettings("Requester phone")]
        public string requester_user_phone => requester_user == null ? string.Empty : requester_user.phone;

        [FieldSettings("Submitter name", SearchPriority = 5)]
        public string submitter_user_name => submitter_user == null ? string.Empty : submitter_user.name;

        [FieldSettings("Submitter email", SearchPriority = 3)]
        public string submitter_user_email => submitter_user == null ? string.Empty : submitter_user.email;

        [FieldSettings("Submitter phone")]
        public string submitter_user_phone => submitter_user == null ? string.Empty : submitter_user.phone;

        [FieldSettings("Assignee name", SearchPriority = 5)]
        public string assignee_user_name => assignee_user == null ? string.Empty : assignee_user.name;

        [FieldSettings("Assignee email", SearchPriority = 3)]
        public string assignee_user_email => assignee_user == null ? string.Empty : assignee_user.email;

        [FieldSettings("Assignee phone")]
        public string assignee_user_phone => assignee_user == null ? string.Empty : assignee_user.phone;

        [FieldSettings("Collaborators")]
        public string collaborator_list => CommaSeparatedValues((from user in collaborator_users where string.IsNullOrWhiteSpace(user.name) select user.name).ToList());

        [FieldSettings("Collaborators")]
        public string organization_name => organization == null ? string.Empty : organization.name;

        [FieldSettings("Due date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime due_at_date => due_at?.Date ?? DateTime.MinValue;

        [FieldSettings("Created at date", Created = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime created_at_date => created_at.Date;

        [FieldSettings("Created at time", Created = true, FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime created_at_time => created_at.Date;

        [FieldSettings("Updated date", Modified = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime updated_at_date => updated_at.Date;

        [FieldSettings("Updated time", Modified = true, FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime updated_at_time => Time(updated_at);

        [FieldSettings("Days overdue", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int days_overdue
        {
            get
            {
                if (due_at_date == DateTime.MinValue) return 0;
                return due_at_date <= DateTime.Today ? 0 : (DateTime.Today - due_at_date).Days;
            }
        }

        #endregion

    }
}
