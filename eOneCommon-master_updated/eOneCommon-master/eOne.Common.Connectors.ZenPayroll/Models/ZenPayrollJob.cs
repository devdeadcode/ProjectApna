using System;
using System.Collections.Generic;

namespace eOne.Common.Connectors.ZenPayroll.Models
{
    public class ZenPayrollJob
    {

        public enum ZenPayrollJobPaymentUnit
        {
            Hour,
            Week,
            Month,
            Year
        }

        public int id { get; set; }
        public string version { get; set; }
        public int employee_id { get; set; }
        public int location_id { get; set; }
        public DateTime hire_date { get; set; }
        public string title { get; set; }
        public decimal rate { get; set; }
        public int current_compensation_id { get; set; }
        public ZenPayrollLocation location { get; set; }
        public List<ZenPayrollCompensation> compensations { get; set; }
        public ZenPayrollJobPaymentUnit payment_unit { get; set; }

    }
}