namespace eOne.Common.Connectors.ZenPayroll.Models
{
    public class ZenPayrollPaidTimeOff
    {

        public enum ZenPayrollPaidTimeOffAccrualUnit
        {
            Hour
        }
        public enum ZenPayrollPaidTimeOffAccrualPeriod
        {
            Year
        }

        public int id { get; set; }
        public string version { get; set; }
        public string name { get; set; }
        public bool paid_at_termination { get; set; }
        public decimal accrual_rate { get; set; }
        public decimal accrual_balance { get; set; }
        public decimal? maximum_accrual_balance { get; set; }
        public ZenPayrollPaidTimeOffAccrualUnit accrual_unit { get; set; }
        public ZenPayrollPaidTimeOffAccrualPeriod accrual_period { get; set; }

    }
}
