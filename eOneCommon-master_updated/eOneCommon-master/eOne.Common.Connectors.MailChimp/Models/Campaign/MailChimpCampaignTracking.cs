namespace eOne.Common.Connectors.MailChimp.Models.Campaign
{
    public class MailChimpCampaignTracking : ConnectorEntityModel
    {

        public bool html_clicks { get; set; }
        public bool text_clicks { get; set; }
        public bool opens { get; set; }
        public bool goal_tracking { get; set; }
        public bool ecomm360 { get; set; }
        public bool google_analytics { get; set; }
        public string clicktale { get; set; }
        public MailChimpCampaignTrackingSalesforce salesforce { get; set; }
        public MailChimpCampaignTrackingHighrise highrise { get; set; }
        public MailChimpCampaignTrackingCapsule capsule { get; set; }

        public bool salesforce_campaign => salesforce.campaign;
        public bool salesforce_notes => salesforce.notes;
        public bool highrise_campaign => highrise.campaign;
        public bool highrise_notes => highrise.notes;
        public bool capsule_notes => capsule.notes;

    }
}
