using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.GoogleAnalytics.Models
{
    public class GoogleAnalyticsUsers : DataConnectorEntityModel
    {
        public enum User_Type
        {
            [Description("New")]
            New,
            [Description("Returning")]
            Returning
        }

        public GoogleAnalyticsTime time { get; set; }

        [FieldSettings("Date", DefaultField = true)]
        public string date => time.date;

        [FieldSettings("User type", EnumType = typeof(User_Type))]
        public User_Type user_type { get; set; }

        [FieldSettings("Number of users", DefaultField = true)]
        public int num_of_users { get; set; }

        [FieldSettings("Number of new users")]
        public int num_of_new_users { get; set; }

        public GoogleAnalyticsPageTracking page { get; set; }

        [FieldSettings("Page views", DefaultField = true)]
        public int views => page.views;

        [FieldSettings("Number of sessions")]
        public int num_of_sessions { get; set; }

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

        public GoogleAnalyticsAudience audience { get; set; }

        [FieldSettings("Gender")]
        public string gender => audience.gender;

        [FieldSettings("Age bracket")]
        public string age_bracket => audience.age_bracket;

        #region Calculations
        [FieldSettings("Percentage of new users")]
        public decimal per_of_new_users {
            get {
                decimal percentage = 0;
                if(num_of_users != 0) {
                    percentage =  (num_of_new_users / num_of_users) * 100;
                }
                return percentage;
            }
        }

        [FieldSettings("Page views per user")]
        public decimal page_views_per_user {
            get {
                decimal views_per_user = 0;
                if(num_of_users != 0) {
                    views_per_user =  views / num_of_users;
                }
                return views_per_user;
            }
        }

        [FieldSettings("Number of returning users")]
        public int num_of_returning_users { get { return num_of_users - num_of_new_users; } }
        #endregion

    }
}
