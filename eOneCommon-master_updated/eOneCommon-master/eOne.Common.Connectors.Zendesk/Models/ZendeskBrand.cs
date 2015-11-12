using System;

namespace eOne.Common.Connectors.Zendesk.Models
{
    public class ZendeskBrand
    {

        public enum ZendeskBrandHelpCenterState
        {
            enabled, 
            disabled,
            restricted
        }

        public string url { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string brand_url { get; set; }
        public bool has_help_center { get; set; }
        public ZendeskBrandHelpCenterState help_center_state { get; set; }
        public bool active { get; set; }
        public bool @default { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string subdomain { get; set; }
        public string host_mapping { get; set; }
        public ZendeskAttachment logo { get; set; }

    }
}