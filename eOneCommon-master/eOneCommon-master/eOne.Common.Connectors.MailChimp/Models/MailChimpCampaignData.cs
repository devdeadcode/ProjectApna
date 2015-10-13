using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.MailChimp.Models
{
    public class MailChimpCampaignData : DataConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Title", DefaultField = true)]
        public string title { get; set; }

        [FieldSettings("Type", DefaultField = true)]
        public string type { get; set; }

        [FieldSettings("Status", DefaultField = true)]
        public string status { get; set; }

        [FieldSettings("Number of emails sent", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int emails_sent { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Id")]
        public string id { get; set; }

        [FieldSettings("Content type")]
        public string content_type { get; set; }

        [FieldSettings("Create time")]
        public string create_time { get; set; }

        [FieldSettings("Send time")]
        public string send_time { get; set; }

        [FieldSettings("From name")]
        public string from_name { get; set; }

        [FieldSettings("From email", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string from_email { get; set; }

        [FieldSettings("Subject")]
        public string subject { get; set; }

        [FieldSettings("To name")]
        public string to_name { get; set; }

        [FieldSettings("Archive URL", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string archive_url { get; set; }

        [FieldSettings("Inline CSS", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool inline_css { get; set; }

        [FieldSettings("Analytics")]
        public string analytics { get; set; }

        [FieldSettings("Analytics tag")]
        public string analytics_tag { get; set; }

        [FieldSettings("Authenticate", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool authenticate { get; set; }

        [FieldSettings("Ecomm 360", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool ecomm360 { get; set; }

        [FieldSettings("Auto tweet", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool auto_tweet { get; set; }

        [FieldSettings("Auto Facebook post")]
        public string auto_fb_post { get; set; }

        [FieldSettings("Auto footer", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool auto_footer { get; set; }

        [FieldSettings("Timewarp", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool timewarp { get; set; }

        [FieldSettings("Timewarp schedule")]
        public string timewarp_schedule { get; set; }

        [FieldSettings("Tests sent")]
        public string tests_sent { get; set; }

        [FieldSettings("Number of tests remaining", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int tests_remain { get; set; }

        [FieldSettings("Segment text")]
        public string segment_text { get; set; }

        [FieldSettings("Number of comments", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int comments_total { get; set; }

        [FieldSettings("Number of unread comments", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int comments_unread { get; set; }

        #endregion

        #region Hidden properties

        public MailChimpCampaignTrackingOptions tracking { get; set; }
        public MailChimpCampaignSavedSegment saved_segment { get; set; }
        public MailChimpCampaignSocialCard social_card { get; set; }
        public int web_id { get; set; }
        public string list_id { get; set; }
        public int folder_id { get; set; }
        public int template_id { get; set; }
        public List<string> segment_opts { get; set; }
        public string parent_id { get; set; }
        public bool is_child { get; set; }

        #endregion

        //struct	type_opts	the type-specific options for the campaign - can be passed to campaigns/create()
        //struct	summary	if available, the basic aggregate stats returned by reports/summary

    }
}
