using System;

namespace eOne.Common.Connectors.Intacct.Models
{
    public class IntacctDate
    {

        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }

        public DateTime date => new DateTime(year, month, day);
    }
}
