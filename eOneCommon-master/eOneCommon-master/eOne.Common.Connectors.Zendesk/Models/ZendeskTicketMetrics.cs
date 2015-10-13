using System;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Zendesk.Models
{
    public class ZendeskTicketMetrics : DataConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Ticket Id", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int ticket_id { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Number of groups", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int group_stations { get; set; }

        [FieldSettings("Number of assignees", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int assignee_stations { get; set; }

        [FieldSettings("Number of times reopened", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int reopens { get; set; }

        [FieldSettings("Number of replies", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int replies { get; set; }

        #endregion

        #region Hidden properties

        public int id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime assignee_updated_at { get; set; }
        public DateTime requester_updated_at { get; set; }
        public DateTime status_updated_at { get; set; }
        public DateTime initially_assigned_at { get; set; }
        public DateTime assigned_at { get; set; }
        public DateTime solved_at { get; set; }
        public DateTime latest_comment_added_at { get; set; }
        public ZendeskMinutesTaken reply_time_in_minutes { get; set; }
        public ZendeskMinutesTaken first_resolution_time_in_minutes { get; set; }
        public ZendeskMinutesTaken full_resolution_time_in_minutes { get; set; }
        public ZendeskMinutesTaken agent_wait_time_in_minutes { get; set; }
        public ZendeskMinutesTaken requester_wait_time_in_minutes { get; set; }
        public ZendeskMinutesTaken on_hold_time_in_minutes { get; set; }
        public ZendeskTicket ticket { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Ticket type", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(ZendeskTicket.ZendeskTicketPriority))]
        public ZendeskTicket.ZendeskTicketType ticket_type => ticket.type;

        [FieldSettings("Ticket priority", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(ZendeskTicket.ZendeskTicketPriority))]
        public ZendeskTicket.ZendeskTicketPriority ticket_priority => ticket.priority;

        [FieldSettings("Ticket status", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(ZendeskTicket.ZendeskTicketStatus))]
        public ZendeskTicket.ZendeskTicketStatus ticket_status => ticket.status;

        [FieldSettings("Ticket recipient")]
        public string ticket_recipient => ticket.recipient;

        [FieldSettings("Ticket subject")]
        public string ticket_subject => ticket.subject;

        [FieldSettings("Ticket description", DefaultField = true)]
        public string ticket_description => ticket.description;

        [FieldSettings("Ticket Url", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string ticket_url => ticket.url;

        [FieldSettings("Created at date", Created = true, DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime created_at_date => created_at.Date;

        [FieldSettings("Updated at date", Modified = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime updated_at_date => updated_at.Date;

        [FieldSettings("Assignee updated at date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime assignee_updated_at_date => assignee_updated_at.Date;

        [FieldSettings("Requester updated at date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime requester_updated_at_date => requester_updated_at.Date;

        [FieldSettings("Status updated at date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime status_updated_at_date => status_updated_at.Date;

        [FieldSettings("Initially assigned at date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime initially_assigned_at_date => initially_assigned_at.Date;

        [FieldSettings("Assigned at date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime assigned_at_date => assigned_at.Date;

        [FieldSettings("Solved at date", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime solved_at_date => solved_at.Date;

        [FieldSettings("Created at time", Created = true, FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime created_at_time => Time(created_at);

        [FieldSettings("Updated at time", Modified = true, FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime updated_at_time => Time(updated_at);

        [FieldSettings("Assignee updated at time", FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime assignee_updated_at_time => Time(assignee_updated_at);

        [FieldSettings("Requester updated at time", FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime requester_updated_at_time => Time(requester_updated_at);

        [FieldSettings("Status updated at time", FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime status_updated_at_time => Time(status_updated_at);

        [FieldSettings("Initially assigned at time", FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime initially_assigned_at_time => Time(initially_assigned_at);

        [FieldSettings("Assigned at time", FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime assigned_at_time => Time(assigned_at);

        [FieldSettings("Solved at time", FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime solved_at_time => Time(solved_at);

        [FieldSettings("Number of minutes to the first reply", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int reply_time_in_minutes_calendar => reply_time_in_minutes.calendar;

        [FieldSettings("Number of minutes to the first resolution time", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int first_resolution_time_in_minutes_calendar => first_resolution_time_in_minutes.calendar;

        [FieldSettings("Number of minutes to the full resolution", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int full_resolution_time_in_minutes_calendar => full_resolution_time_in_minutes.calendar;

        [FieldSettings("Number of minutes the agent spent waiting", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int agent_wait_time_in_minutes_calendar => agent_wait_time_in_minutes.calendar;

        [FieldSettings("Number of minutes spent on hold", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int requester_wait_time_in_minutes_calendar => requester_wait_time_in_minutes.calendar;

        [FieldSettings("Number of minutes spent on hold (total)", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int on_hold_time_in_minutes_calendar => on_hold_time_in_minutes.calendar;

        [FieldSettings("Number of minutes to the first reply (business hours)", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int reply_time_in_minutes_business => reply_time_in_minutes.business;

        [FieldSettings("Number of minutes to the first resolution (business hours)", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int first_resolution_time_in_minutes_business => first_resolution_time_in_minutes.business;

        [FieldSettings("Number of minutes to the full resolution (business hours)", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int full_resolution_time_in_minutes_business => full_resolution_time_in_minutes.business;

        [FieldSettings("Number of minutes the agent spent waiting (business hours)", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int agent_wait_time_in_minutes_business => agent_wait_time_in_minutes.business;

        [FieldSettings("Number of minutes the requester spent waiting  (business hours)", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int requester_wait_time_in_minutes_business => requester_wait_time_in_minutes.business;

        [FieldSettings("Number of minutes spent on hold (business hours)", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int on_hold_time_in_minutes_business => on_hold_time_in_minutes.business;

        #endregion

    }
}
