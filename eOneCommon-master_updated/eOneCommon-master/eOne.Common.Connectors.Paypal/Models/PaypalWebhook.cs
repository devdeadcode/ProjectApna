using System.Collections.Generic;

namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalWebhook : ConnectorEntityModel
    {

        public string id { get; set; }
        public string url { get; set; }
        public List<PaypalLink> links { get; set; }
        public List<PaypalEventType> event_types { get; set; }

    }
}