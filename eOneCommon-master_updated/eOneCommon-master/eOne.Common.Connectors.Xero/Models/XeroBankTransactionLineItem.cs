using System.Collections.Generic;

namespace eOne.Common.Connectors.Xero.Models
{
    public class XeroBankTransactionLineItem
    {

        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitAmount { get; set; }
        public string AccountCode { get; set; }
        public string ItemCode { get; set; }
        public XeroAccount.XeroAccountTaxType TaxType { get; set; }
        public decimal LineAmount { get; set; }
        public List<XeroTrackingCategory> Tracking { get; set; }

    }
}