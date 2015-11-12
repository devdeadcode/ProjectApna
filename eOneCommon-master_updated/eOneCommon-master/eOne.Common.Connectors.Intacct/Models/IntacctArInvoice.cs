using System;

namespace eOne.Common.Connectors.Intacct.Models
{
    public class IntacctArInvoice : IntacctBase
    {

        public string RECORDID { get; set; }
        public string CUSTOMERID { get; set; }
        public string CUSTOMERNAME { get; set; }
        
        public string STATE { get; set; }

        public string CUSTMESSAGEID { get; set; }
        public string DOCNUMBER { get; set; }
        public string DESCRIPTION { get; set; }
        public string DESCRIPTION2 { get; set; }
        public string TERMNAME { get; set; }
        public string BASECURR { get; set; }
        public string CURRENCY { get; set; }
        public string DELIVERY_OPTIONS { get; set; }
        public string PRBATCH { get; set; }

        public decimal? EXCHANGE_RATE { get; set; }
        public decimal? TOTALENTERED { get; set; }
        public decimal? TOTALSELECTED { get; set; }
        public decimal? TOTALPAID { get; set; }
        public decimal? TOTALDUE { get; set; }
        public decimal? TRX_TOTALENTERED { get; set; }
        public decimal? TRX_TOTALSELECTED { get; set; }
        public decimal? TRX_TOTALPAID { get; set; }
        public decimal? TRX_TOTALDUE { get; set; }
        public decimal? TRX_ENTITYDUE { get; set; }

        public DateTime? WHENDISCOUNT { get; set; }
        public DateTime? WHENDUE { get; set; }
        public DateTime? WHENPAID { get; set; }
        public DateTime? EXCH_RATE_DATE { get; set; }

        public string EXCH_RATE_TYPE_ID { get; set; }

    }
}

//	"CUSTMESSAGE.MESSAGE": null,
//	"CONTACT.CONTACTNAME": "Musician's Hut",
//	"CONTACT.PREFIX": null,
//	"CONTACT.FIRSTNAME": null,
//	"CONTACT.INITIAL": null,
//	"CONTACT.LASTNAME": null,
//	"CONTACT.COMPANYNAME": "Musician's Hut",
//	"CONTACT.PRINTAS": "Musician's Hut",
//	"CONTACT.PHONE1": "954-661-5789",
//	"CONTACT.PHONE2": null,
//	"CONTACT.CELLPHONE": null,
//	"CONTACT.PAGER": null,
//	"CONTACT.FAX": null,
//	"CONTACT.EMAIL1": null,
//	"CONTACT.EMAIL2": null,
//	"CONTACT.URL1": null,
//	"CONTACT.URL2": null,
//	"CONTACT.VISIBLE": "true",
//	"CONTACT.MAILADDRESS.ADDRESS1": "Attn: Eric Archuleta",
//	"CONTACT.MAILADDRESS.ADDRESS2": "PO Box 223851",
//	"CONTACT.MAILADDRESS.CITY": "Hollywood",
//	"CONTACT.MAILADDRESS.STATE": "FL",
//	"CONTACT.MAILADDRESS.ZIP": "33022",
//	"CONTACT.MAILADDRESS.COUNTRY": null,
//	"CONTACT.MAILADDRESS.COUNTRYCODE": "",
//	"SHIPTO.CONTACTNAME": null,
//	"SHIPTO.PREFIX": null,
//	"SHIPTO.FIRSTNAME": null,
//	"SHIPTO.INITIAL": null,
//	"SHIPTO.LASTNAME": null,
//	"SHIPTO.COMPANYNAME": null,
//	"SHIPTO.PRINTAS": null,
//	"SHIPTO.PHONE1": null,
//	"SHIPTO.PHONE2": null,
//	"SHIPTO.CELLPHONE": null,
//	"SHIPTO.PAGER": null,
//	"SHIPTO.FAX": null,
//	"SHIPTO.EMAIL1": null,
//	"SHIPTO.EMAIL2": null,
//	"SHIPTO.URL1": null,
//	"SHIPTO.URL2": null,
//	"SHIPTO.VISIBLE": "",
//	"SHIPTO.MAILADDRESS.ADDRESS1": null,
//	"SHIPTO.MAILADDRESS.ADDRESS2": null,
//	"SHIPTO.MAILADDRESS.CITY": null,
//	"SHIPTO.MAILADDRESS.STATE": null,
//	"SHIPTO.MAILADDRESS.ZIP": null,
//	"SHIPTO.MAILADDRESS.COUNTRY": null,
//	"SHIPTO.MAILADDRESS.COUNTRYCODE": "",
//	"BILLTO.CONTACTNAME": null,
//	"BILLTO.PREFIX": null,
//	"BILLTO.FIRSTNAME": null,
//	"BILLTO.INITIAL": null,
//	"BILLTO.LASTNAME": null,
//	"BILLTO.COMPANYNAME": null,
//	"BILLTO.PRINTAS": null,
//	"BILLTO.PHONE1": null,
//	"BILLTO.PHONE2": null,
//	"BILLTO.CELLPHONE": null,
//	"BILLTO.PAGER": null,
//	"BILLTO.FAX": null,
//	"BILLTO.EMAIL1": null,
//	"BILLTO.EMAIL2": null,
//	"BILLTO.URL1": null,
//	"BILLTO.URL2": null,
//	"BILLTO.VISIBLE": "",
//	"BILLTO.MAILADDRESS.ADDRESS1": null,
//	"BILLTO.MAILADDRESS.ADDRESS2": null,
//	"BILLTO.MAILADDRESS.CITY": null,
//	"BILLTO.MAILADDRESS.STATE": null,
//	"BILLTO.MAILADDRESS.ZIP": null,
//	"BILLTO.MAILADDRESS.COUNTRY": null,
//	"BILLTO.MAILADDRESS.COUNTRYCODE": "",
//	"SYSTEMGENERATED": "F",
//	"HASPOSTEDREVREC": "",
//	"AUWHENCREATED": "01\/01\/1970 00:00:00",
//	"WHENMODIFIED": "08\/17\/2010 10:52:02",
