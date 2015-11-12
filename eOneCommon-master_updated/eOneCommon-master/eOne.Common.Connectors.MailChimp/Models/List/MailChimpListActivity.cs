using System;

namespace eOne.Common.Connectors.MailChimp.Models.List
{
    public class MailChimpListActivity : ConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("List name", DefaultField = true)]
        public string list_name => list.name;

        [FieldSettings("Date", DefaultField = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime day { get; set; }

        [FieldSettings("Number of subscribes", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int subs { get; set; }

        [FieldSettings("Number of unsubscribes", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int unsubs { get; set; }

        [FieldSettings("Number of other adds", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int other_adds { get; set; }

        [FieldSettings("Number of other removes", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int other_removes { get; set; }

        [FieldSettings("Total adds", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int total_adds => subs + other_adds;

        [FieldSettings("Total removes", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int total_removes => unsubs + other_removes;

        [FieldSettings("Net adds", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int net_adds => total_adds - total_removes;

        #endregion

        #region Properties

        public int emails_sent { get; set; }
        public int unique_opens { get; set; }
        public int recipient_clicks { get; set; }
        public int hard_bounce { get; set; }
        public int soft_bounce { get; set; }

        #endregion

        #region Hidden properties

        public MailChimpList list { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Click rate", FieldTypeId = Connector.FieldTypeIdPercentage)]
        public decimal click_rate => emails_sent == 0 ? 0 : recipient_clicks / emails_sent * 100;

        [FieldSettings("Open rate", FieldTypeId = Connector.FieldTypeIdPercentage)]
        public decimal open_rate => emails_sent == 0 ? 0 : unique_opens / emails_sent * 100;

        [FieldSettings("Total bounces", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int total_bounces => hard_bounce + soft_bounce;

        [FieldSettings("Bounce rate", FieldTypeId = Connector.FieldTypeIdPercentage)]
        public decimal bounce_rate => emails_sent == 0 ? 0 : total_bounces / emails_sent * 100;

        [FieldSettings("Hard bounce rate", FieldTypeId = Connector.FieldTypeIdPercentage)]
        public decimal hard_bounce_rate => emails_sent == 0 ? 0 : hard_bounce / emails_sent * 100;

        [FieldSettings("Soft bounce rate", FieldTypeId = Connector.FieldTypeIdPercentage)]
        public decimal soft_bounce_rate => emails_sent == 0 ? 0 : soft_bounce / emails_sent * 100;

        [FieldSettings("List ID")]
        public string list_id => list.id;

        [FieldSettings("Number of members for list", FieldTypeId = Connector.FieldTypeIdInteger)]
        public double list_stats_member_count => list.stats_member_count;

        [FieldSettings("Last sent date for list", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? list_stats_campaign_last_sent => list.stats_campaign_last_sent;

        [FieldSettings("Default subject for list")]
        public string list_campaign_defaults_subject => list.campaign_defaults.subject;

        [FieldSettings("Default from name for list")]
        public string list_campaign_defaults_from_name => list.campaign_defaults_from_name;

        [FieldSettings("Default from email for list", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string list_campaign_defaults_from_email => list.campaign_defaults_from_email;

        [FieldSettings("List rating", FieldTypeId = Connector.FieldTypeIdQuantity)]
        public double list_rating => list.list_rating;

        [FieldSettings("List subscribe URL (short)", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string list_subscribe_url_short => list.subscribe_url_short;

        [FieldSettings("List subscribe URL (long)", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string list_subscribe_url_long => list.subscribe_url_long;

        [FieldSettings("List visibility", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(MailChimpList.MailChimpListVisibility))]
        public MailChimpList.MailChimpListVisibility list_visibility => list.visibility;

        [FieldSettings("List created date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? list_date_created => list.date_created;

        #endregion

    }
}
