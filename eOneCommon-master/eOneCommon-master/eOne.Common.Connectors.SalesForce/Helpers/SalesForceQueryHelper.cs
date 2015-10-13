using System.Collections.Generic;
using System.Linq;
using System.Text;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.SalesForce.Helpers
{
    public class SalesForceQueryHelper
    {

        public static string GetSoqlQuery(ConnectorQuery query)
        {
            var soql = new StringBuilder($"SELECT+{GetFieldList(query)}+FROM+{query.Entity.Endpoint}");
            var where = GetWhereClause(query);
            if (!string.IsNullOrWhiteSpace(where)) soql.Append($"+WHERE+{@where}");
            return soql.ToString();
        }

        private static string GetWhereClause(ConnectorQuery query)
        {
            if (HasInvalidRestrictions(query) && query.HasOrConjunctives) return string.Empty;
            var where = new StringBuilder();
            var first = true;
            foreach (var restriction in query.Restrictions)
            {
                if (IsRestrictionValid(restriction))
                {
                    var fieldName = string.IsNullOrWhiteSpace(restriction.Field.ApiName) ? restriction.Field.Name : restriction.Field.ApiName;
                    if (!first) where.Append(GetConjunctive(restriction.ConjunctiveOperator));
                    first = false;
                    
                    switch (restriction.RestrictionType)
                    {
                        case ConnectorRestriction.ConnectorRestrictionType.Between:
                            where.AppendFormat("({0}+>=+{1}+AND+{0}+<=+{2})", fieldName, GetValue(restriction.Values[0]), GetValue(restriction.Values[0]));
                            break;
                        case ConnectorRestriction.ConnectorRestrictionType.NotBetween:
                            where.AppendFormat("({0}+>+{1}+OR+{0}+<+{2})", fieldName, GetValue(restriction.Values[0]), GetValue(restriction.Values[0]));
                            break;
                        case ConnectorRestriction.ConnectorRestrictionType.OneOfList:
                            where.Append($"{fieldName}+IN+({restriction.Values.Select(GetValue).ToList()})");
                            break;
                        case ConnectorRestriction.ConnectorRestrictionType.NotOneOfList:
                            where.Append($"{fieldName}+NOT+IN+({restriction.Values.Select(GetValue).ToList()})");
                            break;
                        default:
                            where.Append($"{fieldName}+{GetComparisonOperator(restriction.RestrictionType)}+{GetValue(restriction.Values[0], restriction.RestrictionType)}");
                            break;
                    }
                }
            }
            return where.ToString();
        }

        private static string GetFieldList(ConnectorQuery query)
        {
            var fields = new List<string>();
            foreach (var field in query.Fields)
            {
                if (field.IsCalculation)
                {
                    // add fields required for the calculation
                    foreach (var requiredFieldName in field.FieldsRequiredForCalculation)
                    {
                        var requiredField = query.Entity.FindField(requiredFieldName);
                        if (requiredField != null)
                        {
                            var fieldName = GetFieldName(requiredField);
                            if (!fields.Contains(fieldName)) fields.Add(fieldName);
                        }
                    }
                }
                else
                {
                    fields.Add(GetFieldName(field));
                }
            }
            return string.Join(",", fields);
        }

        private static string GetFieldName(DataConnectorField field)
        {
            return string.IsNullOrWhiteSpace(field.ApiName) ? field.Name : field.ApiName;
        }

        private static string GetValue(ConnectorRestrictionValue value)
        {
            return GetValue(value, ConnectorRestriction.ConnectorRestrictionType.Equals);
        }

        private static string GetValue(ConnectorRestrictionValue value, ConnectorRestriction.ConnectorRestrictionType type)
        {
            switch (type)
            {
                case ConnectorRestriction.ConnectorRestrictionType.Contains:
                    return $"'%{FormatStringValue(value.Constant)}%'";
                case ConnectorRestriction.ConnectorRestrictionType.StartsWith:
                    return $"'{FormatStringValue(value.Constant)}%'";
                case ConnectorRestriction.ConnectorRestrictionType.EndsWith:
                    return $"'%{FormatStringValue(value.Constant)}'";
                default:
                    return FormatStringValue(value.Constant);
            }
        }

        private static string FormatStringValue(string value)
        {
            return $"{value.Replace("'", "\'")}";
        }

        private static string GetConjunctive(ConnectorRestriction.ConnectorRestrictionConjunctiveOperator conjunctive)
        {
            switch (conjunctive)
            {
                case ConnectorRestriction.ConnectorRestrictionConjunctiveOperator.None:
                    return string.Empty;
                case ConnectorRestriction.ConnectorRestrictionConjunctiveOperator.And:
                    return "+AND+";
                case ConnectorRestriction.ConnectorRestrictionConjunctiveOperator.Or:
                    return "+OR+";
                default:
                    return string.Empty;
            }
        }

        private static string GetComparisonOperator(ConnectorRestriction.ConnectorRestrictionType type)
        {
            switch (type)
            {
                case ConnectorRestriction.ConnectorRestrictionType.Equals:
                    return "=";
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotEqual:
                    return "!=";
                case ConnectorRestriction.ConnectorRestrictionType.GreaterThan:
                    return ">";
                case ConnectorRestriction.ConnectorRestrictionType.LessThan:
                    return "<";
                case ConnectorRestriction.ConnectorRestrictionType.GreaterThanOrEqualTo:
                    return ">=";
                case ConnectorRestriction.ConnectorRestrictionType.LessThanOrEqualTo:
                    return "<=";
                case ConnectorRestriction.ConnectorRestrictionType.Contains:
                case ConnectorRestriction.ConnectorRestrictionType.StartsWith:
                case ConnectorRestriction.ConnectorRestrictionType.EndsWith:
                    return "LIKE";
                default:
                    return string.Empty;
            }
        }

        private static bool HasInvalidRestrictions(ConnectorQuery query)
        {
            return query.Restrictions.Any(restriction => !IsRestrictionValid(restriction));
        }

        private static bool IsRestrictionValid(ConnectorRestriction restriction)
        {
            if (restriction.Field.IsCalculation) return false;
            switch (restriction.RestrictionType)
            {
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotContain:
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotStartWith:
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotEndWith:
                case ConnectorRestriction.ConnectorRestrictionType.ContainsOneOfList:
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotContainOneOfList:
                case ConnectorRestriction.ConnectorRestrictionType.StartsWithOneOfList:
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotStartWithOneOfList:
                case ConnectorRestriction.ConnectorRestrictionType.EndsWithOneOfList:
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotEndWithOneOfList:
                    return false;
                default:
                    return true;
            }
        }

    }
}
