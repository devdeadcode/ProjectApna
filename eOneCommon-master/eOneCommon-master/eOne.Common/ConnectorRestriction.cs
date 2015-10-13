using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using eOne.Common.DataConnectors;
using eOne.Common.Helpers;

namespace eOne.Common
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
            DoesNotEndWithOneOfList
        }

        public ConnectorRestriction()
        {
            Values = new List<ConnectorRestrictionValue>();
        }
        public ConnectorRestriction(DataConnectorField field, ConnectorRestrictionType type, string value)
        {
            Field = field;
            RestrictionType = type;
            Values = new List<ConnectorRestrictionValue> { new ConnectorRestrictionValue(value) };
        }
        public ConnectorRestriction(DataConnectorField field, ConnectorRestrictionType type, DateTime value)
        {
            Field = field;
            RestrictionType = type;
            Values = new List<ConnectorRestrictionValue> { new ConnectorRestrictionValue(value) };
        }
        public ConnectorRestriction(DataConnectorField field, ConnectorRestrictionType type, ConnectorRestrictionValue.ConnectorRestrictionDateValueType value)
        {
            Field = field;
            RestrictionType = type;
            Values = new List<ConnectorRestrictionValue> { new ConnectorRestrictionValue(value) };
        }
        public ConnectorRestriction(DataConnectorField field, ConnectorRestrictionType type, DataConnectorField value)
        {
            Field = field;
            RestrictionType = type;
            Values = new List<ConnectorRestrictionValue> { new ConnectorRestrictionValue(value) };
        }
        public ConnectorRestriction(XElement xmlNode, DataConnectorEntity entity)
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
                Values = new List<ConnectorRestrictionValue>();
                var valuesNode = XmlHelper.GetChildNode(xmlNode, "Values");
                if (valuesNode != null)
                {
                    var valueNodes = from valueNode in valuesNode.Elements() select valueNode;
                    foreach (var value in valueNodes.Select(valueNode => new ConnectorRestrictionValue(valueNode, entity))) Values.Add(value);
                }
                else
                {
                    foreach (var type in (ConnectorRestrictionValue.ConnectorRestrictionValueType[])Enum.GetValues(typeof(ConnectorRestrictionValue.ConnectorRestrictionValueType)))
                    {
                        var typeNode = XmlHelper.GetChildNode(xmlNode, Enum.GetName(typeof(ConnectorRestrictionValue.ConnectorRestrictionValueType), type));
                        if (typeNode != null) Values.Add(new ConnectorRestrictionValue(typeNode, entity));
                    }
                }
            }
        }

        public ConnectorRestrictionFieldType FieldType { get; set; }
        public DataConnectorField Field { get; set; }
        public string Calculation { get; set; }
        public ConnectorRestrictionType RestrictionType { get; set; }
        public List<ConnectorRestrictionValue> Values { get; set; }
        public ConnectorRestrictionConjunctiveOperator ConjunctiveOperator { get; set; }
        public ConnectorQuery Subquery { get; set; }

        public bool MatchFieldAndType(string fieldName, ConnectorRestrictionType type)
        {
            if (Subquery != null) return false;
            return (Field.Name == fieldName && RestrictionType == type);
        }

    }
}
