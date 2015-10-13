using System.ComponentModel;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalMerchantPreferences : DataConnectorEntityModel
    {

        public enum PaypalMerchantPreferencesAutobill
        {
            [Description("Yes")]
            YES,
            [Description("No")]
            NO
        }
        public enum PaypalMerchantPreferencesFailAction
        {
            [Description("Continue")]
            CONTINUE,
            [Description("Cancel")]
            CANCEL
        }

        public string id { get; set; }
        public decimal setup_fee { get; set; }
        public string cancel_url { get; set; }
        public string return_url { get; set; }
        public string notify_url { get; set; }
        public int max_fail_attempts { get; set; }
        public PaypalMerchantPreferencesAutobill auto_bill_amount { get; set; }
        public PaypalMerchantPreferencesFailAction initial_fail_amount_action { get; set; }
        public string char_set { get; set; }
        public string accepted_payment_type { get; set; }

    }
}