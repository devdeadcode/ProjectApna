using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.FreshBooks.Models
{
    public class FreshBooksInvoiceLine : DataConnectorEntityModel
    {

        public enum FreshBooksInvoiceLineType
        {
            Item
        }

        public int line_id { get; set; }
        public decimal amount { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal unit_cost { get; set; }
        public int quantity { get; set; }
        public string tax1_name { get; set; }
        public string tax2_name { get; set; }
        public decimal tax1_percent { get; set; }
        public decimal tax2_percent { get; set; }
        public FreshBooksInvoiceLineType type { get; set; }

    }
}
