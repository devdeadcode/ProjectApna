
using System;
using System.ComponentModel;
using System.Security.AccessControl;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Stripe.Models
{
    class StripeCustomerDiscountCoupon : DataConnectorEntityModel
    {
        public enum StripeCuestomreDiscountCouponsDuration
        {
            [Description("Forever")]
            FOREVER,
            [Description("Once")]
            ONCE,
            [Description("Repeating")]
            REPEATING
        }

        #region Default Properties
        [FieldSettings("Coupon ID", DefaultField = true)]
        public string id { get; set; }

        [FieldSettings("Percent off", FieldTypeId = DataConnector.FieldTypeIdQuantity, DefaultField = true)]
        public decimal percent_off { get; set; }

        [FieldSettings("Valid", FieldTypeId = DataConnector.FieldTypeIdYesNo, DefaultField = true)]
        public bool valid { get; set; }
        #endregion

        #region General properties
        
        [FieldSettings("Currency", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public string currency { get; set; }

        [FieldSettings("Duration", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(StripeCuestomreDiscountCouponsDuration))]
        public StripeCuestomreDiscountCouponsDuration duration { get; set; }

        [FieldSettings("Number of months", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int? duration_in_months { get; set; }

        [FieldSettings("Maximum number of redemptions", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int? max_redemptions { get; set; }

        [FieldSettings("Number of times redeemed", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int times_redeemed { get; set; }

        [FieldSettings("Redeem by", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public long? redeem_by { get; set; }

        #endregion

        #region Hidden properties
        public decimal? amount_off { get; set; }

        public long created { get; set; }

        public StripeCustomers cm { get; set; }
        #endregion

        #region Calculations

        [FieldSettings("Amount off", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? amt_off
        {
            get
            {
                if (amount_off != 0) return amount_off/100;
                return 0;
            }
        }

        [FieldSettings("Type", DefaultField = true)]
        public string type
        {
            get
            {
                if (amount_off > 0) return "Amount";
                return percent_off > 0 ? "Percent" : "None";
            }
        }

        [FieldSettings("Created date", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime created_date
        {
            get
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return epoch.AddSeconds(created);
            }
        }

        [FieldSettings("Fully redeemed", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool fully_redeemed
        {
            get
            {
                if (max_redemptions == null) return false;
                return times_redeemed >= max_redemptions;
            }
        }

        [FieldSettings("Expired", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool expired
        {
            get
            {
                if (redeem_by == null) return false;
                var t = DateTime.UtcNow - new DateTime(1970, 1, 1);
                var today = (long) t.TotalSeconds;
                return redeem_by < today;
            }
        }

        [FieldSettings("Total cost", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? total_cost
        {
            get
            {
                if (amount_off != null) return times_redeemed*amount_off;
                return 0;
            }
        }

        [FieldSettings("Remaining redemptions", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int? remaining_redemptions
        {
            get
            {
                if (max_redemptions != null) return (max_redemptions - times_redeemed);
                return 0;
            }
        }

        [FieldSettings("Percentage of redemptions remaining", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal? perc_of_remainings
        {
            get
            {
                if (max_redemptions != null) return (remaining_redemptions/max_redemptions)*100;
                return 0;
            }
        }


        #endregion

    }
}
