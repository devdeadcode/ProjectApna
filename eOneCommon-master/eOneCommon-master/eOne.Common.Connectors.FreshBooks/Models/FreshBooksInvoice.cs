using System;
using System.Collections.Generic;
using System.ComponentModel;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.FreshBooks.Models
{
    class FreshBooksInvoice : DataConnectorEntityModel
    {

        public enum FreshBooksInvoiceStatus
        {
            [Description("Paid")]
            paid
        }
        public enum FreshBooksInvoiceFolder
        {
            [Description("Active")]
            active,
            [Description("Archived")]
            archived,
            [Description("Deleted")]
            deleted
        }

        public string number { get; set; }
        public DateTime date { get; set; }
        public string organization { get; set; }
        public decimal amount { get; set; }
        public FreshBooksInvoiceStatus status { get; set; }
        public FreshBooksInvoiceFolder folder { get; set; }
        
        public string currency_code { get; set; }
        public string language { get; set; }
        public decimal amount_outstanding { get; set; }
        public string po_number { get; set; }
        public decimal discount { get; set; }
        public string notes { get; set; }
        public string terms { get; set; }
        public string return_uri { get; set; }
        public DateTime updated { get; set; }
        public int recurring_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string p_street1 { get; set; }
        public string p_street2 { get; set; }
        public string p_city { get; set; }
        public string p_state { get; set; }
        public string p_country { get; set; }
        public string p_code { get; set; }
        public string vat_name { get; set; }
        public string vat_number { get; set; }

        public List<FreshBooksInvoiceLine> lines { get; set; }
        public FreshBooksInvoiceLinks links { get; set; }
        public int invoice_id { get; set; }
        public int client_id { get; set; }
        public List<FreshBookContact> contacts { get; set; }
        public int staff_id { get; set; }

    }
}
