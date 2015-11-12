using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.HappyFox.Models
{
    public class HappyFoxTicketUpdate : DataConnectorEntityModel
    {
        
        public enum HappyFoxTicketUpdateType
        {
            [FieldSettings("Change category")]
            CHANGE_CATEGORY,
            [FieldSettings("Change priority")]
            CHANGE_PRIORITY,
            [FieldSettings("Change duedate")]
            CHANGE_DUEDATE,
            [FieldSettings("Change status")]
            CHANGE_STATUS,
            [FieldSettings("Assign user")]
            ASSIGN_USER,
            None
        }

        public enum HappyFoxTicketUpdateBy
        {
            [Description("User")]
            USER,
            [Description("Smartrul")]
            SMARTRUL,
            [Description("Staff")]
            STAFF
        }

        #region Update properties
        
        [FieldSettings("Update date", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public string update_date => timestamp?.Substring(0,10) ?? string.Empty;

        [FieldSettings("Update time", FieldTypeId = DataConnector.FieldTypeIdTime)]
        public string update_time => timestamp?.Substring(11) ?? string.Empty;

        //public HappyFoxUpdatedBy MyProperty { get; set; }
        public HappyFoxUpdatedCategoryChanges category_change { get; set; }

        [FieldSettings("From category")]
        public string from_category => category_change == null ? string.Empty : category_change.old;

        [FieldSettings("To category")]
        public string to_category => category_change == null ? string.Empty : category_change.New;

        public HappyFoxUpdatedStatusChange status_change { get; set; }

        [FieldSettings("From status")]
        public string from_status => status_change == null ? string.Empty : status_change.old_name;

        [FieldSettings("To status")]
        public string to_status => status_change == null ? string.Empty : status_change.new_name;

        public HappyFoxUpdatedAssigneeChange assignee_change { get; set; }

        [FieldSettings("Assigned from")]
        public string assigned_from => assignee_change == null ? string.Empty : assignee_change.old_name;

        [FieldSettings("Assgined to")]
        public string assigned_to => assignee_change == null ? string.Empty : assignee_change.new_name;

        public HappyFoxUpdatedPriorityChange priority_change { get; set; }

        [FieldSettings("From priority")]
        public string from_priority => priority_change == null ? string.Empty : priority_change.old_name;

        [FieldSettings("To priority")]
        public string to_priority => priority_change == null ? string.Empty : priority_change.new_name;

        public HappyFoxUpdatedDueDateChange due_date_change { get; set; }

        [FieldSettings("From due date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public string from_due_date => due_date_change == null ? string.Empty : due_date_change.old;

        [FieldSettings("To due date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public string to_due_date => due_date_change == null ? string.Empty : due_date_change.New;

        #endregion

        #region Ticket Properties
        public HappyFoxTicket ticket { get; set; }

        [FieldSettings("Ticket ID", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int ticket_id => ticket.id;

        [FieldSettings("Ticket subject", DefaultField = true)]
        public string ticket_subject => ticket.subject;

        [FieldSettings("Ticket category")]
        public string ticket_catetgory => ticket.category.name;

        [FieldSettings("Ticket category description")]
        public string ticket_cat_description => ticket.category.description;

        [FieldSettings("Ticket status")]
        public string ticket_status => ticket.status.name;

        [FieldSettings("Ticket status type")]
        public string status_type => ticket.status.behavior;

        [FieldSettings("Time spent on ticket")]
        public string time_on_ticket => ticket.time_spent;

        [FieldSettings("Ticket due date")]
        public string ticket_due_date => ticket.due_date;
        
        [FieldSettings("Ticket priority")]
        public string ticket_priority => ticket.priority.name;

        [FieldSettings("Ticket created date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public string ticket_created_date => ticket.created_date;

        [FieldSettings("Ticket created time", FieldTypeId = DataConnector.FieldTypeIdTime)]
        public string ticket_created_time => ticket.created_time;
        
        [FieldSettings("Contact name")]
        public string contact_name => ticket.user.name;

        [FieldSettings("Contact email", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string contact_email => ticket.user.email;

        [FieldSettings("Assigned to name")]
        public string assigned_to_name => ticket.assigned_to_name;

        [FieldSettings("Assigned to email", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string assigned_to_email => ticket.assigned_to_email;

        #endregion

        #region Hidden properites
        public string timestamp { get; set; }

        public string time { get; set; }

        public string due_date => ticket.due_date;

        public HappyFoxTicketMessage message { get; set; }

        public List<HappyFoxTicketMessage> messages { get; set; }

        public HappyFoxUpdatedBy by { get; set; }

        public HappyFoxTicketCollection ticketcollection { get; set; }
        
        #endregion

        #region Calculations

        [FieldSettings("Update type", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEnum,
            EnumType = typeof (HappyFoxTicketUpdateType))]
        public HappyFoxTicketUpdateType update_type
        {
            get
            {
                
                var update = HappyFoxTicketUpdateType.None;
                if(category_change != null) update= HappyFoxTicketUpdateType.CHANGE_CATEGORY;
                if(priority_change != null) update= HappyFoxTicketUpdateType.CHANGE_PRIORITY;
                if(due_date_change != null) update= HappyFoxTicketUpdateType.CHANGE_DUEDATE;
                if(status_change != null) update= HappyFoxTicketUpdateType.CHANGE_STATUS;
                if(ticket.user != null) update= HappyFoxTicketUpdateType.ASSIGN_USER;
                return update;
            }
            
        }

        [FieldSettings("Update by", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(HappyFoxTicketUpdateType))]
        public HappyFoxTicketUpdateBy update_by {
            get {
                if(updated_by_smartrule == true) {
                    return HappyFoxTicketUpdateBy.SMARTRUL;
                }
                return HappyFoxTicketUpdateBy.USER;
            }
        }

        [FieldSettings("Overdue", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool overdue
        {
            get
            {
                var over_due = false;
                var d1 = Convert.ToDateTime(due_date);
                var d2 = DateTime.Now;
                if (d1 > d2) { over_due = true; }
                return over_due;
            }
        }

        [FieldSettings("Days overdue", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int days_overdue
        {
            get
            {
                var num_of_days_overdue = 0;
                var d1 = Convert.ToDateTime(due_date);
                var d2 = DateTime.Now;
                if (d1 > d2)
                {
                    num_of_days_overdue = (d1.Subtract(d2)).Days;
                }
                return num_of_days_overdue;
            }
        }

        [FieldSettings("Updated by user", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool updated_by_user {
            get {
                var updatedby_user = false || @by.type == "user";
                return updatedby_user;
            }
        }

        [FieldSettings("Updated by smartrule", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool updated_by_smartrule{
            get{
                var updated_by_smartrule = false || @by.type == "smartrule";
                return updated_by_smartrule;
            }
        }
        #endregion
    }
}
