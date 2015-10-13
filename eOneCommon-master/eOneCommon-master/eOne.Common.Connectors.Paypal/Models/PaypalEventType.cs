using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalEventType : DataConnectorEntityModel
    {

        public string name { get; set; }
        public string description { get; set; }

    }
}
