using System;
using System.Collections.Generic;

namespace eOne.Common.Connectors.Zendesk.Models
{
    public class ZendeskTicketAudit : ConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Ticket ID", DefaultField = true)]
        public int ticket_id { get; set; }

        [FieldSettings("Subject", DefaultField = true)]
        public string subject { get; set; }

        #endregion

        //"metadata":  { "custom": { "time_spent": "3m22s" }, "system": { "ip_address": "184.106.40.75" }}

        #region Properties

        [FieldSettings("Created at date", Created = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime created_at { get; set; }

        #endregion

        #region Hidden properties

        public int id { get; set; }
        
        public ZendeskTicket ticket { get; set; }
        public ZendeskTicketVia via { get; set; }
        public List<ZendeskTicketAuditEvent> events { get; set; }
        public int author_id { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Ticket type", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(ZendeskTicket.ZendeskTicketType))]
        public ZendeskTicket.ZendeskTicketType? ticket_type => ticket.type;

        [FieldSettings("Ticket priority", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(ZendeskTicket.ZendeskTicketPriority))]
        public ZendeskTicket.ZendeskTicketPriority? ticket_priority => ticket.priority;

        [FieldSettings("Ticket status", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(ZendeskTicket.ZendeskTicketStatus))]
        public ZendeskTicket.ZendeskTicketStatus ticket_status => ticket.status;

        [FieldSettings("Ticket recipient", DefaultField = true)]
        public string ticket_recipient => ticket.recipient;

        [FieldSettings("Ticket subject", DefaultField = true)]
        public string ticket_subject => ticket.subject;

        [FieldSettings("Ticket description", DefaultField = true)]
        public string ticket_description => ticket.description;

        [FieldSettings("Ticket link", DefaultField = true, FieldTypeId = Connector.FieldTypeIdUrl)]
        public string ticket_url => ticket.url;

        [FieldSettings("Created at time", Created = true, FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime created_at_time => created_at;

        #endregion

    }
}