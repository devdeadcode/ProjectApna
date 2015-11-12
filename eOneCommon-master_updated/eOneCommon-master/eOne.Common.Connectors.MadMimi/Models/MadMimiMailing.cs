using System;
using eOne.Common.Connectors.MadMimi.Helpers;

namespace eOne.Common.Connectors.MadMimi.Models
{
    public class MadMimiMailing
    {

        [FieldSettings("Promotion name", DefaultField = true)]
        public string promotion_name => promotion.name;

        [System.Xml.Serialization.XmlAttribute]
        [FieldSettings("Subject", DefaultField = true)]
        public string subject { get; set; }

        [FieldSettings("Date started", DefaultField = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? started_send_date => DataConversion.ParseDateTime(started_send);

        [System.Xml.Serialization.XmlAttribute]
        [FieldSettings("Total contacts", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int count { get; set; }

        [FieldSettings("Total sent", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int sent => statistics?.sent ?? 0;

        [FieldSettings("Total views", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int views => statistics?.views ?? 0;

        [System.Xml.Serialization.XmlAttribute]
        [FieldSettings("Mailing ID", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int id { get; set; }

        [FieldSettings("Date finished", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? finished_send_date => DataConversion.ParseDateTime(finished_send);

        [FieldSettings("Time started", FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime? started_send_time => started_send_date;

        [FieldSettings("Time finished", FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime? finished_send_time => finished_send_date;

        public MadMimiPromotion promotion { get; set; }
        public MadMimiMailingStatistics statistics { get; set; }
        public string started_send { get; set; }
        public string finished_send { get; set; }

        [FieldSettings("Promotion ID", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int promotion_id => promotion.id;

        //[FieldSettings("Total untraced", FieldTypeId = Connector.FieldTypeIdInteger)]
        //public int untraced => statistics?.untraced ?? 0;

        [FieldSettings("Total clicked", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int clicked => statistics?.clicked ?? 0;

        [FieldSettings("Total forwarded", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int forwarded => statistics?.forwarded ?? 0;

        [FieldSettings("Total bounced", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int bounced => statistics?.bounced ?? 0;

        [FieldSettings("Total unsubscribed", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int unsubscribed => statistics?.unsubscribed ?? 0;

        [FieldSettings("Total marked as spam", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int abused => statistics?.abused ?? 0;

        [FieldSettings("Click rate", FieldTypeId = Connector.FieldTypeIdPercentage)]
        public decimal click_rate
        {
            get
            {
                if (sent == 0) return 0;
                return clicked / sent * 100;
            }
        }

        [FieldSettings("Bounce rate", FieldTypeId = Connector.FieldTypeIdPercentage)]
        public decimal bounce_rate
        {
            get
            {
                if (sent == 0) return 0;
                return bounced / sent * 100;
            }
        }

        [FieldSettings("Unsubscribe rate", FieldTypeId = Connector.FieldTypeIdPercentage)]
        public decimal unsubscribe_rate
        {
            get
            {
                if (sent == 0) return 0;
                return unsubscribed / sent * 100;
            }
        }

        [FieldSettings("Marked as spam rate", FieldTypeId = Connector.FieldTypeIdPercentage)]
        public decimal abuse_rate
        {
            get
            {
                if (sent == 0) return 0;
                return abused / sent * 100;
            }
        }

        [FieldSettings("Forward rate", FieldTypeId = Connector.FieldTypeIdPercentage)]
        public decimal forward_rate
        {
            get
            {
                if (sent == 0) return 0;
                return forwarded / sent * 100;
            }
        }

        [FieldSettings("Send rate", FieldTypeId = Connector.FieldTypeIdPercentage)]
        public decimal sent_rate
        {
            get
            {
                if (count == 0) return 0;
                return sent / count * 100;
            }
        }

    }
}
