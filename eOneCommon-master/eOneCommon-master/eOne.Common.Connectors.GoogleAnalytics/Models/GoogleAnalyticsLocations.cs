using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.GoogleAnalytics.Models
{
    class GoogleAnalyticsLocations : DataConnectorEntityModel
    {
        [FieldSettings("Country", DefaultField = true)]
        public string country { get; set; }

        [FieldSettings("Continent")]
        public string continent { get; set; }

        [FieldSettings("Sub continent")]
        public string subcontinent { get; set; }

        [FieldSettings("Region", DefaultField = true)]
        public string region { get; set; }

        [FieldSettings("Metro")]
        public string metro { get; set; }

        [FieldSettings("City", DefaultField = true)]
        public string city { get; set; }

        [FieldSettings("Latitude")]
        public string latitude { get; set; }

        [FieldSettings("Longtitude")]
        public string longtitude { get; set; }

        public GoogleAnalyticsUsers user { get; set; }

        [FieldSettings("Number of users", DefaultField = true)]
        public int num_of_users => user.num_of_users;

        [FieldSettings("Number of sessions")]
        public int num_of_sessions => user.num_of_sessions;

        [FieldSettings("Page views")]
        public int views => user.views;

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
