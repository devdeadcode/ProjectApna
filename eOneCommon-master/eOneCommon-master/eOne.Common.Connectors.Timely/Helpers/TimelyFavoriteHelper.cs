using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Timely.Helpers
{
    public class TimelyFavoriteHelper
    {

        public static void AddProjectFavorites(DataConnectorEntity entity)
        {
            // add active projects favorite
            var activeProjectsFavorite = entity.AddFavorite("Active projects");
            activeProjectsFavorite.Query.AddFields("name", "client_name");
            activeProjectsFavorite.Query.AddRestriction("active", ConnectorRestriction.ConnectorRestrictionType.Equals, "true");

            // add over budget projects favorite
            var overBudgetProjectsFavorite = entity.AddFavorite("Projects over budget");
            overBudgetProjectsFavorite.Query.AddFields("name", "client_name", "budget", "budget_type", "total_logged_cost", "total_logged_time");
            // add subquery to check for over time budget
            var overTimeBudgetRestriction = new ConnectorRestriction
            {
                ConjunctiveOperator = ConnectorRestriction.ConnectorRestrictionConjunctiveOperator.None
            };
            var overTimeBudgetSubquery = overTimeBudgetRestriction.Subquery = new ConnectorQuery();
            overTimeBudgetSubquery.Entity = entity;
            overTimeBudgetSubquery.AddRestriction("budget_type", ConnectorRestriction.ConnectorRestrictionType.Equals, "H");
            var timeBudgetComparisonRestriction = new ConnectorRestriction
            {
                Field = entity.FindField("total_logged_time"),
                ConjunctiveOperator = ConnectorRestriction.ConnectorRestrictionConjunctiveOperator.And,
                RestrictionType = ConnectorRestriction.ConnectorRestrictionType.GreaterThan
            };
            timeBudgetComparisonRestriction.Values.Add(new ConnectorRestrictionValue(entity.FindField("budget")));
            overTimeBudgetSubquery.Restrictions.Add(timeBudgetComparisonRestriction);
            // add subquery to check for over money budget
            var overMoneyBudgetRestriction = new ConnectorRestriction
            {
                ConjunctiveOperator = ConnectorRestriction.ConnectorRestrictionConjunctiveOperator.Or
            };
            var overMoneyBudgetSubquery = overTimeBudgetRestriction.Subquery = new ConnectorQuery();
            overMoneyBudgetSubquery.Entity = entity;
            overMoneyBudgetSubquery.AddRestriction("budget_type", ConnectorRestriction.ConnectorRestrictionType.Equals, "M");
            var moneyBudgetComparisonRestriction = new ConnectorRestriction
            {
                Field = entity.FindField("total_logged_cost"),
                ConjunctiveOperator = ConnectorRestriction.ConnectorRestrictionConjunctiveOperator.And,
                RestrictionType = ConnectorRestriction.ConnectorRestrictionType.GreaterThan
            };
            moneyBudgetComparisonRestriction.Values.Add(new ConnectorRestrictionValue(entity.FindField("budget")));
            overMoneyBudgetSubquery.Restrictions.Add(moneyBudgetComparisonRestriction);
            // add subqueries as restrictions
            overBudgetProjectsFavorite.Query.Restrictions.Add(overTimeBudgetRestriction);
            overBudgetProjectsFavorite.Query.Restrictions.Add(overMoneyBudgetRestriction);
        }

        public static void AddEventFavorites(DataConnectorEntity entity)
        {
            // add today's events favorite
            var todaysEventsFavorite = entity.AddFavorite("Today's events");
            todaysEventsFavorite.Query.AddFields("project_name", "client_name, user_name, estimated_duration_time");
            todaysEventsFavorite.Query.AddRestriction("day", ConnectorRestriction.ConnectorRestrictionType.Equals, ConnectorRestrictionValue.ConnectorRestrictionDateValueType.Today);

            // add this week's events favorite
            var thisWeeksEventsFavorite = entity.AddFavorite("This week's events");
            thisWeeksEventsFavorite.Query.AddFields("project_name", "client_name, user_name, day, estimated_duration_time");
            thisWeeksEventsFavorite.Query.AddRestriction("day", ConnectorRestriction.ConnectorRestrictionType.GreaterThanOrEqualTo, ConnectorRestrictionValue.ConnectorRestrictionDateValueType.StartOfWeek);
            thisWeeksEventsFavorite.Query.AddRestriction("day", ConnectorRestriction.ConnectorRestrictionType.LessThanOrEqualTo, ConnectorRestrictionValue.ConnectorRestrictionDateValueType.EndOfWeek);

            // add this month's events favorite
            var thisMonthsEventsFavorite = entity.AddFavorite("This month's events");
            thisMonthsEventsFavorite.Query.AddFields("project_name", "client_name, user_name, day, estimated_duration_time");
            thisMonthsEventsFavorite.Query.AddRestriction("day", ConnectorRestriction.ConnectorRestrictionType.GreaterThanOrEqualTo, ConnectorRestrictionValue.ConnectorRestrictionDateValueType.StartOfMonth);
            thisMonthsEventsFavorite.Query.AddRestriction("day", ConnectorRestriction.ConnectorRestrictionType.LessThanOrEqualTo, ConnectorRestrictionValue.ConnectorRestrictionDateValueType.EndOfMonth);
        }

    }
}