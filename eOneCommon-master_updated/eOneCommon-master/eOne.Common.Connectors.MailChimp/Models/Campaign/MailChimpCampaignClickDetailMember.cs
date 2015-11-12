namespace eOne.Common.Connectors.MailChimp.Models.Campaign
{
    public class MailChimpCampaignClickDetailMember
    {

        #region Default properties

        [FieldSettings("Campaign name", DefaultField = true)]
        public string campaign_settings_title => campaign.settings_title;

        [FieldSettings("Link", DefaultField = true, FieldTypeId = Connector.FieldTypeIdUrl)]
        public string click_details_url => click_details.url;

        [FieldSettings("Email address", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEmail)]
        public string email_address { get; set; }

        [FieldSettings("Number of clicks", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int clicks { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Email ID", KeyNumber = 1)]
        public string email_id { get; set; }

        [FieldSettings("Campaign ID")]
        public string campaign_id { get; set; }

        [FieldSettings("Link ID")]
        public string url_id { get; set; }

        #endregion

        #region Hidden properties

        public string list_id { get; set; }
        public MailChimpCampaign campaign { get; set; }
        public MailChimpCampaignClickDetails click_details { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Campaign type", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(MailChimpCampaign.MailChimpCampaignType))]
        public MailChimpCampaign.MailChimpCampaignType campaign_type => campaign.type;

        [FieldSettings("Campaign status", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(MailChimpCampaign.MailChimpCampaignStatus))]
        public MailChimpCampaign.MailChimpCampaignStatus campaign_status => campaign.status;

        #endregion

    }
}