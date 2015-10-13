using System;
using System.Data;

namespace eOne.Common.DataConnectors.Database
{
    abstract class PostgreSqlConnector : DatabaseConnector
    {

        protected PostgreSqlConnector()
        {
            ConnectionStringFormat = "";
            Syntax = new DatabaseConnectorSyntax
            {
                SelectQueryFormat = "select {1} from {2} {3} {4} {0};",
                FieldFormat = "\"{0}\".\"{1}\"",
                Alias = "{0} as \"{1}\"",
                TableFormat = "\"{0}\"",
                TopN = "limit {0}",
                Date = new DatabaseConnectorDateSyntax
                {
                    CurrentDate = "current_date",
                    MakeDate = "{0} * interval '1' year + {1} * interval '1' month + {2} * interval '1' day",
                    StartOfYear = "date_trunc('year', now())",
                    EndOfYear = "date_trunc('year', now()) + interval '1 year - 1 day'",
                    StartOfMonth = "date_trunc('month', now())",
                    EndOfMonth = "date_trunc('month', now()) + interval '1 month - 1 day'",
                    StartOfWeek = "date_trunc('week', now())",
                    EndOfWeek = "date_trunc('week', now()) + interval '1 week - 1 day'",
                    AddDays = "{0} + interval '{1} day'",
                    AddWeeks = "{0} + interval '{1} week'",
                    AddMonths = "{0} + interval '{1} month'",
                    AddYears = "{0} + interval '{1} year'",
                    RemoveDays = "{0} - interval '{1} day'",
                    RemoveWeeks = "{0} - interval '{1} week'",
                    RemoveMonths = "{0} - interval '{1} month'",
                    RemoveYears = "{0} - interval '{1} year'"
                }
            };
        }

        public override DataSet GetDataSet(string querySql)
        {
            throw new NotImplementedException();
        }

    }
}
