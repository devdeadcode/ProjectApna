using System;

namespace eOne.Common.Connectors.Etsy.Models
{
    public class EtsyPayment : ConnectorEntityModel
    {

        public enum EtsyPaymentStatus
        {
            settled,
            authed
        }

        public int payment_id { get; set; }
        public int buyer_user_id { get; set; }
        public int shop_id { get; set; }
        public int receipt_id { get; set; }
        public IsoCurrency currency { get; set; }
        public IsoCurrency shop_currency { get; set; }
        public IsoCurrency buyer_currency { get; set; }
        public int shipping_user_id { get; set; }
        public int shipping_address_id { get; set; }
        public int billing_address_id { get; set; }
        public EtsyPaymentStatus status { get; set; }

        public int amount_gross { get; set; }
        public int amount_fees { get; set; }
        public int amount_net { get; set; }
        public int posted_gross { get; set; }
        public int posted_fees { get; set; }
        public int posted_net { get; set; }
        public int adjusted_gross { get; set; }
        public int adjusted_fees { get; set; }
        public int adjusted_net { get; set; }
        public float shipped_date { get; set; }
        public float create_date { get; set; }
        public float update_date { get; set; }

        public DateTime date_shipped => FromEpochSeconds(shipped_date);
        public DateTime time_shipped => date_shipped;

        public DateTime date_created => FromEpochSeconds(create_date);
        public DateTime time_created => date_created;

        public DateTime date_updated => FromEpochSeconds(update_date);
        public DateTime time_updated => date_updated;

        public decimal gross_amount_usd => (decimal)(amount_gross / 100.0);

        public decimal fees_amount_usd => (decimal)(amount_fees / 100.0);

        public decimal net_amount_usd => (decimal)(amount_net / 100.0);
        public decimal gross_posted_usd => (decimal)(posted_gross / 100.0);

        public decimal fees_posted_usd => (decimal)(posted_fees / 100.0);
        public decimal net_posted_usd => (decimal)(posted_net / 100.0);
        public decimal gross_adjusted_usd => (decimal)(adjusted_gross / 100.0);
        public decimal fees_adjusted_usd => (decimal)(adjusted_fees / 100.0);
        public decimal net_adjusted_usd => (decimal)(adjusted_net / 100.0);
    }
}