using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using eOne.Common.Connectors;
using eOne.Common.Helpers;

namespace eOne.Common.Query
{
    public class ConnectorRestriction
    {

        public enum ConnectorRestrictionConjunctiveOperator { None, And, Or }
        public enum ConnectorRestrictionFieldType { Field, Calculation, Subquery }
        public enum ConnectorRestrictionType
        {
            Equals,
            DoesNotEqual,
            GreaterThan,
            LessThan,
            GreaterThanOrEqualTo,
            LessThanOrEqualTo,
            Contains,
            StartsWith,
            EndsWith,
            DoesNotContain,
            DoesNotStartWith,
            DoesNotEndWith,
            Between,
            NotBetween,
            OneOfList,
            NotOneOfList,
            ContainsOneOfList,
            DoesNotContainOneOfList,
            StartsWithOneOfList,
            DoesNotStartWithOneOfList,
            EndsWithOneOfList,
            DoesNotEndWithOneOfList,
            Fuzzy
        }

        public ConnectorRestriction()
        {
            Values = new List<ConnectorValue>();
        }
        public ConnectorRestriction(ConnectorField field, ConnectorRestrictionType type, string value)
        {
            Field = field;
            RestrictionType = type;
            Values = new List<ConnectorValue> { new ConnectorValue(value) };
        }
        public ConnectorRestriction(ConnectorField field, ConnectorRestrictionType type, DateTime value)
        {
            Field = field;
            RestrictionType = type;
            Values = new List<ConnectorValue> { new ConnectorValue(value) };
        }
        public ConnectorRestriction(ConnectorField field, ConnectorRestrictionType type, ConnectorValue.ConnectorDateValueType value)
        {
            Field = field;
            RestrictionType = type;
            Values = new List<ConnectorValue> { new ConnectorValue(value) };
        }
        public ConnectorRestriction(ConnectorField field, ConnectorRestrictionType type, ConnectorField value)
        {
            Field = field;
            RestrictionType = type;
            Values = new List<ConnectorValue> { new ConnectorValue(value) };
        }
        public ConnectorRestriction(ConnectorField field, bool value)
        {
            var stringValue = value ? "true" : "false";
            Field = field;
            RestrictionType = ConnectorRestrictionType.Equals;
            Values = new List<ConnectorValue> { new ConnectorValue(stringValue) };
        }
        public ConnectorRestriction(XElement xmlNode, ConnectorEntity entity)
        {
            string conjunctive = XmlHelper.GetXmlNodeString(xmlNode, "Conjunctive");
            if (string.IsNullOrEmpty(conjunctive))
            {
                ConjunctiveOperator = ConnectorRestrictionConjunctiveOperator.None;
            } 
            else
            {
                ConjunctiveOperator = (ConnectorRestrictionConjunctiveOperator)Enum.Parse(typeof(ConnectorRestrictionConjunctiveOperator), conjunctive);
            }
            var subqueryNode = XmlHelper.GetChildNode(xmlNode, "Subquery");
            if (subqueryNode != null)
            {
                // todo
            }
            else
            {
                // get field or calculation that is being restricted
                int fieldId = XmlHelper.GetXmlNodeInt(xmlNode, "Field");
                if (fieldId != 0)
                {
                    FieldType = ConnectorRestrictionFieldType.Field;
                    Field = entity.FindField(fieldId);
                }
                else
                {
                    FieldType = ConnectorRestrictionFieldType.Calculation;
                    Calculation = XmlHelper.GetXmlNodeString(xmlNode, "Calculation");
                }
                // get restriction type
                string restrictionType = XmlHelper.GetXmlNodeString(xmlNode, "Type");
                RestrictionType = (ConnectorRestrictionType)Enum.Parse(typeof(ConnectorRestrictionType), restrictionType);
                // get values
                Values = new List<ConnectorValue>();
                var valuesNode = XmlHelper.GetChildNode(xmlNode, "Values");
                if (valuesNode != null)
                {
                    var valueNodes = from valueNode in valuesNode.Elements() select valueNode;
                    foreach (var value in valueNodes.Select(valueNode => new ConnectorValue(valueNode, entity))) Values.Add(value);
                }
                else
                {
                    foreach (var type in (ConnectorValue.ConnectorValueType[])Enum.GetValues(typeof(ConnectorValue.ConnectorValueType)))
                    {
                        var typeNode = XmlHelper.GetChildNode(xmlNode, Enum.GetName(typeof(ConnectorValue.ConnectorValueType), type));
                        if (typeNode != null) Values.Add(new ConnectorValue(typeNode, entity));
                    }
                }
            }
        }

        public ConnectorRestrictionFieldType FieldType { get; set; }
        public ConnectorField Field { get; set; }
        public string Calculation { get; set; }
        public ConnectorRestrictionType RestrictionType { get; set; }
        public List<ConnectorValue> Values { get; set; }
        public ConnectorRestrictionConjunctiveOperator ConjunctiveOperator { get; set; }
        public ConnectorQuery Subquery { get; set; }

        public bool MatchFieldAndType(string fieldName, ConnectorRestrictionType type)
        {
            if (Subquery != null) return false;
            return (Field.Name == fieldName && RestrictionType == type);
        }

    }
}
