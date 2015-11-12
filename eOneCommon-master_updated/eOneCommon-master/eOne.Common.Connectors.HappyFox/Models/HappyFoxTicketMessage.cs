using System;

namespace eOne.Common.Connectors.HappyFox.Models
{
    public class HappyFoxTicketMessage : ConnectorEntityModel
    {

        [FieldSettings("Subject", DefaultField = true)]
        public string subject { get; set; }

        public HappyFoxTicketUpdate update { get; set; }

        [FieldSettings("Date", DefaultField = true)]
        public string date => update.update_date;

        [FieldSettings("Time")]
        public string time => update.time;

        [FieldSettings("Message")]
        public string message { get; set; }
        
        [FieldSettings("HTML message", DefaultField = true)]
        public string html { get; set; }

        public HappyFoxTicket ticket { get; set; }

        [FieldSettings("Ticket ID")]
        public int ticket_id => ticket.id;

        [FieldSettings("Ticket subject", DefaultField = true)]
        public string ticket_subject
        {
            get
            {
                try
                {
                    return ticket.subject;
                }
                catch
                {
                    return null;
                }
            }
        }
        

        public HappyFoxCategory category { get; set; }

        [FieldSettings("Ticket category")]
        public string ticket_category => category.name;

        [FieldSettings("Ticket description")]
        public string ticket_description => category.description;

        public HappyFoxStatus status { get; set; }

        [FieldSettings("Ticket status")]
        public string ticket_status => status.name;

        [FieldSettings("Ticket status type")]
        public string ticket_status_type => status.behavior;

        [FieldSettings("Time sepnd on ticket")]
        public string time_on_ticket => ticket.time_spent;

        [FieldSettings("Ticket due date")]
        public string ticket_due_date => ticket.due_date;

        public HappyFoxTicketPriority priority { get; set; }

        [FieldSettings("Ticket priority")]
        public string ticket_priority => priority.name;

        [FieldSettings("Ticket created date")]
        public string ticket_created_date => ticket.created_date;

        [FieldSettings("Ticket created time")]
        public string ticket_created_time => ticket.created_time;

        public HappyFoxContact contact { get; set; }

        [FieldSettings("Conatact name")]
        public string contact_name => contact.name;

        [FieldSettings("Contact email")]
        public string contact_email => contact.email;

        public HappyFoxTicketAssignedTo assignedTo { get; set; }

        [FieldSettings("Assigned to name")]
        public string assigend_to_name => assignedTo.name;

        [FieldSettings("Assigned to email")]
        public string assigned_to_email => assignedTo.email;

        public HappyFoxTicketCollection ticketCollection { get; set; }

        public HappyFoxTicketUpdate ticketUpdate { get; set; }

        #region Calculations
        [FieldSettings("Overdue")]
        public bool overdue
        {
            get
            {
                bool over_due = false;
                DateTime d1 = Convert.ToDateTime(ticket_due_date);
                DateTime d2 = DateTime.Now;
                if (d1 > d2) { over_due = true; }
                return over_due;
            }
        }

        [FieldSettings("Days overdue")]
        public int days_overdue
        {
            get
            {
                int num_of_days_overdue = 0;
                DateTime d1 = Convert.ToDateTime(ticket_due_date);
                DateTime d2 = DateTime.Now;
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
