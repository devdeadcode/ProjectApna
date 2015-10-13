using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalWebhook : DataConnectorEntityModel
    {

        public string id { get; set; }
        public string url { get; set; }
        public List<PaypalLink> links { get; set; }
        public List<PaypalEventType> event_types { get; set; }

    }
}