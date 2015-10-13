using System;
using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Zendesk.Models
{
    public class ZendeskTicketAudit : DataConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Ticket ID", DefaultField = true)]
        public int ticket_id { get; set; }

        [FieldSettings("Subject", DefaultField = true)]
        public string subject { get; set; }

        #endregion

        //"metadata":  { "custom": { "time_spent": "3m22s" }, "system": { "ip_address": "184.106.40.75" }}

        #region Hidden properties

        public int id { get; set; }
        public DateTime created_at { get; set; }
        public ZendeskTicket ticket { get; set; }
        public ZendeskTicketVia via { get; set; }
        public List<ZendeskTicketAuditEvent> events { get; set; }
        public int author_id { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Ticket type", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(ZendeskTicket.ZendeskTicketType))]
        public ZendeskTicket.ZendeskTicketType ticket_type => ticket.type;

        [FieldSettings("Ticket priority", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(ZendeskTicket.ZendeskTicketPriority))]
        public ZendeskTicket.ZendeskTicketPriority ticket_priority => ticket.priority;

        [FieldSettings("Ticket status", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(ZendeskTicket.ZendeskTicketStatus))]
        public ZendeskTicket.ZendeskTicketStatus ticket_status => ticket.status;

        [FieldSettings("Ticket recipient", DefaultField = true)]
        public string ticket_recipient => ticket.recipient;

        [FieldSettings("Ticket subject", DefaultField = true)]
        public string ticket_subject => ticket.subject;

        [FieldSettings("Ticket description", DefaultField = true)]
        public string ticket_description => ticket.description;

        [FieldSettings("Ticket Url", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string ticket_url => ticket.url;

        [FieldSettings("Created at date", Created = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime created_at_date => created_at.Date;

        [FieldSettings("Created at time", Created = true, FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime created_at_time => created_at.Date;

        #endregion

    }
}