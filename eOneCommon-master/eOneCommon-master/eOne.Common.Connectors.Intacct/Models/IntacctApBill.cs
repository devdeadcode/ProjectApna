using System;
using System.Collections.Generic;

namespace eOne.Common.Connectors.Intacct.Models
{
    public class IntacctApBill
    {

        public string vendorid { get; set; }
        public string termname { get; set; }
        public string batchkey { get; set; }
        public string billno { get; set; }
        public string ponumber { get; set; }
        public string description { get; set; }
        public string externalid { get; set; }
        public string basecurr { get; set; }
        public string currency { get; set; }
        public decimal exchrate { get; set; }
        public string exchratetype { get; set; }
        public string supdocid { get; set; }

        public IntacctDate datecreated { get; set; }
        public IntacctDate dateposted { get; set; }
        public IntacctDate datedue { get; set; }
        public IntacctDate exchratedate { get; set; }
        public List<IntacctCustomField> customfields { get; set; }
        public List<IntacctApBillLineItem> billitems { get; set; }
        public IntacctContact payto { get; set; }
        public IntacctContact returnto { get; set; }
        public string onhold { get; set; }
        public string nogl { get; set; }

        public string payto_contactname => payto.contactname;

        public string returnto_contactname => returnto.contactname;

        public bool onhold_bool => onhold.Trim().Substring(0, 1).ToUpper() == "T";
        public bool nogl_bool => nogl.Trim().Substring(0, 1).ToUpper() == "T";

        public DateTime datecreated_date => datecreated.date;
        public DateTime dateposted_date => dateposted.date;
        public DateTime datedue_date => datedue.date;
        public DateTime exchratedate_date => exchratedate.date;
    }
}
