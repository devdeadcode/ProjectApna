using System;
using System.ComponentModel;

namespace eOne.Common.Connectors.HubSpot.Models
{
    public class HubspotCalendarEvent : ConnectorEntityModel
    {

        #region Enums

        public enum HubspotCalendarEventType
        {
            [Description("Content")]
            CONTENT,
            [Description("Social")]
            SOCIAL,
            [Description("Task")]
            PUBLISHING_TASK
        }
        public enum HubspotCalendarEventState
        {
            [Description("Published")]
            PUBLISHED,
            [Description("Scheduled")]
            SCHEDULED,
            [Description("Published or scheduled")]
            PUBLISHED_OR_SCHEDULED,
            [Description("To do")]
            TODO,
            [Description("Done")]
            DONE
        }
        public enum HubspotCalendarEventCategory
        {
            [Description("Blog post")]
            BLOG_POST,
            [Description("Email")]
            EMAIL,
            LANDING_PAGE,
            CUSTOM,
            [Description("Twitter")]
            twitter,
            [Description("Facebook")]
            facebook,
            [Description("LinkedIn")]
            linkedin,
            [Description("Google+")]
            googlepluspages,
            [Description("Recurring email")]
            recurring_email,
            legacy_page,
            site_page
        }

        #endregion

        #region Default properties

        [FieldSettings("Event type", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(HubspotCalendarEventType), DefaultField = true)]
        public HubspotCalendarEventType eventType { get; set; }

        [FieldSettings("Name", DefaultField = true, Description = true)]
        public string name { get; set; }

        [FieldSettings("Owner name", DefaultField = true)]
        public string owner_name => owner == null ? string.Empty : owner.name;

        [FieldSettings("Event date", DefaultField = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? event_date => FromEpochMilliseconds(eventDate);

        [FieldSettings("Event time", DefaultField = true, FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime? event_time => event_date;

        [FieldSettings("Status", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(HubspotCalendarEventState), DefaultField = true)]
        public HubspotCalendarEventState state { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Event ID", FieldTypeId = Connector.FieldTypeIdInteger, KeyNumber = 1)]
        public long id { get; set; }

        [FieldSettings("Description")]
        public string description { get; set; }

        [FieldSettings("Recurring", FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool recurring { get; set; }

        [FieldSettings("Category", FieldTypeId = Connector.FieldTypeIdEnum, EnumType = typeof(HubspotCalendarEventCategory))]
        public HubspotCalendarEventCategory category_enum => ParseEnum<HubspotCalendarEventCategory>(category.Replace("-", "_"));

        [FieldSettings("User name")]
        public string socialUsername { get; set; }

        [FieldSettings("Display name")]
        public string socialDisplayName { get; set; }

        public string url { get; set; }
        public string avatarUrl { get; set; }

        #endregion

        #region Hidden properties

        public long contentGroupId { get; set; }
        public long contentId { get; set; }
        public string campaignGuid { get; set; }
        public long ownerId { get; set; }
        public long createdBy { get; set; }
        public string previewKey { get; set; }
        public long portalId { get; set; }
        public long eventDate { get; set; }
        public string category { get; set; }
        public HubspotOwner owner { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Owner email", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string owner_email => owner == null ? string.Empty : owner.email;

        #endregion

    }
}

//    "topicIds": null,
