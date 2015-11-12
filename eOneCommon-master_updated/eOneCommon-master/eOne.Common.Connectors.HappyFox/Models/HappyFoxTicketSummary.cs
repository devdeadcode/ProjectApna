using System;
using System.Collections.Generic;
using System.Linq;

namespace eOne.Common.Connectors.HappyFox.Models
{
    public class HappyFoxTicketSummary : ConnectorEntityModel
    {
        [FieldSettings("Name", DefaultField = true)]
        public string name { get; set; }

        [FieldSettings("Email")]
        public string email { get; set; }

        [FieldSettings("Active")]
        public bool active { get; set; }


        public HappyFoxStaffRole role { get; set; }

        [FieldSettings("Role")]
        public string role_name => role.name;

        [FieldSettings("Staff ID")]
        public int role_id => role.id;

        #region Hidden Properties
        public List<HappyFoxTicketSummary> tickets { get; set; }
        List<HappyFoxTicket> data = new List<HappyFoxTicket>();

        public List<HappyFoxTicketUpdate> update_list { get; set; }

        public HappyFoxTicketCollection ticketCollection { get; set; }

        public HappyFoxTicket ticket { get; set; }


        public bool overdue
        {
            get
            {
                try
                {
                    return ticket.overdue;
                }
                catch { return false; }

            }
            set { }
        }

        #endregion


        #region Calculations



        [FieldSettings("Number of tickets", DefaultField = true)]
        public int num_of_tickets
        {
            get
            {
                try
                {
                    return ticket.num_of_tickets;
                }
                catch { return 0; }

            }
            set
            {
                num_of_tickets = tickets.Count;
            }
        }

        [FieldSettings("Number of overdue tickets", DefaultField = true)]
        public int num_of_overdue_tickets
        {
            get
            {
                int num_of_ovrdues = 0;
                if (overdue) { num_of_ovrdues = data.Count; }
                return num_of_ovrdues;
            }
        }

        [FieldSettings("Number of unresponded tickets", DefaultField = true)]
        public int num_of_unresponded_tickets
        {
            get
            {
                int num_of_unres_tickets = 0;
                int null_msg_count = 0;
                try
                {
                    for (int j = 0; j < data.Count; j++)
                    {
                        null_msg_count += update_list.Count(t => t.message == null);
                        if (null_msg_count == update_list.Count) { num_of_unres_tickets++; }
                    }
                    return num_of_unres_tickets;
                }
                catch
                {
                    return 0;
                }

            }
        }

        [FieldSettings("Percentage of overdue tickets")]
        //public decimal perce_of_overdue_tickets => (num_of_overdue_tickets / num_of_tickets) * 100;
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
        //public decimal perce_of_unresponded_tickets => (num_of_unresponded_tickets / num_of_tickets) * 100;

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
                decimal sum = data.Average(t => t.age);
                if (data.Count != 0) { return sum / num_of_tickets; }
                return 0;
            }
        }

        [FieldSettings("Maximum ticket age")]
        public decimal max_ticket_age
        {
            get
            {
                return data.Max(t => t.age);
            }
        }

        [FieldSettings("Average days overdue")]
        public decimal avg_days_overdue
        {
            get
            {
                decimal sum = data.Where(t => t.overdue).Aggregate<HappyFoxTicket, decimal>(0, (current, t) => current + t.days_overdue);
                if (data.Count != 0) { return sum / num_of_tickets; }
                return 0;
            }
        }

        [FieldSettings("Maximum days overdue")]
        public decimal max_days_overdue
        {
            get
            {
                List<decimal> overdues = data.Select(t => t.days_overdue).Select(dummy => (decimal) dummy).ToList();
                return overdues.Max();
            }
        }

        [FieldSettings("Average response time")]
        public decimal avg_response_time
        {
            get
            {
                decimal response_sum = data.Sum(t => t.first_response_time);
                if ((num_of_tickets - num_of_unresponded_tickets) != 0) { return response_sum / (num_of_tickets - num_of_unresponded_tickets); }
                return 0;
            }
        }

        [FieldSettings("Minimum response time")]
        public decimal min_response_time
        {
            get
            {
                List<decimal> response_times = (from t in data where (num_of_tickets - num_of_unresponded_tickets) != 0 select t.first_response_time).ToList();
                return response_times.Min();
            }
        }

        [FieldSettings("Maximum response time")]
        public decimal max_response_time
        {
            get
            {
                List<decimal> response_times = new List<decimal>();
                for (int i = 0; i < data.Count; i++)
                {
                    response_times.Add(data[i].first_response_time);
                }
                return response_times.Max();
            }
        }

        [FieldSettings("Average time spent")]
        public decimal avg_time_spent
        {
            get
            {
                int ticket_count = 0;
                decimal sum = 0;
                foreach (HappyFoxTicket t in data)
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
                List<decimal> spent_times = data.Select(t => Convert.ToDecimal(t.time_spent)).ToList();
                return spent_times.Min();
            }
        }

        [FieldSettings("Maximum time spent")]
        public decimal max_time_spent
        {
            get
            {
                List<decimal> spent_times = data.Select(t => Convert.ToDecimal(t.time_spent)).ToList();
                return spent_times.Max();
            }
            #endregion
        }
    }

}
