namespace eOne.Common.Connectors.Intacct.Models
{
    public class IntacctContact
    {

        public string contactname { get; set; }
        public string printas { get; set; }
        public string companyname { get; set; }
        public string prefix { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string initial { get; set; }
        public string phone1 { get; set; }
        public string phone2 { get; set; }
        public string cellphone { get; set; }
        public string pager { get; set; }
        public string fax { get; set; }
        public string email1 { get; set; }
        public string email2 { get; set; }
        public string url1 { get; set; }
        public string url2 { get; set; }
        public IntacctAddress mailaddress { get; set; }

        public string mail_address => mailaddress.address;
        public string mail_address1 => mailaddress.address1;
        public string mail_address2 => mailaddress.address2;

        public string mail_city => mailaddress.city;

        public string mail_state => mailaddress.state;
        public string mail_zip => mailaddress.zip;
        public string mail_country => mailaddress.country;
    }
}