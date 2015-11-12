using System.ComponentModel;

namespace eOne.Common.Connectors.MailChimp.Models.Campaign
{
    public class MailChimpCampaignRssSchedule : ConnectorEntityModel
    {

        public enum MailChimpCampaignRssScheduleDayOfWeek
        {
            [Description("Sunday")]
            sunday,
            [Description("Monday")]
            monday,
            [Description("Tuesday")]
            tuesday,
            [Description("Wednesday")]
            wednesday,
            [Description("Thursday")]
            thursday,
            [Description("Friday")]
            friday,
            [Description("Saturday")]
            saturday
        }

        public int hour { get; set; }
        public MailChimpCampaignRssScheduleDaily daily_send { get; set; }
        public MailChimpCampaignRssScheduleDayOfWeek weekly_send_day { get; set; }
        public int monthly_send_date { get; set; }

        public bool daily_send_sunday => daily_send.sunday;
        public bool daily_send_monday => daily_send.monday;
        public bool daily_send_tuesday => daily_send.tuesday;
        public bool daily_send_wednesday => daily_send.wednesday;
        public bool daily_send_thursday => daily_send.thursday;
        public bool daily_send_friday => daily_send.friday;
        public bool daily_send_saturday => daily_send.saturday;

    }
}
