using System;
using System.Collections.Generic;
using System.Linq;

namespace eOne.Common.DataConnectors.Rest
{
    
    /// <summary>
    /// Base class for OData connectors
    /// </summary>
    abstract class ODataConnector : RestConnector
    {

        public string GetQueryOptions(ConnectorQuery query)
        {
            if (query == null) return string.Empty;
            string filter = GetFilterQueryOption(query);
            string top = GetTopQueryOption(query);
            string sortby = GetSortByQueryOption(query);
            string select = GetSelectQueryOption(query);
            if (string.IsNullOrWhiteSpace(filter) && string.IsNullOrWhiteSpace(top) && string.IsNullOrWhiteSpace(sortby) && string.IsNullOrWhiteSpace(select)) return string.Empty;
            return $"?{filter}{top}{sortby}{@select}";
        }

        private static string GetFilterQueryOption(ConnectorQuery query)
        {
            if (query.Restrictions.Count == 0 || query.HasOrConjunctives) return string.Empty;
            var filter = query.Restrictions.Select(GetFilterElement).Where(element => !string.IsNullOrWhiteSpace(element)).ToList();
            return filter.Count == 0 ? string.Empty : string.Join(" and ", filter);
        }
        private static string GetFilterElement(ConnectorRestriction restriction)
        {
            string format = GetFilterElementFormat(restriction.RestrictionType);
            if (string.IsNullOrWhiteSpace(format)) return string.Empty;
            string value = GetFilterElementValue(restriction.Values[0]);
            if (string.IsNullOrWhiteSpace(value)) return string.Empty;
            return string.Format(format, restriction.Field.Name, value);
        }
        private static string GetFilterElementFormat(ConnectorRestriction.ConnectorRestrictionType type)
        {
            switch (type)
            {
                case ConnectorRestriction.ConnectorRestrictionType.Equals:
                    return "{0} eq {1}";
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotEqual:
                    return "{0} ne {1}";
                case ConnectorRestriction.ConnectorRestrictionType.GreaterThan:
                    return "{0} gt {1}";
                case ConnectorRestriction.ConnectorRestrictionType.LessThan:
                    return "{0} lt {1}";
                case ConnectorRestriction.ConnectorRestrictionType.GreaterThanOrEqualTo:
                    return "{0} ge {1}";
                case ConnectorRestriction.ConnectorRestrictionType.LessThanOrEqualTo:
                    return "{0} le {1}";
                case ConnectorRestriction.ConnectorRestrictionType.Contains:
                    return "contains({0} {1})";
                case ConnectorRestriction.ConnectorRestrictionType.StartsWith:
                    return "startswith({0} {1})";
                case ConnectorRestriction.ConnectorRestrictionType.EndsWith:
                    return "endswith({0} {1})";
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotContain:
                    return "not contains({0} {1})";
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotStartWith:
                    return "not startswith({0} {1})";
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotEndWith:
                    return "not endswith({0} {1})";
            }
            return string.Empty;
        }
        private static string GetFilterElementValue(ConnectorRestrictionValue value)
        {
            switch (value.Type)
            {
                case ConnectorRestrictionValue.ConnectorRestrictionValueType.Constant:
                    return $"'{value.Constant}'";
                case ConnectorRestrictionValue.ConnectorRestrictionValueType.Field:
                    return value.Field.Name;
            }
            return string.Empty;
        }

        private static string GetSortByQueryOption(ConnectorQuery query)
        {
            return query.OrderBy.Count == 0 ? string.Empty : string.Join(",", query.OrderBy.Select(GetSortByElement).ToList());
        }
        private static string GetSortByElement(Tuple<DataConnectorField, ConnectorQuery.ConnectorQuerySortOrder> sort)
        {
            return sort.Item2 == ConnectorQuery.ConnectorQuerySortOrder.Descending ? $"{sort.Item1.Name} desc" : sort.Item1.Name;
        }

        private static string GetTopQueryOption(ConnectorQuery query)
        {
            return query.MaxRecords == 0 ? string.Empty : $"$top={query.MaxRecords}";
        }

        private static string GetSelectQueryOption(ConnectorQuery query)
        {
            var fields = new List<string>();
            foreach (var field in query.Fields)
            {
                if (field.FieldsRequiredForCalculation.Count > 0)
                {
                    foreach (var required in field.FieldsRequiredForCalculation.Where(required => !fields.Contains(required))) fields.Add(required);
                }
                else
                {
                    if (!fields.Contains(field.Name)) fields.Add(field.Name);
                }
            }
            return fields.Count == 0 ? string.Empty : $"$select={string.Join(", ", fields)}";
        }

    }
}
