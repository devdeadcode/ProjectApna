using System;

namespace eOne.Common.Connectors.Intacct.Models
{
    public class IntacctEmployeeExpense : IntacctBase
    {

        #region Default properties

        [FieldSettings("Expense report number", DefaultField = true, KeyNumber = 1, Description = true)]
        public string RECORDID { get; set; }

        [FieldSettings("Employee name", DefaultField = true)]
        public string ENTITY { get; set; }

        [FieldSettings("Amount", FieldTypeId = Connector.FieldTypeIdCurrency, DefaultField = true)]
        public decimal? TOTALENTERED { get; set; }

        [FieldSettings("Amount due", FieldTypeId = Connector.FieldTypeIdCurrency, DefaultField = true)]
        public decimal? TOTALDUE { get; set; }

        [FieldSettings("Status", DefaultField = true)]
        public string STATE { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Currency")]
        public string CURRENCY { get; set; }

        [FieldSettings("Base currency")]
        public string BASECURR { get; set; }

        [FieldSettings("Reason for expense")]
        public string DESCRIPTION { get; set; }

        [FieldSettings("Memo")]
        public string MEMO { get; set; }

        [FieldSettings("Document number")]
        public string DOCNUMBER { get; set; }

        [FieldSettings("Employee ID")]
        public string EMPLOYEEID { get; set; }

        [FieldSettings("Batch")]
        public string PRBATCH { get; set; }

        [FieldSettings("Amount paid", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal? TOTALPAID { get; set; }

        [FieldSettings("GL posting date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? WHENPOSTED { get; set; }

        [FieldSettings("Date paid", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? WHENPAID { get; set; }

        [FieldSettings("Created date", FieldTypeId = Connector.FieldTypeIdDate, Created = true)]
        public DateTime? AUWHENCREATED { get; set; }

        public decimal? TOTALSELECTED { get; set; }
        public decimal? NR_TOTALENTERED { get; set; }
        public decimal? NR_TRX_TOTALENTERED { get; set; }
        public string SYSTEMGENERATED { get; set; }

        [FieldSettings("Last name", ApiName = "CONTACT.LASTNAME")]
        public string CONTACT_LASTNAME { get; set; }

        [FieldSettings("First name", ApiName = "CONTACT.FIRSTNAME")]
        public string CONTACT_FIRSTNAME { get; set; }

        #endregion

    }
}