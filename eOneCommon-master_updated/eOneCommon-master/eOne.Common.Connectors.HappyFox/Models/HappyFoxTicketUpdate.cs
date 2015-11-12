using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace eOne.Common.Connectors.HappyFox.Models
{
    public class HappyFoxTicketUpdate : ConnectorEntityModel
    {
        
        public enum HappyFoxTicketUpdateType
        {
            [Description("Change priority")]
            CHANGE_PRIORITY,
            [Description("Change category")]
            CHANGE_CATEGORY,
            [Description("Change duedate")]
            CHANGE_DUEDATE,
            [Description("Change status")]
            CHANGE_STATUS,
            [Description("Change user")]
            ASSIGN_USER
        }

        public enum HappyFoxTicketUpdateBy
        {
            [Description("User")]
            USER,
            [Description("Smartrul")]
            SMARTRUL
        }

        [FieldSettings("Update date", DefaultField = true)]
        public string update_date { get; set; }

        [FieldSettings("Update time")]
        public string update_time { get; set; }

        //public HappyFoxUpdatedBy MyProperty { get; set; }
        public HappyFoxUpdatedCategoryChanges category_change { get; set; }

        [FieldSettings("From category")]
        public string from_category => category_change.from_category;

        [FieldSettings("To category")]
        public string to_category => category_change.to_category;

        public HappyFoxUpdatedStatusChange status_change { get; set; }

        [FieldSettings("From status")]
        public string from_status => status_change.from_status;
        
        [FieldSettings("To status")]
        public string to_status => status_change.to_status;

        public HappyFoxUpdatedAssigneeChange assignee_change { get; set; }

        [FieldSettings("Assigned from")]
        public string assigned_from => assignee_change.assigned_from;

        [FieldSettings("Assgined to")]
        public string assigned_to => assignee_change.assigned_to;

        public HappyFoxUpdatedPriorityChange priority_change { get; set; }

        [FieldSettings("From priority")]
        public string from_priority => priority_change.from_priority;

        [FieldSettings("To priority")]
        public string to_priority => priority_change.to_priority;
        
        public HappyFoxUpdatedDueDateChange due_date_change { get; set; }

        [FieldSettings("From due date")]
        public string from_due_date => due_date_change.from_due_date;

        [FieldSettings("To due date")]
        public string to_due_date => due_date_change.to_due_date;

        public HappyFoxTicket ticket { get; set; }

        [FieldSettings("Ticket ID")]
        public int ticket_id => ticket.id;

        [FieldSettings("Ticket subject", DefaultField = true)]
        public string ticket_subject => ticket.subject;

        public HappyFoxCategory category { get; set; }

        [FieldSettings("Ticket category")]
        public string ticket_catetgory => category.name;

        [FieldSettings("Ticket category description")]
        public string ticket_cat_description => category.description;

        public HappyFoxStatus status { get; set; }

        [FieldSettings("Ticket status")]
        public string ticket_status => status.name;

        [FieldSettings("Ticket status type")]
        public string status_type => status.behavior;

        [FieldSettings("Time spent on ticket")]
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

        [FieldSettings("Contact name")]
        public string contact_name => contact.name;

        [FieldSettings("Contact email")]
        public string contact_email => contact.email;

        [FieldSettings("Assigned to name")]
        public string assigned_to_name => ticket.assigned_to_name;

        [FieldSettings("Assigned to email")]
        public string assigned_to_email => ticket.assigned_to_email;

        #region Hidden properites
        public string timestamp { get; set; }

        public string time { get; set; }

        public string due_date => ticket.due_date;

        public HappyFoxTicketMessage message { get; set; }

        public List<HappyFoxTicketMessage> messages { get; set; }

        public List<HappyFoxUpdatedBy> updated_by { get; set; }

        public HappyFoxTicketCollection ticketcollection { get; set; }
        #endregion

        #region Calculations
        [FieldSettings("Update type", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(HappyFoxTicketUpdateType))]
        public HappyFoxTicketUpdateType update_type { get; set; }

        [FieldSettings("Update by", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(HappyFoxTicketUpdateType))]
        public HappyFoxTicketUpdateBy update_by => updated_by_smartrule ? HappyFoxTicketUpdateBy.SMARTRUL : HappyFoxTicketUpdateBy.USER;

        [FieldSettings("Overdue")]
        public bool overdue
        {
            get
            {
                bool over_due = false;
                DateTime d1 = Convert.ToDateTime(due_date);
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
                DateTime d1 = Convert.ToDateTime(due_date);
                DateTime d2 = DateTime.Now;
                if (d1 > d2)
                {
                    num_of_days_overdue = (d1.Subtract(d2)).Days;
                }
                return num_of_days_overdue;
            }
        }

        [FieldSettings("Updated by user")]
        public bool updated_by_user {
            get {
                bool updatedby_user = false;
                foreach (HappyFoxUpdatedBy t in updated_by.Where(t => t.type == "user"))
                {
                    updatedby_user = true;
                }
                return updatedby_user;
            }
        }

        [FieldSettings("Updated by smartrule")]
        public bool updated_by_smartrule{
            get{
                bool updatedby_smartrule = false;
                foreach (HappyFoxUpdatedBy t in updated_by)
                {
                    if (t.type == "smartrule") { updatedby_smartrule = true; }
                }
                return updatedby_smartrule;
            }
        }
        #endregion
    }
}
