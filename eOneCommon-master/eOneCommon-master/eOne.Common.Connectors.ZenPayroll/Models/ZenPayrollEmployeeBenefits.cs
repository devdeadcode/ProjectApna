namespace eOne.Common.Connectors.ZenPayroll.Models
{
    public class ZenPayrollEmployeeBenefit
    {

        public int id { get; set; }
        public string version { get; set; }
        public int employee_id { get; set; }
        public int company_benefit_id { get; set; }
        public bool active { get; set; }
        public decimal employee_deduction { get; set; }
        public decimal? employee_deduction_annual_maximum { get; set; }
        public decimal company_contribution { get; set; }
        public decimal? company_contribution_annual_maximum { get; set; }
        public string limit_option { get; set; }
        public bool deduct_as_percentage { get; set; }
        public bool contribute_as_percentage { get; set; }
        public bool catch_up { get; set; }

    }
}
