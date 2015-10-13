namespace eOne.Common.Connectors.Xero.Models
{
    public class XeroPhone
    {

        public enum XeroPhoneType
        {
            DEFAULT,
            DDI,
            MOBILE,
            FAX
        }

        public XeroPhoneType PhoneType { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneAreaCode { get; set; }
        public string PhoneCountryCode { get; set; }
        
    }
}


