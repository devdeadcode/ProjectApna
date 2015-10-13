using System;

namespace eOne.Common.Connectors.ZenPayroll.Models
{
    public class ZenPayrollCompensation
    {

        public enum ZenPayrollCompensationPaymentUnit
        {
            Hour, 
            Week, 
            Month,
            Year
        }
        public enum ZenPayrollCompensationFlsaStatus
        {
            Exempt,
            Nonexempt
        }

        public int id { get; set; }
        public string version { get; set; }
        public int job_id { get; set; }
        public decimal rate { get; set; }
        public ZenPayrollCompensationPaymentUnit payment_unit { get; set; }
        public DateTime effective_date { get; set; }

    }
}