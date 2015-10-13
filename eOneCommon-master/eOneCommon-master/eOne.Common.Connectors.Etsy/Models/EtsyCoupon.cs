using System;
using System.ComponentModel;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Etsy.Models
{
    public class EtsyCoupon : DataConnectorEntityModel
    {

        #region Enums

        public enum EtsyCouponType
        {
            [Description("Fixed discount")]
            fixed_discount,
            [Description("Percentage discount")]
            pct_discount,
            [Description("Free shipping")]
            free_shipping
        }

        #endregion

        #region Default properties

        [FieldSettings("Coupon code", DefaultField = true)]
        public string coupon_code { get; set; }

        [FieldSettings("Type", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(EtsyCouponType))]
        public EtsyCouponType coupon_type { get; set; }

        [FieldSettings("Active", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool seller_active { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Coupon ID", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int coupon_id { get; set; }

        [FieldSettings("Discount percentage", FieldTypeId = DataConnector.FieldTypeIdPercentage)]
        public int pct_discount { get; set; }

        [FieldSettings("Free shipping", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool free_shipping { get; set; }

        [FieldSettings("Domestic only", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool domestic_only { get; set; }

        [FieldSettings("Currency", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(IsoCurrency))]
        public IsoCurrency currency_code { get; set; }

        [FieldSettings("Fixed discount amount", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal fixed_discount { get; set; }

        [FieldSettings("Minimum purchase price", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal minimum_purchase_price { get; set; }

        #endregion

        #region Hidden properties

        public float expiration_date { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Expiry date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime date_expires => FromEpochSeconds(expiration_date).Date;

        #endregion

    }
}
