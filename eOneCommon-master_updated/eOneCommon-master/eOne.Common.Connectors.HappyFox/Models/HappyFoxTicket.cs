using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace eOne.Common.Connectors.HappyFox.Models
{
    public class HappyFoxTicket : ConnectorEntityModel
    {
        #region Enums
        public enum HappyFoxTicketStatus
        {
            [Description("Status type")]
            STATUS_TYPE
        }
        #endregion

        [FieldSettings("Ticket ID")]
        public int id { get; set; }

        [FieldSettings("Subject", DefaultField = true)]
        public string subject { get; set; }

        public HappyFoxCategory category { get; set; }

        [FieldSettings("Category", DefaultField = true)]
        public string cat_name => category == null ? string.Empty : category.name;

        [FieldSettings("Category description")]
        public string cat_description
        {
            get
            {
                try
                {
                    return category.description;
                }
                catch { return null; }

            }
            set { }
        }
         

        public HappyFoxStatus status { get; set; }

        [FieldSettings("Status", DefaultField = true)]
        public string status_name
        {
            get
            {
                try
                {
                    return status.name;
                }
                catch { return null; }

            }
            set { }
        }
        

        [FieldSettings("Status type", EnumType = typeof(HappyFoxTicketStatus))]
        public HappyFoxTicketStatus status_type { get; set; }

        [FieldSettings("Time spent")]
        public string time_spent { get; set; }

        [FieldSettings("Due date")]
        public string due_date { get; set; }

        public HappyFoxTicketPriority priority { get; set; }

        [FieldSettings("Priority", DefaultField = true)]
        public string priority_name
        {
            get
            {
                try
                {
                    return priority.name;
                }
                catch { return null; }

            }
            set { }
        }
        

        [FieldSettings("Created date")]
        public string created_date { get; set; }

        [FieldSettings("Created time")]
        public string created_time { get; set; }

        [FieldSettings("Last updated time")]
        public string last_updated_time { get; set; }

        [FieldSettings("Last updated date")]
        public string last_updated_date { get; set; }

        [FieldSettings("Last contact reply date")]
        public string last_contact_reply_date { get; set; }

        [FieldSettings("Last contact reply time")]
        public string last_contact_reply_time { get; set; }

        [FieldSettings("Last staff reply date")]
        public string last_staff_reply_date { get; set; }

        [FieldSettings("Last staff reply time")]
        public string last_staff_reply_time { get; set; }

        [FieldSettings("Last accessed at date")]
        public string last_accessed_date { get; set; }

        [FieldSettings("Last accessed at time")]
        public string last_accessed_at_time { get; set; }

        public HappyFoxContact user { get; set; }

        [FieldSettings("Cotact name", DefaultField = true)]
        public string contact_name
        {
            get
            {
                try
                {
                    return user.name;
                }
                catch { return null; }

            }
            set { }
        }
        

        [FieldSettings("Contact email")]
        public string contact_email
        {
            get
            {
                try
                {
                    return user.email;
                }
                catch { return null; }

            }
            set { }
        }

        [FieldSettings("Assigned to name")]
        public string assigned_to_name
        {
            get
            {
                try
                {
                    return assigned_to.name;
                }
                catch { return null; }

            }
            set { }
        }
        

        [FieldSettings("Assigned to email")]
        public string assigned_to_email
        {
            get
            {
                try
                {
                    return assigned_to.email;
                }
                catch { return null; }

            }
            set { }
        }
        

        #region hidden properties
        public List<HappyFoxContact> email { get; set; }

        public List<HappyFoxTicketUpdate> updates { get; set; }

        public List<HappyFoxTicketMessage> messages { get; set; }

        public List<HappyFoxUpdatedDueDateChange> due_date_change { get; set; }

        public List<HappyFoxUpdatedStatusChange> status_change { get; set; }

        public List<HappyFoxUpdatedCategoryChanges> category_change { get; set; }

        public List<HappyFoxUpdatedPriorityChange> priority_change { get; set; }

        public static string created_at { get; set; }

        public List<HappyFoxStatus> statuses { get; set; }

        public List<HappyFoxCategory> categories { get; set; }

        public List<HappyFoxTicketPriority> priorities { get; set; }

        public HappyFoxCustomField customField { get; set; }

        public HappyFoxCannedAction cannedAction { get; set; }

        public HappyFoxCannedActionsAssignedTo cannedAssignedTo { get; set; }

        public HappyFoxCannedActionStatusTo cannedStatusTo { get; set; }

        public HappyFoxStaffRole role { get; set; }

        public HappyFoxTicketCollection ticketCollection { get; set; }

        public HappyFoxTicketSummary assigned_to { get; set; }

        public int num_of_tickets
        {
            get
            {
                return ticketCollection.data.Count;
            }

            set { }
        }


        #endregion


        #region calculations

        [FieldSettings("Number of messages", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int number_of_messages
        {
            get
            {
                    return email?.Count ?? 0;
            }
            set { }
        }
        

        //[FieldSettings("Days overdue", FieldTypeId = Connector.FieldTypeIdInteger)]

        //[FieldSettings("Overdue")]

        [FieldSettings("Number of updates", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int number_of_updates => updates?.Count ?? 0;

        [FieldSettings("Number of due date changes", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int num_of_due_date_changes => due_date_change?.Count ?? 0;

        [FieldSettings("Number of status changes", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int num_of_status_changes => status_change?.Count ?? 0;

        [FieldSettings("Number of category changes", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int num_of_category_changes => category_change?.Count ?? 0;

        [FieldSettings("Number of priority changes", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int num_of_priority_changes => priority_change?.Count ?? 0;

        [FieldSettings("First response time")]
        public decimal first_response_time
        {
            get
            {
                decimal response_time = 0;
                if (messages.Any(t => t != null))
                {
                    TimeSpan diff = TimeSpan.Parse(updates[0].timestamp) - TimeSpan.Parse(created_at);
                    response_time = Convert.ToDecimal(diff.TotalHours);
                }
                return response_time;
            }
        }

        [FieldSettings("Age")]
        public decimal age => Convert.ToDecimal(DateTime.Now - TimeSpan.Parse(created_at));

        [FieldSettings("Original due date")]
        public DateTime original_due_date => Convert.ToDateTime(updates[0].timestamp);

        [FieldSettings("Original status")]
        public string original_status => statuses[0].name;

        [FieldSettings("Original category")]
        public string original_category => categories[0].name;

        [FieldSettings("Original priority")]
        public string original_priority => priorities[0].name;

        [FieldSettings("Overdue", DefaultField = true)]
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

        #region Custom fields
        [FieldSettings("Custome field name")]
        public string cus_name => customField.name;

        [FieldSettings("Depends_on_choice")]
        public bool cus_depends_on_choice => customField.depends_on_choice;

        [FieldSettings("Required")]
        public bool cus_required => customField.required;

        [FieldSettings("Choices")]
        public List<HappyFoxCustomFieldChoices> cus_choices => customField.choices;

        [FieldSettings("d")]
        public int cus_id => customField.id;

        [FieldSettings("Type")]
        public string cus_type => customField.type;

        [FieldSettings("Order")]
        public int cus_order => customField.order;

        [FieldSettings("Visible to staff only")]
        public bool cus_visible_to_staff_only => customField.visible_to_staff_only;
        #endregion


        #region Canned actions
        [FieldSettings("Canned action name")]
        public string canned_name => cannedAction.name;

        [FieldSettings("Canned tags")]
        public string canned_tags => cannedAction.tags;

        [FieldSettings("Assigned name")]
        public string assigend_name => cannedAssignedTo.name;

        [FieldSettings("Assigned email")]
        public string assinged_email => cannedAssignedTo.email;

        [FieldSettings("Role name")]
        public string role_name => role.name;

        [FieldSettings("Role id")]
        public int role_id => role.id;

        [FieldSettings("Assign to active")]
        public bool assign_to_active => cannedAssignedTo.active;

        [FieldSettings("Assign to id")]
        public int assign_to_id => cannedAssignedTo.id;

        [FieldSettings("Assign to category")]
        public List<int> assign_to_category => cannedAssignedTo.categories;

        [FieldSettings("Canned action available to")]
        public List<int> canned_available_to => cannedAction.available_to;

        [FieldSettings("Canned action time spent")]
        public string canned_time_spent => cannedAction.time_spent;

        [FieldSettings("Canned action default priority")]
        public bool canned_default_priority => priority.@default;

        [FieldSettings("canned action priority id")]
        public int canned_priority_id => priority.id;

        [FieldSettings("Canned action priority name")]
        public string canned_priority_name => priority.name;

        [FieldSettings("Canned action priority order")]
        public int canned_priority_order => priority.order;

        [FieldSettings("Canned action html reply")]
        public string html_reply => cannedAction.html_reply;

        [FieldSettings("Canned action status to name")]
        public string status_to_name => cannedStatusTo.name;

        [FieldSettings("Canned action status to color")]
        public string status_to_color => cannedStatusTo.color;

        [FieldSettings("Canned action status to order")]
        public int status_to_order => cannedStatusTo.order;

        [FieldSettings("Canned action status to default")]
        public bool status_to_default => cannedStatusTo.@default;

        [FieldSettings("Canned action status to behavior")]
        public string status_to_behavior => cannedStatusTo.behavior;

        [FieldSettings("Canned action status to id")]
        public int status_to_id => cannedStatusTo.id;

        [FieldSettings("Canned action reply")]
        public string canned_action_reply => cannedAction.reply;

        [FieldSettings("Canned action id")]
        public int canned_action_id => cannedAction.canned_id;

        [FieldSettings("Canned action categories")]
        public List<int> canned_action_categories => cannedAction.canned_category;

        [FieldSettings("Canned action description")]
        public string canned_action_description => cannedAction.canned_description;
        #endregion


    }


    //public int number_0f_due_date_changes =>   

    //TimeSpan diff = secondDate - firstDate;
    //double hours = diff.TotalHours;

    #endregion

}
