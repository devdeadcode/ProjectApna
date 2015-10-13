using System;
using System.ComponentModel;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Shopify.Models
{
    public class ShopifyTransaction : DataConnectorEntityModel
    {

        #region Enums

        public enum ShopifyTransactionKind
        {
            [Description("Authorization")]
            authorization,
            [Description("Capture")]
            capture,
            [Description("Sale")]
            sale,
            [Description("Void")]
            @void,
            [Description("Refund")]
            refund
        }
        public enum ShopifyTransactionStatus
        {
            [Description("Pending")]
            pending,
            [Description("Failure")]
            failure,
            [Description("Success")]
            success,
            [Description("Error")]
            error
        }

        #endregion

        #region Default properties

        [FieldSettings("Type", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(ShopifyTransactionKind), DefaultField = true)]
        public ShopifyTransactionKind kind { get; set; }

        [FieldSettings("Order Id", DefaultField = true)]
        public string order_id { get; set; }

        [FieldSettings("Amount", FieldTypeId = DataConnector.FieldTypeIdCurrency, DefaultField = true)]
        public decimal amount { get; set; }

        [FieldSettings("Status", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(ShopifyTransactionStatus), DefaultField = true)]
        public ShopifyTransactionStatus status { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Authorization")]
        public string authorization { get; set; }

        [FieldSettings("Device Id")]
        public string device_id { get; set; }

        [FieldSettings("Gateway")]
        public string gateway { get; set; }

        [FieldSettings("Source name")]
        public string source_name { get; set; }

        [FieldSettings("Id")]
        public string id { get; set; }

        [FieldSettings("Receipt")]
        public string receipt { get; set; }

        [FieldSettings("Test transaction")]
        public bool test { get; set; }

        [FieldSettings("User Id")]
        public string user_id { get; set; }

        [FieldSettings("Currency")]
        public string currency { get; set; }

        #endregion

        #region Hidden properties

        public DateTime? created_at { get; set; }
        public ShopifyPaymentDetails payment_details { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Created at date", Created = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime created_at_date
        {
            get
            {
                return created_at?.Date ?? DateTime.MinValue;
            }
            set
            {
                created_at = value;
            }
        }

        #region Payment details

        [FieldSettings("Address verification result code")]
        public string payment_details_avs_result_code { get; set; }

        [FieldSettings("Issuer identification number")]
        public string payment_details_credit_card_bin { get; set; }

        [FieldSettings("Credit card response code")]
        public string payment_details_cvv_result_code { get; set; }

        [FieldSettings("Credit card number")]
        public string payment_details_credit_card_number { get; set; }

        [FieldSettings("Credit card company")]
        public string payment_details_credit_card_company { get; set; }

        #endregion

        #endregion

    }
}
