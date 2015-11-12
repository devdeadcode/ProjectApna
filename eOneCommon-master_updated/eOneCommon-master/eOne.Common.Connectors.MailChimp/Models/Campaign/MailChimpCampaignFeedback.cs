using System;
using System.ComponentModel;

namespace eOne.Common.Connectors.MailChimp.Models.Campaign
{
    public class MailChimpCampaignFeedback : ConnectorEntityModel
    {

        #region Enums

        public enum MailChimpCampaignFeedbackSource
        {
            [Description("Email")]
            email,
            [Description("SMS")]
            sms,
            [Description("Web")]
            web,
            [Description("iOS")]
            ios,
            [Description("Android")]
            android,
            [Description("API")]
            api
        }

        #endregion

        #region Default properties

        [FieldSettings("Campaign name", DefaultField = true)]
        public string campaign_settings_title => campaign.settings_title;

        [FieldSettings("Message", DefaultField = true)]
        public string message { get; set; }

        [FieldSettings("Created by", DefaultField = true)]
        public string created_by { get; set; }

        [FieldSettings("Created at date", Created = true, DefaultField = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime created_at { get; set; }

        [FieldSettings("Complete", DefaultField = true, FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool is_complete { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Feedback ID", FieldTypeId = Connector.FieldTypeIdInteger, KeyNumber = 1)]
        public int feedback_id { get; set; }

        [FieldSettings("Updated at date", Modified = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime updated_at { get; set; }

        [FieldSettings("Campaign ID")]
        public string campaign_id { get; set; }

        [FieldSettings("Source", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(MailChimpCampaignFeedbackSource))]
        public MailChimpCampaignFeedbackSource source { get; set; }

        #endregion

        #region Hidden properties

        public int parent_id { get; set; }
        public int block_id { get; set; }
        public MailChimpCampaign campaign { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Campaign type", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(MailChimpCampaign.MailChimpCampaignType))]
        public MailChimpCampaign.MailChimpCampaignType campaign_type => campaign.type;

        [FieldSettings("Campaign status", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(MailChimpCampaign.MailChimpCampaignStatus))]
        public MailChimpCampaign.MailChimpCampaignStatus campaign_status => campaign.status;

        #endregion

    }
}