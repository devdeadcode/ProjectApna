using System;

namespace eOne.Common.Connectors.Intacct.Models
{
    public class IntacctPriceList : IntacctBase
    {

        public string NAME { get; set; }
        public DateTime? DATEFROM { get; set; }
        public DateTime? DATETO { get; set; }
        public string SALEPURCHASE { get; set; }

    }
}