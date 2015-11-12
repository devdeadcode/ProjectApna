namespace eOne.Common.Connectors.Etsy.Models
{
    public class EtsyPaymentAdjustment
    {

        public enum EtsyPaymentAdjustmentStatus
        {
            OPEN, 
            REFUNDED, 
            ERROR, 
            REFUND_FAILED
        }

        public int payment_adjustment_id { get; set; }
        public int payment_id { get; set; }
        public EtsyPaymentAdjustmentStatus status { get; set; }
        public bool is_success { get; set; }
        public int user_id { get; set; }
        public string reason_code { get; set; }
        public int total_adjustment_amount { get; set; }
        public int shop_total_adjustment_amount { get; set; }
        public int buyer_total_adjustment_amount { get; set; }
        public int total_fee_adjustment_amount { get; set; }
        public float create_date { get; set; }
        public float update_date { get; set; }

    }
}
