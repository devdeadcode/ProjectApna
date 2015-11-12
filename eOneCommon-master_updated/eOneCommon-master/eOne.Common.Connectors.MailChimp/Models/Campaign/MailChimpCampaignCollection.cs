using System.Collections.Generic;

namespace eOne.Common.Connectors.MailChimp.Models.Campaign
{
    public class MailChimpCampaignCollection : MailChimpCollection
    {

        public List<MailChimpCampaign> campaigns { get; set; }

    }
}