using System;
using System.Collections.Generic;

namespace eOne.Common.Connectors.Intacct.Models
{
    public class IntacctEmployee
    {

        public enum IntacctEmployeeType
        {
            parttime
        }
        public enum IntacctEmployeeGender
        {
            male,
            female
        }
        public enum IntacctEmployeeStatus
        {
            active
        }

        public string employeeid { get; set; }
        public string ssn { get; set; }
        public string title { get; set; }
        public string locationid { get; set; }
        public string departmentid { get; set; }
        public string supervisorid { get; set; }
        public IntacctDate birthdate { get; set; }
        public IntacctDate startdate { get; set; }
        public IntacctEmployeeType employeetype { get; set; }
        public IntacctEmployeeGender gender { get; set; }
        public IntacctEmployeeStatus status { get; set; }
        public string currency { get; set; }
        public string externalid { get; set; }

        public List<IntacctContactItem> contactlist { get; set; }

        public DateTime birthdate_date => birthdate.date;
        public DateTime startdate_date => startdate.date;
    }
}

//<content>
//  <function controlid="testControlId">
//    <create_employee ignoreduplicates="false"> <!-- optional attribute 'ignoreduplicates' defaults to false. -->
//      <personalinfo> <!-- Required -->
//        <!-- Contact record for employee -->
//        <contact>
//          <contactname>Tiesto, DJ</contactname>
//          <printas>DJ Tiesto</printas>
//          <companyname>Trance Co.</companyname>
//          <prefix>Mr.</prefix>
//          <firstname>DJ</firstname>
//          <lastname>Tiesto</lastname>
//          <initial>I.</initial>
//          <phone1>(999) 999-9999</phone1>
//          <phone2>(888) 888-8888</phone2>
//          <cellphone>(777) 777-7777</cellphone>
//          <pager>(666) 666-6666</pager>
//          <fax>(555) 555-5555</fax>
//          <email1>DJTiesto@tranceco.com</email1>
//          <email2>DJTiesto@tranceco.org</email2>
//          <url1>http://www.tranceco.com</url1>
//          <url2>http://www.tranceco.org</url2>
//          <mailaddress>
//            <address1>368 Outskirts Rd</address1>
//            <address2>, Apt 19</address2>
//            <city>Bronx</city>
//            <state>NY</state>
//            <zip>10400</zip>
//            <country>USA</country>
//          </mailaddress>
//        </contact>
//      </personalinfo>
//    </create_employee>
//  </function>
//</content>