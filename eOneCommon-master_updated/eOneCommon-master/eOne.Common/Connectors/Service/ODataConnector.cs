using System;
using System.Collections.Generic;
using System.Linq;
using eOne.Common.Helpers;
using eOne.Common.Query;

namespace eOne.Common.Connectors.Service
{
    
    /// <summary>
    /// Base class for OData connectors
    /// </summary>
    public abstract class ODataConnector : RestConnector
    {

        protected ODataConnector()
        {
            // set all query filter options to true by default
            ODataAllowFilter = true;
            ODataAllowSelect = true;
            ODataAllowSort = true;
            ODataAllowTop = true;
        }

        #region Enums

        public enum ODataVersionNumber
        {
            v1_0,
            v2_0,
            v3_0,
            v4_0
        }

        #endregion

        #region Properties

        public ODataVersionNumber ODataVersion { get; set; }
        public bool ODataAllowFilter { get; set; }
        public bool ODataAllowSelect { get; set; }
        public bool ODataAllowSort { get; set; }
        public bool ODataAllowTop { get; set; }

        #endregion

        public override List<Tuple<string, string>> GetParameters(ConnectorQuery query)
        {
            // add odata query url parameters
            var parameters = new List<Tuple<string, string>>();
            if (ODataAllowFilter) AddUrlParameter(parameters, "$filter", GetFilterQueryOption(query));
            if (ODataAllowSelect) AddUrlParameter(parameters, "$select", GetSelectQueryOption(query));
            if (ODataAllowSort) AddUrlParameter(parameters, "$orderby", GetSortByQueryOption(query));
            if (ODataAllowTop) AddUrlParameter(parameters, "$top", GetTopQueryOption(query));
            return parameters;
        }

        #region Helpers

        private static void AddUrlParameter(ICollection<Tuple<string, string>> parameters, string name, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                parameters.Add(new Tuple<string, string>(name, value));
            }
        }

        #region Filter

        private string GetFilterQueryOption(ConnectorQuery query)
        {
            // return blank filter if there are no restrictions or the query contains ors or subqueries
            if (query.Restrictions.Count == 0 || query.HasOrConjunctives || query.HasSubqueries) return string.Empty;

            // build list of filter strings
            var filter = query.Restrictions.Select(GetFilterElement).Where(element => !string.IsNullOrWhiteSpace(element)).ToList();

            // join filters with and
            return filter.Count == 0 ? string.Empty : string.Join(" and ", filter);
        }
        private string GetFilterElement(ConnectorRestriction restriction)
        {
            // return empty string if the field is a calculation
            if (restriction.Field.FieldsRequiredForCalculation.Count <= 0) return string.Empty;

            // get format of filter element
            var format = GetFilterElementFormat(restriction.RestrictionType);
            if (string.IsNullOrWhiteSpace(format)) return string.Empty;

            // get filter value
            var value = GetFilterElementValue(restriction.Values[0], restriction.Field.Type.Id);

            // return empty string if value is blank
            return string.IsNullOrWhiteSpace(value) ? string.Empty : string.Format(format, restriction.Field.Name, value);
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
        private string GetFilterElementValue(ConnectorValue value, int fieldTypeId)
        {
            switch (value.Type)
            {
                case ConnectorValue.ConnectorValueType.Constant:
                    if (fieldTypeId == FieldTypeIdDate) return GetFilterDateValue(value.DateConstant);
                    return IsFieldTypeString(fieldTypeId) ? $"'{value.Constant}'" : value.Constant;
                case ConnectorValue.ConnectorValueType.Field:
                    return value.Field.Name;
                case ConnectorValue.ConnectorValueType.DateTimeValue:
                    return GetFilterDateValue(DateHelper.GetDateValue(value.DateValue));
            }
            return string.Empty;
        }
        private string GetFilterDateValue(DateTime value)
        {
            string datetimeValue = $"{value.Year:00}-{value.Month:00}-{value.Day:00}T{value.Hour:00}:{value.Minute:00}:{value.Second:00}.{value.Millisecond:00}Z";
            switch (ODataVersion)
            {
                case ODataVersionNumber.v1_0:
                case ODataVersionNumber.v2_0:
                case ODataVersionNumber.v3_0:
                    return $"datetime'{datetimeValue}'";
                default:
                    return datetimeValue;
            }
        }

        #endregion

        #region Sort

        private static string GetSortByQueryOption(ConnectorQuery query)
        {
            if (query.OrderBy == null) return string.Empty;
            return query.OrderBy.Count == 0 ? string.Empty : string.Join(",", query.OrderBy.Select(GetSortByElement).ToList());
        }
        private static string GetSortByElement(Tuple<ConnectorField, ConnectorQuery.ConnectorQuerySortOrder> sort)
        {
            return sort.Item2 == ConnectorQuery.ConnectorQuerySortOrder.Descending ? $"{sort.Item1.Name} desc" : sort.Item1.Name;
        }

        #endregion

        #region Top

        private static string GetTopQueryOption(ConnectorQuery query)
        {
            return query.MaxRecords == 0 ? string.Empty : query.MaxRecords.ToString();
        }

        #endregion

        #region Select

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

        #endregion

        #endregion

    }
}
