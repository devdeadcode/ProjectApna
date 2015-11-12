namespace eOne.Common.Connectors.MailChimp.Models.Campaign
{
    public class MailChimpCampaignSavedSegment : ConnectorEntityModel
    {

        public int id { get; set; }
        public string type { get; set; }
        public string name { get; set; }

    }
}
