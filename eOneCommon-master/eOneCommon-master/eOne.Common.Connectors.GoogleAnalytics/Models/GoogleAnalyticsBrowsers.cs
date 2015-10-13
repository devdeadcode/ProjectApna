using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.GoogleAnalytics.Models
{
    class GoogleAnalyticsBrowsers : DataConnectorEntityModel
    {
        public enum Device_Category
        {
            [FieldSettings("Desktop")]
            Desktop,
            [FieldSettings("Tablet")]
            Tablet,
            [FieldSettings("Mobile")]
            Mobile
        }

        [FieldSettings("Browser", DefaultField = true)]
        public string browser { get; set; }

        [FieldSettings("Browser version")]
        public string browser_version { get; set; }

        [FieldSettings("Opertating system", DefaultField = true)]
        public string os { get; set; }

        [FieldSettings("Operation system version")]
        public string os_version { get; set; }

        [FieldSettings("Device category", DefaultField = true, EnumType = typeof(Device_Category))]
        public Device_Category dev_cat { get; set; }

        public GoogleAnalyticsUsers user { get; set; }

        [FieldSettings("Number of users", DefaultField = true)]
        public int num_of_users => user.num_of_users;
        
        [FieldSettings("Page views")]
        public int page_views => user.views;

        [FieldSettings("Number of sessions")]
        public int num_of_sessions => user.num_of_sessions;

        public GoogleAnalyticsTime time { get; set; }

        [FieldSettings("Date")]
        public string date => time.date;

        [FieldSettings("Year")]
        public string year => time.year;

        [FieldSettings("Month")]
        public string month => time.month;

        [FieldSettings("Week")]
        public string week => time.week;

        [FieldSettings("Day of month")]
        public string day_of_month => time.day_of_month;

        [FieldSettings("Hour")]
        public string hour => time.hour;

        [FieldSettings("Day_of_week")]
        public string day_of_week => time.day_of_week;

        public GoogleAnalyticsSystem system { get; set; }

        [FieldSettings("Language")]
        public string language => system.language;

        [FieldSettings("Flash version")]
        public string flashVersion => system.flashVersion;

        [FieldSettings("Java enabled")]
        public string javaEnabled => system.javaEnabled;

        [FieldSettings("Screen colors")]
        public string screenColors => system.screenColors;

        [FieldSettings("Screen resolution")]
        public string screenResolution => system.screenResolutions;
    }
}
