using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace eOne.Common.Connectors.Database
{
    public abstract class SqlServerConnector: DatabaseConnector
    {
        
        protected SqlServerConnector()
        {
            ConnectionStringFormat = "Server={0};Database={1};User Id={2};Password={3}";
            Syntax.SelectQueryFormat = "select {0} {1} from {2} {3} {4}";
            Syntax.FieldFormat = "{0}.[{1}]";
            Syntax.Alias = "{0} as '{1}'";
            Syntax.TableFormat = "[{0}]";
            Syntax.ServerFormat = "[{0}]";
            Syntax.TopN = "top {0}";
            Syntax.Date = new DatabaseConnectorDateSyntax
            {
                CurrentDate = "cast(getdate()) as date",
                MakeDate = "dateadd(year, {0} - 1900, dateadd(month,  {1} - 1, {2} - 1))",
                StartOfYear = "dateadd(year, datediff(year, 0, getdate()), 0)",
                EndOfYear = "dateadd(year, datediff(year, 0, getdate()) + 1, - 1)",
                StartOfMonth = "dateadd(month, datediff(month, 0, getdate()), 0)",
                EndOfMonth = "dateadd(month, datediff(month, 0, getdate()) + 1, -1)",
                StartOfWeek = "dateadd(day, 1 - datepart(weekday, getdate()), cast(getdate() as date))",
                EndOfWeek = "dateadd(day, 7 - datepart(weekday, getdate()), cast(getdate() as date))",
                AddDays = "dateadd(day, {1}, {0})",
                AddWeeks = "dateadd(week, {1}, {0})",
                AddMonths = "dateadd(month, {1}, {0})",
                AddYears = "dateadd(year, {1}, {0})",
                RemoveDays = "dateadd(day, -{1}, {0})",
                RemoveWeeks = "dateadd(week, -{1}, {0})",
                RemoveMonths = "dateadd(month, -{1}, {0})",
                RemoveYears = "dateadd(year, -{1}, {0})"
            };
            Syntax.Join.Format = " {0} JOIN {1} WITH (NOLOCK) ON {2}";
            var stringFieldType = FindFieldType(FieldTypeIdString);
            stringFieldType.SqlFunction = "rtrim({0})";
        }

        #region Properties

        public int CommandTimeout = 1000;

        #endregion

        #region Methods

        public override DataSet GetDataSet(string querySql)
        {
            var connectionString = GetConnectionString();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var adapter = new SqlDataAdapter(querySql, connection))
                {
                    adapter.SelectCommand.CommandTimeout = CommandTimeout;
                    var data = new DataSet();
                    adapter.Fill(data);
                    connection.Close();
                    return data;
                }
            }
        }
        public override object GetValue(string querySql)
        {
            var connectionString = GetConnectionString();
            var connection = new SqlConnection(connectionString);
            var command = new SqlCommand
            {
                CommandText = querySql,
                CommandType = CommandType.Text,
                Connection = connection
            };
            connection.Open();
            var value = command.ExecuteScalar();
            connection.Close();
            return value;
        }
        public bool ObjectExists(string objectName, string objectType)
        {
            var objectCountSql = "select count(*) as ObjectCount from {0}..sysobjects where name = {1} and xtype = {2}";
            objectCountSql = string.Format(objectCountSql, Database, FormatString(objectName), FormatString(objectType));
            return (int)GetValue(objectCountSql) > 0;
        }
        public override List<string> GetDatabaseTableList(string database)
        {
            string tableListSql = $"select name from {database}..sysobjects where xtype in ('U', 'V')";
            var tableListData = GetDataSet(tableListSql);
            var tableList = (from DataRow table in tableListData.Tables[0].Rows select table["name"].ToString()).ToList();
            return tableList;
        }
        public override List<string> GetTableFieldList(string database, string table)
        {
            var fieldListSql = string.Format("select name from {0}..syscolumns where id in (select id from {0}..sysobjects where xtype in ('U', 'V') and name = '{1}')", database, table);
            var fieldListData = GetDataSet(fieldListSql);
            var fieldList = (from DataRow field in fieldListData.Tables[0].Rows select field["name"].ToString()).ToList();
            return fieldList;
        }

        public override string GetTableSql(ConnectorTable table)
        {
            return table.JoinToTable == null ? $"{FormatTable(table)} WITH (NOLOCK)" : base.GetTableSql(table);
        }

        #endregion

    }
}
