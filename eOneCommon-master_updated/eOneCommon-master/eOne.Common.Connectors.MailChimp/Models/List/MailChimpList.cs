using System;
using System.ComponentModel;

namespace eOne.Common.Connectors.MailChimp.Models.List
{
    public class MailChimpList : ConnectorEntityModel
    {

        #region Enums

        public enum MailChimpListVisibility
        {
            [Description("Public")]
            pub,
            [Description("Private")]
            prv
        }

        #endregion

        #region Default properties

        [FieldSettings("Name", DefaultField = true, Description = true)]
        public string name { get; set; }

        [FieldSettings("Number of members", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public double stats_member_count => stats.member_count;

        [FieldSettings("Last sent date", DefaultField = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? stats_campaign_last_sent => stats.campaign_last_sent;

        [FieldSettings("Default subject", DefaultField = true)]
        public string campaign_defaults_subject => campaign_defaults.subject;

        [FieldSettings("Default from name", DefaultField = true)]
        public string campaign_defaults_from_name => campaign_defaults.from_name;

        [FieldSettings("Default from email", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEmail)]
        public string campaign_defaults_from_email => campaign_defaults.from_email;

        #endregion

        #region Properties

        [FieldSettings("Permission reminder")]
        public string permission_reminder { get; set; }

        [FieldSettings("Subscribe notification email", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string notify_on_subscribe { get; set; }

        [FieldSettings("Unsubscribe notification email", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string notify_on_unsubscribe { get; set; }

        [FieldSettings("ID", KeyNumber = 1)]
        public string id { get; set; }

        [FieldSettings("Email type option", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool email_type_option { get; set; }

        [FieldSettings("Use archive bar", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool use_archive_bar { get; set; }

        [FieldSettings("List rating", FieldTypeId = Connector.FieldTypeIdQuantity)]
        public double list_rating { get; set; }

        [FieldSettings("Subscribe URL (short)", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string subscribe_url_short { get; set; }

        [FieldSettings("Subscribe URL (long)", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string subscribe_url_long { get; set; }

        [FieldSettings("Beamer address")]
        public string beamer_address { get; set; }

        [FieldSettings("Visibility", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(MailChimpListVisibility))]
        public MailChimpListVisibility visibility { get; set; }

        [FieldSettings("Created date", Created = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? date_created { get; set; }

        #endregion

        #region Hidden properties

        public int web_id { get; set; }
        public MailChimpListStatistics stats { get; set; }
        public MailChimpListContact contact { get; set; }
        public MailChimpListDefaults campaign_defaults { get; set; }

        #endregion

        #region Calculations

        #region Stats

        [FieldSettings("Last subscribe date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? stats_last_sub_date => stats.last_sub_date;

        [FieldSettings("Last unsubscribe date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? stats_last_unsub_date => stats.last_unsub_date;

        [FieldSettings("Number of unsubscribed members", FieldTypeId = Connector.FieldTypeIdInteger)]
        public double stats_unsubscribe_count => stats.unsubscribe_count;

        [FieldSettings("Number of cleaned members", FieldTypeId = Connector.FieldTypeIdInteger)]
        public double stats_cleaned_count => stats.cleaned_count;

        [FieldSettings("Number of members since last campaign", FieldTypeId = Connector.FieldTypeIdInteger)]
        public double stats_member_count_since_send => stats.member_count_since_send;

        [FieldSettings("Number of unsubscribed members since last campaign", FieldTypeId = Connector.FieldTypeIdInteger)]
        public double stats_unsubscribe_count_since_send => stats.unsubscribe_count_since_send;

        [FieldSettings("Number of cleaned members since last campaign", FieldTypeId = Connector.FieldTypeIdInteger)]
        public double stats_cleaned_count_since_send => stats.cleaned_count_since_send;

        [FieldSettings("Number of merge fields", FieldTypeId = Connector.FieldTypeIdInteger)]
        public double stats_merge_field_count => stats.merge_field_count;

        [FieldSettings("Average subscriptions per month", FieldTypeId = Connector.FieldTypeIdQuantity)]
        public double stats_avg_sub_rate => stats.avg_sub_rate;

        [FieldSettings("Average unsubscribes per month", FieldTypeId = Connector.FieldTypeIdQuantity)]
        public double stats_avg_unsub_rate => stats.avg_unsub_rate;

        [FieldSettings("Target subscription rate", FieldTypeId = Connector.FieldTypeIdQuantity)]
        public double stats_target_sub_rate => stats.target_sub_rate;

        [FieldSettings("Open rate", FieldTypeId = Connector.FieldTypeIdQuantity)]
        public double stats_open_rate => stats.open_rate;

        [FieldSettings("Click rate", FieldTypeId = Connector.FieldTypeIdQuantity)]
        public double stats_click_rate => stats.click_rate;

        #endregion

        #region Contact

        [FieldSettings("Contact address 1")]
        public string contact_address1 => contact.address1;

        [FieldSettings("Contact address 2")]
        public string contact_address2 => contact.address2;

        [FieldSettings("Contact city")]
        public string contact_city => contact.city;

        [FieldSettings("Contact state")]
        public string contact_state => contact.state;

        [FieldSettings("Contact postal code")]
        public string contact_zip => contact.zip;

        [FieldSettings("Contact country")]
        public string contact_country => contact.country;

        [FieldSettings("Contact company")]
        public string contact_company => contact.company;

        [FieldSettings("Contact phone", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string contact_phone => contact.phone;

        [FieldSettings("Contact address", FieldTypeId = Connector.FieldTypeIdAddress)]
        public string contact_address => BuildAddress(contact_address1, contact_address2, contact_city, contact_state, contact_zip);

        #endregion

        [FieldSettings("Default language", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(IsoLanguage))]
        public IsoLanguage campaign_defaults_language => campaign_defaults.language;

        #endregion

    }
}


         
         


