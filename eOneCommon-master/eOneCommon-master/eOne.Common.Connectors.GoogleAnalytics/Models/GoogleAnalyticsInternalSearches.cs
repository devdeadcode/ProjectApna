using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.GoogleAnalytics.Models
{
    public class GoogleAnalyticsInternalSearches : DataConnectorEntityModel
    {
        [FieldSettings("Keyword", DefaultField = true)]
        public string keyword { get; set; }

        [FieldSettings("Refinements")]
        public string refinements { get; set; }

        [FieldSettings("Category")]
        public string category { get; set; }

        [FieldSettings("Start page")]
        public string string_Page { get; set; }

        [FieldSettings("Destination page")]
        public string destinationPage { get; set; }

        [FieldSettings("Number of searches", DefaultField = true)]
        public int num_of_searches { get; set; }

        [FieldSettings("Number of unique searches")]
        public int num_of_unique_searches { get; set; }

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

        [FieldSettings("Percentage of sessions with search")]
        public decimal per_of_sessions_with_search { get; set; }

        [FieldSettings("Time after search")]
        public int MyProperty { get; set; }

        [FieldSettings("Search exit rate")]
        public decimal search_exit_rate { get; set; }

        [FieldSettings("Number of refinements")]
        public int num_of_refinements { get; set; }
    }
}
