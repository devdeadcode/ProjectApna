using System;

namespace eOne.Common.Connectors.Xero.Models
{
    public class XeroAllocation
    {

        public XeroInvoice Invoice { get; set; }
        public decimal AppliedAmount { get; set; }
        public DateTime Date { get; set; }

    }
}