using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;
using System.Globalization;

namespace eOne.Common.Connectors.GoogleAnalytics.Models
{
    public class GoogleAnalyticsPageTracking : DataConnectorEntityModel
    {
        #region Dimensions
        [FieldSettings("Path", DefaultField = true)]
        public string pagePath { get; set; }

        [FieldSettings("Title", DefaultField = true)]
        public string pageTitle { get; set; }

        [FieldSettings("Date")]
        public string date { get; set; }
        #endregion
        
        #region Metrics
        [FieldSettings("Page views", DefaultField = true)]
        public int pageviews { get; set; }

        [FieldSettings("Unique page views")]
        public int uniquePageviews { get; set; }

        [FieldSettings("Time on page")]
        public string timeOnPage { get; set; }
        #endregion
        
        #region Calculations
        [FieldSettings("Average time on page")]
        public decimal avg_time_on_page
        {
            get
            {
                decimal rate = 0;
                DateTime time = DateTime.Parse(timeOnPage);
                
                if (pageviews != 0)
                {
                    rate = time.Second / pageviews;
                }
                return rate;
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

        //[FieldSettings("Entrance rate")]
        //public decimal entrance_rate {
        //    get {
        //        decimal rate = 0;

        //        if(pageviews != 0) {
        //            rate = (entrances / pageviews) * 100;
        //        }
        //        return rate;
        //    }
        //}

        //[FieldSettings("Exit rate")]
        //public decimal exit_rate {
        //    get {
        //        decimal rate = 0;

        //        if(pageviews != 0) {
        //            rate = (exits / pageviews) * 100;
        //        }
        //        return rate;
        //    }
        //}
    }
}
