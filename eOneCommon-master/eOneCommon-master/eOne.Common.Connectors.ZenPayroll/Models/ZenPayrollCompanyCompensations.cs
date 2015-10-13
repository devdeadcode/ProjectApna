using System.Collections.Generic;

namespace eOne.Common.Connectors.ZenPayroll.Models
{
    public class ZenPayrollCompanyCompensations
    {

        public List<ZenPayrollCompanyCompensation> hourly { get; set; }
        public List<ZenPayrollCompanyCompensation> @fixed { get; set; }
        public List<ZenPayrollCompanyCompensation> paid_time_off { get; set; }

    }
}
