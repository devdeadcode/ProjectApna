using System.ComponentModel;

namespace eOne.Common.Connectors.Intacct.Models
{
    public class IntacctVendor : IntacctBase
    {

        #region Enums

        public enum IntacctVendorPaymentMethod
        {
            check
        }
        public enum IntacctVendorBillingType
        {
            [Description("Balance brought forward")]
            balanceforward,
            [Description("Open item")]
            openitem
        }
        public enum IntacctVendorForm1099Type
        {
            [Description("Dividend Income (Form 1099-DIV)")]	
            DIV,
            [Description("Interest Income (Form 1099-INT)")]
            INT,
            [Description("Miscellaneous Income (Form 1099-MISC)")]
            MISC,
            [Description("Distributions From Pensions, Annuities...(Form 1099-R)")]
            R,
            [Description("Proceeds From Real Estate Transactions (Form 1099-S)")]
            S,
            [Description("Taxable Distributions Received (Form 1099-PATR)")]
            PATR,
            [Description("Certain Government Payments (Form 1099-G)")]
            G,
            [Description("Certain Gambling Winnings (Form W-2G)")]
            W_2G
        }

        #endregion

        #region Default properties

        [FieldSettings("Vendor ID", DefaultField = true, KeyNumber = 1)]
        public string VENDORID { get; set; }

        [FieldSettings("Name", DefaultField = true, Description = true)]
        public string NAME { get; set; }

        [FieldSettings("Total due", DefaultField = true, FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal? TOTALDUE { get; set; }

        #endregion

        #region Properties

        [FieldSettings("1099 name")]
        public string NAME1099 { get; set; }

        [FieldSettings("Parent ID")]
        public string PARENTID { get; set; }

        [FieldSettings("Parent name")]
        public string PARENTNAME { get; set; }

        [FieldSettings("Tax ID")]
        public string TAXID { get; set; }

        [FieldSettings("Term")]
        public string TERMNAME { get; set; }

        [FieldSettings("Billing type", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(IntacctVendorBillingType))]
        public IntacctVendorBillingType BILLINGTYPE { get; set; }

        [FieldSettings("One-time", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool ONETIME { get; set; }

        [FieldSettings("On hold", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool ONHOLD { get; set; }

        [FieldSettings("Do not cut check", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool DONOTCUTCHECK { get; set; }

        [FieldSettings("Display term discount on check stub", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool DISPLAYTERMDISCOUNT { get; set; }

        [FieldSettings("Merge payment requests", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool MERGEPAYMENTREQ { get; set; }

        [FieldSettings("Taxable", ApiName = "DISPLAYCONTACT.TAXABLE", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool DISPLAYCONTACT_TAXABLE { get; set; }

        [FieldSettings("Primary contact name", ApiName = "DISPLAYCONTACT.CONTACTNAME")]
        public string DISPLAYCONTACT_CONTACTNAME { get; set; }

        [FieldSettings("Primary contact first name", ApiName = "DISPLAYCONTACT.FIRSTNAME")]
        public string DISPLAYCONTACT_FIRSTNAME { get; set; }

        [FieldSettings("Primary contact last name", ApiName = "DISPLAYCONTACT.LASTNAME")]
        public string DISPLAYCONTACT_LASTNAME { get; set; }

        [FieldSettings("Primary contact print as", ApiName = "DISPLAYCONTACT.PRINTAS")]
        public string DISPLAYCONTACT_PRINTAS { get; set; }

        [FieldSettings("Primary contact primary phone", ApiName = "DISPLAYCONTACT.PHONE1", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string DISPLAYCONTACT_PHONE1 { get; set; }

        [FieldSettings("Primary contact mobile", ApiName = "DISPLAYCONTACT.CELLPHONE", FieldTypeId = Connector.FieldTypeIdPhone)]
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

        [FieldSettings("Primary contact state/province", ApiName = "DISPLAYCONTACT.MAILADDRESS.STATE")]
        public string DISPLAYCONTACT_MAILADDRESS_STATE { get; set; }

        [FieldSettings("Primary contact postal code", ApiName = "DISPLAYCONTACT.MAILADDRESS.ZIP")]
        public string DISPLAYCONTACT_MAILADDRESS_ZIP { get; set; }

        [FieldSettings("Primary contact country", ApiName = "DISPLAYCONTACT.MAILADDRESS.COUNTRY")]
        public string DISPLAYCONTACT_MAILADDRESS_COUNTRY { get; set; }

        [FieldSettings("Primary contact country code", ApiName = "DISPLAYCONTACT.MAILADDRESS.COUNTRYCODE")]
        public string DISPLAYCONTACT_MAILADDRESS_COUNTRYCODE { get; set; }

        [FieldSettings("Pay to contact name", ApiName = "PAYTO.CONTACTNAME")]
        public string PAYTO_CONTACTNAME { get; set; }

        [FieldSettings("Pay to contact first name", ApiName = "PAYTO.FIRSTNAME")]
        public string PAYTO_FIRSTNAME { get; set; }

        [FieldSettings("Pay to contact last name", ApiName = "PAYTO.LASTNAME")]
        public string PAYTO_LASTNAME { get; set; }

        [FieldSettings("Pay to contact print as", ApiName = "PAYTO.PRINTAS")]
        public string PAYTO_PRINTAS { get; set; }

        [FieldSettings("Pay to contact primary phone", ApiName = "PAYTO.PHONE1", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string PAYTO_PHONE1 { get; set; }

        [FieldSettings("Pay to contact mobile", ApiName = "PAYTO.CELLPHONE", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string PAYTO_CELLPHONE { get; set; }

        [FieldSettings("Pay to contact pager", ApiName = "PAYTO.PAGER")]
        public string PAYTO_PAGER { get; set; }

        [FieldSettings("Pay to contact fax", ApiName = "PAYTO.FAX")]
        public string PAYTO_FAX { get; set; }

        [FieldSettings("Pay to contact email", ApiName = "PAYTO.EMAIL1", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string PAYTO_EMAIL1 { get; set; }

        [FieldSettings("Pay to contact secondary email", ApiName = "PAYTO.EMAIL2", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string PAYTO_EMAIL2 { get; set; }

        [FieldSettings("Pay to contact URL", ApiName = "PAYTO.URL1", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string PAYTO_URL1 { get; set; }

        [FieldSettings("Pay to contact address 1", ApiName = "PAYTO.MAILADDRESS.ADDRESS1")]
        public string PAYTO_MAILADDRESS_ADDRESS1 { get; set; }

        [FieldSettings("Pay to contact address 2", ApiName = "PAYTO.MAILADDRESS.ADDRESS2")]
        public string PAYTO_MAILADDRESS_ADDRESS2 { get; set; }

        [FieldSettings("Pay to contact city", ApiName = "PAYTO.MAILADDRESS.CITY")]
        public string PAYTO_MAILADDRESS_CITY { get; set; }

        [FieldSettings("Pay to contact state/province", ApiName = "PAYTO.MAILADDRESS.STATE")]
        public string PAYTO_MAILADDRESS_STATE { get; set; }

        [FieldSettings("Pay to contact postal code", ApiName = "PAYTO.MAILADDRESS.ZIP")]
        public string PAYTO_MAILADDRESS_ZIP { get; set; }

        [FieldSettings("Pay to contact country", ApiName = "PAYTO.MAILADDRESS.COUNTRY")]
        public string PAYTO_MAILADDRESS_COUNTRY { get; set; }

        [FieldSettings("Pay to contact country code", ApiName = "PAYTO.MAILADDRESS.COUNTRYCODE")]
        public string PAYTO_MAILADDRESS_COUNTRYCODE { get; set; }

        [FieldSettings("Return to contact name", ApiName = "RETURNTO.CONTACTNAME")]
        public string RETURNTO_CONTACTNAME { get; set; }

        [FieldSettings("Return to contact first name", ApiName = "RETURNTO.FIRSTNAME")]
        public string RETURNTO_FIRSTNAME { get; set; }

        [FieldSettings("Return to contact last name", ApiName = "RETURNTO.LASTNAME")]
        public string RETURNTO_LASTNAME { get; set; }

        [FieldSettings("Return to contact print as", ApiName = "RETURNTO.PRINTAS")]
        public string RETURNTO_PRINTAS { get; set; }

        [FieldSettings("Return to contact primary phone", ApiName = "RETURNTO.PHONE1", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string RETURNTO_PHONE1 { get; set; }

        [FieldSettings("Return to contact mobile", ApiName = "RETURNTO.CELLPHONE", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string RETURNTO_CELLPHONE { get; set; }

        [FieldSettings("Return to contact pager", ApiName = "RETURNTO.PAGER")]
        public string RETURNTO_PAGER { get; set; }

        [FieldSettings("Return to contact fax", ApiName = "RETURNTO.FAX")]
        public string RETURNTO_FAX { get; set; }

        [FieldSettings("Return to contact email", ApiName = "RETURNTO.EMAIL1", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string RETURNTO_EMAIL1 { get; set; }

        [FieldSettings("Return to contact secondary email", ApiName = "RETURNTO.EMAIL2", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string RETURNTO_EMAIL2 { get; set; }

        [FieldSettings("Return to contact URL", ApiName = "RETURNTO.URL1", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string RETURNTO_URL1 { get; set; }

        [FieldSettings("Return to contact address 1", ApiName = "RETURNTO.MAILADDRESS.ADDRESS1")]
        public string RETURNTO_MAILADDRESS_ADDRESS1 { get; set; }

        [FieldSettings("Return to contact address 2", ApiName = "RETURNTO.MAILADDRESS.ADDRESS2")]
        public string RETURNTO_MAILADDRESS_ADDRESS2 { get; set; }

        [FieldSettings("Return to contact city", ApiName = "RETURNTO.MAILADDRESS.CITY")]
        public string RETURNTO_MAILADDRESS_CITY { get; set; }

        [FieldSettings("Return to contact state/province", ApiName = "RETURNTO.MAILADDRESS.STATE")]
        public string RETURNTO_MAILADDRESS_STATE { get; set; }

        [FieldSettings("Return to contact postal code", ApiName = "RETURNTO.MAILADDRESS.ZIP")]
        public string RETURNTO_MAILADDRESS_ZIP { get; set; }

        [FieldSettings("Return to contact country", ApiName = "RETURNTO.MAILADDRESS.COUNTRY")]
        public string RETURNTO_MAILADDRESS_COUNTRY { get; set; }

        [FieldSettings("Return to contact country code", ApiName = "RETURNTO.MAILADDRESS.COUNTRYCODE")]
        public string RETURNTO_MAILADDRESS_COUNTRYCODE { get; set; }

        [FieldSettings("Vendor account number")]
        public string VENDORACCOUNTNO { get; set; }

        [FieldSettings("Default currency")]
        public string CURRENCY { get; set; }

        [FieldSettings("Comments")]
        public string COMMENTS { get; set; }

        [FieldSettings("Payment priority")]
        public string PAYMENTPRIORITY { get; set; }

        [FieldSettings("Credit limit", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal? CREDITLIMIT { get; set; }

        [FieldSettings("Type")]
        public string VENDTYPE { get; set; }

        [FieldSettings("GL group")]
        public string GLGROUP { get; set; }

        #endregion

    }
}

//	"VENDTYPE1099TYPE": null,
//	"PRICESCHEDULE": null,
//	"DISCOUNT": null,
//	"PRICELIST": null,
//	"ACCOUNTLABEL": null,
//	"FORM1099TYPE": null,
//	"FORM1099BOX": null,
//	"PAYDATEVALUE": null,
//	"OWNER.EQGLACCOUNT": null,
//	"OWNER.EQGLACCOUNTLABEL": null,
//	"OWNER.HOLDDIST": "",
//	"OWNER.ACCOUNTLABEL.LABEL": null,
//	"ACHENABLED": "",
//	"WIREENABLED": "",
//	"CHECKENABLED": "",
//	"ACHBANKROUTINGNUMBER": null,
//	"ACHACCOUNTNUMBER": null,
//	"ACHACCOUNTTYPE": "",
//	"ACHREMITTANCETYPE": "",
//	"WIREBANKNAME": null,
//	"WIREBANKROUTINGNUMBER": null,
//	"WIREACCOUNTNUMBER": null,
//	"WIREACCOUNTTYPE": "",
//	"PMPLUSREMITTANCETYPE": "",
//	"PMPLUSEMAIL": null,
//	"PMPLUSFAX": null,
//	"OEPRCLSTKEY": null,
//	"DISPLOCACCTNOCHECK": "",
//	"PAYMENTNOTIFY": "",
//	"PAYMETHODREC": null,
//	"OUTSOURCECHECK": "",
//	"OUTSOURCECHECKSTATE": "",
//	"OUTSOURCEACH": "",
//	"OUTSOURCEACHSTATE": "",
//	"VENDORACHACCOUNTID": null,
//	"VENDORACCOUNTOUTSOURCEACH": "",
//	"RSEMINAR": null,
//	"R351854": null