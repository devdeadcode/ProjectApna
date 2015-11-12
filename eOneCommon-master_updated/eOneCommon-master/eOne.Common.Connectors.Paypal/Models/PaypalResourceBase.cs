using System;

namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalResourceBase : ConnectorEntityModel
    {

        public string id { get; set; }
        public PaypalAmount amount { get; set; }
        public DateTime create_time { get; set; }
        public DateTime update_time { get; set; }
        public string parent_payment { get; set; }

    }
}