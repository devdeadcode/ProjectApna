using System;

namespace eOne.Common.Connectors.Zendesk.Models
{
    public class ZendeskTicketMetric : ConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Ticket ID", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger, KeyNumber = 1)]
        public int ticket_id { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Number of groups", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int group_stations { get; set; }

        [FieldSettings("Number of assignees", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int assignee_stations { get; set; }

        [FieldSettings("Number of times reopened", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int reopens { get; set; }

        [FieldSettings("Number of replies", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int replies { get; set; }

        [FieldSettings("Created at date", Created = true, DefaultField = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime created_at { get; set; }

        [FieldSettings("Updated at date", Modified = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime updated_at { get; set; }

        [FieldSettings("Assignee updated at date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime assignee_updated_at { get; set; }

        [FieldSettings("Requester updated at date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime requester_updated_at { get; set; }

        [FieldSettings("Status updated at date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime status_updated_at { get; set; }

        [FieldSettings("Initially assigned at date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime initially_assigned_at { get; set; }

        [FieldSettings("Assigned at date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime assigned_at { get; set; }

        [FieldSettings("Solved at date", DefaultField = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? solved_at { get; set; }

        [FieldSettings("Last comment added at date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime latest_comment_added_at { get; set; }

        #endregion

        #region Hidden properties

        public int id { get; set; }
        public ZendeskMinutesTaken reply_time_in_minutes { get; set; }
        public ZendeskMinutesTaken first_resolution_time_in_minutes { get; set; }
        public ZendeskMinutesTaken full_resolution_time_in_minutes { get; set; }
        public ZendeskMinutesTaken agent_wait_time_in_minutes { get; set; }
        public ZendeskMinutesTaken requester_wait_time_in_minutes { get; set; }
        public ZendeskMinutesTaken on_hold_time_in_minutes { get; set; }
        public ZendeskTicket ticket { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Ticket type", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(ZendeskTicket.ZendeskTicketPriority))]
        public ZendeskTicket.ZendeskTicketType? ticket_type => ticket?.type;

        [FieldSettings("Ticket priority", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(ZendeskTicket.ZendeskTicketPriority))]
        public ZendeskTicket.ZendeskTicketPriority? ticket_priority => ticket?.priority;

        [FieldSettings("Ticket status", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(ZendeskTicket.ZendeskTicketStatus))]
        public ZendeskTicket.ZendeskTicketStatus? ticket_status => ticket?.status;

        [FieldSettings("Ticket recipient")]
        public string ticket_recipient => ticket?.recipient;

        [FieldSettings("Ticket subject")]
        public string ticket_subject => ticket?.subject;

        [FieldSettings("Ticket description", DefaultField = true)]
        public string ticket_description => ticket?.description;

        [FieldSettings("Ticket link", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string ticket_url => ticket?.url;
        
        [FieldSettings("Created at time", Created = true, FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime created_at_time => created_at;

        [FieldSettings("Updated at time", Modified = true, FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime updated_at_time => updated_at;

        [FieldSettings("Assignee updated at time", FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime assignee_updated_at_time => assignee_updated_at;

        [FieldSettings("Requester updated at time", FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime requester_updated_at_time => requester_updated_at;

        [FieldSettings("Status updated at time", FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime status_updated_at_time => status_updated_at;

        [FieldSettings("Initially assigned at time", FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime initially_assigned_at_time => initially_assigned_at;

        [FieldSettings("Assigned at time", FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime assigned_at_time => assigned_at;

        [FieldSettings("Solved at time", FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime solved_at_time => solved_at ?? DateTime.MinValue;

        [FieldSettings("Number of minutes to the first reply", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int reply_time_in_minutes_calendar => reply_time_in_minutes.calendar ?? 0;

        [FieldSettings("Number of minutes to the first resolution time", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int first_resolution_time_in_minutes_calendar => first_resolution_time_in_minutes.calendar ?? 0;

        [FieldSettings("Number of minutes to the full resolution", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int full_resolution_time_in_minutes_calendar => full_resolution_time_in_minutes.calendar ?? 0;

        [FieldSettings("Number of minutes the agent spent waiting", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int agent_wait_time_in_minutes_calendar => agent_wait_time_in_minutes.calendar ?? 0;

        [FieldSettings("Number of minutes spent on hold", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int requester_wait_time_in_minutes_calendar => requester_wait_time_in_minutes.calendar ?? 0;

        [FieldSettings("Number of minutes spent on hold (total)", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int on_hold_time_in_minutes_calendar => on_hold_time_in_minutes.calendar ?? 0;

        [FieldSettings("Number of minutes to the first reply (business hours)", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int reply_time_in_minutes_business => reply_time_in_minutes.business ?? 0;

        [FieldSettings("Number of minutes to the first resolution (business hours)", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int first_resolution_time_in_minutes_business => first_resolution_time_in_minutes.business ?? 0;

        [FieldSettings("Number of minutes to the full resolution (business hours)", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int full_resolution_time_in_minutes_business => full_resolution_time_in_minutes.business ?? 0;

        [FieldSettings("Number of minutes the agent spent waiting (business hours)", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int agent_wait_time_in_minutes_business => agent_wait_time_in_minutes.business ?? 0;

        [FieldSettings("Number of minutes the requester spent waiting  (business hours)", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int requester_wait_time_in_minutes_business => requester_wait_time_in_minutes.business ?? 0;

        [FieldSettings("Number of minutes spent on hold (business hours)", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int on_hold_time_in_minutes_business => on_hold_time_in_minutes.business ?? 0;

        #endregion

    }
}
