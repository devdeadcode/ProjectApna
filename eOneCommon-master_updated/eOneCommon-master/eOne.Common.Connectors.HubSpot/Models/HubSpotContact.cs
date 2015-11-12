using System;
using System.ComponentModel;

namespace eOne.Common.Connectors.HubSpot.Models
{
    public class HubspotContact : ConnectorEntityModel
    {

        #region Enums

        public enum HubspotContactLeadStatus
        {
            [Description("New")]
            NEW,
            [Description("Open")]
            OPEN,
            [Description("In progress")]
            IN_PROGRESS,
            [Description("Open deal")]
            OPEN_DEAL,
            [Description("Unqualified")]
            UNQUALIFIED
        }
        public enum HubspotContactEmailSubscription
        {
            [Description("None")]
            none,
            [Description("Monthly")]
            monthly,
            [Description("Instant")]
            instant,
            [Description("Daily")]
            daily,
            [Description("Weekly")]
            weekly
        }
        public enum HubspotContactSource
        {
            [Description("Organic search")]
            ORGANIC_SEARCH,
            [Description("Paid search")]
            PAID_SEARCH,
            [Description("Email marketing")]
            EMAIL_MARKETING,
            [Description("Social media")]
            SOCIAL_MEDIA,
            [Description("Referrals")]
            REFERRALS,
            [Description("Other campaigns")]
            OTHER_CAMPAIGNS,
            [Description("Direct traffic")]
            DIRECT_TRAFFIC,
            [Description("Offline")]
            OFFLINE,
            [Description("Unknown")]
            UNKNOWN
        }
        public enum HubspotContactLifecycleStage
        {
            [Description("Subscriber")]
            subscriber,
            [Description("Lead")]
            lead,
            [Description("Marketing qualified lead")]
            marketingqualifiedlead,
            [Description("Sales qualified lead")]
            salesqualifiedlead,
            [Description("Opportunity")]
            opportunity,
            [Description("Customer")]
            customer,
            [Description("Evagelist")]
            evangelist,
            [Description("Other")]
            other
        }
        public enum HubspotContactNumberOfEmployees
        {
            [Description("Unknown")]
            unknown,
            [Description("1-5")]
            _1_5,
            [Description("5-25")]
            _5_25,
            [Description("25-50")]
            _25_50,
            [Description("50-100")]
            _50_100,
            [Description("100-500")]
            _100_500,
            [Description("500-1000")]
            _500_1000,
            [Description("1000+")]
            _1000
        }

        #endregion 

        #region Default properties

        [FieldSettings("Name", DefaultField = true, FieldsRequiredForCalculation = "firstname,lastname")]
        public string name => $"{firstname.Trim()} {lastname.Trim()}".Trim();

        [FieldSettings("Company", DefaultField = true)]
        public string company => properties?.company == null ? string.Empty : properties.company.value;

        [FieldSettings("Email", FieldTypeId = Connector.FieldTypeIdEmail, DefaultField = true)]
        public string email => properties?.email == null ? string.Empty : properties.email.value;

        #endregion

        #region Properties

        [FieldSettings("Contact ID", KeyNumber = 1, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int vid { get; set; }

        #endregion

        #region Hidden properties

        public long addedAt { get; set; }
        public HubspotContactProperties properties { get; set; }
        public HubspotIdentityProfiles identity_profiles { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("First name")]
        public string firstname => properties?.firstname == null ? string.Empty : properties.firstname.value;

        [FieldSettings("Last name")]
        public string lastname => properties?.lastname == null ? string.Empty : properties.lastname.value;

        [FieldSettings("Modified date", FieldTypeId = Connector.FieldTypeIdDate, Modified = true)]
        public DateTime? lastmodifieddate
        {
            get
            {
                if (string.IsNullOrWhiteSpace(properties?.lastmodifieddate?.value)) return null;
                return FromEpochMilliseconds(long.Parse(properties.lastmodifieddate.value));
            }
        }

        [FieldSettings("Created date", FieldTypeId = Connector.FieldTypeIdDate, Created = true)]
        public DateTime addedAtDate => FromEpochMilliseconds(addedAt);

        [FieldSettings("In workflow", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool currentlyinworkflow => properties?.currentlyinworkflow != null && properties.currentlyinworkflow.value == "true";

        [FieldSettings("Opted out of all email", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool hs_email_optout => properties?.hs_email_optout != null && properties.hs_email_optout.value == "true";

        [FieldSettings("Has ineligible email", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool hs_email_is_ineligible => properties?.hs_email_is_ineligible != null && properties.hs_email_is_ineligible.value == "true";

        [FieldSettings("Last deal amount", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal recent_deal_amount => properties?.recent_deal_amount == null ? 0 : decimal.Parse(properties.recent_deal_amount.value);

        [FieldSettings("Total revenue", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal total_revenue => properties?.total_revenue == null ? 0 : decimal.Parse(properties.total_revenue.value);

        [FieldSettings("Annual revenue", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal annualrevenue => properties?.annualrevenue == null ? 0 : decimal.Parse(properties.annualrevenue.value);

        [FieldSettings("Average page views", FieldTypeId = Connector.FieldTypeIdQuantity)]
        public decimal hs_analytics_average_page_views => properties?.hs_analytics_average_page_views == null ? 0 : decimal.Parse(properties.hs_analytics_average_page_views.value);

        [FieldSettings("Event revenue", FieldTypeId = Connector.FieldTypeIdCurrency)]
        public decimal hs_analytics_revenue => properties?.hs_analytics_revenue == null ? 0 : decimal.Parse(properties.hs_analytics_revenue.value);

        [FieldSettings("Days to close", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int days_to_close => properties?.days_to_close == null ? 0 : int.Parse(properties.days_to_close.value);

        [FieldSettings("Number of associated deals", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int num_associated_deals => properties?.num_associated_deals == null ? 0 : int.Parse(properties.num_associated_deals.value);

        [FieldSettings("Number of followers", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int followercount => properties?.followercount == null ? 0 : int.Parse(properties.followercount.value);

        [FieldSettings("Number of page views", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int hs_analytics_num_page_views => properties?.hs_analytics_num_page_views == null ? 0 : int.Parse(properties.hs_analytics_num_page_views.value);

        [FieldSettings("Number of visits", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int hs_analytics_num_visits => properties?.hs_analytics_num_visits == null ? 0 : int.Parse(properties.hs_analytics_num_visits.value);

        [FieldSettings("Klout score", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int kloutscoregeneral => properties?.kloutscoregeneral == null ? 0 : int.Parse(properties.kloutscoregeneral.value);

        [FieldSettings("Facebook clicks", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int hs_social_facebook_clicks => properties?.hs_social_facebook_clicks == null ? 0 : int.Parse(properties.hs_social_facebook_clicks.value);

        [FieldSettings("LinkedIn clicks", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int hs_social_linkedin_clicks => properties?.hs_social_linkedin_clicks == null ? 0 : int.Parse(properties.hs_social_linkedin_clicks.value);

        [FieldSettings("Twitter clicks", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int hs_social_twitter_clicks => properties?.hs_social_twitter_clicks == null ? 0 : int.Parse(properties.hs_social_twitter_clicks.value);

        [FieldSettings("Number of notes", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int num_notes => properties?.num_notes == null ? 0 : int.Parse(properties.num_notes.value);

        [FieldSettings("Google+ clicks", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int hs_social_google_plus_clicks => properties?.hs_social_google_plus_clicks == null ? 0 : int.Parse(properties.hs_social_google_plus_clicks.value);

        [FieldSettings("LinkedIn connections", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int linkedinconnections => properties?.linkedinconnections == null ? 0 : int.Parse(properties.linkedinconnections.value);

        [FieldSettings("Number of emails sent", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int hs_email_delivered => properties?.hs_email_delivered == null ? 0 : int.Parse(properties.hs_email_delivered.value);

        [FieldSettings("Number of emails opened", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int hs_email_open => properties?.hs_email_open == null ? 0 : int.Parse(properties.hs_email_open.value);

        [FieldSettings("Number of emails clicked", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int hs_email_click => properties?.hs_email_click == null ? 0 : int.Parse(properties.hs_email_click.value);

        [FieldSettings("Number of emails bounced", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int hs_email_bounce => properties?.hs_email_bounce == null ? 0 : int.Parse(properties.hs_email_bounce.value);

        [FieldSettings("Hubspot score", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int hubspotscore => properties?.hubspotscore == null ? 0 : int.Parse(properties.hubspotscore.value);

        [FieldSettings("Number of event completions", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int hs_analytics_num_event_completions => properties?.hs_analytics_num_event_completions == null ? 0 : int.Parse(properties.hs_analytics_num_event_completions.value);

        [FieldSettings("Number of times contacted", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int num_contacted_notes => properties?.num_contacted_notes == null ? 0 : int.Parse(properties.num_contacted_notes.value);

        [FieldSettings("Number of social media clicks", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int hs_social_num_broadcast_clicks => properties?.hs_social_num_broadcast_clicks == null ? 0 : int.Parse(properties.hs_social_num_broadcast_clicks.value);

        [FieldSettings("Predictive lead score", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int hs_predictivecontactscore => properties?.hs_predictivecontactscore == null ? 0 : int.Parse(properties.hs_predictivecontactscore.value);

        [FieldSettings("Number of forms submitted", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int num_conversion_events => properties?.num_conversion_events == null ? 0 : int.Parse(properties.num_conversion_events.value);

        [FieldSettings("Number of unique forms submitted", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int num_unique_conversion_events => properties?.num_unique_conversion_events == null ? 0 : int.Parse(properties.num_unique_conversion_events.value);

        [FieldSettings("Owner ID")]
        public string hubspot_owner_id => properties?.hubspot_owner_id == null ? string.Empty : properties.hubspot_owner_id.value;

        [FieldSettings("Facebook ID")]
        public string hs_facebookid => properties?.hs_facebookid == null ? string.Empty : properties.hs_facebookid.value;

        [FieldSettings("Google+ ID")]
        public string hs_googleplusid => properties?.hs_googleplusid == null ? string.Empty : properties.hs_googleplusid.value;

        [FieldSettings("LinkedIn ID")]
        public string hs_linkedinid => properties?.hs_linkedinid == null ? string.Empty : properties.hs_linkedinid.value;

        [FieldSettings("Twitter ID")]
        public string hs_twitterid => properties?.hs_twitterid == null ? string.Empty : properties.hs_twitterid.value;

        [FieldSettings("Twitter handle")]
        public string twitterhandle => properties?.twitterhandle == null ? string.Empty : properties.twitterhandle.value;

        [FieldSettings("Salutation")]
        public string salutation => properties?.salutation == null ? string.Empty : properties.salutation.value;

        [FieldSettings("Mobile phone", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string mobilephone => properties?.mobilephone == null ? string.Empty : properties.mobilephone.value;

        [FieldSettings("Address")]
        public string address => properties?.address == null ? string.Empty : properties.address.value;

        [FieldSettings("City")]
        public string city => properties?.city == null ? string.Empty : properties.city.value;

        [FieldSettings("State")]
        public string state => properties?.state == null ? string.Empty : properties.state.value;

        [FieldSettings("Zip")]
        public string zip => properties?.zip == null ? string.Empty : properties.zip.value;

        [FieldSettings("Country")]
        public string country => properties?.country == null ? string.Empty : properties.country.value;

        [FieldSettings("Phone", FieldTypeId = Connector.FieldTypeIdPhone)]
        public string phone => properties?.phone == null ? string.Empty : properties.phone.value;

        [FieldSettings("Fax")]
        public string fax => properties?.fax == null ? string.Empty : properties.fax.value;

        [FieldSettings("Job title")]
        public string jobtitle => properties?.jobtitle == null ? string.Empty : properties.jobtitle.value;

        [FieldSettings("Twitter profile image", FieldTypeId = Connector.FieldTypeIdImage)]
        public string twitterprofilephoto => properties?.twitterprofilephoto == null ? string.Empty : properties.twitterprofilephoto.value;

        [FieldSettings("Photo", FieldTypeId = Connector.FieldTypeIdImage)]
        public string photo => properties?.photo == null ? string.Empty : properties.photo.value;

        [FieldSettings("Linked bio")]
        public string linkedinbio => properties?.linkedinbio == null ? string.Empty : properties.linkedinbio.value;

        [FieldSettings("Twitter bio")]
        public string twitterbio => properties?.twitterbio == null ? string.Empty : properties.twitterbio.value;

        [FieldSettings("Persona")]
        public string hs_persona => properties?.hs_persona == null ? string.Empty : properties.hs_persona.value;

        [FieldSettings("Website", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string website => properties?.website == null ? string.Empty : properties.website.value;

        [FieldSettings("Industry")]
        public string industry => properties?.industry == null ? string.Empty : properties.industry.value;

        [FieldSettings("Message")]
        public string message => properties?.message == null ? string.Empty : properties.message.value;

        [FieldSettings("IP address")]
        public string ipaddress => properties?.ipaddress == null ? string.Empty : properties.ipaddress.value;

        [FieldSettings("Timezone from IP address")]
        public string hs_ip_timezone => properties?.hs_ip_timezone == null ? string.Empty : properties.hs_ip_timezone.value;

        [FieldSettings("City from IP address")]
        public string ip_city => properties?.ip_city == null ? string.Empty : properties.ip_city.value;

        [FieldSettings("Country from IP address")]
        public string ip_country => properties?.ip_country == null ? string.Empty : properties.ip_country.value;

        [FieldSettings("Country code from IP address")]
        public string ip_country_code => properties?.ip_country_code == null ? string.Empty : properties.ip_country_code.value;

        [FieldSettings("Lattitude and longitude from IP address")]
        public string ip_latlon => properties?.ip_latlon == null ? string.Empty : properties.ip_latlon.value;

        [FieldSettings("State from IP address")]
        public string ip_state => properties?.ip_state == null ? string.Empty : properties.ip_state.value;

        [FieldSettings("State code from IP address")]
        public string ip_state_code => properties?.ip_state_code == null ? string.Empty : properties.ip_state_code.value;

        [FieldSettings("Zip from IP address")]
        public string ip_zipcode => properties?.ip_zipcode == null ? string.Empty : properties.ip_zipcode.value;

        [FieldSettings("Last form submitted")]
        public string recent_conversion_event_name => properties?.recent_conversion_event_name == null ? string.Empty : properties.recent_conversion_event_name.value;

        [FieldSettings("First page seen", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string hs_analytics_first_url => properties?.hs_analytics_first_url == null ? string.Empty : properties.hs_analytics_first_url.value;

        [FieldSettings("Last page seen", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string hs_analytics_last_url => properties?.hs_analytics_last_url == null ? string.Empty : properties.hs_analytics_last_url.value;

        [FieldSettings("First form submitted")]
        public string first_conversion_event_name => properties?.first_conversion_event_name == null ? string.Empty : properties.first_conversion_event_name.value;

        [FieldSettings("First referring website", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string hs_analytics_first_referrer => properties?.hs_analytics_first_referrer == null ? string.Empty : properties.hs_analytics_first_referrer.value;

        [FieldSettings("Last referring website", FieldTypeId = Connector.FieldTypeIdUrl)]
        public string hs_analytics_last_referrer => properties?.hs_analytics_last_referrer == null ? string.Empty : properties.hs_analytics_last_referrer.value;

        [FieldSettings("Last email name")]
        public string hs_email_last_email_name => properties?.hs_email_last_email_name == null ? string.Empty : properties.hs_email_last_email_name.value;

        [FieldSettings("Original source drill-down 1")]
        public string hs_analytics_source_data_1 => properties?.hs_analytics_source_data_1 == null ? string.Empty : properties.hs_analytics_source_data_1.value;

        [FieldSettings("Original source drill-down 2")]
        public string hs_analytics_source_data_2 => properties?.hs_analytics_source_data_2 == null ? string.Empty : properties.hs_analytics_source_data_2.value;
        
        [FieldSettings("Email confirmation status")]
        public string hs_emailconfirmationstatus => properties?.hs_emailconfirmationstatus == null ? string.Empty : properties.hs_emailconfirmationstatus.value;

        [FieldSettings("Lead status", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(HubspotContactLeadStatus))]
        public HubspotContactLeadStatus hs_lead_status => properties?.hs_lead_status == null ? HubspotContactLeadStatus.NEW : ParseEnum<HubspotContactLeadStatus>(properties.hs_lead_status.value);

        [FieldSettings("Subscription type", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(HubspotContactEmailSubscription))]
        public HubspotContactEmailSubscription blog_default_hubspot_blog_subscription => properties?.blog_default_hubspot_blog_subscription == null ? HubspotContactEmailSubscription.none : ParseEnum<HubspotContactEmailSubscription>(properties.blog_default_hubspot_blog_subscription.value);

        [FieldSettings("Source", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(HubspotContactSource))]
        public HubspotContactSource hs_analytics_source => properties?.hs_analytics_source == null ? HubspotContactSource.UNKNOWN : ParseEnum<HubspotContactSource>(properties.hs_analytics_source.value);

        [FieldSettings("Lifecycle stage", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(HubspotContactLifecycleStage))]
        public HubspotContactLifecycleStage lifecyclestage => properties?.lifecyclestage == null ? HubspotContactLifecycleStage.other : ParseEnum<HubspotContactLifecycleStage>(properties.lifecyclestage.value);

        [FieldSettings("Number of employees", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(HubspotContactNumberOfEmployees))]
        public HubspotContactNumberOfEmployees numemployees => properties?.numemployees == null ? HubspotContactNumberOfEmployees.unknown : ParseEnum<HubspotContactNumberOfEmployees>($"_{properties.numemployees.value.Replace("-", "_").Replace("+", "")}");

        [FieldSettings("First conversion date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? first_conversion_date
        {
            get
            {
                if (string.IsNullOrWhiteSpace(properties?.first_conversion_date?.value)) return null;
                return FromEpochMilliseconds(long.Parse(properties.first_conversion_date.value));
            }
        }

        [FieldSettings("First deal created date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? first_deal_created_date
        {
            get
            {
                if (string.IsNullOrWhiteSpace(properties?.first_deal_created_date?.value)) return null;
                return FromEpochMilliseconds(long.Parse(properties.first_deal_created_date.value));
            }
        }

        [FieldSettings("Date owner assigned", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? hubspot_owner_assigneddate
        {
            get
            {
                if (string.IsNullOrWhiteSpace(properties?.hubspot_owner_assigneddate?.value)) return null;
                return FromEpochMilliseconds(long.Parse(properties.hubspot_owner_assigneddate.value));
            }
        }

        [FieldSettings("Last conversion date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? recent_conversion_date
        {
            get
            {
                if (string.IsNullOrWhiteSpace(properties?.recent_conversion_date?.value)) return null;
                return FromEpochMilliseconds(long.Parse(properties.recent_conversion_date.value));
            }
        }

        [FieldSettings("Last deal created date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? recent_deal_close_date
        {
            get
            {
                if (string.IsNullOrWhiteSpace(properties?.recent_deal_close_date?.value)) return null;
                return FromEpochMilliseconds(long.Parse(properties.recent_deal_close_date.value));
            }
        }

        [FieldSettings("Last social media engagement date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? hs_social_last_engagement
        {
            get
            {
                if (string.IsNullOrWhiteSpace(properties?.hs_social_last_engagement?.value)) return null;
                return FromEpochMilliseconds(long.Parse(properties.hs_social_last_engagement.value));
            }
        }

        [FieldSettings("Last email sent date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? hs_email_last_send_date
        {
            get
            {
                if (string.IsNullOrWhiteSpace(properties?.hs_email_last_send_date?.value)) return null;
                return FromEpochMilliseconds(long.Parse(properties.hs_email_last_send_date.value));
            }
        }

        [FieldSettings("Last email open date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? hs_email_last_open_date
        {
            get
            {
                if (string.IsNullOrWhiteSpace(properties?.hs_email_last_open_date?.value)) return null;
                return FromEpochMilliseconds(long.Parse(properties.hs_email_last_open_date.value));
            }
        }

        [FieldSettings("Last email click date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? hs_email_last_click_date
        {
            get
            {
                if (string.IsNullOrWhiteSpace(properties?.hs_email_last_click_date?.value)) return null;
                return FromEpochMilliseconds(long.Parse(properties.hs_email_last_click_date.value));
            }
        }

        [FieldSettings("First email send date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? hs_email_first_send_date
        {
            get
            {
                if (string.IsNullOrWhiteSpace(properties?.hs_email_first_send_date?.value)) return null;
                return FromEpochMilliseconds(long.Parse(properties.hs_email_first_send_date.value));
            }
        }

        [FieldSettings("First email open date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? hs_email_first_open_date
        {
            get
            {
                if (string.IsNullOrWhiteSpace(properties?.hs_email_first_open_date?.value)) return null;
                return FromEpochMilliseconds(long.Parse(properties.hs_email_first_open_date.value));
            }
        }

        [FieldSettings("First email click date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? hs_email_first_click_date
        {
            get
            {
                if (string.IsNullOrWhiteSpace(properties?.hs_email_first_click_date?.value)) return null;
                return FromEpochMilliseconds(long.Parse(properties.hs_email_first_click_date.value));
            }
        }

        [FieldSettings("Date that contact became a lead", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? hs_lifecyclestage_lead_date
        {
            get
            {
                if (string.IsNullOrWhiteSpace(properties?.hs_lifecyclestage_lead_date?.value)) return null;
                return FromEpochMilliseconds(long.Parse(properties.hs_lifecyclestage_lead_date.value));
            }
        }

        [FieldSettings("Date that contact became a marketing qualified lead", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? hs_lifecyclestage_marketingqualifiedlead_date
        {
            get
            {
                if (string.IsNullOrWhiteSpace(properties?.hs_lifecyclestage_marketingqualifiedlead_date?.value)) return null;
                return FromEpochMilliseconds(long.Parse(properties.hs_lifecyclestage_marketingqualifiedlead_date.value));
            }
        }

        [FieldSettings("Date that contact became an opportunity", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? hs_lifecyclestage_opportunity_date
        {
            get
            {
                if (string.IsNullOrWhiteSpace(properties?.hs_lifecyclestage_opportunity_date?.value)) return null;
                return FromEpochMilliseconds(long.Parse(properties.hs_lifecyclestage_opportunity_date.value));
            }
        }

        [FieldSettings("Date that contact became a sales qualified lead", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? hs_lifecyclestage_salesqualifiedlead_date
        {
            get
            {
                if (string.IsNullOrWhiteSpace(properties?.hs_lifecyclestage_salesqualifiedlead_date?.value)) return null;
                return FromEpochMilliseconds(long.Parse(properties.hs_lifecyclestage_salesqualifiedlead_date.value));
            }
        }

        [FieldSettings("Date that contact became an evangelist", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? hs_lifecyclestage_evangelist_date
        {
            get
            {
                if (string.IsNullOrWhiteSpace(properties?.hs_lifecyclestage_evangelist_date?.value)) return null;
                return FromEpochMilliseconds(long.Parse(properties.hs_lifecyclestage_evangelist_date.value));
            }
        }

        [FieldSettings("Date that contact became a customer", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? hs_lifecyclestage_customer_date
        {
            get
            {
                if (string.IsNullOrWhiteSpace(properties?.hs_lifecyclestage_customer_date?.value)) return null;
                return FromEpochMilliseconds(long.Parse(properties.hs_lifecyclestage_customer_date.value));
            }
        }

        [FieldSettings("Date that contact became a subscriber", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? hs_lifecyclestage_subscriber_date
        {
            get
            {
                if (string.IsNullOrWhiteSpace(properties?.hs_lifecyclestage_subscriber_date?.value)) return null;
                return FromEpochMilliseconds(long.Parse(properties.hs_lifecyclestage_subscriber_date.value));
            }
        }

        [FieldSettings("Date that contact became other lifecycle", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? hs_lifecyclestage_other_date
        {
            get
            {
                if (string.IsNullOrWhiteSpace(properties?.hs_lifecyclestage_other_date?.value)) return null;
                return FromEpochMilliseconds(long.Parse(properties.hs_lifecyclestage_other_date.value));
            }
        }

        [FieldSettings("Last update of an email event", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? hs_email_lastupdated
        {
            get
            {
                if (string.IsNullOrWhiteSpace(properties?.hs_email_lastupdated?.value)) return null;
                return FromEpochMilliseconds(long.Parse(properties.hs_email_lastupdated.value));
            }
        }

        [FieldSettings("Close date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? closedate
        {
            get
            {
                if (string.IsNullOrWhiteSpace(properties?.closedate?.value)) return null;
                return FromEpochMilliseconds(long.Parse(properties.closedate.value));
            }
        }

        [FieldSettings("Last contact date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? notes_last_contacted
        {
            get
            {
                if (string.IsNullOrWhiteSpace(properties?.notes_last_contacted?.value)) return null;
                return FromEpochMilliseconds(long.Parse(properties.notes_last_contacted.value));
            }
        }

        [FieldSettings("Notes last updated", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? notes_last_updated
        {
            get
            {
                if (string.IsNullOrWhiteSpace(properties?.notes_last_updated?.value)) return null;
                return FromEpochMilliseconds(long.Parse(properties.notes_last_updated.value));
            }
        }

        [FieldSettings("Next activity date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? notes_next_activity_date
        {
            get
            {
                if (string.IsNullOrWhiteSpace(properties?.notes_next_activity_date?.value)) return null;
                return FromEpochMilliseconds(long.Parse(properties.notes_next_activity_date.value));
            }
        }

        [FieldSettings("Last visit date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? hs_analytics_last_visit_timestamp
        {
            get
            {
                if (string.IsNullOrWhiteSpace(properties?.hs_analytics_last_visit_timestamp?.value)) return null;
                return FromEpochMilliseconds(long.Parse(properties.hs_analytics_last_visit_timestamp.value));
            }
        }

        [FieldSettings("First visit date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? hs_analytics_first_visit_timestamp
        {
            get
            {
                if (string.IsNullOrWhiteSpace(properties?.hs_analytics_first_visit_timestamp?.value)) return null;
                return FromEpochMilliseconds(long.Parse(properties.hs_analytics_first_visit_timestamp.value));
            }
        }

        #endregion

    }
}
