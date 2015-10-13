using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.GoogleAnalytics.Models
{
    public class GoogleAnalyticsKeywords : DataConnectorEntityModel
    {
        [FieldSettings("Keyword", DefaultField = true)]
        public string keyword { get; set; }

        [FieldSettings("Number of searches", DefaultField = true)]
        public int num_of_numbers { get; set; }

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
    }
}
