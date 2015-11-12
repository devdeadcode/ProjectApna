using System;

namespace eOne.Common.Connectors.Intacct.Models
{
    public class IntacctArInvoiceLineItem
    {

        public string ACCOUNTNO { get; set; }
        public string ACCOUNTTITLE { get; set; }
        public string OFFSETGLACCOUNTNO { get; set; }
        public string OFFSETGLACCOUNTTITLE { get; set; }
        public string ACCOUNTLABEL { get; set; }
        public string DEPARTMENTID { get; set; }
        public string DEPARTMENTNAME { get; set; }
        public string LOCATIONID { get; set; }
        public string LOCATIONNAME { get; set; }
        public string ENTRYDESCRIPTION { get; set; }
        public string EXCH_RATE_TYPE_ID { get; set; }
        public string CURRENCY { get; set; }
        public string BASECURR { get; set; }
        public string GLACCOUNTNO { get; set; }
        public string GLACCOUNTTITLE { get; set; }
        public string PROJECTID { get; set; }
        public string PROJECTNAME { get; set; }
        public string CUSTOMERID { get; set; }
        public string CUSTOMERNAME { get; set; }
        public string VENDORID { get; set; }
        public string VENDORNAME { get; set; }
        public string EMPLOYEEID { get; set; }
        public string EMPLOYEENAME { get; set; }
        public string ITEMID { get; set; }
        public string ITEMNAME { get; set; }
        public string CLASSID { get; set; }
        public string CLASSNAME { get; set; }

        public DateTime? ENTRY_DATE { get; set; }
        public DateTime? EXCH_RATE_DATE { get; set; }

        public decimal? AMOUNT { get; set; }
        public decimal? TRX_AMOUNT { get; set; }
        public decimal? EXCHANGE_RATE { get; set; }
        public decimal? TOTALPAID { get; set; }
        public decimal? TRX_TOTALPAID { get; set; }
        public decimal? TOTALSELECTED { get; set; }
        public decimal? TRX_TOTALSELECTED { get; set; }
        public decimal? SUBTOTAL { get; set; }

        public int LINE_NO { get; set; }

    }
}


//	"ALLOCATION": null,
//	"LINEITEM": "T",
//	"PARENTENTRY": null,
//	"DEFERREVENUE": "",
//	"REVRECTEMPLATEKEY": null,
//	"REVRECTEMPLATE": null,
//	"DEFERREDREVACCTKEY": null,
//	"DEFERREDREVACCTNO": null,
//	"DEFERREDREVACCTTITLE": null,
//	"REVRECSTARTDATE": null,
//	"REVRECENDDATE": null,
//	"BASELOCATION": null,
//	"STATE": "A",