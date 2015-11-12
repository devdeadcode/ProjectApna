using System;

namespace eOne.Common.Connectors.Intacct.Models
{
    public class IntacctTimesheet : IntacctBase
    {

        #region Default properties

        [FieldSettings("Employee ID", DefaultField = true)]
        public string EMPLOYEEID { get; set; }

        [FieldSettings("Employee name", DefaultField = true)]
        public string EMPLOYEENAME { get; set; }

        [FieldSettings("Description", DefaultField = true)]
        public string DESCRIPTION { get; set; }

        [FieldSettings("Begin date", DefaultField = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? BEGINDATE { get; set; }

        [FieldSettings("End date", DefaultField = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? ENDDATE { get; set; }

        [FieldSettings("Status", DefaultField = true)]
        public string STATE { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Record number", KeyNumber = 1)]
        public string RECORDNO { get; set; }

        [FieldSettings("First name")]
        public string EMPLOYEEFIRSTNAME { get; set; }

        [FieldSettings("Last name")]
        public string EMPLOYEELASTNAME { get; set; }

        #endregion

    }
}


	//"LINES#": "3",
	//"CONFIG": "0,7,PI,true,true,1",
	//"UOM": "HM",
	//"HOURSINDAY": "8",
	//"EMPLOYEE_LOCATIONID": "100",
	//"EMPLOYEE_DEPARTMENTID": "10",
	//"EMPLOYEE_CLASSID": null,
	//"METHOD": "",
	//"ACTUALCOST": "false",
