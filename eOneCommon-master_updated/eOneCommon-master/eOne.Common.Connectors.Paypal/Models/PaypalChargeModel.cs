namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalChargeModel : ConnectorEntityModel
    {

        public enum PaypalChargeModelType
        {
            SHIPPING, 
            TAX
        }

        public string id { get; set; }
        public PaypalChargeModelType type { get; set; }
        public decimal amount { get; set; }

    }
}