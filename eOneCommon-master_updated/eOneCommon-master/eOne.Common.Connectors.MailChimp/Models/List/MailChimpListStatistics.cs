using System;

namespace eOne.Common.Connectors.MailChimp.Models.List
{
    public class MailChimpListStatistics : ConnectorEntityModel
    {

        public double member_count { get; set; }
        public double unsubscribe_count { get; set; }
        public double cleaned_count { get; set; }
        public double member_count_since_send { get; set; }
        public double unsubscribe_count_since_send { get; set; }
        public double cleaned_count_since_send { get; set; }
        public double avg_sub_rate { get; set; }
        public double avg_unsub_rate { get; set; }
        public double target_sub_rate { get; set; }
        public double open_rate { get; set; }
        public double click_rate { get; set; }
        public double merge_field_count { get; set; }
        public DateTime? campaign_last_sent { get; set; }
        public DateTime? last_sub_date { get; set; }
        public DateTime? last_unsub_date { get; set; }

    }
}
