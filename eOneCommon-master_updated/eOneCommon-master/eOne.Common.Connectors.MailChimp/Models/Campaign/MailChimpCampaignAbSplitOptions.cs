using System;
using System.ComponentModel;

namespace eOne.Common.Connectors.MailChimp.Models.Campaign
{
    public class MailChimpCampaignAbSplitOptions
    {

        public enum MailChimpCampaignAbSplitTest
        {
            [Description("Subject")]
            subject,
            [Description("From name")]
            from_name,
            [Description("Schedule")]
            schedule
        }
        public enum MailChimpCampaignAbSplitPickWinner
        {
            [Description("Opens")]
            opens,
            [Description("Clicks")]
            clicks,
            [Description("Manual")]
            manual
        }
        public enum MailChimpCampaignAbSplitWaitUnit
        {
            [Description("Hours")]
            hours,
            [Description("Days")]
            days
        }

        public MailChimpCampaignAbSplitTest split_test { get; set; }
        public MailChimpCampaignAbSplitPickWinner pick_winner { get; set; }
        public MailChimpCampaignAbSplitWaitUnit wait_units { get; set; }
        public int wait_time { get; set; }
        public int split_size { get; set; }
        public string from_name_a { get; set; }
        public string from_name_b { get; set; }
        public string reply_email_a { get; set; }
        public string reply_email_b { get; set; }
        public string subject_a { get; set; }
        public string subject_b { get; set; }
        public DateTime? send_time_a { get; set; }
        public DateTime? send_time_b { get; set; }
        public DateTime? send_time_winner { get; set; }

    }
}