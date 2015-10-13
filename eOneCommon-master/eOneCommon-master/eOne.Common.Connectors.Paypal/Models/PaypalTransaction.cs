using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalTransaction : DataConnectorEntityModel
    {

        public string invoice_number { get; set; }
        public string description { get; set; }
        public string custom { get; set; }
        public string soft_descriptor { get; set; }

        public List<PaypalItem> item_list { get; set; }
        public List<PaypalResource> related_resources { get; set; }
        public PaypalPaymentOptions payment_options { get; set; }
        public PaypalAmount amount { get; set; }

    }
}
