using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalItem : DataConnectorEntityModel
    {
        
        public string quantity { get; set; }
        public string name { get; set; }
        public string price { get; set; }
        public string currency { get; set; }
        public string sku { get; set; }
        public string description { get; set; }
        public string tax { get; set; }

    }
}


