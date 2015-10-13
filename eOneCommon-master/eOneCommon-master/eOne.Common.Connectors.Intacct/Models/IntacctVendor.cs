using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace eOne.Common.Connectors.Intacct.Models
{
    public class IntacctVendor
    {

        public enum IntacctVendorPaymentMethod
        {
            check
        }
        public enum IntacctVendorBillingType
        {
            balanceforward
        }
        public enum IntacctVendorStatus
        {
            active
        }
        public enum IntacctVendorForm1099Type
        {
            [Description("Dividend Income (Form 1099-DIV)")]	
            DIV,
            [Description("Interest Income (Form 1099-INT)")]
            INT,
            [Description("Miscellaneous Income (Form 1099-MISC)")]
            MISC,
            [Description("Distributions From Pensions, Annuities...(Form 1099-R)")]
            R,
            [Description("Proceeds From Real Estate Transactions (Form 1099-S)")]
            S,
            [Description("Taxable Distributions Received (Form 1099-PATR)")]
            PATR,
            [Description("Certain Government Payments (Form 1099-G)")]
            G,
            [Description("Certain Gambling Winnings (Form W-2G)")]
            W_2G
        }

        public string vendorid { get; set; }
        public string name { get; set; }
        public string parentid { get; set; }
        public string termname { get; set; }
        public string accountlabel { get; set; }
        public string vendtype { get; set; }
        public string taxid { get; set; }
        public decimal creditlimit { get; set; }
        public IntacctVendorPaymentMethod paymethod { get; set; }
        public bool outsourcecheck { get; set; }
        public IntacctVendorBillingType billingtype { get; set; }
        public string vendoraccountno { get; set; }
        public bool onhold { get; set; }
        public bool donotcutcheck { get; set; }
        public string comments { get; set; }
        public bool status { get; set; }
        public string currency { get; set; }
        public bool onetime { get; set; }
        public string externalid { get; set; }
        public IntacctContact primary { get; set; }
        public IntacctContact returnto { get; set; }
        public IntacctContact payto { get; set; }
        public IntacctContact contactinfo { get; set; }
        public List<IntacctContactItem> contactlist { get; set; }
        public string name1099 { get; set; }
        public string form1099type { get; set; }
        public IntacctVendorForm1099Type form1099type_enum => (IntacctVendorForm1099Type)Enum.Parse(typeof(IntacctVendorForm1099Type), form1099type.Replace('_', '-'));
        public string form1099box { get; set; }

        public List<IntacctCustomField> customfields { get; set; }

    }
}


//<content>
//  <function controlid="testControlId">
//    <create_vendor ignoreduplicates="false">
//      <paymentnotify></paymentnotify> 
//      <achenabled></achenabled> 
//      <wireenabled></wireenabled>        
//      <checkenabled></checkenabled> 
//      <achbankroutingnumber></achbankroutingnumber> 
//      <achaccountnumber></achaccountnumber> 
//      <achaccounttype></achaccounttype> 
//      <achremittancetype></achremittancetype> 
//      <wirebankname></wirebankname> 
//      <wirebankroutingnumber></wirebankroutingnumber> 
//      <wireaccountnumber></wireaccountnumber> 
//      <wireaccounttype></wireaccounttype> 
//      <pmplusremittancetype></pmplusremittancetype> 
//      <pmplusemail></pmplusemail>  
//      <pmplusfax></pmplusfax> 
//      <displaytermdiscount></displaytermdiscount> 
//      <!-- in multi-entity you can add
//        <visibility>
//           <visibility_type/>
//           <restricted_locs/>
//           <restricted_depts/>
//        </visibility>
//      -->
//      <supdocid></supdocid> 
//      <mergepaymentreq>false</mergepaymentreq> 
//      <offsetglaccountno>1610</offsetglaccountno>
//    </create_vendor>
//  </function>
//</content>
