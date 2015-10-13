using System;
using System.ComponentModel;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Etsy.Models
{
    public class EtsyFeedback : DataConnectorEntityModel
    {

        #region Enums

        public enum EtsyFeedbackType
        {
            [Description("Positive")]
            positive,
            [Description("Negative")]
            negative,
            [Description("Neutral")]
            neutral
        }

        #endregion

        #region Default properties

        [FieldSettings("Transaction title", DefaultField = true)]
        public string transaction_title => transaction.title;

        [FieldSettings("Date", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime creation_date => FromEpochSeconds(creation_tsz).Date;

        [FieldSettings("Type", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(EtsyFeedbackType))]
        public EtsyFeedbackType type
        {
            get
            {
                if (value == 0) return EtsyFeedbackType.neutral;
                return value < 0 ? EtsyFeedbackType.negative : EtsyFeedbackType.positive;
            }
        }

        [FieldSettings("Message", DefaultField = true)]
        public string message { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Feedback ID", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int feedback_id { get; set; }

        #endregion

        #region Hidden properties

        public int transaction_id { get; set; }
        public int creator_user_id { get; set; }
        public int target_user_id { get; set; }
        public int seller_user_id { get; set; }
        public int buyer_user_id { get; set; }
        public float creation_tsz { get; set; }
        public int image_feedback_id { get; set; }
        public int image_url_25x25 { get; set; }
        public int image_url_155x125 { get; set; }
        public int image_url_fullxfull { get; set; }
        public int value { get; set; }
        public EtsyTransaction transaction { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Time", FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime creation_time => Time(FromEpochSeconds(creation_tsz));

        [FieldSettings("Transaction description")]
        public string transaction_description => transaction.description;

        [FieldSettings("Transaction price", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal transaction_price => transaction.price;

        [FieldSettings("Transaction currency", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(IsoCurrency))]
        public IsoCurrency transaction_currency_code => transaction.currency_code;

        [FieldSettings("Transaction quantity", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int transaction_quantity => transaction.quantity;

        [FieldSettings("Transaction shipping cost", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal transaction_shipping_cost => transaction.shipping_cost;

        [FieldSettings("Digital transaction", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool transaction_is_digital => transaction.is_digital;

        [FieldSettings("Digtial transaction file data")]
        public string transaction_file_data => transaction.file_data;

        [FieldSettings("Quick sale transaction", FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool transaction_is_quick_sale => transaction.is_quick_sale;

        [FieldSettings("Transaction link", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string transaction_url => transaction.url;

        #endregion

    }
}