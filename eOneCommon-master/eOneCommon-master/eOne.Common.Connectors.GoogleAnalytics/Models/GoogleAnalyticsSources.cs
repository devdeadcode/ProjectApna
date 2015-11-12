using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;
using System.Globalization;

namespace eOne.Common.Connectors.GoogleAnalytics.Models
{
    public class GoogleAnalyticsSources : DataConnectorEntityModel
    {
        #region Dimensions
        [FieldSettings("Source", DefaultField = true)]
        public string source { get; set; }

        [FieldSettings("Social network")]
        public string socialNetwork { get; set; }

        [FieldSettings("Campaign")]
        public string campaign { get; set; }

        [FieldSettings("Medium", DefaultField = true)]
        public string medium { get; set; }

        [FieldSettings("Date")]
        public string date { get; set; }
        #endregion

        #region Metrics
        [FieldSettings("Number of users", DefaultField = true)]
        public int users { get; set; }
        #endregion
        
        #region Calculations
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
