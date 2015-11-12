using System.Collections.Generic;

namespace eOne.Common.Connectors.MailChimp.Models.Campaign
{
    public class MailChimpCampaignFeedbackCollection : MailChimpCollection
    {

        public List<MailChimpCampaignFeedback> feedback { get; set; }
        public string campaign_id { get; set; }

    }
}