using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Etsy.Models
{
    public class EtsyReceiptShipment : DataConnectorEntityModel
    {

        public string carrier_name { get; set; }
        public int receipt_shipping_id { get; set; }
        public string tracking_code { get; set; }
        public string tracking_url { get; set; }
        public string buyer_note { get; set; }
        public float notification_date { get; set; }

    }
}