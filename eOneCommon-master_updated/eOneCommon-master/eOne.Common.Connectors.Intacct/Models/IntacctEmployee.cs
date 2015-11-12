using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace eOne.Common.Connectors.Intacct.Models
{
    public class IntacctEmployee : IntacctBase
    {

        #region Enums

        public enum IntacctEmployeeGender
        {
            [Description("Male")]
            male,
            [Description("Female")]
            female
        }

        #endregion

        #region Default properties

        [FieldSettings("Employee ID", DefaultField = true, KeyNumber = 1)]
        public string EMPLOYEEID { get; set; }

        [FieldSettings("Name", DefaultField = true, ApiName = "PERSONALINFO.CONTACTNAME", Description = true)]
        public string PERSONALINFO_CONTACTNAME { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Social security number")]
        public string SSN { get; set; }

        [FieldSettings("Title")]
        public string TITLE { get; set; }

        [FieldSettings("Location ID")]
        public string LOCATIONID { get; set; }

        [FieldSettings("Gender", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(IntacctEmployeeGender))]
        public IntacctEmployeeGender GENDER { get; set; }

        [FieldSettings("Location name")]
        public string LOCATIONNAME { get; set; }

        [FieldSettings("Department ID")]
        public string DEPARTMENTID { get; set; }

        [FieldSettings("Department name")]
        public string DEPARTMENTTITLE { get; set; }

        [FieldSettings("Supervisor ID")]
        public string SUPERVISORID { get; set; }

        [FieldSettings("Supervisor name")]
        public string SUPERVISORNAME { get; set; }

        [FieldSettings("Employee type")]
        public string EMPLOYEETYPE { get; set; }

        [FieldSettings("Birth date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? BIRTHDATE { get; set; }

        [FieldSettings("Start date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? STARTDATE { get; set; }

        [FieldSettings("End date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? ENDDATE { get; set; }

        [FieldSettings("Prefix", ApiName = "PERSONALINFO.PREFIX")]
        public string PERSONALINFO_PREFIX { get; set; }

        [FieldSettings("First name", ApiName = "PERSONALINFO.FIRSTNAME")]
        public string PERSONALINFO_FIRSTNAME { get; set; }

        [FieldSettings("Initial", ApiName = "PERSONALINFO.INITIAL")]
        public string PERSONALINFO_INITIAL { get; set; }

        [FieldSettings("Last name", ApiName = "PERSONALINFO.LASTNAME")]
        public string PERSONALINFO_LASTNAME { get; set; }

        [FieldSettings("Company name", ApiName = "PERSONALINFO.COMPANYNAME")]
        public string PERSONALINFO_COMPANYNAME { get; set; }

        [FieldSettings("Print as", ApiName = "PERSONALINFO.PRINTAS")]
        public string PERSONALINFO_PRINTAS { get; set; }

        [FieldSettings("Phone 1", ApiName = "PERSONALINFO.PHONE1", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string PERSONALINFO_PHONE1 { get; set; }

        [FieldSettings("Phone 2", ApiName = "PERSONALINFO.PHONE2", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string PERSONALINFO_PHONE2 { get; set; }

        [FieldSettings("Mobile phone", ApiName = "PERSONALINFO.CELLPHONE", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string PERSONALINFO_CELLPHONE { get; set; }

        [FieldSettings("Pager", ApiName = "PERSONALINFO.PAGER")]
        public string PERSONALINFO_PAGER { get; set; }

        [FieldSettings("Fax", ApiName = "PERSONALINFO.FAX")]
        public string PERSONALINFO_FAX { get; set; }

        [FieldSettings("Email 1", ApiName = "PERSONALINFO.EMAIL1", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string PERSONALINFO_EMAIL1 { get; set; }

        [FieldSettings("Email 2", ApiName = "PERSONALINFO.EMAIL2", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string PERSONALINFO_EMAIL2 { get; set; }

        [FieldSettings("URL 1", ApiName = "PERSONALINFO.URL1", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string PERSONALINFO_URL1 { get; set; }

        [FieldSettings("URL 2", ApiName = "PERSONALINFO.URL2", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string PERSONALINFO_URL2 { get; set; }

        [FieldSettings("Address 1", ApiName = "PERSONALINFO.MAILADDRESS.ADDRESS1")]
        public string PERSONALINFO_MAILADDRESS_ADDRESS1 { get; set; }

        [FieldSettings("Address 2", ApiName = "PERSONALINFO.MAILADDRESS.ADDRESS2")]
        public string PERSONALINFO_MAILADDRESS_ADDRESS2 { get; set; }

        [FieldSettings("City", ApiName = "PERSONALINFO.MAILADDRESS.CITY")]
        public string PERSONALINFO_MAILADDRESS_CITY { get; set; }

        [FieldSettings("State", ApiName = "PERSONALINFO.MAILADDRESS.STATE")]
        public string PERSONALINFO_MAILADDRESS_STATE { get; set; }

        [FieldSettings("Postal code", ApiName = "PERSONALINFO.MAILADDRESS.ZIP")]
        public string PERSONALINFO_MAILADDRESS_ZIP { get; set; }

        [FieldSettings("Country", ApiName = "PERSONALINFO.MAILADDRESS.COUNTRY")]
        public string PERSONALINFO_MAILADDRESS_COUNTRY { get; set; }

        [FieldSettings("Country code", ApiName = "PERSONALINFO.MAILADDRESS.COUNTRYCODE")]
        public string PERSONALINFO_MAILADDRESS_COUNTRYCODE { get; set; }

        [FieldSettings("Merge payment requests", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool MERGEPAYMENTREQ { get; set; }

        [FieldSettings("Class ID")]
        public string CLASSID { get; set; }

        [FieldSettings("Class name")]
        public string CLASSNAME { get; set; }

        [FieldSettings("1099 name")]
        public string NAME1099 { get; set; }

        [FieldSettings("Send automatic payment notification", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool? PAYMENTNOTIFY { get; set; }

        [FieldSettings("Default currency")]
        public string CURRENCY { get; set; }

        [FieldSettings("Termination type")]
        public string TERMINATIONTYPE { get; set; }

        #endregion

        public bool GENERIC { get; set; }

    }
}

	//"EMPLOYEETYPE1099TYPE": null,
	//"SUPDOCFOLDERNAME": null,
	//"FORM1099TYPE": null,
	//"FORM1099BOX": null,
	//"EARNINGTYPENAME": null,
	//"EARNINGTYPEMETHOD": null,
	//"POSTACTUALCOST": "",
	//"ACHENABLED": "",
	//"ACHBANKROUTINGNUMBER": null,
	//"ACHACCOUNTNUMBER": null,
	//"ACHACCOUNTTYPE": "",
	//"ACHREMITTANCETYPE": "",
	//"RATTENDEE": null,
	//"R367282": null

