using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.MailChimp.Models
{
    public class MailChimpListStatistics : DataConnectorEntityModel
    {

        public double member_count { get; set; }
        public double unsubscribe_count { get; set; }
        public double cleaned_count { get; set; }
        public double member_count_since_send { get; set; }
        public double unsubscribe_count_since_send { get; set; }
        public double cleaned_count_since_send { get; set; }
        public double campaign_count { get; set; }
        public double grouping_count { get; set; }
        public double group_count { get; set; }
        public double merge_var_count { get; set; }
        public double avg_sub_rate { get; set; }
        public double avg_unsub_rate { get; set; }
        public double target_sub_rate { get; set; }
        public double open_rate { get; set; }
        public double click_rate { get; set; }

    }
}
