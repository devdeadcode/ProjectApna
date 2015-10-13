using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Newtonsoft.Json;

namespace eOne.Common.DataConnectors.Database
{
    public abstract class DatabaseConnector : DataConnector
    {

        #region Classes

        public class DatabaseConnectorSyntax
        {
            public string SelectQueryFormat { get; set; }
            public string FieldFormat { get; set; }
            public string Alias { get; set; }
            public string TableFormat { get; set; }
            public string ServerFormat { get; set; }
            public string TopN { get; set; }
            public string FieldSeparator { get; set; }
            public DatabaseConnectorOrderBySyntax OrderBy { get; set; }
            public DatabaseConnectorJoinSyntax Join { get; set; }
            public DatabaseConnectorWhereSyntax Where { get; set; }
            public DatabaseConnectorDateSyntax Date { get; set; }
            public DatabaseConnectorListSyntax List { get; set; }
        }
        public class DatabaseConnectorOrderBySyntax
        {
            public string Format { get; set; }
            public string ItemFormat { get; set; }
            public string Ascending { get; set; }
            public string Descending { get; set; }
        }
        public class DatabaseConnectorJoinSyntax
        {
            public string Format { get; set; }
            public string Equality { get; set; }
            public string And { get; set; }
            public string Left { get; set; }
            public string Inner { get; set; }
        }
        public class DatabaseConnectorWhereSyntax
        {
            public string Format { get; set; }
            public string And { get; set; }
            public string Or { get; set; }
            public string Wildcard { get; set; }
            public string ValueSeparator { get; set; }
            public string IsEqual { get; set; }
            public string DoeNotEqual { get; set; }
            public string GreaterThan { get; set; }
            public string LessThan { get; set; }
            public string GreaterThanEquals { get; set; }
            public string LessThanEquals { get; set; }
            public string Between { get; set; }
            public string OneOf { get; set; }
            public string NotBetween { get; set; }
            public string NotOneOf { get; set; }
            public string Contains { get; set; }
            public string StartsWith { get; set; }
            public string EndsWith { get; set; }
            public string NotContains { get; set; }
            public string NotStartsWith { get; set; }
            public string NotEndsWith { get; set; }
            public string ContainsOneOf { get; set; }
            public string StartsWithOneOf { get; set; }
            public string EndsWithOneOf { get; set; }
            public string NotContainsOneOf { get; set; }
            public string NotStartsWithOneOf { get; set; }
            public string NotEndsWithOneOf { get; set; }
        }
        public class DatabaseConnectorDateSyntax
        {
            public string CurrentDate { get; set; }
            public string MakeDate { get; set; }
            public string StartOfYear { get; set; }
            public string EndOfYear { get; set; }
            public string StartOfMonth { get; set; }
            public string EndOfMonth { get; set; }
            public string StartOfWeek { get; set; }
            public string EndOfWeek { get; set; }
            public string AddDays { get; set; }
            public string RemoveDays { get; set; }
            public string AddWeeks { get; set; }
            public string RemoveWeeks { get; set; }
            public string AddMonths { get; set; }
            public string RemoveMonths { get; set; }
            public string AddYears { get; set; }
            public string RemoveYears { get; set; }
            public string Day { get; set; }
            public string Month { get; set; }
            public string Year { get; set; }
        }
        public class DatabaseConnectorListSyntax
        {
            public string Item { get; set; }
            public string Case { get; set; }
        }

        #endregion

        protected DatabaseConnector()
        {
            Syntax = new DatabaseConnectorSyntax
            {
                FieldSeparator = ",",
                Join = new DatabaseConnectorJoinSyntax
                {
                    Format = " {0} JOIN {1} ON {2}",
                    And = " AND ",
                    Equality = "{0} = {1}",
                    Inner = "INNER",
                    Left = "LEFT"
                },
                OrderBy = new DatabaseConnectorOrderBySyntax
                {
                    Format = "ORDER by {0}",
                    ItemFormat = "{0} {1}",
                    Ascending = "ASC",
                    Descending = "DESC"
                },
                Where = new DatabaseConnectorWhereSyntax
                {
                    Format = " WHERE {0}",
                    And = " AND ",
                    Or = " OR ",
                    Wildcard = "%",
                    ValueSeparator = ",",
                    IsEqual = "{0} = {1}",
                    DoeNotEqual = "{0} <> {1}",
                    GreaterThan = "{0} > {1}",
                    LessThan = "{0} < {1}",
                    GreaterThanEquals = "{0} >= {1}",
                    LessThanEquals = "{0} <= {1}",
                    Between = "{0} BETWEEN {1} and {2}",
                    OneOf = "{0} IN ({1})",
                    NotBetween = "{0} NOT BETWEEN {1} and {2}",
                    NotOneOf = "{0} NOT IN ({1})",
                    Contains = "{0} LIKE '%{1}%'",
                    StartsWith = "{0} LIKE '{1}%'",
                    EndsWith = "{0} LIKE '{1}%'",
                    NotContains = "{0} NOT LIKE '%{1}%'",
                    NotStartsWith = "{0} NOT LIKE '{1}%'",
                    NotEndsWith = "{0} NOT LIKE '{1}%'"
                },
                List = new DatabaseConnectorListSyntax
                {
                    Item = "when {0} then {1}",
                    Case = "case {0} {1} end"
                }
            };
        }

        #region Properties

        public string Server { get; set; }
        public string Database { get; set; }
        public int Port { get; set; }
        public DatabaseConnectorSyntax Syntax { get; set; }
        public string ConnectionStringFormat { get; set; }
        public string CustomConnectionString { get; set; }

        #endregion

        #region Methods

        public override string GetData(string queryXml, DataConnectorSerializationType type)
        {
            var dataSet = GetDataSet(GetQuerySql(queryXml));
            if (dataSet == null) return string.Empty;
            switch (type)
            {
                case DataConnectorSerializationType.Json:
                    return JsonConvert.SerializeObject(dataSet);
                case DataConnectorSerializationType.Xml:
                    return dataSet.GetXml();
            }
            return string.Empty;
        }
        public override IEnumerable<object> GetRecords(ConnectorQuery query)
        {
            return null;
        }
        public abstract DataSet GetDataSet(string querySql);
        public abstract object GetValue(string querySql);
        public virtual string GetConnectionString()
        {
            if (!string.IsNullOrWhiteSpace(CustomConnectionString)) return CustomConnectionString;
            return string.IsNullOrWhiteSpace(ConnectionStringFormat) ? string.Empty : string.Format(ConnectionStringFormat, Server, Database, Username, Password);
        }
        public string GetQuerySql(string queryXml)
        {
            var query = new ConnectorQuery(this, queryXml);
            return GetQuerySql(query);
        }
        public virtual string GetQuerySql(ConnectorQuery query)
        {
            string topN = GetTopN(query.MaxRecords);
            string fieldList = GetFieldList(query);
            string tables = GetTables(query);
            string whereClause = GetWhereClause(query);
            string orderByClause = GetOrderByClause(query);
            string querySql = string.Format(Syntax.SelectQueryFormat, topN, fieldList, tables, whereClause, orderByClause);
            if (query.Companies.Count <= 0) return string.Format(querySql, Database);
            if (query.Companies.Count == 1) return string.Format(querySql, query.Companies[0].DatabaseName);
            var companySql = query.Companies.Select(company => string.Format(querySql, company.DatabaseName)).ToList();
            return $"select * from ({string.Join(" union all ", companySql)})";
        }
        public virtual string GetTopN(int maxRecords)
        {
            return (maxRecords > 0) ? string.Format(Syntax.TopN, maxRecords) : "";
        }
        public virtual string GetFieldList(ConnectorQuery query)
        {
            var fieldList = query.Fields.Select(field => FormatField(field, true)).ToList();
            return string.Join(Syntax.FieldSeparator, fieldList);
        }
        public virtual string GetTables(ConnectorQuery query)
        {
            return query.Entity.Tables.Aggregate("", (current, table) => current + GetTableSql(table));
        }
        public virtual string GetTableSql(DataConnectorTable table)
        {
            if (table.JoinToTable == null) return FormatTable(table);
            var joins = table.JoinFields.Select(field => string.Format(Syntax.Join.Equality, FormatField(field.Item1, FormatTable(table)), FormatField(field.Item2, FormatTable(table.JoinToTable)))).ToList();
            return string.Format(Syntax.Join.Format, GetJoinType(table.JoinType), FormatTable(table), string.Join(Syntax.Join.And, joins));
        }
        public virtual string GetJoinType(DataConnectorTable.DataConnectorTableJoinType type)
        {
            switch (type)
            {
                case DataConnectorTable.DataConnectorTableJoinType.Inner:
                    return Syntax.Join.Inner;
                case DataConnectorTable.DataConnectorTableJoinType.Left:
                    return Syntax.Join.Left;
            }
            return string.Empty;
        }
        public virtual string FormatTable(DataConnectorTable table)
        {
            string tableSql = string.Format(Syntax.TableFormat, table.Name);
            if (!string.IsNullOrWhiteSpace(table.Database)) return tableSql;
            string serverSql = string.Format(Syntax.ServerFormat, string.IsNullOrWhiteSpace(table.Database) ? "{0}" : table.Database);
            return $"{serverSql}..{tableSql}";
        }
        public virtual string FormatField(DataConnectorField field, bool useAlias = false)
        {
            string fieldSql;
            if (!string.IsNullOrWhiteSpace(field.Calculation))
            {
                fieldSql = field.Calculation;
            }
            else
            {
                fieldSql = FormatField(field.Name, field.Table);
                if (!string.IsNullOrWhiteSpace(field.Type?.SqlFunction)) fieldSql = string.Format(field.Type.SqlFunction, fieldSql, "{0}");
            }
            if (field.ListItems.Count > 0) fieldSql = GetListSql(fieldSql, field);
            return useAlias ? string.Format(Syntax.Alias, fieldSql, field.DisplayName) : fieldSql; 
        }
        public virtual string FormatField(string field, string table)
        {
            return string.Format(Syntax.FieldFormat, table, field);
        }
        public virtual string FormatString(string value)
        {
            return string.IsNullOrEmpty(value) ? "''" : $"'{value.Replace("'", "''")}'";
        }
        public virtual string GetListSql(string fieldSql, DataConnectorField field)
        {
            var listItems = field.ListItems.Select(item => string.Format(Syntax.List.Item, FormatString(item.Item), FormatString(item.Value))).ToList();
            return string.Format(Syntax.List.Case, fieldSql, string.Join(" ", listItems));
        }
        public virtual string GetWhereClause(ConnectorQuery query)
        {
            if (query.Restrictions.Count <= 0) return string.Empty;
            var restrictions = query.Restrictions.Select(GetWhereItem).ToList();
            // todo - handle ands, ors and subqueries
            return string.Format(Syntax.Where.Format, string.Join(Syntax.Where.And, restrictions));
        }
        public virtual string GetWhereItem(ConnectorRestriction restriction)
        {
            string field = FormatField(restriction.Field);
            switch (restriction.RestrictionType)
            {
                case ConnectorRestriction.ConnectorRestrictionType.Between:
                case ConnectorRestriction.ConnectorRestrictionType.NotBetween:
                    string value1 = GetWhereValue(restriction.Values[0]);
                    string value2 = GetWhereValue(restriction.Values[1]);
                    return string.Format(GetWhereComparison(restriction.RestrictionType), field, value1, value2);
                case ConnectorRestriction.ConnectorRestrictionType.OneOfList:
                case ConnectorRestriction.ConnectorRestrictionType.NotOneOfList:
                case ConnectorRestriction.ConnectorRestrictionType.ContainsOneOfList:
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotContainOneOfList:
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotEndWithOneOfList:
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotStartWithOneOfList:
                case ConnectorRestriction.ConnectorRestrictionType.EndsWithOneOfList:
                case ConnectorRestriction.ConnectorRestrictionType.StartsWithOneOfList:
                    string values = GetWhereValueList(restriction.Values);
                    return string.Format(GetWhereComparison(restriction.RestrictionType), field, values);
            }
            string value = GetWhereValue(restriction.Values[0]);
            return string.Format(GetWhereComparison(restriction.RestrictionType), field, value);
        }
        public virtual string GetWhereComparison(ConnectorRestriction.ConnectorRestrictionType type)
        {
            switch (type)
            {
                case ConnectorRestriction.ConnectorRestrictionType.Between:
                    return Syntax.Where.Between;
                case ConnectorRestriction.ConnectorRestrictionType.Contains:
                    return Syntax.Where.Contains;
                case ConnectorRestriction.ConnectorRestrictionType.ContainsOneOfList:
                    return Syntax.Where.ContainsOneOf;
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotContain:
                    return Syntax.Where.NotContains;
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotContainOneOfList:
                    return Syntax.Where.NotContainsOneOf;
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotEndWith:
                    return Syntax.Where.NotEndsWith;
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotEndWithOneOfList:
                    return Syntax.Where.NotEndsWithOneOf;
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotEqual:
                    return Syntax.Where.DoeNotEqual;
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotStartWith:
                    return Syntax.Where.NotStartsWith;
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotStartWithOneOfList:
                    return Syntax.Where.NotStartsWithOneOf;
                case ConnectorRestriction.ConnectorRestrictionType.EndsWith:
                    return Syntax.Where.EndsWith;
                case ConnectorRestriction.ConnectorRestrictionType.EndsWithOneOfList:
                    return Syntax.Where.EndsWithOneOf;
                case ConnectorRestriction.ConnectorRestrictionType.Equals:
                    return Syntax.Where.IsEqual;
                case ConnectorRestriction.ConnectorRestrictionType.GreaterThan:
                    return Syntax.Where.GreaterThan;
                case ConnectorRestriction.ConnectorRestrictionType.GreaterThanOrEqualTo:
                    return Syntax.Where.GreaterThanEquals;
                case ConnectorRestriction.ConnectorRestrictionType.LessThan:
                    return Syntax.Where.LessThan;
                case ConnectorRestriction.ConnectorRestrictionType.LessThanOrEqualTo:
                    return Syntax.Where.LessThanEquals;
                case ConnectorRestriction.ConnectorRestrictionType.NotBetween:
                    return Syntax.Where.NotBetween;
                case ConnectorRestriction.ConnectorRestrictionType.NotOneOfList:
                    return Syntax.Where.NotOneOf;
                case ConnectorRestriction.ConnectorRestrictionType.OneOfList:
                    return Syntax.Where.OneOf;
                case ConnectorRestriction.ConnectorRestrictionType.StartsWith:
                    return Syntax.Where.StartsWith;
                case ConnectorRestriction.ConnectorRestrictionType.StartsWithOneOfList:
                    return Syntax.Where.StartsWith;
            }
            return string.Empty;
        }
        public virtual string GetWhereValueList(List<ConnectorRestrictionValue> values)
        {
            return string.Join(Syntax.Where.ValueSeparator, values.Select(GetWhereValue).ToList());
        }
        public virtual string GetWhereValue(ConnectorRestrictionValue value)
        {
            switch (value.Type)
            {
                case ConnectorRestrictionValue.ConnectorRestrictionValueType.Constant:
                    return value.Constant;
                case ConnectorRestrictionValue.ConnectorRestrictionValueType.Field:
                    return FormatField(value.Field);
                case ConnectorRestrictionValue.ConnectorRestrictionValueType.DateTimeValue:
                    return GetWhereDataValue(value.DateValue);
                // todo - handle calculations
            }
            return string.Empty;
        }
        public virtual string GetWhereDataValue(ConnectorRestrictionValue.ConnectorRestrictionDateValueType dateType)
        {
            switch (dateType)
            {
                case ConnectorRestrictionValue.ConnectorRestrictionDateValueType.Today:
                    return Syntax.Date.CurrentDate;
                case ConnectorRestrictionValue.ConnectorRestrictionDateValueType.Tomorrow:
                    return string.Format(Syntax.Date.AddDays, Syntax.Date.CurrentDate, 1);
                case ConnectorRestrictionValue.ConnectorRestrictionDateValueType.Yesterday:
                    return string.Format(Syntax.Date.RemoveDays, Syntax.Date.CurrentDate, 1);
                case ConnectorRestrictionValue.ConnectorRestrictionDateValueType.StartOfMonth:
                    return Syntax.Date.StartOfMonth;
                case ConnectorRestrictionValue.ConnectorRestrictionDateValueType.StartOfWeek:
                    return Syntax.Date.StartOfWeek;
                case ConnectorRestrictionValue.ConnectorRestrictionDateValueType.StartOfYear:
                    return Syntax.Date.StartOfYear;
                case ConnectorRestrictionValue.ConnectorRestrictionDateValueType.EndOfMonth:
                    return Syntax.Date.EndOfMonth;
                case ConnectorRestrictionValue.ConnectorRestrictionDateValueType.EndOfWeek:
                    return Syntax.Date.EndOfWeek;
                case ConnectorRestrictionValue.ConnectorRestrictionDateValueType.EndOfYear:
                    return Syntax.Date.EndOfYear;
            }
            return string.Empty;
        }
        public virtual string GetOrderByClause(ConnectorQuery query)
        {
            if (query.OrderBy.Count <= 0) return string.Empty;
            var orderByItems = query.OrderBy.Select(GetSortItem).ToList();
            return string.Format(Syntax.OrderBy.Format, string.Join(Syntax.FieldSeparator, orderByItems));
        }
        public virtual string GetSortItem(Tuple<DataConnectorField, ConnectorQuery.ConnectorQuerySortOrder> sortItem)
        {
            return string.Format(Syntax.OrderBy.ItemFormat, FormatField(sortItem.Item1), GetSortOrder(sortItem.Item2));
        }
        public virtual string GetSortOrder(ConnectorQuery.ConnectorQuerySortOrder sortOrder)
        {
            switch (sortOrder)
            {
                case ConnectorQuery.ConnectorQuerySortOrder.Ascending:
                    return Syntax.OrderBy.Ascending;
                case ConnectorQuery.ConnectorQuerySortOrder.Descending:
                    return Syntax.OrderBy.Descending;
            }
            return string.Empty;
        }
        public abstract List<string> GetDatabaseTableList(string database);
        public abstract List<string> GetTableFieldList(string database, string table);

        #endregion

    }
}
