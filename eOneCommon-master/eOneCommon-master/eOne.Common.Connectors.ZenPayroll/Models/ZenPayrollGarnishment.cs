namespace eOne.Common.Connectors.ZenPayroll.Models
{
    public class ZenPayrollGarnishment
    {

        public int id { get; set; }
        public string version { get; set; }
        public int employee_id { get; set; }
        public bool active { get; set; }
        public decimal amount { get; set; }
        public string description { get; set; }
        public bool court_ordered { get; set; }
        public int times { get; set; }
        public bool recurring { get; set; }
        public decimal annual_maximum { get; set; }
        public decimal pay_period_maximum { get; set; }
        public bool deduct_as_percentage { get; set; }

    }
}