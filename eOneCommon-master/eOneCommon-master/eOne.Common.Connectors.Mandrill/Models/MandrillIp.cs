using System;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Mandrill.Models
{
    public class MandrillIp : DataConnectorEntityModel
    {

        public string ip { get; set; }
        public DateTime created_at { get; set; }
        public string pool { get; set; }
        public string domain { get; set; }
        public MandrillDns custom_dns { get; set; }
        public MandrillWarmup warmup { get; set; }

        public DateTime created_at_date => created_at.Date;

        public DateTime created_at_time => Time(created_at);

        public bool custom_dns_enabled => custom_dns.enabled;

        public bool custom_dns_valid => custom_dns.valid;

        public string custom_dns_error => custom_dns.error;

        public bool warmup_warming_up => warmup.warming_up;

        public DateTime warmup_start_at_date => warmup.start_at_date;

        public DateTime warmup_start_at_time => warmup.start_at_time;

        public DateTime warmup_end_at_date => warmup.end_at_date;

        public DateTime warmup_end_at_time => warmup.end_at_time;
    }
}