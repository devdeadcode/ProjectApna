using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;
using System.Globalization;

namespace eOne.Common.Connectors.GoogleAnalytics.Models
{
    class GoogleAnalyticsSessions : DataConnectorEntityModel
    {
        #region Dimensions
        [FieldSettings("Date", DefaultField = true)]
        public string date { set; get; }
        #endregion

        #region Metrics
        [FieldSettings("Number of sessions", DefaultField = true)]
        public int sessions { get; set; }

        [FieldSettings("Duration")]
        public string sessionDuration { get; set; }

        [FieldSettings("Bounces")]
        public int bounces { get; set; }

        [FieldSettings("Hits")]
        public int hits { get; set; }
        #endregion
        
        #region Calculations
        [FieldSettings("Bounce rate")]
        public decimal bounce_rate {
            get {
                decimal rate = 0;
                if(sessions != 0) {
                    rate = bounces / sessions;
                }
                return rate;
            }
        }

        [FieldSettings("Average session duration", DefaultField = true )]
        public decimal avg_session_duration {
            get {
                decimal average = 0;
                if(sessions != 0) {
                    average = Convert.ToDecimal(sessionDuration) / sessions;
                }
                return average;
            }
        }

        [FieldSettings("Year")]
        public string year => date.Substring(0, 4);

        [FieldSettings("Month")]
        public string month
        {
            get
            {
                DateTime theTime = DateTime.ParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture);
                return theTime.ToString("MMMM");
            }
        }

        [FieldSettings("Week")]
        public int week
        {
            get
            {
                DateTime theTime = DateTime.ParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture);
                DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(theTime);
                if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday) { theTime = theTime.AddDays(3); }
                return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(theTime, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            }
            set { }
        }

        [FieldSettings("Day of month")]
        public string day => date.Substring(6, 8);

        [FieldSettings("Day of week")]
        public string dayOfWeek
        {
            get
            {
                DateTime theTime = DateTime.ParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture);
                return theTime.ToString("dddd");
            }
        }
        #endregion
    }
}
