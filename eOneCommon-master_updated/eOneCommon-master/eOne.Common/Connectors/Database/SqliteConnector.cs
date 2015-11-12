using System;
using System.Data;

namespace eOne.Common.Connectors.Database
{
    abstract class SqliteConnector : DatabaseConnector
    {

        protected SqliteConnector()
        {
            ConnectionStringFormat = "";
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
                }
            };
        }

        public override DataSet GetDataSet(string querySql)
        {
            throw new NotImplementedException();
        }

    }
}
