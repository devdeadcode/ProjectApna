namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalSale : PaypalResourceSalesBase
    {

        public enum PaypalSaleState
        {
            pending,
            completed,
            refunded,
            partially_refunded
        }
        public enum PaypalSaleReasonCode
        {
            CHARGEBACK, 
            GUARANTEE, 
            BUYER_COMPLAINT, 
            REFUND, 
            UNCONFIRMED_SHIPPING_ADDRESS, 
            ECHECK, 
            INTERNATIONAL_WITHDRAWAL, 
            RECEIVING_PREFERENCE_MANDATES_MANUAL_ACTION, 
            PAYMENT_REVIEW, 
            REGULATORY_REVIEW, 
            UNILATERAL, 
            VERIFICATION_REQUIRED
        }

        public string description { get; set; }
        
        public PaypalSaleState state { get; set; }
        public PaypalSaleReasonCode reason_code { get; set; }
        public decimal transaction_fee { get; set; }
        public decimal receivable_amount { get; set; }
        public string exchange_rate { get; set; }
        public string receipt_id { get; set; }
        
    }
}