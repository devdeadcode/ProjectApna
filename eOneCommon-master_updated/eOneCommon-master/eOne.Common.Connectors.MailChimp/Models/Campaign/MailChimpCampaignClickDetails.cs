using System;

namespace eOne.Common.Connectors.MailChimp.Models.Campaign
{
    public class MailChimpCampaignClickDetails
    {

        #region Default properties

        [FieldSettings("Campaign name", DefaultField = true)]
        public string campaign_settings_title => campaign.settings_title;

        [FieldSettings("Link", DefaultField = true, FieldTypeId = Connector.FieldTypeIdUrl)]
        public string url { get; set; }

        [FieldSettings("Total clicks", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int total_clicks { get; set; }

        [FieldSettings("Click percentage", DefaultField = true, FieldTypeId = Connector.FieldTypeIdPercentage)]
        public decimal click_percentage { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Unique clicks", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int unique_clicks { get; set; }

        [FieldSettings("Unique click percentage", FieldTypeId = Connector.FieldTypeIdPercentage)]
        public decimal unique_click_percentage { get; set; }

        [FieldSettings("Last click date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime last_click { get; set; }

        [FieldSettings("Campaign ID")]
        public string campaign_id { get; set; }

        [FieldSettings("Click detail ID", KeyNumber = 1)]
        public string id { get; set; }

        #endregion

        #region Hidden properties

        public MailChimpCampaign campaign { get; set; }
        public MailChimpCampaignAbSplitClickDetails ab_split { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Total clicks - A", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int total_clicks_a => ab_split.a.total_clicks_a;

        [FieldSettings("Total clicks - B", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int total_clicks_b => ab_split.b.total_clicks_b;

        [FieldSettings("Click percentage - A", FieldTypeId = Connector.FieldTypeIdPercentage)]
        public decimal click_percentage_a => ab_split.a.click_percentage_a;

        [FieldSettings("Click percentage - B", FieldTypeId = Connector.FieldTypeIdPercentage)]
        public decimal click_percentage_b => ab_split.b.total_clicks_b;

        [FieldSettings("Unique clicks - A", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int unique_clicks_a => ab_split.a.unique_clicks_a;

        [FieldSettings("Unique clicks - B", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int unique_clicks_b => ab_split.b.unique_clicks_b;

        [FieldSettings("Unique click percentage - A", FieldTypeId = Connector.FieldTypeIdPercentage)]
        public decimal unique_click_percentage_a => ab_split.a.unique_click_percentage_a;

        [FieldSettings("Unique click percentage - B", FieldTypeId = Connector.FieldTypeIdPercentage)]
        public decimal unique_click_percentage_b => ab_split.b.unique_click_percentage_b;

        [FieldSettings("Campaign type", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(MailChimpCampaign.MailChimpCampaignType))]
        public MailChimpCampaign.MailChimpCampaignType campaign_type => campaign.type;

        [FieldSettings("Campaign status", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(MailChimpCampaign.MailChimpCampaignStatus))]
        public MailChimpCampaign.MailChimpCampaignStatus campaign_status => campaign.status;

        [FieldSettings("Last click time", FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime last_click_time => last_click;

        #endregion

    }
}