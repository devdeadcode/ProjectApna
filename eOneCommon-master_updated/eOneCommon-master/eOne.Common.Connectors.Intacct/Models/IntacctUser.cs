namespace eOne.Common.Connectors.Intacct.Models
{
    public class IntacctUser : IntacctBase
    {

        #region Default properties

        [FieldSettings("User ID", DefaultField = true, KeyNumber = 1)]
        public string LOGINID { get; set; } 

        [FieldSettings("Name", DefaultField = true, Description = true, ApiName = "CONTACTINFO.CONTACTNAME")]
        public string CONTACTINFO_CONTACTNAME { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Description")]
        public string DESCRIPTION { get; set; }

        [FieldSettings("Prefix", ApiName = "CONTACTINFO.PREFIX")]
        public string CONTACTINFO_PREFIX { get; set; }

        [FieldSettings("First name", ApiName = "CONTACTINFO.FIRSTNAME")]
        public string CONTACTINFO_FIRSTNAME { get; set; }

        [FieldSettings("Initial", ApiName = "CONTACTINFO.INITIAL")]
        public string CONTACTINFO_INITIAL { get; set; }

        [FieldSettings("Last name", ApiName = "CONTACTINFO.LASTNAME")]
        public string CONTACTINFO_LASTNAME { get; set; }

        [FieldSettings("Company name", ApiName = "CONTACTINFO.COMPANYNAME")]
        public string CONTACTINFO_COMPANYNAME { get; set; }

        [FieldSettings("Print as", ApiName = "CONTACTINFO.PRINTAS")]
        public string CONTACTINFO_PRINTAS { get; set; }

        [FieldSettings("Phone 1", ApiName = "CONTACTINFO.PHONE1", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string CONTACTINFO_PHONE1 { get; set; }

        [FieldSettings("Phone 2", ApiName = "CONTACTINFO.PHONE2", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string CONTACTINFO_PHONE2 { get; set; }

        [FieldSettings("Mobile phone", ApiName = "CONTACTINFO.CELLPHONE", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string CONTACTINFO_CELLPHONE { get; set; }

        [FieldSettings("Pager", ApiName = "CONTACTINFO.PAGER")]
        public string CONTACTINFO_PAGER { get; set; }

        [FieldSettings("Fax", ApiName = "CONTACTINFO.FAX")]
        public string CONTACTINFO_FAX { get; set; }

        [FieldSettings("Email 1", ApiName = "CONTACTINFO.EMAIL1", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string CONTACTINFO_EMAIL1 { get; set; }

        [FieldSettings("Email 2", ApiName = "CONTACTINFO.EMAIL2", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string CONTACTINFO_EMAIL2 { get; set; }

        [FieldSettings("URL 1", ApiName = "CONTACTINFO.URL1", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string CONTACTINFO_URL1 { get; set; }

        [FieldSettings("URL 2", ApiName = "CONTACTINFO.URL2", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string CONTACTINFO_URL2 { get; set; }

        [FieldSettings("Address 1", ApiName = "CONTACTINFO.MAILADDRESS.ADDRESS1")]
        public string CONTACTINFO_MAILADDRESS_ADDRESS1 { get; set; }

        [FieldSettings("Address 2", ApiName = "CONTACTINFO.MAILADDRESS.ADDRESS2")]
        public string CONTACTINFO_MAILADDRESS_ADDRESS2 { get; set; }

        [FieldSettings("City", ApiName = "CONTACTINFO.MAILADDRESS.CITY")]
        public string CONTACTINFO_MAILADDRESS_CITY { get; set; }

        [FieldSettings("State", ApiName = "CONTACTINFO.MAILADDRESS.STATE")]
        public string CONTACTINFO_MAILADDRESS_STATE { get; set; }

        [FieldSettings("Postal code", ApiName = "CONTACTINFO.MAILADDRESS.ZIP")]
        public string CONTACTINFO_MAILADDRESS_ZIP { get; set; }

        [FieldSettings("Country", ApiName = "CONTACTINFO.MAILADDRESS.COUNTRY")]
        public string CONTACTINFO_MAILADDRESS_COUNTRY { get; set; }

        [FieldSettings("Country code", ApiName = "CONTACTINFO.MAILADDRESS.COUNTRYCODE")]
        public string CONTACTINFO_MAILADDRESS_COUNTRYCODE { get; set; }

        [FieldSettings("Admin privileges")]
        public string ADMIN { get; set; }

        #endregion

    }
}