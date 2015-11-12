using System;

namespace eOne.Common.Connectors.Intacct.Models
{
    public class IntacctSalesDocument : IntacctBase
    {

        #region Default properties

        [FieldSettings("Document type", DefaultField = true)]
        public string DOCPARID { get; set; }

        [FieldSettings("Document number", DefaultField = true)]
        public string DOCNO { get; set; }

        [FieldSettings("Customer name", DefaultField = true)]
        public string CUSTVENDNAME { get; set; }

        [FieldSettings("Reference", DefaultField = true)]
        public string PONUMBER { get; set; }

        [FieldSettings("State", DefaultField = true)]
        public string STATE { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Base currency")]
        public string BASECURR { get; set; }

        [FieldSettings("Currency")]
        public string CURRENCY { get; set; }

        [FieldSettings("Message")]
        public string MESSAGE { get; set; }

        [FieldSettings("Ship via")]
        public string SHIPVIA { get; set; }

        [FieldSettings("Project ID")]
        public string PROJECT { get; set; }

        [FieldSettings("Project name")]
        public string PROJECTNAME { get; set; }

        [FieldSettings("Customer ID")]
        public string CUSTVENDID { get; set; }

        [FieldSettings("Payment status")]
        public string PAYMENTSTATUS { get; set; }

        [FieldSettings("Contract ID")]
        public string CONTRACTID { get; set; }

        [FieldSettings("Contract description")]
        public string CONTRACTDESC { get; set; }

        [FieldSettings("Due date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? WHENDUE { get; set; }

        [FieldSettings("Exchange rate date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? EXCHRATEDATE { get; set; }

        [FieldSettings("Original document date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? ORIGDOCDATE { get; set; }

        [FieldSettings("Subtotal", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal? SUBTOTAL { get; set; }

        [FieldSettings("Total", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal? TOTAL { get; set; }

        [FieldSettings("Total paid", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal? TOTALPAID { get; set; }

        public decimal? TOTALENTERED { get; set; }
        public decimal? TOTALDUE { get; set; }
        public decimal? TRX_SUBTOTAL { get; set; }
        public decimal? TRX_TOTAL { get; set; }
        public decimal? TRX_TOTALPAID { get; set; }
        public decimal? TRX_TOTALENTERED { get; set; }
        public decimal? TRX_TOTALDUE { get; set; }

        [FieldSettings("Exchange rate", FieldTypeId = Connector.FieldTypeIdQuantity)]
        public decimal? EXCHRATE { get; set; }

        [FieldSettings("Payment terms", ApiName = "TERM.NAME")]
        public string TERM_NAME { get; set; }

        #region Contact

        [FieldSettings("Contact name", ApiName = "CONTACT.CONTACTNAME")]
        public string CONTACT_CONTACTNAME { get; set; }

        [FieldSettings("Contact first name", ApiName = "CONTACT.FIRSTNAME")]
        public string CONTACT_FIRSTNAME { get; set; }

        [FieldSettings("Contact middle name", ApiName = "CONTACT.INITIAL")]
        public string CONTACT_INITIAL { get; set; }

        [FieldSettings("Contact last name", ApiName = "CONTACT.LASTNAME")]
        public string CONTACT_LASTNAME { get; set; }

        [FieldSettings("Contact print as", ApiName = "CONTACT.PRINTAS")]
        public string CONTACT_PRINTAS { get; set; }

        [FieldSettings("Contact phone", ApiName = "CONTACT.PHONE1", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string CONTACT_PHONE1 { get; set; }

        [FieldSettings("Contact mobile phone", ApiName = "CONTACT.CELLPHONE", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string CONTACT_CELLPHONE { get; set; }

        [FieldSettings("Contact pager", ApiName = "CONTACT.PAGER")]
        public string CONTACT_PAGER { get; set; }

        [FieldSettings("Contact fax", ApiName = "CONTACT.FAX")]
        public string CONTACT_FAX { get; set; }

        [FieldSettings("Contact email", ApiName = "CONTACT.EMAIL1", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string CONTACT_EMAIL1 { get; set; }

        [FieldSettings("Contact secondary email", ApiName = "CONTACT.EMAIL2", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string CONTACT_EMAIL2 { get; set; }

        [FieldSettings("Contact URL", ApiName = "CONTACT.URL1", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string CONTACT_URL1 { get; set; }

        [FieldSettings("Contact address 1", ApiName = "CONTACT.MAILADDRESS.ADDRESS1")]
        public string CONTACT_MAILADDRESS_ADDRESS1 { get; set; }

        [FieldSettings("Contact address 2", ApiName = "CONTACT.MAILADDRESS.ADDRESS2")]
        public string CONTACT_MAILADDRESS_ADDRESS2 { get; set; }

        [FieldSettings("Contact city", ApiName = "CONTACT.MAILADDRESS.CITY")]
        public string CONTACT_MAILADDRESS_CITY { get; set; }

        [FieldSettings("Contact state", ApiName = "CONTACT.MAILADDRESS.STATE")]
        public string CONTACT_MAILADDRESS_STATE { get; set; }

        [FieldSettings("Contact postal code", ApiName = "CONTACT.MAILADDRESS.ZIP")]
        public string CONTACT_MAILADDRESS_ZIP { get; set; }

        [FieldSettings("Contact country", ApiName = "CONTACT.MAILADDRESS.COUNTRY")]
        public string CONTACT_MAILADDRESS_COUNTRY { get; set; }

        [FieldSettings("Contact country code", ApiName = "CONTACT.MAILADDRESS.COUNTRYCODE")]
        public string CONTACT_MAILADDRESS_COUNTRYCODE { get; set; }

        #endregion

        #region Ship to contact

        [FieldSettings("Ship to contact name", ApiName = "SHIPTO.CONTACTNAME")]
        public string SHIPTO_CONTACTNAME { get; set; }

        [FieldSettings("Ship to first name", ApiName = "SHIPTO.FIRSTNAME")]
        public string SHIPTO_FIRSTNAME { get; set; }

        [FieldSettings("Ship to middle name", ApiName = "SHIPTO.INITIAL")]
        public string SHIPTO_INITIAL { get; set; }

        [FieldSettings("Ship to last name", ApiName = "SHIPTO.LASTNAME")]
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

        [FieldSettings("Bill to first name", ApiName = "BILLTO.FIRSTNAME")]
        public string BILLTO_FIRSTNAME { get; set; }

        [FieldSettings("Bill to middle name", ApiName = "BILLTO.INITIAL")]
        public string BILLTO_INITIAL { get; set; }

        [FieldSettings("Bill to last name", ApiName = "BILLTO.LASTNAME")]
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

        #region Hidden properties

        public string NOTE { get; set; }
        public string PRINTED { get; set; }
        public string DOCPAR_IN_OUT { get; set; }
        public string VENDORDOCNO { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Printed", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool IsPrinted => ToBoolean(PRINTED);

        #endregion

    }
}


//	"CREATEDFROM": null,
//	"CLOSED": "",
//	"WAREHOUSE.LOCATIONID": null,
//	"BACKORDER": "No",
//	"ENTGLGROUP": null,
//	"SCHOPKEY": null,
//	"SALESCONTRACT": "N",
//	"USEDASCONTRACT": null,
//	"EXCH_RATE_TYPE_ID": null,
//	"RENEWEDDOC": null,
//	"SYSTEMGENERATED": "false",
//	"INVOICERUNKEY": null,
//	"HASPOSTEDREVREC": null,
//	"SIGN_FLAG": "1",
//	"VSOEPRICELIST": null,
//	"VSOEPRCLSTKEY": null,
//	"HASADVBILLING": "",
//	"INVOICERUN_EXPENSEPRICEMARKUP": null,
//	"INVOICERUN_DESCRIPTION": null,

