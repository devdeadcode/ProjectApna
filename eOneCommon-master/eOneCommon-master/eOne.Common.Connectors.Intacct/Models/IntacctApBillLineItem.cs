using System.Collections.Generic;

namespace eOne.Common.Connectors.Intacct.Models
{
    public class IntacctApBillLineItem
    {

        public string accountlabel { get; set; }
        public string glaccountno { get; set; }
        public decimal amount { get; set; }
        public List<IntacctCustomField> customfields { get; set; }
        public IntacctDate revrecstartdate { get; set; }
        public IntacctDate revrecenddate { get; set; }
        public bool billable { get; set; }
        public string projectid { get; set; }
        public string customerid { get; set; }
        public string vendorid { get; set; }
        public string employeeid { get; set; }
        public string itemid { get; set; }
        public string classid { get; set; }
        public decimal totalpaid { get; set; }
        public decimal totaldue { get; set; }
        public string locationid { get; set; }
        public string departmentid { get; set; }
        public string memo { get; set; }
        

    }
}

//                    <allocationid></allocationid>
//                    <item1099></item1099>
//                    <key></key>
//                    <revrectemplate></revrectemplate>
//                    <devrefaccount></devrefaccount>

