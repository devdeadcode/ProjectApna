using System.Collections.Generic;

namespace eOne.Common.Connectors.Intacct.Models
{
    public class IntacctCustomer
    {

        public enum IntacctCustomerDeliveryOption
        {
            print, 
            email,
            online
        }

        public string customerid { get; set; }
        public string name { get; set; }
        public List<IntacctCustomerDeliveryOption> deliveryoptions { get; set; }
        public IntacctContact primary { get; set; }
        public IntacctContact billto { get; set; }
        public IntacctContact shipto { get; set; }
        public IntacctContact contactinfo { get; set; }
        public List<IntacctContactItem> contactlist { get; set; }

    }
}

//<content>
//    <function controlid="testControlId">
//        <create_customer>
//            <parentid></parentid>
//            <termname></termname>
//            <custrepid></custrepid>
//            <shippingmethod></shippingmethod>
//            <custtype></custtype>
//            <taxid></taxid>
//            <creditlimit></creditlimit>
//            <territoryid></territoryid>
//            <resaleno></resaleno>
//            <accountlabel></accountlabel>
//            <glaccountno></glaccountno>
//            <glgroup></glgroup>
//            <onhold></onhold>
//            <comments></comments>
//            <status></status>
//            <currency></currency>
//            <externalid></externalid>
//            <vsoepricelist></vsoepricelist>
//            <customfields></customfields>
//            <!-- in multi-entity you can add
//                <visibility>
//                    <visibility_type/>
//                    <restricted_locs/>
//                    <restricted_depts/>
//                </visibility>
//            -->
//            <supdocid></supdocid>
//        </create_customer>
//    </function>
//</content>