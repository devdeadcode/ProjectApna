using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;
using System.Globalization;

namespace eOne.Common.Connectors.GoogleAnalytics.Models
{
    public class GoogleAnalyticsUsers : DataConnectorEntityModel
    {

        #region Dimensions
        [FieldSettings("Date", DefaultField = true)]
        public string date { get; set; }

        [FieldSettings("User type", DefaultField = true)]
        public string userType { get; set; }

        [FieldSettings("Gender", DefaultField = true)]
        public string userGender { get; set; }

        [FieldSettings("Age bracket", DefaultField = true)]
        public string userAgebracket { get; set; }
        #endregion

        #region Metrics
        [FieldSettings("Number of users", DefaultField = true)]
        public int users { get; set; }

        [FieldSettings("Page views", DefaultField = true)]
        public int pageviews { get; set; }

        [FieldSettings("Number of new users", DefaultField = true)]
        public int newUsers { get; set; }

        public int mr() { return 1; }
        #endregion
        
        #region Hidden Properties
        public GoogleAnalyticsTime time { get; set; }

        public GoogleAnalyticsAudience audience { get; set; }

        public GoogleAnalyticsPageTracking page { get; set; }

        public int sessions { get; set; }

        public List<List<string>> rows { get; set; }
        #endregion
        
        #region Calculations
        [FieldSettings("Year", DefaultField = true)]
        public string year
        {
            get
            {
                try { return date.Substring(0, 4); }
                catch { return null; }
            }
            set { }
        }


        [FieldSettings("Month", DefaultField = true)]
        public string month
        {
            get
            {
                try
                {
                    DateTime theTime = DateTime.ParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture);
                    return theTime.ToString("MMMM");
                }
                catch
                {
                    return null;
                }

            }
        }

        [FieldSettings("Week", DefaultField = true)]
        public int week
        {
            get
            {
                try
                {
                    DateTime theTime = DateTime.ParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture);
                    DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(theTime);
                    if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday) { theTime = theTime.AddDays(3); }
                    return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(theTime, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                }
                catch
                {
                    return 0;
                }
            }
            set { }
        }

        [FieldSettings("Day of month", DefaultField = true)]
        public string day
        {
            get
            {
                try { return date.Substring(6); }
                catch { return null; }
            }
            set { }
        }


        [FieldSettings("Day of week", DefaultField = true)]
        public string dayOfWeek
        {
            get
            {
                try
                {
                    DateTime theTime = DateTime.ParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture);
                    return theTime.ToString("dddd");
                }
                catch
                {
                    return null;
                }
            }
            set { }
        }

        [FieldSettings("Percentage of new users", DefaultField = true)]
        public decimal per_of_new_users
        {
            get
            {
                try{return Decimal.Divide(newUsers, users) * 100;}
                catch{return 0;}
            }
            set { }
        }

        [FieldSettings("Page views per user", DefaultField = true)]
        public decimal page_views_per_user
        {
            get
            {
                try{return Decimal.Divide(pageviews, users);}
                catch{return 0;}
            }
            set { }
        }

        [FieldSettings("Number of returning users", DefaultField = true)]
        public int num_of_returning_users { get { return users - newUsers; } }
        #endregion

        //[FieldSettings("Hour")]
        //public string hour => time.hour;









    }
}
