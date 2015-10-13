using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.MailChimp.Models
{
    public class MailChimpListError : DataConnectorEntityModel
    {

        public string param { get; set; }
        public int code { get; set; }
        public string error { get; set; }

    }
}
