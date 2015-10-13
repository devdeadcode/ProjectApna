using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using eOne.Common.DataConnectors;
using eOne.Common.Helpers;

namespace eOne.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class ConnectorQuery
    {

        #region Enums

        public enum ConnectorQuerySortOrder { Ascending, Descending }

        #endregion

        #region Constructors

        public ConnectorQuery()
        {
            Companies = new List<DataConnectorCompany>();
            Fields = new List<DataConnectorField>();
            Restrictions = new List<ConnectorRestriction>();
            Page = 1;
        }
        public ConnectorQuery(DataConnector connector, string queryXml)
        {
            Connector = connector;
            Companies = new List<DataConnectorCompany>();
            Fields = new List<DataConnectorField>();
            Restrictions = new List<ConnectorRestriction>();
            OrderBy = new List<Tuple<DataConnectorField, ConnectorQuerySortOrder>>();
            ParseQueryXml(queryXml);
            Page = 1;
        }
        public ConnectorQuery(ConnectorSearch search)
        {
            Connector = search.Connector;
            Companies = search.Companies;
            Entity = search.Entity;
            Restrictions = Search.GetSearchRestrictions(search);
            Page = 1;
        }

        #endregion

        #region Properties

        public DataConnector Connector { get; set; }
        public DataConnectorEntity Entity { get; set; }
        public List<DataConnectorCompany> Companies { get; set; }
        public List<DataConnectorField> Fields { get; set; }
        public List<ConnectorRestriction> Restrictions { get; set; }
        public List<Tuple<DataConnectorField, ConnectorQuerySortOrder>> OrderBy { get; set; }
        public int MaxRecords { get; set; }
        public bool HasOrConjunctives
        {
            get
            {
                return Restrictions.Any(restriction => restriction.ConjunctiveOperator == ConnectorRestriction.ConnectorRestrictionConjunctiveOperator.Or);
            }
        }
        public bool ApplyHtmlFormatting { get; set; }
        public int Page { get; set; }

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
            AddField(Entity.FindField(fieldId));
        }
        public void AddField(string fieldName)
        {
            AddField(Entity.FindField(fieldName));
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
            if (field != null) OrderBy.Add(new Tuple<DataConnectorField, ConnectorQuerySortOrder>(field, sortOrder));
        }
        public ConnectorRestriction AddRestriction(string field, ConnectorRestriction.ConnectorRestrictionType type, string value)
        {
            return AddRestriction(new ConnectorRestriction(Entity.FindField(field), type, value));
        }
        public ConnectorRestriction AddRestriction(string field, ConnectorRestriction.ConnectorRestrictionType type, DateTime value)
        {
            return AddRestriction(new ConnectorRestriction(Entity.FindField(field), type, value));
        }
        public ConnectorRestriction AddRestriction(string field, ConnectorRestriction.ConnectorRestrictionType type, ConnectorRestrictionValue.ConnectorRestrictionDateValueType value)
        {
            return AddRestriction(new ConnectorRestriction(Entity.FindField(field), type, value));
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

        private void AddField(DataConnectorField field)
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
            int maxRecords = XmlHelper.GetXmlNodeInt(xmldoc, "MaxRecords");
            MaxRecords = (maxRecords == 0) ? Entity.DefaultMaxRecords: maxRecords;
            if (Entity == null) return;
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
                    var sortOrder = (ConnectorQuerySortOrder)Enum.Parse(typeof(ConnectorQuerySortOrder), sortOrderString);
                    AddOrderBy(fieldId, sortOrder);
                }
            }
        }

        #endregion

    }
}
