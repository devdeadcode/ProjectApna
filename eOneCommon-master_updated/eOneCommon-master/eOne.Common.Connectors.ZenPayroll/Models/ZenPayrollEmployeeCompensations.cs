using System.Collections.Generic;

namespace eOne.Common.Connectors.ZenPayroll.Models
{
    public class ZenPayrollEmployeeCompensations
    {

        public int employee_id { get; set; }
        public List<ZenPayrollEmployeeCompensation> fixed_compensations { get; set; }
        public List<ZenPayrollEmployeeCompensation> hourly_compensations { get; set; }
        public List<ZenPayrollEmployeeCompensation> paid_time_off { get; set; }

        public ZenPayrollEmployee Employee { get; set; }

    }
}
