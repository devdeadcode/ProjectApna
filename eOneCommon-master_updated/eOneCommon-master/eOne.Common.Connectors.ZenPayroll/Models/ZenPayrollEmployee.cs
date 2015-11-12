using System;
using System.Collections.Generic;
using System.Linq;

namespace eOne.Common.Connectors.ZenPayroll.Models
{
    public class ZenPayrollEmployee
    {

        public string first_name { get; set; }
        public string middle_initial { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string ssn { get; set; }
        public DateTime date_of_birth { get; set; }
        public List<ZenPayrollJob> jobs { get; set; }
        public ZenPayrollAddress home_address { get; set; }
        public bool terminated { get; set; }
        public List<ZenPayrollGarnishment> garnishments { get; set; }
        public List<ZenPayrollPaidTimeOff> eligible_paid_time_off { get; set; }
        public List<ZenPayrollTermination> terminations { get; set; }
        public ZenPayrollFederalTaxInformation federal_tax_information { get; set; }

        public int id { get; set; }
        public string version { get; set; }

        public string name => first_name.Trim() + " " + last_name.Trim();

        public string home_address_address => home_address.address;
        public string home_address_street_1 => home_address.street_1;

        public string home_address_street_2 => home_address.street_2;
        public string home_address_city => home_address.city;

        public string home_address_state => home_address.state;

        public string home_address_zip => home_address.zip;
        public string home_address_country => home_address.country;

        public bool has_active_garnishments
        {
            get
            {
                return garnishments.Any(garnishment => garnishment.active);
            }
        }
        public string job_title
        {
            get
            {
                if (jobs == null || jobs.Count > 0) return string.Empty;
                return jobs[0].title;
            }
        }
        public decimal job_rate
        {
            get
            {
                if (jobs == null || jobs.Count > 0) return 0;
                return jobs[0].rate;
            }
        }
        public ZenPayrollJob.ZenPayrollJobPaymentUnit job_payment_unit
        {
            get
            {
                if (jobs == null || jobs.Count > 0) return 0;
                return jobs[0].payment_unit;
            }
        }

    }
}
