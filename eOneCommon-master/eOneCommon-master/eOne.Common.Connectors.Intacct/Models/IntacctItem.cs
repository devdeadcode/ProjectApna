using System;

namespace eOne.Common.Connectors.Intacct.Models
{
    public class IntacctItem
    {

        public string itemid { get; set; }
        public string name { get; set; }
        public IntacctDate datelastsold { get; set; }
        public IntacctDate datelastrecvd { get; set; }

        public DateTime datelastsold_date => datelastsold.date;

        public DateTime datelastrecvd_date => datelastrecvd.date;
    }
}


//<content>
// <function controlid="testControlId">
//  <create_item> 
//   <status></status>
//   <itemtype></itemtype>
//   <enable_bins></enable_bins>
//   <extended_description></extended_description>
//   <productlineid></productlineid>
//   <substituteid></substituteid>
//   <ship_weight></ship_weight>
//   <taxable></taxable>
//   <cost_method> </cost_method>
//   <standard_cost> </standard_cost> 
//   <average_cost> </average_cost> 
//   <base_price></base_price>
//   <standard_unit></standard_unit>
//   <purchase_unit> </purchase_unit>
//   <purchase_unit_factor> </purchase_unit_factor> 
//   <sales_unit> </sales_unit>
//   <sales_unit_factor></sales_unit_factor>
//   <default_warehouse></default_warehouse>
//   <glgroup></glgroup>
//   <note></note>
//   <inventory_precision> </inventory_precision> 
//   <purchasing_precision> </purchasing_precision> 
//   <sales_precision> </sales_precision> 
//   <upc> </upc> 
//   <hasstartenddates> </hasstartenddates> 
//   <term_period> </term_period> 
//   <defaultnoofperiods> </defaultnoofperiods> 
//   <computepriceforshortterm></computepriceforshortterm>
//   <itaxgroup>
//    <key></key>
//    <name></name>
//   </itaxgroup>
//   <taxgroup> </taxgroup> 
//   <revenue_posting> </revenue_posting> 
//   <vendlineitems> 
//    <vendlineitem>
//        <vendorid></vendorid>
//        <stockno></stockno>
//        <leadtime></leadtime>
//    </vendlineitem>
//   </vendlineitems>
//   <econorderqty></econorderqty>
//   <whslineitems> </whslineitems> 
//   <complineitems> </complineitems> 
//   <taxcode> </taxcode> 
//   <vsoecategory></vsoecategory>
//   <vsoedlvrstatus> </vsoedlvrstatus> 
//   <vsoerevdefstatus> </vsoerevdefstatus> 
//   <customfields></customfields>
//  </create_item>
// </funcion>
//</content>
