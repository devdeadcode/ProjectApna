using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.HappyFox.Models
{
    public class HappyFoxTicketSummary : DataConnectorEntityModel
    {
        #region General properties

        [FieldSettings("Name", DefaultField = true)]
        public string name { get; set; }

        [FieldSettings("Email", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string email { get; set; }

        [FieldSettings("Active")]
        public bool active { get; set; }

        [FieldSettings("Role")]
        public string role_name { get; set; }

        [FieldSettings("Staff ID", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int role_id { get; set; }

        #endregion


        #region Hidden Properties
        

        public HappyFoxTicket ticket { get; set; }
        
        public List<HappyFoxTicket> ticketList { get; set; }
        #endregion


        #region Calculations

        [FieldSettings("Number of tickets", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int num_of_tickets => ticketList.Count;

        [FieldSettings("Number of overdue tickets", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int num_of_overdue_tickets => ticketList.Count(tick => tick.overdue);
        
        [FieldSettings("Number of unresponded tickets", FieldTypeId = DataConnector.FieldTypeIdInteger, DefaultField = true)]
        public int num_of_unresponded_tickets => ticketList.Where(tick => tick.assigned_to != null).Count(tick => tick.number_of_messages == 1);
        
        [FieldSettings("Percentage of overdue tickets")]
        public decimal perce_of_overdue_tickets
        {
            get
            {
                if (num_of_tickets != 0)
                {
                    return (num_of_overdue_tickets / num_of_tickets) * 100;
                }
                return 0;
            }
        }

        [FieldSettings("Percentage of unresponded tickets")]
        public decimal perce_of_unresponded_tickets
        {
            get
            {
                if (num_of_tickets != 0)
                {
                    return (num_of_unresponded_tickets / num_of_tickets) * 100;
                }
                return 0;
            }
        }

        [FieldSettings("Average ticket age")]
        public decimal avg_ticket_age
        {
            get
            {
                var sum = ticketList.Sum(val => val.age);
                if (ticketList.Count != 0) { return sum / ticketList.Count; }
                return 0;
            }
        }

        [FieldSettings("Maximum ticket age")]
        public decimal max_ticket_age
        {
            get
            {
                var ages = ticketList.Select(t => t.age).ToList();
                return ages.Max();
            }
        }

        [FieldSettings("Average days overdue")]
        public decimal avg_days_overdue
        {
            get
            {
                var sum = ticketList.Where(t => t.overdue).Aggregate<HappyFoxTicket, decimal>(0, (current, t) => current + t.days_overdue);
                if (ticketList.Count != 0) { return sum / num_of_tickets; }
                return 0;
            }
        }

        [FieldSettings("Maximum days overdue")]
        public decimal max_days_overdue
        {
            get
            {
                var overdues = ticketList.Select(t => t.days_overdue).Select(dummy => (decimal)dummy).ToList();
                return overdues.Max();
            }
        }

        [FieldSettings("Average response time")]
        public decimal avg_response_time
        {
            get
            {
                var response_sum = ticketList.Sum(t => t.first_response_time);
                if ((num_of_tickets - num_of_unresponded_tickets) != 0) { return response_sum / (num_of_tickets - num_of_unresponded_tickets); }
                return 0;
            }
        }

        [FieldSettings("Minimum response time")]
        public decimal min_response_time
        {
            get
            {
                var response_times = ticketList.Select(t => t.first_response_time).ToList();
                return response_times.Min();
            }
        }

        [FieldSettings("Maximum response time")]
        public decimal max_response_time
        {
            get
            {
                var response_times = ticketList.Select(t => t.first_response_time).ToList();
                return response_times.Max();
            }
        }

        [FieldSettings("Average time spent")]
        public decimal avg_time_spent
        {
            get
            {
                var ticket_count = 0;
                decimal sum = 0;
                foreach (var t in ticketList)
                {
                    sum += Convert.ToDecimal(t.time_spent);
                    if (t.time_spent != null) { ticket_count++; }
                }

                if (ticket_count != 0) { return sum / ticket_count; }
                return 0;
            }
        }

        [FieldSettings("Minimum time spent")]
        public decimal min_time_spent
        {
            get
            {
                var spent_times = ticketList.Select(t => Convert.ToDecimal(t.time_spent)).ToList();
                return spent_times.Min();
            }
        }

        [FieldSettings("Maximum time spent")]
        public decimal max_time_spent
        {
            get
            {
                var spent_times = ticketList.Select(t => Convert.ToDecimal(t.time_spent)).ToList();
                return spent_times.Max();
            }

        }
        #endregion
    }

}
