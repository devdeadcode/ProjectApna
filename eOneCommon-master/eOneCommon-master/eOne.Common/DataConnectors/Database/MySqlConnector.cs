using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;

namespace eOne.Common.DataConnectors.Database
{
    public abstract class MySqlConnector : DatabaseConnector
    {
        
        protected MySqlConnector()
        {
            ConnectionStringFormat = "Server={0};Database={1};Uid={2};Pwd={3};";
            Syntax = new DatabaseConnectorSyntax
            {
                SelectQueryFormat = "SELECT {1} FROM {2} {3} {4} {0}",
                FieldFormat = "'{0}'.'{1}'",
                Alias = "{0} AS '{1}'",
                TableFormat = "'{0}'",
                TopN = "LIMIT {0}",
                Date = new DatabaseConnectorDateSyntax
                {
                    CurrentDate = "CURDATE()",
                    MakeDate = "DATE_ADD(DATE_ADD(MAKEDATE({0}, 1), INTERVAL {1}-1 MONTH), INTERVAL {2}-1 DAY)",
                    StartOfYear = "MAKEDATE(YEAR(NOW()), 1)",
                    EndOfYear = "MAKEDATE(YEAR(NOW()) + 1, 1) - INTERVAL 1 DAY",
                    StartOfMonth = "CURDATE() - INTERVAL (DAYOFMONTH(CURDATE()) - 1) DAY",
                    EndOfMonth = "LAST_DAY(CURDATE())",
                    StartOfWeek = "CURDATE() - INTERVAL (DAYOFWEEK(CURDATE()) - 1) DAY",
                    EndOfWeek = "CURDATE() + INTERVAL (7 - DAYOFWEEK(CURDATE())) DAY",
                    AddDays = "{0} + INTERVAL {1} DAY",
                    AddWeeks = "{0} + INTERVAL {1} WEEK",
                    AddMonths = "{0} + INTERVAL {1} MONTH",
                    AddYears = "{0} + INTERVAL {1} YEAR",
                    RemoveDays = "{0} - INTERVAL {1} DAY",
                    RemoveWeeks = "{0} - INTERVAL {1} WEEK",
                    RemoveMonths = "{0} - INTERVAL {1} MONTH",
                    RemoveYears = "{0} - INTERVAL {1} YEAR"
                },
                Where =
                {
                    ContainsOneOf = "{0} REGEXP {1}",
                    StartsWithOneOf = "{0} REGEXP {1}",
                    EndsWithOneOf = "{0} REGEXP {1}",
                    NotContainsOneOf = "{0} NOT REGEXP {1}",
                    NotStartsWithOneOf = "{0} NOT REGEXP {1}",
                    NotEndsWithOneOf = "{0} NOT REGEXP {1}"
                }
            };
        }

        public override string GetWhereItem(ConnectorRestriction restriction)
        {
            string field = FormatField(restriction.Field);
            switch (restriction.RestrictionType)
            {
                case ConnectorRestriction.ConnectorRestrictionType.ContainsOneOfList:
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotContainOneOfList:
                    return string.Format(GetWhereComparison(restriction.RestrictionType), field, string.Join("|", restriction.Values));
                case ConnectorRestriction.ConnectorRestrictionType.EndsWithOneOfList:
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotEndWithOneOfList:
                    var endsWithValues = restriction.Values.Select(value => $"{value}$").ToList();
                    return string.Format(GetWhereComparison(restriction.RestrictionType), field, string.Join("|", endsWithValues));
                case ConnectorRestriction.ConnectorRestrictionType.StartsWithOneOfList:
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotStartWithOneOfList:
                    var startsWithValues = restriction.Values.Select(value => $"^{value}").ToList();
                    return string.Format(GetWhereComparison(restriction.RestrictionType), field, string.Join("|", startsWithValues));
            }
            return base.GetWhereItem(restriction);
        }

        public override DataSet GetDataSet(string querySql)
        {
            string connectionString = GetConnectionString();
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (var adapter = new MySqlDataAdapter(querySql, connection))
                {
                    var data = new DataSet();
                    adapter.Fill(data);
                    connection.Close();
                    return data;
                }
            }
        }

    }
}
