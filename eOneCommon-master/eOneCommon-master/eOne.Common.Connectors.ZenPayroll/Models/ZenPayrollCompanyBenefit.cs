namespace eOne.Common.Connectors.ZenPayroll.Models
{
    public class ZenPayrollCompanyBenefit
    {

        public int id { get; set; }
        public string version { get; set; }
        public int benefit_id { get; set; }
        public int company_id { get; set; }
        public bool active { get; set; }
        public string description { get; set; }
        public bool supports_percentage_amounts { get; set; }

    }
}