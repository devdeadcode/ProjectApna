using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace eOne.Common.Connectors.Zendesk.Models
{
    public class ZendeskTicket : ConnectorEntityModel
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

        [FieldSettings("Ticket ID", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger, KeyNumber = 1, Description = true)]
        public int id { get; set; }

        [FieldSettings("Type", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(ZendeskTicketType))]
        public ZendeskTicketType? type { get; set; }

        [FieldSettings("Priority", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(ZendeskTicketPriority))]
        public ZendeskTicketPriority? priority { get; set; }

        [FieldSettings("Status", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(ZendeskTicketStatus))]
        public ZendeskTicketStatus status { get; set; }

        [FieldSettings("Recipient", DefaultField = true, SearchPriority = 3)]
        public string recipient { get; set; }

        [FieldSettings("Subject", DefaultField = true, SearchPriority = 4)]
        public string subject { get; set; }

        [FieldSettings("Description", DefaultField = true, SearchPriority = 4)]
        public string description { get; set; }

        [FieldSettings("Link", DefaultField = true, SearchPriority = 0, FieldTypeId = Connector.FieldTypeIdUrl)]
        public string url { get; set; }

        #endregion

        #region Properties

        [FieldSettings("External ID")]
        public string external_id { get; set; }

        [FieldSettings("Raw subject")]
        public string raw_subject { get; set; }

        [FieldSettings("Has incidents", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool has_incidents { get; set; }

        [FieldSettings("Created at date", Created = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime created_at { get; set; }

        [FieldSettings("Updated date", Modified = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime updated_at { get; set; }

        [FieldSettings("Due date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? due_at { get; set; }

        #endregion

        #region Hidden properties

        public List<string> tags { get; set; }
        public ZendeskTicketSatisfactionRating satisfaction_rating { get; set; }
        public List<ZendeskTicketCustomFieldValue> custom_fields { get; set; }
        public int? requester_id { get; set; }
        public int? submitter_id { get; set; }
        public int? assignee_id { get; set; }
        public int? organization_id { get; set; }
        public int? group_id { get; set; }
        public int? forum_topic_id { get; set; }
        public int? problem_id { get; set; }
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
        public string satisfaction_rating_comment => satisfaction_rating == null ? string.Empty : satisfaction_rating.comment;

        [FieldSettings("Satisfaction rating score", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(ZendeskTicketSatisfactionRating.ZendeskTicketSatisfactionRatingScore))]
        public ZendeskTicketSatisfactionRating.ZendeskTicketSatisfactionRatingScore? satisfaction_rating_score => satisfaction_rating?.score;

        [FieldSettings("Group name")]
        public string group_name => @group == null ? string.Empty : @group.name;

        [FieldSettings("Requester name", SearchPriority = 5)]
        public string requester_user_name => requester_user == null ? string.Empty : requester_user.name;

        [FieldSettings("Requester email", SearchPriority = 3, FieldTypeId = Connector.FieldTypeIdEmail)]
        public string requester_user_email => requester_user == null ? string.Empty : requester_user.email;

        [FieldSettings("Requester phone", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string requester_user_phone => requester_user == null ? string.Empty : requester_user.phone;

        [FieldSettings("Submitter name", SearchPriority = 5)]
        public string submitter_user_name => submitter_user == null ? string.Empty : submitter_user.name;

        [FieldSettings("Submitter email", SearchPriority = 3, FieldTypeId = Connector.FieldTypeIdEmail)]
        public string submitter_user_email => submitter_user == null ? string.Empty : submitter_user.email;

        [FieldSettings("Submitter phone", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string submitter_user_phone => submitter_user == null ? string.Empty : submitter_user.phone;

        [FieldSettings("Assignee name", SearchPriority = 5)]
        public string assignee_user_name => assignee_user == null ? string.Empty : assignee_user.name;

        [FieldSettings("Assignee email", SearchPriority = 3, FieldTypeId = Connector.FieldTypeIdEmail)]
        public string assignee_user_email => assignee_user == null ? string.Empty : assignee_user.email;

        [FieldSettings("Assignee phone", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string assignee_user_phone => assignee_user == null ? string.Empty : assignee_user.phone;

        [FieldSettings("Collaborators")]
        public string collaborator_list => collaborator_users == null ? string.Empty : CommaSeparatedValues((from user in collaborator_users where string.IsNullOrWhiteSpace(user.name) select user.name).ToList());

        [FieldSettings("Organization name")]
        public string organization_name => organization == null ? string.Empty : organization.name;

        [FieldSettings("Created at time", Created = true, FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime created_at_time => created_at;

        [FieldSettings("Updated time", Modified = true, FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime updated_at_time => updated_at;

        [FieldSettings("Days overdue", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int days_overdue
        {
            get
            {
                if (due_at == null || due_at <= DateTime.Today) return 0;
                return ((TimeSpan)(DateTime.Today - due_at)).Days;
            }
        }

        #endregion

    }
}
