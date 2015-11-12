using System;
using System.Collections.Generic;

namespace eOne.Common.Connectors.Zendesk.Models
{
    public class ZendeskRequest
    {

        public enum ZendeskRequestStatus
        {
            @new,
            open,
            pending, 
            hold, 
            solved, 
            closed
        }
        public enum ZendeskRequestPriority
        {
            low, 
            normal, 
            high, 
            urgent
        }
        public enum ZendeskRequestType
        {
            question, 
            incident, 
            problem, 
            task
        }

        public int id { get; set; }
        public string url { get; set; }
        public string subject { get; set; }
        public string description { get; set; }
        public ZendeskRequestStatus status { get; set; }
        public ZendeskRequestPriority priority { get; set; }
        public ZendeskRequestType type { get; set; }
        public bool can_be_solved_by_me { get; set; }
        public bool solved { get; set; }
        public DateTime due_at { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public int organization_id { get; set; }
        public int requester_id { get; set; }
        public int assignee_id { get; set; }
        public int group_id { get; set; }
        public List<int> collaborator_ids { get; set; }
        public int ticket_form_id { get; set; }

    }
}

//custom_fields	Array	no	no	The fields and entries for this request
//via	Via	yes	no	This object explains how the request was created
