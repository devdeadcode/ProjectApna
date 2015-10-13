using System;

namespace eOne.Common.Connectors.ZenPayroll.Models
{
    public class ZenPayrollTermination
    {

        public int id { get; set; }
        public string version { get; set; }
        public int employee_id { get; set; }
        public bool active { get; set; }
        public DateTime effective_date { get; set; }
        public bool run_termination_payroll { get; set; }

    }
}