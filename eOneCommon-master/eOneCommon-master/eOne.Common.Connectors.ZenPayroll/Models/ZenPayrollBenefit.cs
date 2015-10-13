namespace eOne.Common.Connectors.ZenPayroll.Models
{
    public class ZenPayrollBenefit
    {

        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool pretax { get; set; }
        public bool posttax { get; set; }
        public bool imputed { get; set; }
        public bool healthcare { get; set; }
        public bool retirement { get; set; }
        public bool yearly_limit { get; set; }

    }
}