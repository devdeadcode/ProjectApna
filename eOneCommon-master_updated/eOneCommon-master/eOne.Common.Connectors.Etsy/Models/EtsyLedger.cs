namespace eOne.Common.Connectors.Etsy.Models
{
    public class EtsyLedger : ConnectorEntityModel
    {

        public int ledger_id { get; set; }
        public string shop_id { get; set; }
        public string currency { get; set; }
        public float create_date { get; set; }
        public float update_date { get; set; }

    }
}