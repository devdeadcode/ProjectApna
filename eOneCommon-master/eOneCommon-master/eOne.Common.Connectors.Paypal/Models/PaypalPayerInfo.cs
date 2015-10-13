using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalPayerInfo : DataConnectorEntityModel
    {

        public enum PaypalPayerInfoTaxType
        {
            BR_CPF,
            BR_CNPJ
        }

        public string email { get; set; }
        public string salutation { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string suffix { get; set; }
        public string payer_id { get; set; }
        public PaypalShippingAddress shipping_address { get; set; }
        public string phone { get; set; }
        public string country_code { get; set; }
        public PaypalPayerInfoTaxType tax_id_type { get; set; }
        public string tax_id { get; set; }

    }
}