using System.ComponentModel;
using System.Linq;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.GoogleAnalytics.Models
{
    class GoogleAnalyticsMobileDevices : DataConnectorEntityModel
    {
        [FieldSettings("Brand", DefaultField = true)]
        public string brand { get; set; }

        [FieldSettings("Model", DefaultField = true)]
        public string model { get; set; }

        [FieldSettings("Name", DefaultField = true)]
        public string name { get; set; }

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
