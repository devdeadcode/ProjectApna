using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.GoogleAnalytics.Models
{
    class GoogleAnalyticsSessions : DataConnectorEntityModel
    {
        [FieldSettings("Duration")]
        public string duration { get; set; }

        public GoogleAnalyticsUsers user { get; set; }

        [FieldSettings("Number of sessions", DefaultField = true)]
        public int num_of_sessions => user.num_of_sessions;

        [FieldSettings("Bounces")]
        public int bounces { get; set; }

        [FieldSettings("Hits")]
        public int hits { get; set; }

        public GoogleAnalyticsTime time { get; set; }

        [FieldSettings("Date", DefaultField = true)]
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

        #region Calculations
        [FieldSettings("Bounce rate")]
        public decimal bounce_rate {
            get {
                decimal rate = 0;
                if(num_of_sessions != 0) {
                    rate = bounces / num_of_sessions;
                }
                return rate;
            }
        }

        [FieldSettings("Average session duration")]
        public decimal avg_session_duration {
            get {
                decimal average = 0;
                if(num_of_sessions != 0) {
                    average = Convert.ToInt16(duration) / num_of_sessions;
                }
                return average;
            }
        }
        #endregion
    }
}
