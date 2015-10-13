using System.Collections.Generic;

namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalResourceSalesBase : PaypalResourceBase
    {
        
        public enum PaypalResourcePaymentMode
        {
            INSTANT_TRANSFER,
            MANUAL_BANK_TRANSFER,
            DELAYED_TRANSFER,
            ECHECK
        }
        public enum PaypalResourceProtectionEligibility
        {
            ELIGIBLE,
            PARTIALLY_ELIGIBLE,
            INELIGIBLE
        }
        public enum PaypalResourceProtectionEligibilityType
        {
            ITEM_NOT_RECEIVED_ELIGIBLE,
            UNAUTHORIZED_PAYMENT_ELIGIBLE
        }

        public string clearing_time { get; set; }
        public PaypalResourceProtectionEligibilityType protection_eligibility_type { get; set; }
        public PaypalResourceProtectionEligibility protection_eligibility { get; set; }
        public List<PaypalLink> links { get; set; }
        public PaypalFmfDetails fmf_details { get; set; }
        public PaypalResourcePaymentMode payment_mode { get; set; }
        
    }
}
