using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.MailChimp.Models
{
    public class MailChimpCampaignTrackingOptions : DataConnectorEntityModel
    {

        public bool html_clicks { get; set; }
        public bool text_clicks { get; set; }
        public bool opens { get; set; }

    }
}
