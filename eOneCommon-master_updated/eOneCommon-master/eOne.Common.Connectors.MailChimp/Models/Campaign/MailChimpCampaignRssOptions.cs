using System;
using System.ComponentModel;

namespace eOne.Common.Connectors.MailChimp.Models.Campaign
{
    public class MailChimpCampaignRssOptions
    {

        public enum MailChimpCampaignRssFrequency
        {
            [Description("Daily")]
            daily,
            [Description("Weekly")]
            weekly,
            [Description("Monthly")]
            monthly
        }

        public string feed_url { get; set; }
        public MailChimpCampaignRssFrequency frequency { get; set; }
        public DateTime last_sent { get; set; }
        public MailChimpCampaignRssSchedule schedule { get; set; }

        public int schedule_hour => schedule.hour;
        public MailChimpCampaignRssSchedule.MailChimpCampaignRssScheduleDayOfWeek schedule_weekly_send_day => schedule.weekly_send_day;
        public int schedule_monthly_send_date => schedule.monthly_send_date;
        public bool schedule_daily_send_sunday => schedule.daily_send_sunday;
        public bool schedule_daily_send_monday => schedule.daily_send_monday;
        public bool schedule_daily_send_tuesday => schedule.daily_send_tuesday;
        public bool schedule_daily_send_wednesday => schedule.daily_send_wednesday;
        public bool schedule_daily_send_thursday => schedule.daily_send_thursday;
        public bool schedule_daily_send_friday => schedule.daily_send_friday;
        public bool schedule_daily_send_saturday => schedule.daily_send_saturday;

    }
}


