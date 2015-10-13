using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalFmfDetails : DataConnectorEntityModel
    {

        public enum PaypalFmfDetailsFilterType
        {
            ACCEPT,
            PENDING,
            DENY,
            REPORT
        }
        public enum PaypalFmfDetailsFilterId
        {
            MAXIMUM_TRANSACTION_AMOUNT,
            UNCONFIRMED_ADDRESS,
            COUNTRY_MONITOR,
            AVS_NO_MATCH,
            AVS_PARTIAL_MATCH,
            AVS_UNAVAILABLE_OR_UNSUPPORTED,
            CARD_SECURITY_CODE_MISMATCH,
            BILLING_OR_SHIPPING_ADDRESS_MISMATCH,
            RISKY_ZIP_CODE,
            SUSPECTED_FREIGHT_FORWARDER_CHECK,
            RISKY_EMAIL_ADDRESS_DOMAIN_CHECK,
            RISKY_BANK_IDENTIFICATION_NUMBER_CHECK,
            RISKY_IP_ADDRESS_RANGE,
            LARGE_ORDER_NUMBER,
            TOTAL_PURCHASE_PRICE_MINIMUM,
            IP_ADDRESS_VELOCITY,
            PAYPAL_FRAUD_MODEL
        }

        public PaypalFmfDetailsFilterType filter_type { get; set; }
        public PaypalFmfDetailsFilterId filter_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

    }
}

