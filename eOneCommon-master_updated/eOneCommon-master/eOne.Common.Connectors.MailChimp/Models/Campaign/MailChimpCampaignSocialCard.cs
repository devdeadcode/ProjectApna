namespace eOne.Common.Connectors.MailChimp.Models.Campaign
{
    public class MailChimpCampaignSocialCard : ConnectorEntityModel
    {

        public string title { get; set; }
        public string description { get; set; }
        public string image_url { get; set; }

    }
}