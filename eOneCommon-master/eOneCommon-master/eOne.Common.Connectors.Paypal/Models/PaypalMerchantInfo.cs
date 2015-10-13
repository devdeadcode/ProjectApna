namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalMerchantInfo
    {

        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public PaypalAddress address { get; set; }
        public string business_name { get; set; }
        public string phone { get; set; }
        public string fax { get; set; }
        public string website { get; set; }
        public string tax_id { get; set; }
        public string additional_info { get; set; }

    }
}