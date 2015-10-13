using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.GoogleAnalytics.Models
{
    public class GoogleAnalyticsPageTracking : DataConnectorEntityModel
    {
        [FieldSettings("Host name")]
        public string hostName { get; set; }

        [FieldSettings("Path", DefaultField = true)]
        public string path { get; set; }

        [FieldSettings("Title", DefaultField = true)]
        public string title { get; set; }

        [FieldSettings("Depth")]
        public string depth { get; set; }

        [FieldSettings("Number of entrances")]
        public int entrances { get; set; }

        [FieldSettings("Page views", DefaultField = true)]
        public int views { get; set; }

        [FieldSettings("Unique page views", DefaultField = true)]
        public int uniqueViews { get; set; }

        [FieldSettings("Time on page", DefaultField = true)]
        public string time_on_page { get; set; }

        [FieldSettings("Number of exits")]
        public int num_of_exits { get; set; }

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
        

        #region Calculations
        [FieldSettings("Entrance rate")]
        public decimal entrance_rate {
            get {
                decimal rate = 0;

                if(views != 0) {
                    rate = (entrances / views) * 100;
                }
                return rate;
            }
        }

        [FieldSettings("Exit rate")]
        public decimal exit_rate {
            get {
                decimal rate = 0;

                if(views != 0) {
                    rate = (num_of_exits / views) * 100;
                }
                return rate;
            }
        }

        [FieldSettings("Average time on page")]
        public decimal avg_time_on_page
        {
            get
            {
                decimal rate = 0;
                DateTime time = DateTime.Parse(time_on_page);
                
                if (views != 0)
                {
                    rate = time.Second / views;
                }
                return rate;
            }
        }
        #endregion

    }
}
