using System;

namespace eOne.Common.Connectors.Intacct.Models
{
    public class IntacctTimesheetEntry : IntacctBase
    {

        #region Default properties

        [FieldSettings("Timesheet date", DefaultField = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? ENTRYDATE { get; set; }

        [FieldSettings("Project name", DefaultField = true)]
        public string PROJECTNAME { get; set; }

        [FieldSettings("Time type", DefaultField = true)]
        public string TIMETYPE { get; set; }

        [FieldSettings("Item name", DefaultField = true)]
        public string ITEMNAME { get; set; }

        [FieldSettings("Department name", DefaultField = true)]
        public string DEPARTMENTNAME { get; set; }

        [FieldSettings("Location name", DefaultField = true)]
        public string LOCATIONNAME { get; set; }

        [FieldSettings("Number of hours", DefaultField = true, FieldTypeId = Connector.FieldTypeIdQuantity)]
        public decimal? QTY { get; set; }

        [FieldSettings("Status", DefaultField = true)]
        public string STATE { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Record number", KeyNumber = 1)]
        public string RECORDNO { get; set; }

        [FieldSettings("Line number", KeyNumber = 2)]
        public int LINENO { get; set; }

        [FieldSettings("Project ID")]
        public string PROJECTID { get; set; }

        [FieldSettings("Project class ID")]
        public string PROJECT_CLASSID { get; set; }

        [FieldSettings("Customer ID")]
        public string CUSTOMERID { get; set; }

        [FieldSettings("Customer name")]
        public string CUSTOMERNAME { get; set; }

        [FieldSettings("Item ID")]
        public string ITEMID { get; set; }

        [FieldSettings("Description")]
        public string DESCRIPTION { get; set; }

        [FieldSettings("Notes")]
        public string NOTES { get; set; }

        [FieldSettings("Location ID")]
        public string LOCATIONID { get; set; }

        [FieldSettings("Department ID")]
        public string DEPARTMENTID { get; set; }

        [FieldSettings("Employee ID")]
        public string EMPLOYEEID { get; set; }

        [FieldSettings("Employee name")]
        public string EMPLOYEENAME { get; set; }

        [FieldSettings("Billable", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool BILLABLE { get; set; }

        [FieldSettings("Billed", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool BILLED { get; set; }

        public DateTime? TS_BEGINDATE { get; set; }
        public DateTime? TS_ENDDATE { get; set; }

        #endregion

        #region Calculations

        public string DayOfWeekName => DayOfWeek(ENTRYDATE);

        #endregion

    }
}


//	"TASKNAME": null,
//	"TASK_CLASSID": null,
//	"TS_LOCATIONID": null,
//	"STATGLENTRYLINENO": null,
//	"LABORGLENTRYLINENO": null,
//	"LABORGLENTRYOFFSETLINENO": null,
//	"LABORGLENTRYAMOUNT": null,
//	"LABORGLENTRYTRXAMOUNT": null,
//	"LABORGLENTRYCOSTRATE": null,
//	"STATGLENTRYAMOUNT": null,
//	"VARGLENTRYAMOUNT": null,
//	"VARGLENTRYTRXAMOUNT": null,
//	"VARGLENTRYLINENO": null,
//	"VARGLENTRYOFFSETLINENO": null,
//	"CLZNAMOUNT": null,
//	"EXTCOSTRATE": null,
//	"EXTBILLRATE": null,
//	"VENDORID": null,
//	"VENDORNAME": null,
//	"CLASSID": null,
//	"CLASSNAME": null

