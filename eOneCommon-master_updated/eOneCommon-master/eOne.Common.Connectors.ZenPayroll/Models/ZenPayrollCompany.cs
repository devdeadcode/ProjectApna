using System.Collections.Generic;

namespace eOne.Common.Connectors.ZenPayroll.Models
{
    public class ZenPayrollCompany
    {

        public int id { get; set; }
        public string name { get; set; }
        public string trade_name { get; set; }
        public List<ZenPayrollCompanyLocation> locations { get; set; }
        public ZenPayrollCompanyCompensations compensations { get; set; }

    }
}