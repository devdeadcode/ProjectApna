using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.GoogleAnalytics.Helpers
{
    class GoogleAnalyticsHelper
    {
        public static void AddUsersFavorites(DataConnectorEntity entity)
        {
            // add New vs returning users favorite
            var newvsreturing = entity.AddFavorite("New vs returning users");
            newvsreturing.Query.AddFields("date", "newUsers", "num_of_returning_users", "users", "per_of_new_users");

            //add Today's users favorite
            var todaysusers = entity.AddFavorite("Today's users");
            todaysusers.Query.AddFields("users", "sessions", "views");
            todaysusers.Query.AddRestriction("date", ConnectorRestriction.ConnectorRestrictionType.Equals, "Today"); //double check

            //add This week's users
            var thisweeksusers = entity.AddFavorite("This week's users");
            thisweeksusers.Query.AddFields("day_of_week", "num_of_users", "num_of_sessions", "views");
            thisweeksusers.Query.AddRestriction("date", ConnectorRestriction.ConnectorRestrictionType.GreaterThanOrEqualTo, "Sunday"); //double check
            thisweeksusers.Query.AddRestriction("date", ConnectorRestriction.ConnectorRestrictionType.LessThanOrEqualTo, "Saturday"); //double check

            //add This month's users
            var thismonthsusers = entity.AddFavorite("This month's users");
            thismonthsusers.Query.AddFields("day", "users", "views");
            thismonthsusers.Query.AddRestriction("date", ConnectorRestriction.ConnectorRestrictionType.GreaterThanOrEqualTo, "January"); //double check
            thismonthsusers.Query.AddRestriction("date", ConnectorRestriction.ConnectorRestrictionType.LessThanOrEqualTo, "December"); //double check
        }

        public static void AddBrowserFavorites(DataConnectorEntity entity)
        {
            //add Mobile browser favorite
            var mobile = entity.AddFavorite("Mobile");
            mobile.Query.AddFields("browser", "operatingSystem", "users");
            mobile.Query.AddRestriction("deviceCategory", ConnectorRestriction.ConnectorRestrictionType.Equals, "Mobile");

            //add Tablet browser favorite
            var tablet = entity.AddFavorite("Mobile");
            tablet.Query.AddFields("browser", "operatingSystem", "users");
            tablet.Query.AddRestriction("deviceCategory", ConnectorRestriction.ConnectorRestrictionType.Equals, "Tablet");

            //add desktop browser favorite
            var desktop = entity.AddFavorite("Mobile");
            desktop.Query.AddFields("browser", "operatingSystem", "users");
            desktop.Query.AddRestriction("deviceCategory", ConnectorRestriction.ConnectorRestrictionType.Equals, "Desktop");
        }

        public static void AddSessionfavorites(DataConnectorEntity entity)
        {
            // add This week's sessions favorite
            var thisweekssessions = entity.AddFavorite("This week's sessions");
            thisweekssessions.Query.AddFields("day_of_week", "num_of_sessions", "avg_session_durations");

            //add This month's sessions favorite
            var thismonthssessions = entity.AddFavorite("This month's sessions");
            thismonthssessions.Query.AddFields("day_of_month", "num_of_sessions", "avg_session_duration");
        }

        public static void AddPagesFavorites(DataConnectorEntity entity)
        {
            // add Landing pages favorite
            var landingpages = entity.AddFavorite("Landing pages");
            landingpages.Query.AddFields("path", "title", "views", "entrances", "entrance_rate");
            landingpages.Query.AddRestriction("entrances", ConnectorRestriction.ConnectorRestrictionType.GreaterThan, 0);

            // add Exit pages favorite
            var exitpages = entity.AddFavorite("Exit pages");
            exitpages.Query.AddFields("path", "views", "num_of_exits", "exit_rate");
            exitpages.Query.AddRestriction("num_of_exits", ConnectorRestriction.ConnectorRestrictionType.GreaterThan, 0);
        }

    }
}
