using System;
using eOne.Common.Connectors.MailChimp.Models.Campaign;

namespace eOne.Common.Connectors.MailChimp.Models.List
{
    public class MailChimpListAbuse
    {

        [FieldSettings("Email", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEmail)]
        public string email_address { get; set; }

        [FieldSettings("Date", DefaultField = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime date { get; set; }

        [FieldSettings("List name", DefaultField = true)]
        public string list_name => list.name;

        [FieldSettings("Abuse ID", KeyNumber = 1)]
        public string id { get; set; }

        [FieldSettings("Campaign ID")]
        public string campaign_id { get; set; }

        [FieldSettings("List ID")]
        public string list_id { get; set; }

        public string email_id { get; set; }
        public MailChimpList list { get; set; }
        public MailChimpCampaignCollection campaign { get; set; }
        
        [FieldSettings("Number of members for list", FieldTypeId = Connector.FieldTypeIdInteger)]
        public double list_stats_member_count => list.stats_member_count;

        [FieldSettings("Last sent date for list", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? list_stats_campaign_last_sent => list.stats_campaign_last_sent;

        [FieldSettings("Default subject for list")]
        public string list_campaign_defaults_subject => list.campaign_defaults.subject;

        [FieldSettings("Default from name for list")]
        public string list_campaign_defaults_from_name => list.campaign_defaults_from_name;

        [FieldSettings("Default from email for list", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string list_campaign_defaults_from_email => list.campaign_defaults_from_email;

        [FieldSettings("List rating", FieldTypeId = Connector.FieldTypeIdQuantity)]
        public double list_rating => list.list_rating;

        [FieldSettings("List subscribe URL (short)", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string list_subscribe_url_short => list.subscribe_url_short;

        [FieldSettings("List subscribe URL (long)", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string list_subscribe_url_long => list.subscribe_url_long;

        [FieldSettings("List visibility", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(MailChimpList.MailChimpListVisibility))]
        public MailChimpList.MailChimpListVisibility list_visibility => list.visibility;

        [FieldSettings("List created date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? list_date_created => list.date_created;

    }
}

