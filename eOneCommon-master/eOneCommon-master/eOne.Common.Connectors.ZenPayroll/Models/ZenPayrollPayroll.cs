using System;
using System.Collections.Generic;

namespace eOne.Common.Connectors.ZenPayroll.Models
{
    public class ZenPayrollPayroll
    {

        public string version { get; set; }
        public DateTime payroll_deadline { get; set; }
        public bool processed { get; set; }
        public ZenPayrollPayPeriod pay_period { get; set; }
        public ZenPayrollPayrollTotals totals { get; set; }
        public List<ZenPayrollEmployeeCompensations> employee_compensations { get; set; }

        public DateTime start_date => pay_period.start_date;
        public DateTime end_date => pay_period.end_date;
    }
}
