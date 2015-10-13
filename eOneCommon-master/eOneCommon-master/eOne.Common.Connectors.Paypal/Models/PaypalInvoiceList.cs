using System.Collections.Generic;

namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalInvoiceList
    {

        public int total_count { get; set; }
        public List<PaypalInvoice> invoices { get; set; }

    }
}


