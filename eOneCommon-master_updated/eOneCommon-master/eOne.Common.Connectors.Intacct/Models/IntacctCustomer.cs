using System;

namespace eOne.Common.Connectors.Intacct.Models
{
    public class IntacctCustomer : IntacctBase
    {

        #region Default properties

        [FieldSettings("Customer ID", DefaultField = true, KeyNumber = 1)]
        public string CUSTOMERID { get; set; }

        [FieldSettings("Name", DefaultField = true, Description = true)]
        public string NAME { get; set; }

        [FieldSettings("Total due", DefaultField = true, FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal? TOTALDUE { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Primary contact name", ApiName = "DISPLAYCONTACT.CONTACTNAME")]
        public string DISPLAYCONTACT_CONTACTNAME { get; set; }

        [FieldSettings("Comments")]
        public string COMMENTS { get; set; }

        [FieldSettings("Default currency")]
        public string CURRENCY { get; set; }

        [FieldSettings("One-time", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool ONETIME { get; set; }

        [FieldSettings("On hold", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool ONHOLD { get; set; }

        [FieldSettings("Enable online card payment", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool ENABLEONLINECARDPAYMENT { get; set; }

        [FieldSettings("Enable online ACH payment", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool ENABLEONLINEACHPAYMENT { get; set; }

        [FieldSettings("Term")]
        public string TERMNAME { get; set; }

        [FieldSettings("Parent ID")]
        public string PARENTID { get; set; }

        [FieldSettings("Parent name")]
        public string PARENTNAME { get; set; }

        [FieldSettings("Tax ID")]
        public string TAXID { get; set; }

        [FieldSettings("Last invoice date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? LAST_INVOICEDATE { get; set; }

        [FieldSettings("Last statement date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? LAST_STATEMENTDATE { get; set; }

        [FieldSettings("Sales rep ID")]
        public string CUSTREPID { get; set; }

        [FieldSettings("Sales rep name")]
        public string CUSTREPNAME { get; set; }

        [FieldSettings("Statement/invoice delivery method")]
        public string DELIVERY_OPTIONS { get; set; }

        [FieldSettings("Resale number")]
        public string RESALENO { get; set; }

        [FieldSettings("GL group")]
        public string GLGROUP { get; set; }

        [FieldSettings("Account label")]
        public string ACCOUNTLABEL { get; set; }

        [FieldSettings("Territory ID")]
        public string TERRITORYID { get; set; }

        [FieldSettings("Credit limit")]
        public decimal? CREDITLIMIT { get; set; }

        [FieldSettings("Shipping method")]
        public string SHIPPINGMETHOD { get; set; }

        [FieldSettings("Price list")]
        public string PRICELIST { get; set; }

        [FieldSettings("Fair value price list")]
        public string VSOEPRICELIST { get; set; }

        [FieldSettings("Default invoice message")]
        public string CUSTMESSAGEID { get; set; }

        [FieldSettings("Customer type")]
        public string CUSTTYPE { get; set; }

        #region Primary contact

        [FieldSettings("Primary contact first name", ApiName = "DISPLAYCONTACT.FIRSTNAME")]
        public string DISPLAYCONTACT_FIRSTNAME { get; set; }

        [FieldSettings("Primary contact last name", ApiName = "DISPLAYCONTACT.LASTNAME")]
        public string DISPLAYCONTACT_LASTNAME { get; set; }

        [FieldSettings("Primary contact middle name", ApiName = "DISPLAYCONTACT.INITIAL")]
        public string DISPLAYCONTACT_INITIAL { get; set; }

        [FieldSettings("Primary contact print as", ApiName = "DISPLAYCONTACT.PRINTAS")]
        public string DISPLAYCONTACT_PRINTAS { get; set; }

        [FieldSettings("Taxable", ApiName = "DISPLAYCONTACT.TAXABLE", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool DISPLAYCONTACT_TAXABLE { get; set; }

        [FieldSettings("Tax group", ApiName = "DISPLAYCONTACT.TAXGROUP")]
        public string DISPLAYCONTACT_TAXGROUP { get; set; }

        [FieldSettings("Primary contact phone", ApiName = "DISPLAYCONTACT.PHONE1", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string DISPLAYCONTACT_PHONE1 { get; set; }

        [FieldSettings("Primary contact mobile phone", ApiName = "DISPLAYCONTACT.CELLPHONE", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string DISPLAYCONTACT_CELLPHONE { get; set; }

        [FieldSettings("Primary contact pager", ApiName = "DISPLAYCONTACT.PAGER")]
        public string DISPLAYCONTACT_PAGER { get; set; }

        [FieldSettings("Primary contact fax", ApiName = "DISPLAYCONTACT.FAX")]
        public string DISPLAYCONTACT_FAX { get; set; }

        [FieldSettings("Primary contact email", ApiName = "DISPLAYCONTACT.EMAIL1", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string DISPLAYCONTACT_EMAIL1 { get; set; }

        [FieldSettings("Primary contact secondary email", ApiName = "DISPLAYCONTACT.EMAIL2", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string DISPLAYCONTACT_EMAIL2 { get; set; }

        [FieldSettings("Primary contact URL", ApiName = "DISPLAYCONTACT.URL1", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string DISPLAYCONTACT_URL1 { get; set; }

        [FieldSettings("Primary contact address 1", ApiName = "DISPLAYCONTACT.MAILADDRESS.ADDRESS1")]
        public string DISPLAYCONTACT_MAILADDRESS_ADDRESS1 { get; set; }

        [FieldSettings("Primary contact address 2", ApiName = "DISPLAYCONTACT.MAILADDRESS.ADDRESS2")]
        public string DISPLAYCONTACT_MAILADDRESS_ADDRESS2 { get; set; }

        [FieldSettings("Primary contact city", ApiName = "DISPLAYCONTACT.MAILADDRESS.CITY")]
        public string DISPLAYCONTACT_MAILADDRESS_CITY { get; set; }

        [FieldSettings("Primary contact state", ApiName = "DISPLAYCONTACT.MAILADDRESS.STATE")]
        public string DISPLAYCONTACT_MAILADDRESS_STATE { get; set; }

        [FieldSettings("Primary contact postal code", ApiName = "DISPLAYCONTACT.MAILADDRESS.ZIP")]
        public string DISPLAYCONTACT_MAILADDRESS_ZIP { get; set; }

        [FieldSettings("Primary contact country", ApiName = "DISPLAYCONTACT.MAILADDRESS.COUNTRY")]
        public string DISPLAYCONTACT_MAILADDRESS_COUNTRY { get; set; }

        [FieldSettings("Primary contact country code", ApiName = "DISPLAYCONTACT.MAILADDRESS.COUNTRYCODE")]
        public string DISPLAYCONTACT_MAILADDRESS_COUNTRYCODE { get; set; }

        [FieldSettings("Primary contact status", ApiName = "DISPLAYCONTACT.STATUS")]
        public IntacctStatus DISPLAYCONTACT_STATUS { get; set; }

        #endregion

        #region Ship to contact

        [FieldSettings("Ship to contact name", ApiName = "SHIPTO.CONTACTNAME")]
        public string SHIPTO_CONTACTNAME { get; set; }

        [FieldSettings("Ship to contact first name", ApiName = "SHIPTO.FIRSTNAME")]
        public string SHIPTO_FIRSTNAME { get; set; }

        [FieldSettings("Ship to contact middle name", ApiName = "SHIPTO.INITIAL")]
        public string SHIPTO_INITIAL { get; set; }

        [FieldSettings("Ship to contact last name", ApiName = "SHIPTO.LASTNAME")]
        public string SHIPTO_LASTNAME { get; set; }

        [FieldSettings("Ship to contact print as", ApiName = "SHIPTO.PRINTAS")]
        public string SHIPTO_PRINTAS { get; set; }

        [FieldSettings("Ship to contact phone", ApiName = "SHIPTO.PHONE1", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string SHIPTO_PHONE1 { get; set; }

        [FieldSettings("Ship to contact mobile phone", ApiName = "SHIPTO.CELLPHONE", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string SHIPTO_CELLPHONE { get; set; }

        [FieldSettings("Ship to contact pager", ApiName = "SHIPTO.PAGER")]
        public string SHIPTO_PAGER { get; set; }

        [FieldSettings("Ship to contact fax", ApiName = "SHIPTO.FAX")]
        public string SHIPTO_FAX { get; set; }

        [FieldSettings("Ship to contact email", ApiName = "SHIPTO.EMAIL1", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string SHIPTO_EMAIL1 { get; set; }

        [FieldSettings("Ship to contact secondary email", ApiName = "SHIPTO.EMAIL2", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string SHIPTO_EMAIL2 { get; set; }

        [FieldSettings("Ship to contact URL", ApiName = "SHIPTO.URL1", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string SHIPTO_URL1 { get; set; }

        [FieldSettings("Ship to contact address 1", ApiName = "SHIPTO.MAILADDRESS.ADDRESS1")]
        public string SHIPTO_MAILADDRESS_ADDRESS1 { get; set; }

        [FieldSettings("Ship to contact address 2", ApiName = "SHIPTO.MAILADDRESS.ADDRESS2")]
        public string SHIPTO_MAILADDRESS_ADDRESS2 { get; set; }

        [FieldSettings("Ship to contact city", ApiName = "SHIPTO.MAILADDRESS.CITY")]
        public string SHIPTO_MAILADDRESS_CITY { get; set; }

        [FieldSettings("Ship to contact state", ApiName = "SHIPTO.MAILADDRESS.STATE")]
        public string SHIPTO_MAILADDRESS_STATE { get; set; }

        [FieldSettings("Ship to contact postal code", ApiName = "SHIPTO.MAILADDRESS.ZIP")]
        public string SHIPTO_MAILADDRESS_ZIP { get; set; }

        [FieldSettings("Ship to contact country", ApiName = "SHIPTO.MAILADDRESS.COUNTRY")]
        public string SHIPTO_MAILADDRESS_COUNTRY { get; set; }

        [FieldSettings("Ship to contact country code", ApiName = "SHIPTO.MAILADDRESS.COUNTRYCODE")]
        public string SHIPTO_MAILADDRESS_COUNTRYCODE { get; set; }

        #endregion

        #region Bill to contact

        [FieldSettings("Bill to contact name", ApiName = "BILLTO.CONTACTNAME")]
        public string BILLTO_CONTACTNAME { get; set; }

        [FieldSettings("Bill to contact first name", ApiName = "BILLTO.FIRSTNAME")]
        public string BILLTO_FIRSTNAME { get; set; }

        [FieldSettings("Bill to contact middle name", ApiName = "BILLTO.INITIAL")]
        public string BILLTO_INITIAL { get; set; }

        [FieldSettings("Bill to contact last name", ApiName = "BILLTO.LASTNAME")]
        public string BILLTO_LASTNAME { get; set; }

        [FieldSettings("Bill to contact print as", ApiName = "BILLTO.PRINTAS")]
        public string BILLTO_PRINTAS { get; set; }

        [FieldSettings("Bill to contact phone", ApiName = "BILLTO.PHONE1", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string BILLTO_PHONE1 { get; set; }

        [FieldSettings("Bill to contact mobile phone", ApiName = "BILLTO.CELLPHONE", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string BILLTO_CELLPHONE { get; set; }

        [FieldSettings("Bill to contact pager", ApiName = "BILLTO.PAGER")]
        public string BILLTO_PAGER { get; set; }

        [FieldSettings("Bill to contact fax", ApiName = "BILLTO.FAX")]
        public string BILLTO_FAX { get; set; }

        [FieldSettings("Bill to contact email", ApiName = "BILLTO.EMAIL1", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string BILLTO_EMAIL1 { get; set; }

        [FieldSettings("Bill to contact secondary email", ApiName = "BILLTO.EMAIL2", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string BILLTO_EMAIL2 { get; set; }

        [FieldSettings("Bill to contact URL", ApiName = "BILLTO.URL1", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string BILLTO_URL1 { get; set; }

        [FieldSettings("Bill to contact address 1", ApiName = "BILLTO.MAILADDRESS.ADDRESS1")]
        public string BILLTO_MAILADDRESS_ADDRESS1 { get; set; }

        [FieldSettings("Bill to contact address 2", ApiName = "BILLTO.MAILADDRESS.ADDRESS2")]
        public string BILLTO_MAILADDRESS_ADDRESS2 { get; set; }

        [FieldSettings("Bill to contact city", ApiName = "BILLTO.MAILADDRESS.CITY")]
        public string BILLTO_MAILADDRESS_CITY { get; set; }

        [FieldSettings("Bill to contact state", ApiName = "BILLTO.MAILADDRESS.STATE")]
        public string BILLTO_MAILADDRESS_STATE { get; set; }

        [FieldSettings("Bill to contact postal code", ApiName = "BILLTO.MAILADDRESS.ZIP")]
        public string BILLTO_MAILADDRESS_ZIP { get; set; }

        [FieldSettings("Bill to contact country", ApiName = "BILLTO.MAILADDRESS.COUNTRY")]
        public string BILLTO_MAILADDRESS_COUNTRY { get; set; }

        [FieldSettings("Bill to contact country code", ApiName = "BILLTO.MAILADDRESS.COUNTRYCODE")]
        public string BILLTO_MAILADDRESS_COUNTRYCODE { get; set; }

        #endregion

        #endregion

    }
}