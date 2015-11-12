using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using eOne.Common.Helpers;
using eOne.Common.Query;

namespace eOne.Common.Connectors.Intacct.Helpers
{
    public class RequestHelper
    {

        public static string GetRequestXml(ConnectorQuery query)
        {
            return GetRequestXml(query.Connector, query.Entity.Endpoint, GetFieldList(query), GetQuery(query));
        }
        public static string GetRequestXml(Connector connector, string endpoint, string fields, string query)
        {
            if (connector == null) return string.Empty;

            var xml = new XmlDocument();

            var xmlDeclaration = xml.CreateXmlDeclaration("1.0", "iso-8859-1", null);
            var root = xml.DocumentElement;
            xml.InsertBefore(xmlDeclaration, root);

            var requestElement = xml.CreateElement(string.Empty, "request", string.Empty);
            xml.AppendChild(requestElement);

            var controlElement = AddChildElement(xml, requestElement, "control");
            AddTextElement(xml, controlElement, "senderid", "eOne");
            AddTextElement(xml, controlElement, "password", "t3Fu96ujjt");
            AddTextElement(xml, controlElement, "controlid", "popdock");
            AddTextElement(xml, controlElement, "uniqueid", "false");
            AddTextElement(xml, controlElement, "dtdversion", "3.0");

            var operationElement = AddChildElement(xml, requestElement, "operation");

            var authenticationElement = AddChildElement(xml, operationElement, "authentication");

            var loginElement = AddChildElement(xml, authenticationElement, "login");
            AddTextElement(xml, loginElement, "userid", connector.Username);
            AddTextElement(xml, loginElement, "companyid", connector.Companies[0].DatabaseName);
            AddTextElement(xml, loginElement, "password", connector.Password);

            var contentElement = AddChildElement(xml, operationElement, "content");

            var functionElement = xml.CreateElement(string.Empty, "function", string.Empty);
            functionElement.SetAttribute("controlid", "popdock");
            contentElement.AppendChild(functionElement);

            var readElement = AddChildElement(xml, functionElement, "readByQuery");
            AddTextElement(xml, readElement, "object", endpoint);
            AddTextElement(xml, readElement, "fields", fields);
            AddTextElement(xml, readElement, "query", query);
            AddTextElement(xml, readElement, "returnFormat", "json");

            return xml.OuterXml;
        }

        private static string GetFieldList(ConnectorQuery query)
        {
            if (query.FieldNamesUsed == null || query.FieldNamesUsed.Count == 0) return "*";
            return string.Join(",", query.FieldNamesUsed);
        }
        private static string GetQuery(ConnectorQuery query)
        {
            if (query.Restrictions == null || query.Restrictions.Count == 0) return string.Empty;
            if (query.Restrictions.Any(restriction => !IsRestrictionTypeValid(restriction.RestrictionType)) && query.HasOrConjunctives) return string.Empty;

            var validRestrictions = query.Restrictions.Where(restriction => IsRestrictionTypeValid(restriction.RestrictionType)).ToList();
            if (validRestrictions.Count == 0) return string.Empty;

            validRestrictions[0].ConjunctiveOperator = ConnectorRestriction.ConnectorRestrictionConjunctiveOperator.None;
            var restrictionStrings = validRestrictions.Select(GetRestrictionString).ToList();
            return string.Join(string.Empty, restrictionStrings);
        }

        private static string GetRestrictionString(ConnectorRestriction restriction)
        {
            var conjunctive = GetConjunctive(restriction.ConjunctiveOperator);
            var field = string.IsNullOrWhiteSpace(restriction.Field.ApiName) ? restriction.Field.Name : restriction.Field.ApiName;
            switch (restriction.RestrictionType)
            {
                case ConnectorRestriction.ConnectorRestrictionType.Between:
                    return $"{conjunctive}({field} &gt;= {GetRestrictionValue(restriction, 0)} and {field} &lt;= {GetRestrictionValue(restriction, 1)})";
                case ConnectorRestriction.ConnectorRestrictionType.NotBetween:
                    return $"{conjunctive}({field} &lt; {GetRestrictionValue(restriction, 0)} or {field} &gt; {GetRestrictionValue(restriction, 1)})";
                default:
                    var type = GetRestrictionOperator(restriction.RestrictionType);
                    var valueString = RestrictionTypeHasMultipleValues(restriction.RestrictionType) ? GetRestrictionValueList(restriction) : GetRestrictionValue(restriction, 0);
                    return $"{conjunctive}{field}{type}{valueString}";
            }
        }
        private static string GetConjunctive(ConnectorRestriction.ConnectorRestrictionConjunctiveOperator conjunctive)
        {
            switch (conjunctive)
            {
                case ConnectorRestriction.ConnectorRestrictionConjunctiveOperator.And:
                    return " and ";
                case ConnectorRestriction.ConnectorRestrictionConjunctiveOperator.Or:
                    return " or ";
                default:
                    return string.Empty;
            }
        }
        private static string GetRestrictionOperator(ConnectorRestriction.ConnectorRestrictionType type)
        {
            switch (type)
            {
                case ConnectorRestriction.ConnectorRestrictionType.Equals:
                    return " = ";
                case ConnectorRestriction.ConnectorRestrictionType.GreaterThan:
                    return " &gt; ";
                case ConnectorRestriction.ConnectorRestrictionType.LessThan:
                    return " &lt; ";
                case ConnectorRestriction.ConnectorRestrictionType.GreaterThanOrEqualTo:
                    return " &gt;= ";
                case ConnectorRestriction.ConnectorRestrictionType.LessThanOrEqualTo:
                    return " &lt;= ";
                case ConnectorRestriction.ConnectorRestrictionType.Contains:
                case ConnectorRestriction.ConnectorRestrictionType.StartsWith:
                case ConnectorRestriction.ConnectorRestrictionType.EndsWith:
                    return " like ";
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotContain:
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotStartWith:
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotEndWith:
                    return " not like ";
                case ConnectorRestriction.ConnectorRestrictionType.OneOfList:
                    return " in ";
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotEqual:
                case ConnectorRestriction.ConnectorRestrictionType.NotOneOfList:
                    return " not in ";
            }
            return string.Empty;
        }
        private static bool IsRestrictionTypeValid(ConnectorRestriction.ConnectorRestrictionType type)
        {
            switch (type)
            {
                case ConnectorRestriction.ConnectorRestrictionType.Equals:
                case ConnectorRestriction.ConnectorRestrictionType.GreaterThan:
                case ConnectorRestriction.ConnectorRestrictionType.LessThan:
                case ConnectorRestriction.ConnectorRestrictionType.GreaterThanOrEqualTo:
                case ConnectorRestriction.ConnectorRestrictionType.LessThanOrEqualTo:
                case ConnectorRestriction.ConnectorRestrictionType.Contains:
                case ConnectorRestriction.ConnectorRestrictionType.StartsWith:
                case ConnectorRestriction.ConnectorRestrictionType.EndsWith:
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotContain:
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotStartWith:
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotEndWith:
                case ConnectorRestriction.ConnectorRestrictionType.OneOfList:
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotEqual:
                case ConnectorRestriction.ConnectorRestrictionType.NotOneOfList:
                case ConnectorRestriction.ConnectorRestrictionType.Between:
                case ConnectorRestriction.ConnectorRestrictionType.NotBetween:
                    return true;
                default:
                    return false;
            }
        }
        private static bool RestrictionTypeHasMultipleValues(ConnectorRestriction.ConnectorRestrictionType type)
        {
            switch (type)
            {
                case ConnectorRestriction.ConnectorRestrictionType.OneOfList:
                case ConnectorRestriction.ConnectorRestrictionType.DoesNotEqual:
                case ConnectorRestriction.ConnectorRestrictionType.NotOneOfList:
                    return true;
                default:
                    return false;
            }
        }
        private static string GetRestrictionValue(ConnectorRestriction restriction, int valueNumber)
        {
            return GetRestrictionValue(restriction.Values[valueNumber], restriction.Field.Type, restriction.RestrictionType);
        }
        private static string GetRestrictionValue(ConnectorValue value, ConnectorFieldType fieldType, ConnectorRestriction.ConnectorRestrictionType restrictionType)
        {
            switch (value.Type)
            {
                case ConnectorValue.ConnectorValueType.Constant:
                    return fieldType.Id == Connector.FieldTypeIdDate ? FormatDate(value.DateConstant) : GetConstantValue(value.Constant, fieldType.Id, restrictionType);
                case ConnectorValue.ConnectorValueType.Field:
                    return string.IsNullOrWhiteSpace(value.Field.ApiName) ? value.Field.Name : value.Field.ApiName;
                case ConnectorValue.ConnectorValueType.DateTimeValue:
                    return FormatDate(DateHelper.GetDateValue(value.DateValue));
            }
            return string.Empty;
        }
        private static string GetRestrictionValueList(ConnectorRestriction restriction)
        {
            return GetRestrictionValueList(restriction.Values, restriction.Field.Type, restriction.RestrictionType);
        }
        private static string GetRestrictionValueList(List<ConnectorValue> values, ConnectorFieldType fieldType, ConnectorRestriction.ConnectorRestrictionType restrictionType)
        {
            var valueStrings = values.Select(value => GetRestrictionValue(value, fieldType, restrictionType)).ToList();
            return $"({string.Join(",", valueStrings)})";
        }
        private static string GetConstantValue(string constant, int fieldTypeId, ConnectorRestriction.ConnectorRestrictionType restrictionType)
        {
            switch (fieldTypeId)
            {
                case Connector.FieldTypeIdCurrency:
                case Connector.FieldTypeIdInteger:
                case Connector.FieldTypeIdPercentage:
                case Connector.FieldTypeIdQuantity:
                    return constant;
                case Connector.FieldTypeIdYesNo:
                    return constant.ToLower() == "true" ? "1" : "0";
                default:
                    switch (restrictionType)
                    {
                        case ConnectorRestriction.ConnectorRestrictionType.Contains:
                        case ConnectorRestriction.ConnectorRestrictionType.DoesNotContain:
                            return $"'%{EscapeInvalidCharacters(constant)}%'";
                        case ConnectorRestriction.ConnectorRestrictionType.StartsWith:
                        case ConnectorRestriction.ConnectorRestrictionType.DoesNotStartWith:
                            return $"'{EscapeInvalidCharacters(constant)}%'";
                        case ConnectorRestriction.ConnectorRestrictionType.EndsWith:
                        case ConnectorRestriction.ConnectorRestrictionType.DoesNotEndWith:
                            return $"'%{EscapeInvalidCharacters(constant)}'";
                        default:
                            return $"'{EscapeInvalidCharacters(constant)}'";
                    }
            }
        }
        private static string EscapeInvalidCharacters(string value)
        {
            return value.Replace("'", "\'").Replace("<", "&lt;").Replace(">", "&gt;");
        }
        private static string FormatDate(DateTime value)
        {
            return $"'{value.Month}/{value.Day}/{value.Year}'";
        }

        private static void AddTextElement(XmlDocument doc, XmlNode node, string name, string value)
        {
            var element = doc.CreateElement(string.Empty, name, string.Empty);
            var textNode = doc.CreateTextNode(value);
            element.AppendChild(textNode);
            node.AppendChild(element);
        }
        private static XmlNode AddChildElement(XmlDocument doc, XmlNode node, string name)
        {
            var element = doc.CreateElement(string.Empty, name, string.Empty);
            node.AppendChild(element);
            return element;
        }

    }
}
