using System.Collections.Generic;

namespace eOne.Common.Connectors.MailChimp.Models.Campaign
{
    public class MailChimpCampaignClickDetailsCollection : MailChimpCollection
    {

        public List<MailChimpCampaignClickDetails> urls_clicked { get; set; }
        public string campaign_id { get; set; }

    }
}
