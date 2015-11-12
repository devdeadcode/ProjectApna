using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace eOne.Common.Connectors.MailChimp.Models.Campaign
{
    public class MailChimpCampaign : ConnectorEntityModel
    {

        #region Enums

        public enum MailChimpCampaignType
        {
            [Description("Regular")]
            regular,
            [Description("Plain text")]
            plaintext,
            [Description("A/B split")]
            absplit,
            [Description("RSS")]
            rss,
            [Description("Variate")]
            variate
        }
        public enum MailChimpCampaignStatus
        {
            [Description("Save")]
            save,
            [Description("Paused")]
            paused,
            [Description("Schedule")]
            schedule,
            [Description("Sending")]
            sending,
            [Description("Sent")]
            sent
        }

        #endregion

        #region Default properties

        [FieldSettings("Name", DefaultField = true, Description = true)]
        public string settings_title => settings.title;

        [FieldSettings("Type", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(MailChimpCampaignType))]
        public MailChimpCampaignType type { get; set; }

        [FieldSettings("Status", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(MailChimpCampaignStatus))]
        public MailChimpCampaignStatus status { get; set; }

        [FieldSettings("Number of emails sent", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int emails_sent { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Campaign ID", KeyNumber = 1)]
        public string id { get; set; }

        [FieldSettings("Content type")]
        public string content_type { get; set; }

        [FieldSettings("Create date", Created = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime create_time { get; set; }

        [FieldSettings("Send date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? send_time { get; set; }

        [FieldSettings("Archive URL", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string archive_url { get; set; }

        #endregion

        #region Hidden properties

        public MailChimpCampaignSettings settings { get; set; }
        public MailChimpCampaignTracking tracking { get; set; }
        public MailChimpCampaignRssOptions rss_opts { get; set; }
        public MailChimpCampaignVariateSettings variate_settings { get; set; }
        public MailChimpCampaignAbSplitOptions ab_split_opts { get; set; }
        public MailChimpCampaignSocialCard social_card { get; set; }
        public int web_id { get; set; }
        public string list_id { get; set; }
        public int folder_id { get; set; }
        public int template_id { get; set; }
        public List<string> segment_opts { get; set; }
        public string parent_id { get; set; }
        public bool is_child { get; set; }

        #endregion

        #region Calculations

        #region Settings

        [FieldSettings("Subject")]
        public string settings_subject_line => settings.subject_line;

        [FieldSettings("From name")]
        public string settings_from_name => settings.from_name;

        [FieldSettings("Reply to email", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string settings_reply_to => settings.reply_to;

        [FieldSettings("To name")]
        public string settings_to_name => settings.to_name;

        [FieldSettings("Authenticate", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool settings_authenticate => settings.authenticate;

        [FieldSettings("Auto footer", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool settings_auto_footer => settings.auto_footer;

        [FieldSettings("Inline CSS", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool settings_inline_css => settings.inline_css;

        [FieldSettings("Auto tweet", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool settings_auto_tweet => settings.auto_tweet;

        [FieldSettings("Auto Facebook post", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool settings_auto_fb_post => settings.auto_fb_post;

        [FieldSettings("Allow Facebook comments", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool settings_fb_comments => settings.fb_comments;

        [FieldSettings("Timewarp", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool settings_timewarp => settings.timewarp;

        public bool settings_drag_and_drop => settings.drag_and_drop;
        public bool settings_use_conversation => settings.use_conversation;

        #endregion

        #region Tracking

        public bool tracking_html_clicks => tracking.html_clicks;
        public bool tracking_text_clicks => tracking.text_clicks;
        public bool tracking_opens => tracking.opens;
        public bool tracking_goal_tracking => tracking.goal_tracking;
        public bool tracking_ecomm360 => tracking.ecomm360;
        public bool tracking_google_analytics => tracking.google_analytics;
        public string tracking_clicktale => tracking.clicktale;
        public bool tracking_salesforce_campaign => tracking.salesforce_campaign;
        public bool tracking_salesforce_notes => tracking.salesforce_notes;
        public bool tracking_highrise_campaign => tracking.highrise_campaign;
        public bool tracking_highrise_notes => tracking.highrise_notes;
        public bool tracking_capsule_notes => tracking.capsule_notes;

        #endregion

        #region RSS options

        public string rss_opts_feed_url => rss_opts.feed_url;
        public MailChimpCampaignRssOptions.MailChimpCampaignRssFrequency rss_opts_frequency => rss_opts.frequency;
        public DateTime rss_opts_last_sent => rss_opts.last_sent;
        public int rss_opts_schedule_hour => rss_opts.schedule_hour;
        public MailChimpCampaignRssSchedule.MailChimpCampaignRssScheduleDayOfWeek rss_opts_schedule_weekly_send_day => rss_opts.schedule_weekly_send_day;
        public int rss_opts_schedule_monthly_send_date => rss_opts.schedule_monthly_send_date;
        public bool rss_opts_schedule_daily_send_sunday => rss_opts.schedule_daily_send_sunday;
        public bool rss_opts_schedule_daily_send_monday => rss_opts.schedule_daily_send_monday;
        public bool rss_opts_schedule_daily_send_tuesday => rss_opts.schedule_daily_send_tuesday;
        public bool rss_opts_schedule_daily_send_wednesday => rss_opts.schedule_daily_send_wednesday;
        public bool rss_opts_schedule_daily_send_thursday => rss_opts.schedule_daily_send_thursday;
        public bool rss_opts_schedule_daily_send_friday => rss_opts.schedule_daily_send_friday;
        public bool rss_opts_schedule_daily_send_saturday => rss_opts.schedule_daily_send_saturday;

        #endregion

        #region Variate settings

        public string variate_settings_winning_subject_line => variate_settings.winning_subject_line;
        public DateTime? variate_settings_winning_send_time => variate_settings.winning_send_time;
        public string variate_settings_winning_from_name => variate_settings.winning_from_name;
        public string variate_settings_winning_reply_to_address => variate_settings.winning_reply_to_address;
        public string variate_settings_winning_contents => variate_settings.winning_contents;
        public int variate_settings_number_of_combinations => variate_settings.number_of_combinations;
        public int variate_settings_number_of_subject_lines => variate_settings.number_of_subject_lines;
        public int variate_settings_number_of_send_times => variate_settings.number_of_send_times;
        public int variate_settings_number_of_from_names => variate_settings.number_of_from_names;
        public int variate_settings_number_of_reply_to_addresses => variate_settings.number_of_reply_to_addresses;
        public int variate_settings_number_of_contents => variate_settings.number_of_contents;

        #endregion

        #region A/B split options

        public MailChimpCampaignAbSplitOptions.MailChimpCampaignAbSplitTest ab_split_opts_split_test => ab_split_opts.split_test;
        public MailChimpCampaignAbSplitOptions.MailChimpCampaignAbSplitPickWinner ab_split_opts_pick_winner => ab_split_opts.pick_winner;
        public MailChimpCampaignAbSplitOptions.MailChimpCampaignAbSplitWaitUnit ab_split_opts_wait_units => ab_split_opts.wait_units;
        public int ab_split_opts_wait_time => ab_split_opts.wait_time;
        public int ab_split_opts_split_size => ab_split_opts.split_size;
        public string ab_split_opts_from_name_a => ab_split_opts.from_name_a;
        public string ab_split_opts_from_name_b => ab_split_opts.from_name_b;
        public string ab_split_opts_reply_email_a => ab_split_opts.reply_email_a;
        public string ab_split_opts_reply_email_b => ab_split_opts.reply_email_b;
        public string ab_split_opts_subject_a => ab_split_opts.subject_a;
        public string ab_split_opts_subject_b => ab_split_opts.subject_b;
        public DateTime? ab_split_opts_send_time_a => ab_split_opts.send_time_a;
        public DateTime? ab_split_opts_send_time_b => ab_split_opts.send_time_b;
        public DateTime? ab_split_opts_send_time_winner => ab_split_opts.send_time_winner;

        #endregion

        #region Social card

        public string social_card_title => social_card.title;
        public string social_card_description => social_card.description;
        public string social_card_image_url => social_card.image_url;

        #endregion

        #endregion

    }
}

