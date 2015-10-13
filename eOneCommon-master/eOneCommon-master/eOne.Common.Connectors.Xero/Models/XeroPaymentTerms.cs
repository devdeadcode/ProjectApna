namespace eOne.Common.Connectors.Xero.Models
{
    public class XeroPaymentTerms
    {

        public enum XeroPaymentTermsType
        {
            DAYSAFTERBILLDATE,
            DAYSAFTERBILLMONTH,
            OFCURRENTMONTH,
            OFFOLLOWINGMONTH
        }

        public int Day { get; set; }
        public XeroPaymentTermsType Type { get; set; }

    }
}
