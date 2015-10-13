using System;
using System.Collections.Generic;
using System.ComponentModel;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalBillingPlan: DataConnectorEntityModel
    {

        #region Enums

        public enum PaypalBillingPlanState
        {
            [Description("Created")]
            CREATED,
            [Description("Active")]
            ACTIVE,
            [Description("Inactive")]
            INACTIVE,
            [Description("Deleted")]
            DELETED
        }
        public enum PaypalBillingPlanType
        {
            [Description("Fixed")]
            FIXED,
            [Description("Infinite")]
            INFINITE
        }

        #endregion

        #region Default properties

        [FieldSettings("Name", DefaultField = true)]
        public string name { get; set; }

        [FieldSettings("Description", DefaultField = true)]
        public string description { get; set; }

        [FieldSettings("Status", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(PaypalBillingPlanState))]
        public PaypalBillingPlanState state { get; set; }

        [FieldSettings("Type", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(PaypalBillingPlanType))]
        public PaypalBillingPlanType type { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Plan ID", KeyNumber = 1)]
        public string id { get; set; }

        #endregion

        #region Hidden properties

        public DateTime create_time { get; set; }
        public DateTime update_time { get; set; }
        public List<PaypalLink> links { get; set; }
        public PaypalMerchantPreferences merchant_preferences { get; set; }
        public List<PaypalTerm> terms { get; set; }
        public List<PaypalPaymentDefinition> payment_definitions { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Created at date", Created = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime createDate => create_time.Date;

        [FieldSettings("Updated at date", Modified = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime updateDate => update_time.Date;

        [FieldSettings("Created at time", Created = true, FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime createTime => Time(create_time);

        [FieldSettings("Updated at time", Modified = true, FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime updateTime => Time(update_time);

        [FieldSettings("Merchant preferences setup fee", FieldTypeId = DataConnector.FieldTypeIdCurrency)]
        public decimal merchant_preferences_setup_fee => merchant_preferences.setup_fee;

        [FieldSettings("Merchant preferences cancel URL", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string merchant_preferences_cancel_url => merchant_preferences.cancel_url;

        [FieldSettings("Merchant preferences return URL", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string merchant_preferences_return_url => merchant_preferences.return_url;

        [FieldSettings("Merchant preferences notify URL", FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string merchant_preferences_notify_url => merchant_preferences.notify_url;

        [FieldSettings("Merchant preferences max fail attempts", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int merchant_preferences_max_fail_attempts => merchant_preferences.max_fail_attempts;

        [FieldSettings("Merchant preferences auto-bill")]
        public PaypalMerchantPreferences.PaypalMerchantPreferencesAutobill merchant_preferences_auto_bill_amount => merchant_preferences.auto_bill_amount;

        [FieldSettings("Merchant preferences initial fail amount action", FieldTypeId = DataConnector.FieldTypeIdEnum, EnumType = typeof(PaypalMerchantPreferences.PaypalMerchantPreferencesFailAction))]
        public PaypalMerchantPreferences.PaypalMerchantPreferencesFailAction merchant_preferences_initial_fail_amount_action => merchant_preferences.initial_fail_amount_action;

        [FieldSettings("Merchant preferences character set")]
        public string merchant_preferences_char_set => merchant_preferences.char_set;

        [FieldSettings("Merchant preferences accepted payment type")]
        public string merchant_preferences_accepted_payment_type => merchant_preferences.accepted_payment_type;

        #endregion

    }
}