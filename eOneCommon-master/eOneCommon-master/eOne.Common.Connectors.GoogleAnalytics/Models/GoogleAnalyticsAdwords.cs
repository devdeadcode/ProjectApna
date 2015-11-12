using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;
using System.Globalization;

namespace eOne.Common.Connectors.GoogleAnalytics.Models
{
    public class GoogleAnalyticsAdwords : DataConnectorEntityModel
    {
        public enum Format
        {
            [Description("Text")]
            Text,
            [Description("Image")]
            Image,
            [Description("Flash")]
            Flash,
            [Description("Video")]
            Video
        }

        #region Dimensions
        [FieldSettings("Search query", DefaultField = true)]
        public string adMatchedQuery { get; set; }

        [FieldSettings("Network")]
        public string adDistributionNetwork { get; set; }

        [FieldSettings("Date")]
        public string date { get; set; }
        #endregion

        #region Metrics
        [FieldSettings("Impressions", DefaultField = true)]
        public int impressions { get; set; }

        [FieldSettings("Clicks", DefaultField = true)]
        public int adClicks { get; set; }

        [FieldSettings("Cost", DefaultField = true)]
        public decimal adCost { get; set; }
        
        #endregion

        //[FieldSettings("Format", EnumType = typeof(Format))]
        //public Format adFormat { get; set; }


        #region Hidden Calculations
        public GoogleAnalyticsTime time { get; set; }

        public List<List<string>> rows { get; set; }
        #endregion

        #region Calculations
        [FieldSettings("Coust per thousand impressions")]
        public decimal cost_per_1000_imps {
            get {
                decimal costPer1000 = 0;
                if(impressions != 0) {
                    costPer1000 = adCost / (impressions * 1000);
                }
                return costPer1000;
            }
        }

        [FieldSettings("Cost pers click")]
        public decimal cost_per_click {
            get {
                decimal costPerClick = 0;
                if(adClicks != 0) {
                    costPerClick =  adCost / adClicks;
                }
                return costPerClick;
            }
        }

        [FieldSettings("Click through rate")]
        public decimal click_through_rate {
            get {
                decimal clickThroughRate = 0;
                if(impressions != 0) {
                    clickThroughRate =  adClicks / impressions;
                }
                return clickThroughRate;
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
