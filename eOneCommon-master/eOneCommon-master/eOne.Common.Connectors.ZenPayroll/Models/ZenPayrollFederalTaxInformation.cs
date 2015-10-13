namespace eOne.Common.Connectors.ZenPayroll.Models
{
    public class ZenPayrollFederalTaxInformation
    {

        public enum ZenPayrollFederalTaxInformationFilingStatus
        {
            Single
        }

        public string version { get; set; }
        public int employee_id { get; set; }
        public int withholding_allowance { get; set; }
        public decimal additional_withholding { get; set; }
        public bool decline_withholding { get; set; }
        public ZenPayrollFederalTaxInformationFilingStatus filing_status { get; set; }

    }
}
