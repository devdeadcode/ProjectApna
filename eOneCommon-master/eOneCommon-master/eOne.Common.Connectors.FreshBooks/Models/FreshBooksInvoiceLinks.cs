using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.FreshBooks.Models
{
    public class FreshBooksInvoiceLinks : DataConnectorEntityModel
    {

        public string client_view { get; set; }
        public string view { get; set; }
        public string edit { get; set; }

    }
}
