using System;
using System.Collections.Generic;

namespace eOne.Common.Connectors.ZenPayroll.Models
{
    public class ZenPayrollPayPeriod
    {

        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public ZenPayrollPayPeriodPayroll payroll { get; set; }
        public List<ZenPayrollPayPeriodEligibleEmployee> eligible_employees { get; set; }

    }
}
