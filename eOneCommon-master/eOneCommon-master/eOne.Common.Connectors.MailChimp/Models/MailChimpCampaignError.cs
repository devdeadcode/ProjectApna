using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.MailChimp.Models
{
    public class MailChimpCampaignError : DataConnectorEntityModel
    {

        public string filter { get; set; }
        public string value { get; set; }
        public int code { get; set; }
        public string error { get; set; }

    }
}
