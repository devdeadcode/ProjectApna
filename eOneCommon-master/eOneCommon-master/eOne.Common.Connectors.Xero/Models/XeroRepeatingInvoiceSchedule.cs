using System;

namespace eOne.Common.Connectors.Xero.Models
{
    public class XeroRepeatingInvoiceSchedule
    {

        public enum XeroRepeatingInvoiceScheduleUnit
        {
            WEEKLY,
            MONTHLY
        }
        public enum XeroRepeatingInvoiceDueDateType
        {
            DAYSAFTERBILLDATE,	
            DAYSAFTERBILLMONTH,	
            OFCURRENTMONTH,
            OFFOLLOWINGMONTH
        }

        public int Period { get; set; }
        public XeroRepeatingInvoiceScheduleUnit Unit { get; set; }
        public int DueDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime NextScheduledDate { get; set; }
        public DateTime? EndDate { get; set; }
        public XeroRepeatingInvoiceDueDateType DueDateType { get; set; }

    }
}