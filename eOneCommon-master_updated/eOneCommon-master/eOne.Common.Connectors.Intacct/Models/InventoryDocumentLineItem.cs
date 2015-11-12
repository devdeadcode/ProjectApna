using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eOne.Common.Connectors.Intacct.Models
{
    public class InventoryDocumentLineItem : IntacctBase
    {

        public string ITEMID { get; set; }
        public string ITEMNAME { get; set; }
        public string ITEMDESC { get; set; }
        public string UNIT { get; set; }
        public string EXTENDED_DESCRIPTION { get; set; }

        public decimal? QUANTITY { get; set; }
        public decimal? QTY_CONVERTED { get; set; }
        public decimal? RETAILPRICE { get; set; }
        public decimal? PRICE { get; set; }
        public decimal? TOTAL { get; set; }

    }
}

//{
//	"RECORDNO": "1",
//	"DOCHDRNO": "1",
//	"DOCHDRID": "Beginning Balance-BBIN0001",
//	"BUNDLENUMBER": null,
//	"LINE_NO": "0",
//	"": "Each",
//	"WAREHOUSE.LOCATION_NO": "WH1",
//	"WAREHOUSE.NAME": "Warehouse 1",
//	"MEMO": null,
//	"ITEM.TAXABLE": "",
//	"ITEM.TAXGROUP.RECORDNO": null,
//	"ITEM.RENEWALMACRO.MACROID": null,
//	"ITEMGLGROUP": "3",
//	"STATE": null,
//	"GLENTRYKEY1": null,
//	"GLENTRYKEY2": null,
//	"COST": null,
//	"COST_METHOD": "S",
//	"AVERAGE_COST": null,
//	"WHSE_AVERAGE_COST": null,
//	"UIQTY": "1",
//	"DISCOUNTPERCENT": null,
//	"UIPRICE": "100",
//	"UIVALUE": "100",
//	"LOCATIONID": null,
//	"LOCATIONNAME": null,
//	"DEPARTMENTID": null,
//	"DEPARTMENTNAME": null,
//	"DEPTKEY": null,
//	"LOCATIONKEY": null,
//	"TIMETYPEKEY": null,
//	"TIMETYPENAME": null,
//	"TIMENOTES": null,
//	"EEACCOUNTLABELKEY": null,
//	"EEACCOUNTLABEL": null,
//	"SOURCE_DOCKEY": null,
//	"SOURCE_DOCLINEKEY": null,
//	"REVRECTEMPLATE": null,
//	"REVRECTEMPLATEKEY": null,
//	"REVRECSTARTDATE": null,
//	"ITEMTERM": null,
//	"TERMPERIOD": null,
//	"REVRECENDDATE": null,
//	"PRORATEPRICE": "",
//	"DEFERREVENUE": null,
//	"SC_REVRECTEMPLATE": null,
//	"SC_REVRECTEMPLATEKEY": null,
//	"SC_REVRECSTARTDATE": null,
//	"SC_REVRECENDDATE": null,
//	"SC_STARTDATE": null,
//	"ITEM.ITEMTYPE": "Inventory",
//	"ITEM.NUMDEC_SALE": "2",
//	"ITEM.NUMDEC_STD": "2",
//	"ITEM.NUMDEC_PUR": "2",
//	"ITEM.REVPOSTING": "Kit Level",
//	"ITEM.COMPUTEFORSHORTTERM": "false",
//	"ITEM.RENEWALMACROKEY": null,
//	"ITEM.UOMGRPKEY": "5",
//	"DISCOUNT_MEMO": null,
//	"ITEM.REVPRINTING": "Kit",
//	"CURRENCY": "USD",
//	"EXCHRATEDATE": null,
//	"EXCHRATETYPE": null,
//	"EXCHRATE": "1",
//	"TRX_PRICE": null,
//	"TRX_VALUE": null,
//	"SCHEDULENAME": null,
//	"SCHEDULEID": null,
//	"RECURDOCNAME": null,
//	"RECURDOCID": null,
//	"RECURDOCENTRYKEY": null,
//	"RENEWALMACRO": null,
//	"RENEWALMACROKEY": null,
//	"OVERRIDETAX": "",
//	"SC_CREATERECURSCHED": "",
//	"SC_EXISTINGSCHED": null,
//	"SC_EXTENDLINEPERIOD": "",
//	"SC_INSTALLPRICING": "",
//	"RECURCONTRACTID": null,
//	"SOURCE_DOCID": null,
//	"BILLABLE": "",
//	"BILLED": "",
//	"BILLABLETIMEENTRYKEY": null,
//	"BILLABLEPRENTRYKEY": null,
//	"BILLABLEDOCENTRYKEY": null,
//	"FORM1099": null,
//	"PERCENTVAL": null,
//	"TAXABSVAL": null,
//	"TAXABLEAMOUNT": null,
//	"LINETOTAL": null,
//	"DISCOUNT": null,
//	"TRX_TAXABSVAL": null,
//	"TRX_LINETOTAL": null,
//	"TAXVALOVERRIDE": "",
//	"PROJECTKEY": null,
//	"PROJECTNAME": null,
//	"TASKKEY": null,
//	"TASKNAME": null,
//	"BILLINGTEMPLATEKEY": null,
//	"BILLINGTEMPLATE": null,
//	"BILLINGSCHEDULEKEY": null,
//	"BILLINGSCHEDULEENTRY.PERCENTCOMPLETED": null,
//	"BILLINGSCHEDULEENTRY.PERCENTBILLED": null,
//	"BILLINGSCHEDULEENTRY.BILLEDQTY": null,
//	"BILLINGSCHEDULEENTRY.ESTQTY": null,
//	"BILLINGSCHEDULEENTRY.TRUNCPERCENTCOMPLETED": null,
//	"BILLINGSCHEDULEENTRY.BILLINGTEMPLATEENTRYKEY": null,
//	"BILLINGSCHEDULEENTRY.BILLINGSCHEDULEKEY": null,
//	"PROJECTDIMKEY": null,
//	"PROJECTID": null
//},
