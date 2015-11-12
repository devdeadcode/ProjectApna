namespace eOne.Common.Connectors.ZenPayroll.Models
{
    public class ZenPayrollEmployeeCompensation
    {

        public string name { get; set; }
        public decimal hours { get; set; }
        public int job_id { get; set; }
        public decimal compensation_multiplier { get; set; }

    }
}