using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using eOne.Common.Connectors;
using eOne.Common.Helpers;

namespace eOne.Common.Query
{
    /// <summary>
    /// 
    /// </summary>
    public class ConnectorQuery
    {

        #region Enums

        public enum ConnectorQuerySortOrder { Ascending, Descending }
        public enum ConnectorQueryGenericDateType
        {
            ThisWeek,
            ThisMonth,
            ThisYear
        }

        #endregion

        #region Constructors

        public ConnectorQuery()
        {
            Companies = new List<ConnectorCompany>();
            Fields = new List<ConnectorField>();
            Restrictions = new List<ConnectorRestriction>();
            OrderBy = new List<Tuple<ConnectorField, ConnectorQuerySortOrder>>();
            Page = 1;
            IsSearch = false;
        }
        public ConnectorQuery(Connector connector, string queryXml)
        {
            Connector = connector;
            Companies = new List<ConnectorCompany>();
            Fields = new List<ConnectorField>();
            Restrictions = new List<ConnectorRestriction>();
            OrderBy = new List<Tuple<ConnectorField, ConnectorQuerySortOrder>>();
            ParseQueryXml(queryXml);
            Page = 1;
            IsSearch = false;
        }
        public ConnectorQuery(ConnectorSearch search)
        {
            Search = search;
            Connector = search.Connector;
            Companies = search.Companies;
            Entity = search.Entity;
            Restrictions = SearchHelper.GetSearchRestrictions(search);
            Fields = new List<ConnectorField>();
            OrderBy = new List<Tuple<ConnectorField, ConnectorQuerySortOrder>>();
            AddDefaultFields();
            Page = 1;
            IsSearch = true;
        }

        #endregion

        #region Properties

        public Connector Connector { get; set; }
        public ConnectorEntity Entity { get; set; }
        public List<ConnectorCompany> Companies { get; set; }
        public List<ConnectorField> Fields { get; set; }
        public List<ConnectorRestriction> Restrictions { get; set; }
        public List<Tuple<ConnectorField, ConnectorQuerySortOrder>> OrderBy { get; set; }
        public int MaxRecords { get; set; }
        public bool HasOrConjunctives
        {
            get
            {
                return Restrictions.Any(restriction => restriction.ConjunctiveOperator == ConnectorRestriction.ConnectorRestrictionConjunctiveOperator.Or);
            }
        }
        public bool HasSubqueries
        {
            get
            {
                return Restrictions.Any(restriction => restriction.FieldType == ConnectorRestriction.ConnectorRestrictionFieldType.Subquery);
            }
        }
        public bool ApplyHtmlFormatting { get; set; }
        public int Page { get; set; }
        public string MaxId { get; set; }
        public string MinId { get; set; }
        public DateTime MaxDate { get; set; }
        public DateTime MinDate { get; set; }
        public string Cursor { get; set; }
        public bool IsSearch { get; set; }
        public List<string> FieldNamesUsed
        {
            get
            {
                var properties = new List<string>();
                foreach (var field in Fields)
                {
                    if (field.IsCalculation)
                    {
                        properties.AddRange(field.FieldsRequiredForCalculation);
                    }
                    else
                    {
                        properties.Add(string.IsNullOrWhiteSpace(field.ApiName) ? field.Name : field.ApiName);
                    }
                }
                foreach (var restriction in Restrictions)
                {
                    properties.Add(string.IsNullOrWhiteSpace(restriction.Field.ApiName) ? restriction.Field.Name : restriction.Field.ApiName);
                    properties.AddRange(from value in restriction.Values where value.Field != null select string.IsNullOrWhiteSpace(value.Field.ApiName) ? value.Field.Name : value.Field.ApiName);
                }
                properties = properties.Distinct().ToList();
                properties.Remove(string.Empty);
                return properties;
            }
        }
        public ConnectorSearch Search { get; set; }
        public bool ApiFieldsUsed
        {
            get
            {
                if (Fields.Any(field => !string.IsNullOrWhiteSpace(field.ApiName))) return true;
                foreach (var restriction in Restrictions)
                {
                    if (!string.IsNullOrWhiteSpace(restriction.Field.ApiName)) return true;
                    if (restriction.Values.Any(value => value.Type == ConnectorValue.ConnectorValueType.Field && !string.IsNullOrWhiteSpace(value.Field.ApiName))) return true;
                }
                return false;
            }
        }

        #endregion
         
        #region Methods

        public void AddCompany(int companyId)
        {
            var company = Connector.FindCompany(companyId);
            if (company != null) Companies.Add(company);
        }
        public void AddDefaultFields()
        {
            foreach (var field in Entity.Fields.Where(field => field.DefaultField)) AddField(field);
        }
        public void AddField(int fieldId)
        {
            var field = Entity.FindField(fieldId);
            if (field != null) AddField(field);
        }
        public void AddField(string fieldName)
        {
            var field = Entity.FindField(fieldName);
            if (field != null) AddField(field);
        }
        public void AddFields(params int[] fieldIds)
        {
            foreach (var fieldId in fieldIds) AddField(fieldId);
        }
        public void AddFields(params string[] fieldNames)
        {
            foreach (var fieldName in fieldNames) AddField(fieldName);
        }
        public void AddOrderBy(int fieldId, ConnectorQuerySortOrder sortOrder = ConnectorQuerySortOrder.Ascending)
        {
            var field = Entity.FindField(fieldId);
            if (field != null) OrderBy.Add(new Tuple<ConnectorField, ConnectorQuerySortOrder>(field, sortOrder));
        }
        public void AddOrderBy(string fieldName, ConnectorQuerySortOrder sortOrder = ConnectorQuerySortOrder.Ascending)
        {
            var field = Entity.FindField(fieldName);
            if (field != null) OrderBy.Add(new Tuple<ConnectorField, ConnectorQuerySortOrder>(field, sortOrder));
        }
        public ConnectorRestriction AddRestriction(string fieldName, ConnectorRestriction.ConnectorRestrictionType type, string value)
        {
            var field = Entity.FindField(fieldName);
            return field == null ? null : AddRestriction(new ConnectorRestriction(field, type, value));
        }
        public ConnectorRestriction AddRestriction(string fieldName, ConnectorRestriction.ConnectorRestrictionType type, DateTime value)
        {
            var field = Entity.FindField(fieldName);
            return field == null ? null : AddRestriction(new ConnectorRestriction(field, type, value));
        }
        public ConnectorRestriction AddRestriction(string fieldName, ConnectorRestriction.ConnectorRestrictionType type, ConnectorValue.ConnectorDateValueType value)
        {
            var field = Entity.FindField(fieldName);
            return field == null ? null : AddRestriction(new ConnectorRestriction(field, type, value));
        }
        public ConnectorRestriction AddBooleanRestriction(string fieldName, bool value)
        {
            var field = Entity.FindField(fieldName);
            return field == null ? null : AddRestriction(new ConnectorRestriction(field, value));
        }
        public ConnectorRestriction AddDateRestriction(string fieldName, ConnectorQueryGenericDateType type)
        {
            var field = Entity.FindField(fieldName);
            if (field == null) return null;

            var restriction = new ConnectorRestriction
            {
                Field = field,
                RestrictionType = ConnectorRestriction.ConnectorRestrictionType.Between
            };

            switch (type)
            {
                case ConnectorQueryGenericDateType.ThisWeek:
                    restriction.Values.Add(new ConnectorValue(ConnectorValue.ConnectorDateValueType.StartOfWeek));
                    restriction.Values.Add(new ConnectorValue(ConnectorValue.ConnectorDateValueType.EndOfWeek));
                    break;
                case ConnectorQueryGenericDateType.ThisMonth:
                    restriction.Values.Add(new ConnectorValue(ConnectorValue.ConnectorDateValueType.StartOfMonth));
                    restriction.Values.Add(new ConnectorValue(ConnectorValue.ConnectorDateValueType.EndOfMonth));
                    break;
                case ConnectorQueryGenericDateType.ThisYear:
                    restriction.Values.Add(new ConnectorValue(ConnectorValue.ConnectorDateValueType.StartOfYear));
                    restriction.Values.Add(new ConnectorValue(ConnectorValue.ConnectorDateValueType.EndOfYear));
                    break;
            }

            AddRestriction(restriction);
            return restriction;
        }

        public ConnectorRestriction AddFieldComparisonRestriction(string field, ConnectorRestriction.ConnectorRestrictionType type, string compareToField)
        {
            return AddRestriction(new ConnectorRestriction(Entity.FindField(field), type, Entity.FindField(compareToField)));
        }

        public bool HasBooleanRestriction(string fieldName, bool value = true)
        {
            foreach (var restriction in Restrictions.Where(restriction => restriction.Field.Name == fieldName))
            {
                switch (restriction.RestrictionType)
                {
                    case ConnectorRestriction.ConnectorRestrictionType.Equals:
                        if (value && restriction.Values[0].Constant == "true") return true;
                        if (!value && restriction.Values[0].Constant == "false") return true;
                        break;
                    case ConnectorRestriction.ConnectorRestrictionType.DoesNotEqual:
                        if (value && restriction.Values[0].Constant == "false") return true;
                        if (!value && restriction.Values[0].Constant == "true") return true;
                        break;
                }
            }
            return false;
        }

        public ConnectorRestriction FindRestrictionByFieldAndType(string fieldName, ConnectorRestriction.ConnectorRestrictionType type)
        {
            return HasOrConjunctives ? null : Restrictions.FirstOrDefault(item => item.MatchFieldAndType(fieldName, type));
        }

        #endregion

        #region Helpers

        private void AddField(ConnectorField field)
        {
            if (field != null) Fields.Add(field);
        }

        private ConnectorRestriction AddRestriction(ConnectorRestriction restriction)
        {
            restriction.ConjunctiveOperator = Restrictions.Count > 0 ? ConnectorRestriction.ConnectorRestrictionConjunctiveOperator.And : ConnectorRestriction.ConnectorRestrictionConjunctiveOperator.None;
            Restrictions.Add(restriction);
            return restriction;
        }

        private void ParseQueryXml(string queryXml)
        {
            var xmldoc = XElement.Parse(queryXml);
            ParseQueryXml(xmldoc);
        }

        private void ParseQueryXml(XElement xmldoc)
        {
            int entityId = XmlHelper.GetXmlNodeInt(xmldoc, "Entity");
            Entity = Connector.FindEntity(entityId);
            if (Entity == null) return;
            int maxRecords = XmlHelper.GetXmlNodeInt(xmldoc, "MaxRecords");
            MaxRecords = (maxRecords == 0) ? Entity.DefaultMaxRecords : maxRecords;
            ParseCompaniesXml(xmldoc);
            ParseFieldsXml(xmldoc);
            ParseRestrictionsXml(xmldoc);
            ParseOrderByXml(xmldoc);
        }

        private void ParseCompaniesXml(XElement xmldoc)
        {
            var companiesNode = XmlHelper.GetChildNode(xmldoc, "Companies");
            if (companiesNode == null) return;
            var companyNodes = from companyNode in companiesNode.Elements("Company") select companyNode;
            foreach (var companyId in companyNodes.Select(companyNode => int.Parse(companyNode.Value))) AddCompany(companyId);
        }

        private void ParseFieldsXml(XElement xmldoc)
        {
            var fieldsNode = XmlHelper.GetChildNode(xmldoc, "Fields");
            if (fieldsNode == null) return;
            var fieldNodes = from fieldNode in fieldsNode.Elements("Field") select fieldNode;
            foreach (var fieldId in fieldNodes.Select(fieldNode => int.Parse(fieldNode.Value))) AddField(fieldId);
        }

        private void ParseRestrictionsXml(XElement xmldoc)
        {
            var restrictionsNode = XmlHelper.GetChildNode(xmldoc, "Restrictions");
            if (restrictionsNode == null) return;
            var restrictionNodes = from restrictionNode in restrictionsNode.Elements("Restriction") select restrictionNode;
            foreach (var restriction in restrictionNodes.Select(restrictionNode => new ConnectorRestriction(restrictionNode, Entity))) Restrictions.Add(restriction);
        }

        private void ParseOrderByXml(XElement xmldoc)
        {
            var orderByNode = XmlHelper.GetChildNode(xmldoc, "OrderBy");
            if (orderByNode == null) return;
            var orderByFieldNodes = from orderByFieldNode in orderByNode.Elements("OrderByField") select orderByFieldNode;
            foreach (var orderByFieldNode in orderByFieldNodes)
            {
                int fieldId = XmlHelper.GetXmlNodeInt(orderByFieldNode, "Field");
                string sortOrderString = XmlHelper.GetXmlNodeString(orderByFieldNode, "SortOrder");
                if (string.IsNullOrWhiteSpace(sortOrderString))
                {
                    AddOrderBy(fieldId);
                }
                else
                {
                    var sortOrder = (ConnectorQuerySortOrder) Enum.Parse(typeof (ConnectorQuerySortOrder), sortOrderString);
                    AddOrderBy(fieldId, sortOrder);
                }
            }
        }

        #endregion
    }
}
