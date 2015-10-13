using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.MailChimp.Models
{
    public class MailChimpCampaignSocialCard : DataConnectorEntityModel
    {

        public string title { get; set; }
        public string description { get; set; }
        public string image_url { get; set; }
        public string enabled { get; set; }

    }
}
