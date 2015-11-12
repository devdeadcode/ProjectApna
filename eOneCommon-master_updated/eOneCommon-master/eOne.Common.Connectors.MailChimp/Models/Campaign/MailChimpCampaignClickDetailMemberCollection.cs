using System.Collections.Generic;

namespace eOne.Common.Connectors.MailChimp.Models.Campaign
{
    public class MailChimpCampaignClickDetailMemberCollection : MailChimpCollection
    {

        public List<MailChimpCampaignClickDetails> members { get; set; }
        public string campaign_id { get; set; }

    }
}
