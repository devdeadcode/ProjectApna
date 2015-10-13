using System.Collections.Generic;

namespace eOne.Common.Connectors.Intacct.Models
{
    public class IntacctProject
    {

        public string projectid { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public IntacctDate begindate { get; set; }
        public IntacctDate enddate { get; set; }
        public List<IntacctCustomField> customfields { get; set; }
        public List<IntacctProjectResource> projectresources { get; set; }
        public string currency { get; set; }
        public string locationid { get; set; }
        public string customerid { get; set; }
        public string ponumber { get; set; }
        public decimal poamount { get; set; }
        public decimal budgetqty { get; set; }

    }
}

//      <parentid></parentid>
//      <projectcategory></projectcategory> <!-- REQUIRED -->
//      <projecttype></projecttype>
//      <projectstatus></projectstatus>
//      <managerid></managerid>
//      <primarycontact></primarycontact>
//      <billtocontact></billtocontact>
//      <shiptocontact></shiptocontact>
//      <departmentid></departmentid>
//      <sonumber></sonumber>
//      <pqnumber></pqnumber>
//      <status></status>
