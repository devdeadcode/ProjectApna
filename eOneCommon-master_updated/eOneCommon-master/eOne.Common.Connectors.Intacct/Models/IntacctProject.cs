using System;

namespace eOne.Common.Connectors.Intacct.Models
{
    public class IntacctProject : IntacctBase
    {

        #region Default properties

        [FieldSettings("Project ID", DefaultField = true, KeyNumber = 1)]
        public string PROJECTID { get; set; }

        [FieldSettings("Name", DefaultField = true, Description = true)]
        public string NAME { get; set; }

        [FieldSettings("Category", DefaultField = true)]
        public string PROJECTCATEGORY { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Description")]
        public string DESCRIPTION { get; set; }

        [FieldSettings("Currency")]
        public string CURRENCY { get; set; }

        [FieldSettings("Budgeted amount", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal? BUDGETAMOUNT { get; set; }

        [FieldSettings("Contract amount", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal? CONTRACTAMOUNT { get; set; }

        [FieldSettings("Actual amount", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal? ACTUALAMOUNT { get; set; }

        [FieldSettings("Budgeted duration", FieldTypeId = Connector.FieldTypeIdQuantity)]
        public decimal? BUDGETQTY { get; set; }

        [FieldSettings("Estimated duration", FieldTypeId = Connector.FieldTypeIdQuantity)]
        public decimal? ESTQTY { get; set; }

        [FieldSettings("Actual duration", FieldTypeId = Connector.FieldTypeIdQuantity)]
        public decimal? ACTUALQTY { get; set; }

        [FieldSettings("Approved duration", FieldTypeId = Connector.FieldTypeIdQuantity)]
        public decimal? APPROVEDQTY { get; set; }

        [FieldSettings("Remaining duration", FieldTypeId = Connector.FieldTypeIdQuantity)]
        public decimal? REMAININGQTY { get; set; }

        [FieldSettings("Calculated percentage completed", FieldTypeId = Connector.FieldTypeIdQuantity)]
        public decimal? PERCENTCOMPLETE { get; set; }

        [FieldSettings("Observed percentage completed", FieldTypeId = Connector.FieldTypeIdQuantity)]
        public decimal? OBSPERCENTCOMPLETE { get; set; }

        [FieldSettings("Begin date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? BEGINDATE { get; set; }

        [FieldSettings("End date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? ENDDATE { get; set; }

        [FieldSettings("Customer ID")]
        public string CUSTOMERID { get; set; }

        [FieldSettings("Customer name")]
        public string CUSTOMERNAME { get; set; }

        [FieldSettings("Parent ID")]
        public string PARENTID { get; set; }

        [FieldSettings("Parent name")]
        public string PARENTNAME { get; set; }

        [FieldSettings("Invoice with parent", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool INVOICEWITHPARENT { get; set; }

        [FieldSettings("Project status")]
        public string PROJECTSTATUS { get; set; }

        [FieldSettings("Manager ID")]
        public string MANAGERID { get; set; }

        [FieldSettings("Manager name")]
        public string MANAGERCONTACTNAME { get; set; }

        [FieldSettings("Department ID")]
        public string DEPARTMENTID { get; set; }

        [FieldSettings("Department name")]
        public string DEPARTMENTNAME { get; set; }

        [FieldSettings("Location ID")]
        public string LOCATIONID { get; set; }

        [FieldSettings("Location name")]
        public string LOCATIONNAME { get; set; }

        [FieldSettings("Sales order number")]
        public string SONUMBER { get; set; }

        [FieldSettings("Purchase order number")]
        public string PONUMBER { get; set; }

        [FieldSettings("Purchase order amount", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal? POAMOUNT { get; set; }

        [FieldSettings("Purchase quote number")]
        public string PQNUMBER { get; set; }

        [FieldSettings("Term")]
        public string TERMNAME { get; set; }

        [FieldSettings("Class ID")]
        public string CLASSID { get; set; }

        [FieldSettings("Class name")]
        public string CLASSNAME { get; set; }

        [FieldSettings("Billing type")]
        public string BILLINGTYPE { get; set; }

        [FieldSettings("Primary contact", ApiName = "CONTACTINFO.CONTACTNAME")]
        public string CONTACTINFO_CONTACTNAME { get; set; }

        [FieldSettings("Ship to contact", ApiName = "SHIPTO.CONTACTNAME")]
        public string SHIPTO_CONTACTNAME { get; set; }

        [FieldSettings("Bill to contact", ApiName = "BILLTO.CONTACTNAME")]
        public string BILLTO_CONTACTNAME { get; set; }

        [FieldSettings("Sales contact ID")]
        public string SALESCONTACTID { get; set; }

        [FieldSettings("Sales contact name")]
        public string SALESCONTACTNAME { get; set; }

        [FieldSettings("Invoice currency")]
        public string INVOICECURRENCY { get; set; }

        [FieldSettings("User restrictions")]
        public string USERRESTRICTIONS { get; set; }

        [FieldSettings("External user")]
        public string CUSTUSERID { get; set; }

        [FieldSettings("Project type")]
        public string PROJECTTYPE { get; set; }

        [FieldSettings("Labor pricing method")]
        public string BILLINGPRICING { get; set; }

        [FieldSettings("Expense pricing method")]
        public string EXPENSEPRICING { get; set; }

        [FieldSettings("PO/AP pricing method")]
        public string POAPPRICING { get; set; }

        [FieldSettings("Labor cost plus fee percentage", FieldTypeId = Connector.FieldTypeIdPercentage)]
        public decimal? BILLINGRATE { get; set; }

        [FieldSettings("Expense cost plus fee percentage", FieldTypeId = Connector.FieldTypeIdPercentage)]
        public decimal? EXPENSERATE { get; set; }

        [FieldSettings("PO/AP cost plus fee percentage", FieldTypeId = Connector.FieldTypeIdPercentage)]
        public decimal? POAPRATE { get; set; }

        [FieldSettings("Budgeted cost", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal? BUDGETEDCOST { get; set; }

        [FieldSettings("GL budget ID")]
        public string BUDGETID { get; set; }

        [FieldSettings("Invoice message")]
        public string INVOICEMESSAGE { get; set; }

        [FieldSettings("Billable employee expenses", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool BILLABLEEXPDEFAULT { get; set; }

        [FieldSettings("Billable AP/PO", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool BILLABLEAPPODEFAULT { get; set; }

        [FieldSettings("Reference number")]
        public string DOCNUMBER { get; set; }

        #endregion

    }
}