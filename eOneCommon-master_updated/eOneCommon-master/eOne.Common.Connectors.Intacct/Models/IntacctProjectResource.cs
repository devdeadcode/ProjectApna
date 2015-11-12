using System;

namespace eOne.Common.Connectors.Intacct.Models
{
    public class IntacctProjectResource : IntacctBase
    {

        #region Default properties

        [FieldSettings("Project ID", DefaultField = true)]
        public string PROJECTID { get; set; }

        [FieldSettings("Project name", DefaultField = true)]
        public string PROJECTNAME { get; set; }

        [FieldSettings("Employee name", DefaultField = true)]
        public string EMPLOYEECONTACTNAME { get; set; }

        [FieldSettings("Item name", DefaultField = true)]
        public string ITEMNAME { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Item ID")]
        public string ITEMID { get; set; }

        [FieldSettings("Description")]
        public string DESCRIPTION { get; set; }

        [FieldSettings("Start date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? STARTDATE { get; set; }

        [FieldSettings("Employee ID")]
        public string EMPLOYEEID { get; set; }

        #endregion

        public string BILLINGPRICING { get; set; }
        public string EXPENSEPRICING { get; set; }
        public string POAPPRICING { get; set; }

        public decimal? BILLINGRATE { get; set; }
        public decimal? EXPENSERATE { get; set; }
        public decimal? POAPRATE { get; set; }

    }
}