using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.HappyFox.Models
{
    public class HappyFoxTicket : DataConnectorEntityModel
    {
        #region Enums
        public enum HappyFoxTicketStatus
        {
            [Description("Status type")]
            STATUS_TYPE
        }
        #endregion

        #region Default fields
        [FieldSettings("Subject", DefaultField = true)]
        public string subject { get; set; }

        [FieldSettings("Category", DefaultField = true)]
        public string cat_name => category == null ? string.Empty : category.name;

        [FieldSettings("Status", DefaultField = true)]
        public string status_name => status == null ? string.Empty : status.name;

        [FieldSettings("Priority", DefaultField = true)]
        public string priority_name => priority == null ? string.Empty : priority.name;
        
        [FieldSettings("Cotact name", DefaultField = true)]
        public string contact_name => user == null ? string.Empty : user.name;
        #endregion

        #region General fields
        [FieldSettings("Ticket ID", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int id { get; set; }
        
        public HappyFoxCategory category { get; set; }
        
        [FieldSettings("Category description")]
        public string cat_description => category == null ? string.Empty : category.description;
        
        public HappyFoxStatus status { get; set; }
        
        [FieldSettings("Status type", EnumType = typeof(HappyFoxTicketStatus), FieldTypeId = DataConnector.FieldTypeIdEnum)]
        public HappyFoxTicketStatus status_type { get; set; }

        [FieldSettings("Time spent", FieldTypeId = DataConnector.FieldTypeIdTime)]
        public string time_spent { get; set; }

        [FieldSettings("Due date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public string due_date { get; set; }

        public HappyFoxTicketPriority priority { get; set; }

        [FieldSettings("Created date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public string created_date => created_at?.Substring(0, 10) ?? string.Empty;
        
        [FieldSettings("Created time", FieldTypeId = DataConnector.FieldTypeIdTime)]
        public string created_time=> created_at?.Substring(11) ?? string.Empty;
        
        [FieldSettings("Last updated time", FieldTypeId = DataConnector.FieldTypeIdTime)]
        public string last_updated_time => last_updated_at?.Substring(11) ?? string.Empty;
        
        [FieldSettings("Last updated date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public string last_updated_date => last_updated_at?.Substring(0, 10) ?? string.Empty;
        
        [FieldSettings("Last contact reply date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public string last_contact_reply_date => last_user_reply_at?.Substring(0, 10) ?? string.Empty;
         
        [FieldSettings("Last contact reply time", FieldTypeId = DataConnector.FieldTypeIdTime)]
        public string last_contact_reply_time => last_user_reply_at?.Substring(11) ?? string.Empty;
        
        [FieldSettings("Last staff reply date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public string last_staff_reply_date => last_staff_reply_at?.Substring(0, 10) ?? string.Empty;
        
        [FieldSettings("Last staff reply time", FieldTypeId = DataConnector.FieldTypeIdTime)]
        public string last_staff_reply_time => last_staff_reply_at?.Substring(11) ?? string.Empty;
        
        [FieldSettings("Last accessed at date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public string last_accessed_date => last_accessed_at?.Substring(0, 10) ?? string.Empty;
        
        [FieldSettings("Last accessed at time", FieldTypeId = DataConnector.FieldTypeIdTime)]
        public string last_accessed_at_time => last_accessed_at?.Substring(11) ?? string.Empty;
        
        public HappyFoxContact user { get; set; }
        
        [FieldSettings("Contact email", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string contact_email => user == null ? string.Empty : user.email;
        
        [FieldSettings("Assigned to name")]
        public string assigned_to_name => assigned_to == null ? string.Empty : assigned_to.name;
        
        [FieldSettings("Assigned to email", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string assigned_to_email => assigned_to == null ? string.Empty : assigned_to.email;

        #endregion

        #region hidden properties
        
        public List<HappyFoxTicketUpdate> updates { get; set; }
        
        public string first_message { get; set; }
        
        public string created_at { get; set; }

        public string last_updated_at { get; set; }

        public string last_user_reply_at { get; set; }

        public string last_staff_reply_at { get; set; }

        public string last_accessed_at { get; set; }
        
        public HappyFoxTicketCollection ticketCollection { get; set; }

        public HappyFoxTicketAssignedTo assigned_to { get; set; }
        
        public int num_of_tickets
        {
            get
            {
                if (ticketCollection == null) return 0;
                return ticketCollection.data.Count;
            }
            
        }



        #endregion
        
        #region calculations

        [FieldSettings("Number of messages", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int number_of_messages => updates.Count(val => val.message != null);
        
        [FieldSettings("Number of updates", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int number_of_updates => updates?.Count ?? 0;

        [FieldSettings("Number of due date changes", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int num_of_due_date_changes => updates.Count(val => val.due_date_change != null);
        
        [FieldSettings("Number of status changes", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int num_of_status_changes => updates.Count(val => val.status_change != null);
        
        [FieldSettings("Number of category changes", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int num_of_category_changes => updates.Count(val => val.category_change != null);
        
        [FieldSettings("Number of priority changes", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int num_of_priority_changes => updates.Count(val => val.priority_change != null);
        
        [FieldSettings("First response time")]
        public decimal first_response_time
        {
            get
            {
                decimal response_time = 0;

                foreach (var val in updates)
                {
                    if (val.message.text != first_message) continue;
                    var date1 = DateTime.ParseExact(val.timestamp, "yyyy-MM-dd HH:mm:ss", null);
                    var date2 = DateTime.ParseExact(created_at, "yyyy-MM-dd HH:mm:ss", null);
                    response_time = date1.Subtract(date2).Hours;
                    break;
                }
                return response_time;
            }
        }

        [FieldSettings("Age")]
        public decimal age => (DateTime.UtcNow - DateTime.ParseExact(created_at, "yyyy-MM-dd HH:mm:ss", null)).Hours;

        [FieldSettings("Original due date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public string original_due_date => (from val in ticketCollection.data where val.due_date != null select val.due_date.ToString()).FirstOrDefault();
        
        [FieldSettings("Original status")]
        public string original_status => (from val in ticketCollection.data where val.status != null select val.status.name).FirstOrDefault();
        
        [FieldSettings("Original category")]
        public string original_category => (from val in ticketCollection.data where val.category != null select val.category.name).FirstOrDefault();

        [FieldSettings("Original priority")]
        public string original_priority => (from val in ticketCollection.data where val.priority != null select val.priority.name).FirstOrDefault();

        [FieldSettings("Overdue", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdYesNo)]
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

        [FieldSettings("Days overdue", FieldTypeId = DataConnector.FieldTypeIdInteger)]
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

        public List<HappyFoxCustomField> custom_fields { get; set; }

        #endregion

        #region Actions
        public HappyFoxCannedAction cannedAction { get; set; }
        #endregion
    }

}
