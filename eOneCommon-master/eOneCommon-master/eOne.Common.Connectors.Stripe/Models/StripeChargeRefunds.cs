using System;
using System.ComponentModel;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Stripe.Models
{
    class StripeChargeRefunds : DataConnectorEntityModel
    {
        #region enum
        public enum StripeRefundReason
        {
            [Description("Duplicate")]
            duplicate,
            [Description("Fraudulent")]
            fraudulent,
            [Description("Requested by customer")]
            requested_by_customer
        }
        #endregion

        #region Default properties
        [FieldSettings("Refund ID", DefaultField = true)]
        public string id { get; set; }

        [FieldSettings("Reason", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(StripeRefundReason))]
        public StripeRefundReason? reason { get; set; }

        #endregion

        #region General properties

        [FieldSettings("Currency")]
        public string currency { get; set; }
       
        [FieldSettings("Receipt number")]
        public string receipt_number { get; set; }

        [FieldSettings("Description")]
        public string description { get; set; }

        [FieldSettings("Charge ID")]
        public string charge_id => ch.id;

        [FieldSettings("Charge description")]
        public string charge_description => ch.description;

        [FieldSettings("Charge status", FieldTypeId = DataConnector.FieldTypeIdEnum,
            EnumType = typeof (StripeCharges.StripeChargesStatus))]
        public StripeCharges.StripeChargesStatus charge_status => ch.status;
        #endregion

        #region Hidden properties

        public StripeCharges ch { get; set; }
        public long created { get; set; }
        public decimal amount { get; set; }
        #endregion

        #region Calculations
        [FieldSettings("Created date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime created_date
        {
            get
            {
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return epoch.AddSeconds(created);
            }
        }

        [FieldSettings("Amount", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal amount_per
        {
            get
            {
                if (amount != 0) return amount / 100;
                return 0;
            }

        }

        [FieldSettings("Charge amount", FieldTypeId = DataConnector.FieldTypeIdQuantity)]
        public decimal charge_amount
        {
            get
            {
                if (ch.amount != 0) return ch.amount / 100;
                return 0;
            }

        }
        #endregion
    }
}
