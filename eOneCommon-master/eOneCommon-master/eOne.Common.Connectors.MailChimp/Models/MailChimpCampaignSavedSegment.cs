using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.MailChimp.Models
{
    public class MailChimpCampaignSavedSegment : DataConnectorEntityModel
    {

        public int id { get; set; }
        public string type { get; set; }
        public string name { get; set; }

    }
}
