using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;
using System.Globalization;

namespace eOne.Common.Connectors.GoogleAnalytics.Models
{
    public class GoogleAnalyticsAdsense : DataConnectorEntityModel
    {
        #region dimensions
        [FieldSettings("Date", DefaultField = true)]
        public string date { get; set; }
        #endregion
        
        #region metrics
        [FieldSettings("Revenue", DefaultField = true)]
        public string adsenseRevenue { get; set; }

        [FieldSettings("Views")]
        public int adsenseAdsViewed { get; set; }

        [FieldSettings("Clicks")]
        public int adsenseAdsClicks { get; set; }

        [FieldSettings("Impressions")]
        public int adsensePageImpressions { get; set; }

        #endregion
        
        #region Calculations
        [FieldSettings("CTR")]
        public double ctr
        {
            get
            {
                if (adsensePageImpressions != 0) return adsenseAdsClicks / adsensePageImpressions;
                else return 0;
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
        public string day => date.Substring(6);

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
