namespace eOne.Common.Connectors.Intacct.Models
{
    public class IntacctContact : IntacctBase
    {

        #region Default properties

        [FieldSettings("Name", DefaultField = true, Description = true)]
        public string CONTACTNAME { get; set; }

        [FieldSettings("Company name", DefaultField = true)]
        public string COMPANYNAME { get; set; }

        [FieldSettings("Phone", FieldTypeId = Connector.FieldTypeIdPhone, DefaultField = true)]
        public string PHONE1 { get; set; }

        [FieldSettings("Email", FieldTypeId = Connector.FieldTypeIdEmail, DefaultField = true)]
        public string EMAIL1 { get; set; }

        #endregion

        #region Properties

        [FieldSettings("First name")]
        public string FIRSTNAME { get; set; }

        [FieldSettings("Last name")]
        public string LASTNAME { get; set; }

        [FieldSettings("Mobile phone", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string CELLPHONE { get; set; }

        [FieldSettings("Pager")]
        public string PAGER { get; set; }

        [FieldSettings("Fax")]
        public string FAX { get; set; }

        [FieldSettings("Secondary email", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string EMAIL2 { get; set; }

        [FieldSettings("URL", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string URL1 { get; set; }

        [FieldSettings("Visible", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool VISIBLE { get; set; }

        [FieldSettings("Address 1", ApiName = "MAILADDRESS.ADDRESS1")]
        public string MAILADDRESS_ADDRESS1 { get; set; }

        [FieldSettings("Address 2", ApiName = "MAILADDRESS.ADDRESS2")]
        public string MAILADDRESS_ADDRESS2 { get; set; }

        [FieldSettings("City", ApiName = "MAILADDRESS.CITY")]
        public string MAILADDRESS_CITY { get; set; }

        [FieldSettings("State", ApiName = "MAILADDRESS.STATE")]
        public string MAILADDRESS_STATE { get; set; }

        [FieldSettings("Postal code", ApiName = "MAILADDRESS.ZIP")]
        public string MAILADDRESS_ZIP { get; set; }

        [FieldSettings("Country", ApiName = "MAILADDRESS.COUNTRY")]
        public string MAILADDRESS_COUNTRY { get; set; }

        [FieldSettings("Country code", ApiName = "MAILADDRESS.COUNTRYCODE")]
        public string MAILADDRESS_COUNTRYCODE { get; set; }

        [FieldSettings("Print as")]
        public string PRINTAS { get; set; }

        [FieldSettings("Middle name")]
        public string INITIAL { get; set; }

        #endregion

    }
}
