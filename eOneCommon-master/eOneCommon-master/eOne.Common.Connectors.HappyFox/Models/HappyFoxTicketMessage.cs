using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.HappyFox.Models
{
    public class HappyFoxTicketMessage : DataConnectorEntityModel
    {
        #region Default properties
        [FieldSettings("Subject", DefaultField = true)]
        public string subject { get; set; }

        [FieldSettings("Date", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public string date => update.update_date;

        [FieldSettings("Ticket subject", DefaultField = true)]
        public string ticket_subject => update.ticket.subject;
        #endregion

        #region General properties

        [FieldSettings("Time", FieldTypeId = DataConnector.FieldTypeIdTime)]
        public string time => update.update_time;

        [FieldSettings("Message")]
        public string text { get; set; }

        [FieldSettings("HTML message")]
        public string html { get; set; }
        
        [FieldSettings("Ticket ID", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int ticket_id => update.ticket.id;

        [FieldSettings("Ticket category")]
        public string ticket_category => update.ticket.category.name;

        [FieldSettings("Ticket description")]
        public string ticket_description => update.ticket.category.description;

        [FieldSettings("Ticket status")]
        public string ticket_status => update.ticket.status.name;

        [FieldSettings("Ticket status type")]
        public string ticket_status_type => update.ticket.status.behavior;

        [FieldSettings("Time sepnd on ticket")]
        public string time_on_ticket => update.ticket.time_spent;

        [FieldSettings("Ticket due date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public string ticket_due_date => update.ticket.due_date;

        [FieldSettings("Ticket priority")]
        public string ticket_priority => update.ticket.priority.name;

        [FieldSettings("Ticket created date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public string ticket_created_date => update.ticket.created_date;

        [FieldSettings("Ticket created time", FieldTypeId = DataConnector.FieldTypeIdTime)]
        public string ticket_created_time => update.ticket.created_time;

        [FieldSettings("Conatact name")]
        public string contact_name => update.ticket.user.name;

        [FieldSettings("Contact email", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string contact_email => update.ticket.user.email;

        [FieldSettings("Assigned to name")]
        public string assigend_to_name => update.ticket.assigned_to_name;

        [FieldSettings("Assigned to email", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string assigned_to_email => update.ticket.assigned_to_email;
        #endregion

        #region Hidden Properties
        public HappyFoxTicketUpdate update { get; set; }
        #endregion

        #region Calculations
        [FieldSettings("Overdue")]
        public bool overdue
        {
            get
            {
                var over_due = false;
                var d1 = Convert.ToDateTime(ticket_due_date);
                var d2 = DateTime.Now;
                if (d1 > d2) { over_due = true; }
                return over_due;
            }
        }

        [FieldSettings("Days overdue")]
        public int days_overdue
        {
            get
            {
                var num_of_days_overdue = 0;
                var d1 = Convert.ToDateTime(ticket_due_date);
                var d2 = DateTime.Now;
                if (d1 > d2)
                {
                    num_of_days_overdue = (d1.Subtract(d2)).Days;
                }
                return num_of_days_overdue;
            }
        }
        #endregion

    }
}
