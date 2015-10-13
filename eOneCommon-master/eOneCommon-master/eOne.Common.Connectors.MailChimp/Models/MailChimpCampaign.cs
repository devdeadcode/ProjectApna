using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.MailChimp.Models
{
    public class MailChimpCampaign : DataConnectorEntityModel
    {

        public int total { get; set; }
        public List<MailChimpCampaignData> data { get; set; }
        public List<MailChimpCampaignError> errors { get; set; }

    }
}
