using System.Collections.Generic;

namespace eOne.Common.Connectors.Etsy.Models
{
    public class EtsyShop : ConnectorEntityModel
    {

        public int shop_id { get; set; }
        public string shop_name { get; set; }
        public int user_id { get; set; }
        public float creation_tsz { get; set; }
        public string title { get; set; }
        public string announcement { get; set; }
        public IsoCurrency currency_code { get; set; }
        public bool is_vacation { get; set; }
        public string vacation_message { get; set; }
        public string sale_message { get; set; }
        public string digital_sale_message { get; set; }
        public float last_updated_tsz { get; set; }
        public int listing_active_count { get; set; }
        public string login_name { get; set; }
        public float lat { get; set; }
        public float lon { get; set; }
        public bool accepts_custom_requests { get; set; }
        public string policy_welcome { get; set; }
        public string policy_payment { get; set; }
        public string policy_shipping { get; set; }
        public string policy_refunds { get; set; }
        public string policy_additional { get; set; }
        public string policy_seller_info { get; set; }
        public float policy_updated_tsz { get; set; }
        public string vacation_autoreply { get; set; }
        public string ga_code { get; set; }
        public string url { get; set; }
        public string image_url_760x100 { get; set; }
        public int num_favorers { get; set; }
        public List<string> languages { get; set; }
        public int upcoming_local_event_id { get; set; }
        public string icon_url_fullxfull { get; set; }

    }
}