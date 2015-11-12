namespace eOne.Common.Connectors.ZenPayroll.Models
{
    public class ZenPayrollPayrollTotals
    {

        public decimal company_debit { get; set; }
        public decimal reimbursements { get; set; }
        public decimal net_pay { get; set; }
        public decimal correction { get; set; }
        public decimal employer_taxes { get; set; }
        public decimal employee_taxes { get; set; }
        public decimal benefits { get; set; }

    }
}