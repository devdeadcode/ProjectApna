using System.Collections.Generic;

namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalPaymentDefinition : ConnectorEntityModel
    {

        public enum PaypalPaymentDefinitionType
        {
            TRIAL, 
            REGULAR
        }
        public enum PaypalPaymentFrequency
        {
            WEEK, 
            DAY, 
            YEAR, 
            MONTH
        }

        public string id { get; set; }
        public string name { get; set; }
        public PaypalPaymentDefinitionType type { get; set; }
        public string frequency_interval { get; set; }
        public PaypalPaymentFrequency frequency { get; set; }
        public int cycles { get; set; }
        public decimal amount { get; set; }
        public List<PaypalChargeModel> charge_models { get; set; }

    }
}
