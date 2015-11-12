using System;
using eOne.Common.Connectors.MadMimi.Helpers;

namespace eOne.Common.Connectors.MadMimi.Models
{
    public class MadMimiMailingRecipient
    {

        [FieldSettings("Promotion name", DefaultField = true)]
        public string promotion_name => mailing.promotion_name;

        [FieldSettings("Subject", DefaultField = true)]
        public string mailing_subject => mailing.subject;

        [FieldSettings("Email address", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEmail)]
        public string email_address { get; set; }

        [FieldSettings("Status", DefaultField = true)]
        public string state { get; set; }

        public string sent_at { get; set; }
        public MadMimiMailing mailing { get; set; }

        public DateTime? sent_at_date => DataConversion.ParseDateTime(sent_at);
        public DateTime? sent_at_time => sent_at_date;
        public DateTime? mailing_start_date => mailing.started_send_date;
        public DateTime? mailing_finish_date => mailing.finished_send_date;
        public int mailing_id => mailing.id;
        public int promotion_id => mailing.promotion_id;

    }
}
