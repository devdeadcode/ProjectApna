namespace eOne.Common.Connectors.Intacct.Models
{
    class IntacctBudget : IntacctBase
    {

        public string BUDGETID { get; set; }
        public string DESCRIPTION { get; set; }
        public bool SYSTEMGENERATED { get; set; }
        public bool DEFAULT_BUDGET { get; set; }
        public string CURRENCY { get; set; }

    }
}