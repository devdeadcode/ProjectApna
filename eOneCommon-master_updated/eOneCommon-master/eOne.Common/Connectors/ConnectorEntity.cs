using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using eOne.Common.Actions;
using eOne.Common.Helpers;
using eOne.Common.Query;

namespace eOne.Common.Connectors
{
    public class ConnectorEntity
    {

        #region Enums

        public enum RelatedEntityType
        {
            Transaction,
            Detail
        }

        #endregion

        #region Classes

        public class ConnectorEntityJoin
        {
            public ConnectorEntityJoin(ConnectorField fromField, ConnectorField toField)
            {
                FromField = fromField;
                ToField = toField;
            }
            public ConnectorField FromField { get; set; }
            public ConnectorField ToField { get; set; }
        }
        
        public class RelatedEntity
        {
            public RelatedEntity()
            {
                JoinFields = new List<ConnectorEntityJoin>();
                Restrictions = new List<ConnectorRestriction>();
            }
            
            public string Name { get; set; }
            public ConnectorEntity Entity { get; set; }
            public RelatedEntityType Type { get; set; }
            public List<ConnectorEntityJoin> JoinFields { get; set; }
            public List<ConnectorRestriction> Restrictions { get; set; }
        }

        #endregion

        #region Constructors

        public ConnectorEntity()
        {
            Fields = new List<ConnectorField>();
            Tables = new List<ConnectorTable>();
            Fields = new List<ConnectorField>();
            IdFields = new List<ConnectorField>();
            Restrictions = new List<ConnectorRestriction>();
            Actions = new List<ConnectorAction>();
            RelatedEntities = new List<RelatedEntity>();
            Favorites = new List<Favorite>();
        }
        public ConnectorEntity(int id, string name, Connector connector)
        {
            Id = id;
            Name = name;
            ItemName = name;
            Enabled = true;
            ParentConnector = connector;
            Tables = new List<ConnectorTable>();
            Fields = new List<ConnectorField>();
            IdFields = new List<ConnectorField>();
            Restrictions = new List<ConnectorRestriction>();
            Actions = new List<ConnectorAction>();
            RelatedEntities = new List<RelatedEntity>();
            Favorites = new List<Favorite>();
        }
        public ConnectorEntity(int id, string name, Type model, Connector connector, bool cached = false)
        {
            Id = id;
            Name = name;
            ItemName = name;
            Cached = cached;
            Enabled = true;
            RecordType = model;
            ParentConnector = connector;
            ListType = typeof(List<>).MakeGenericType(model);
            Tables = new List<ConnectorTable>();
            Fields = new List<ConnectorField>();
            IdFields = new List<ConnectorField>();
            Restrictions = new List<ConnectorRestriction>();
            Actions = new List<ConnectorAction>();
            RelatedEntities = new List<RelatedEntity>();
            Favorites = new List<Favorite>();
            var fieldId = 1;
            var properties = model.GetProperties();
            foreach (var property in properties)
            {
                // get field attributes - only add fields that have attributes
                var fieldSettingsAttribute = property.GetCustomAttribute(typeof(FieldSettingsAttribute), true);
                if (fieldSettingsAttribute != null)
                {
                    var fieldSettings = (FieldSettingsAttribute)fieldSettingsAttribute;
                    var field = new ConnectorField
                    {
                        Id = fieldId,
                        Name = property.Name,
                        ParentEntity = this,
                        DisplayName = fieldSettings.DisplayName,
                        DefaultField = fieldSettings.DefaultField,
                        Type = connector.FindFieldType(fieldSettings.FieldTypeId),
                        KeyNumber = fieldSettings.KeyNumber,
                        Hidden = fieldSettings.Hidden,
                        DisplayString = fieldSettings.Description,
                        SearchPriority = fieldSettings.SearchPriority,
                        ApiName = fieldSettings.ApiName
                    };
                    if (fieldSettings.SearchPriority != 1) field.SearchPriority = fieldSettings.DefaultField ? field.Type.DefaultSearchPriority : field.Type.SearchPriority;
                    if (fieldSettings.FieldsRequiredForCalculation != null) field.FieldsRequiredForCalculation = fieldSettings.FieldsRequiredForCalculation.Split(',').ToList();
                    switch (field.Type.Id)
                    {
                        case Connector.FieldTypeIdDate:
                            field.ModifyDate = fieldSettings.Modified;
                            field.CreateDate = fieldSettings.Created;
                            break;
                        case Connector.FieldTypeIdTime:
                            field.ModifyTime = fieldSettings.Modified;
                            field.CreateTime = fieldSettings.Created;
                            break;
                    }
                    if (fieldSettings.EnumType != null)
                    {
                        foreach (var item in Enum.GetNames(fieldSettings.EnumType))
                        {
                            var memberInfo = fieldSettings.EnumType.GetMember(item);
                            var descriptionAttribute = memberInfo[0].GetCustomAttribute(typeof(DescriptionAttribute), false);
                            if (descriptionAttribute != null)
                            {
                                var value = ((DescriptionAttribute)descriptionAttribute).Description;
                                field.ListItems.Add(new ConnectorField.ConnectorFieldListItem(item, value));
                            }
                            else
                            {
                                field.ListItems.Add(new ConnectorField.ConnectorFieldListItem(item, item));
                            }
                        }
                    }
                    Fields.Add(field);
                    fieldId++;
                }
            }
        }

        #endregion

        #region Properties

        public List<ConnectorAction> Actions { get; set; }
        public bool Cached { get; set; }
        public List<ConnectorField> DefaultFields
        {
            get
            {
                return Fields.Where(field => field.DefaultField).ToList();
            }
        }
        public int DefaultMaxRecords { get; set; }
        public ConnectorQuery DefaultQuery
        {
            get
            {
                var query = new ConnectorQuery
                {
                    Connector = ParentConnector,
                    Entity = this
                };
                if (ParentConnector.Multicompany)
                {
                    foreach (var company in ParentConnector.Companies) query.Companies.Add(company);
                }
                foreach (var field in Fields.Where(field => field.DefaultField)) query.Fields.Add(field);
                return query;
            }
        }
        public string DefaultQueryXml
        {
            get
            {
                var queryXml = new StringBuilder("<Query>");
                queryXml.Append($"<Connector>{ParentConnector.Id}</Connector><Entity>{Id}</Entity>");
                if (ParentConnector.Multicompany)
                {
                    var companies = ParentConnector.Companies.Aggregate("", (current, company) => current + $"<Company>{company.Id}</Company>");
                    queryXml.Append($"<Companies>{companies}</Companies>");
                }
                var defaultFields = Fields.Where(field => field.DefaultField).Aggregate("", (current, field) => current + $"<Field>{field.Id}</Field>");
                queryXml.Append($"<Fields>{defaultFields}</Fields>");
                queryXml.Append("</Query>");
                return queryXml.ToString();
            }
        }
        public bool Enabled { get; set; }
        public string Endpoint { get; set; }
        public List<Favorite> Favorites { get; set; }
        public List<ConnectorField> Fields { get; set; }
        public string Filename { get; set; }
        public ConnectorEntityGroup Group { get; set; }
        public bool HasPopupWindow { get; set; }
        public int Id { get; set; }
        public List<ConnectorField> IdFields { get; set; }
        public string IdValue { get; set; }
        public string ItemName { get; set; }
        public string Name { get; set; }
        public Type ListType { get; set; }
        public Connector ParentConnector { get; set; }
        public string Path { get; set; }
        public ConnectorEntity PopupDetailsList { get; set; }
        public Type RecordType { get; set; }
        public List<RelatedEntity> RelatedEntities { get; set; }
        public List<ConnectorRestriction> Restrictions { get; set; }
        public bool SummaryList { get; set; }
        public List<ConnectorTable> Tables { get; set; }
        public string XmlName => XmlHelper.GetXmlEntityName(Name);
        public ConnectorField CreateDateField => Fields.FirstOrDefault(field => field.CreateDate);
        public ConnectorField CreateTimeField => Fields.FirstOrDefault(field => field.CreateTime);
        public ConnectorField ModifyDateField => Fields.FirstOrDefault(field => field.ModifyDate);
        public ConnectorField ModifyTimeField => Fields.FirstOrDefault(field => field.ModifyTime);
        public bool ApiFieldsUsed
        {
            get
            {
                return Fields.Any(field => !string.IsNullOrWhiteSpace(field.ApiName));
            }
        }

        public List<ConnectorField> SearchStringFields
        {
            get
            {
                return Fields.Where(field => field.IsString && field.SearchPriority > 0).OrderByDescending(field => field.SearchPriority).ToList();
            }
        }
        public List<ConnectorField> SearchNumericFields
        {
            get
            {
                return Fields.Where(field => field.IsNumeric && field.SearchPriority > 0).OrderBy(field => field.SearchPriority).ToList();
            }
        }

        #endregion

        #region Methods

        public ConnectorAction AddAction(int id, string description, bool addKeyFields = false)
        {
            var action = new ConnectorAction(id, description)
            {
                BaseEntity = this,
                BaseConnector = ParentConnector
            };
            if (addKeyFields)
            {
                foreach (var field in Fields.Where(field => field.KeyNumber > 0)) action.AddParameter(field.DisplayName, field.Name);
            }
            Actions.Add(action);
            return action;
        }
        public ConnectorAction AddUpdateFieldsAction(int id, string description, params string[] fieldNames)
        {
            var action = AddAction(id, description, true);
            foreach (var field in fieldNames.Select(FindField).Where(field => field != null))
            {
                action.AddParameter(field.DisplayName, field.Name, ConnectorActionParameter.ConnectorActionParameterType.Value);
            }
            return action;
        }
        public ConnectorAction AddDeleteAction(int id, string description, string prompt)
        {
            var action = AddAction(id, description, true);
            action.ConfirmationPrompt = prompt;
            return action;
        }
        public ConnectorField AddCalculation(string calculation, string displayName, int typeId, bool defaultField = false)
        {
            var field = new ConnectorField
            {
                Id = GetNextFieldId(),
                Calculation = calculation,
                ParentEntity = this,
                DisplayName = displayName,
                DefaultField = defaultField,
            };
            if (ParentConnector != null) field.Type = ParentConnector.FindFieldType(typeId);
            Fields.Add(field);
            return field;
        }
        public Favorite AddFavorite(string name, bool addDefaultFields = false)
        {
            var favorite = new Favorite
            {
                Name = name,
                Query = {Entity = this}
            };
            if (addDefaultFields) favorite.Query.AddDefaultFields();
            Favorites.Add(favorite);
            return favorite;
        }
        public ConnectorField AddField(string name, string displayName, int typeId, bool defaultField = false)
        {
            var field = new ConnectorField
            {
                Id = GetNextFieldId(),
                ParentEntity = this,
                Name = name,
                DisplayName = displayName,
                DefaultField = defaultField
            };
            if (ParentConnector != null) field.Type = ParentConnector.FindFieldType(typeId);
            field.SearchPriority = defaultField ? 2 : 1;
            Fields.Add(field);
            return field;
        }
        public ConnectorField AddField(string name, string displayName, int typeId, string table, bool defaultField = false)
        {
            var field = new ConnectorField
            {
                Id = GetNextFieldId(),
                ParentEntity = this,
                Name = name,
                DisplayName = displayName,
                DefaultField = defaultField,
                Table = table
            };
            if (ParentConnector != null) field.Type = ParentConnector.FindFieldType(typeId);
            field.SearchPriority = defaultField ? 2 : 1;
            Fields.Add(field);
            return field;
        }
        public void RemoveField(int id)
        {
            var field = FindField(id);
            if (field != null) Fields.Remove(field);
        }
        public void RemoveField(string name)
        {
            var field = FindField(name);
            if (field != null) Fields.Remove(field);
        }
        public RelatedEntity AddRelatedEntity(string name, ConnectorEntity entity, string fromFieldName, string toFieldName, RelatedEntityType type = RelatedEntityType.Detail)
        {
            var related = new RelatedEntity { Name = name, Entity = entity, Type = type };
            var fromField = FindField(fromFieldName);
            var toField = entity.FindField(toFieldName);
            var join = new ConnectorEntityJoin(fromField, toField);
            related.JoinFields.Add(join);
            RelatedEntities.Add(related);
            return related;
        }
        public ConnectorTable AddScript(string script, string alias)
        {
            var table = new ConnectorTable { Script = script, ParentEntity = this };
            Tables.Add(table);
            return table;
        }
        public ConnectorTable AddScript(string script, string alias, string joinToTable, ConnectorTable.ConnectorTableJoinType joinType = ConnectorTable.ConnectorTableJoinType.Left)
        {
            var table = new ConnectorTable
            {
                Script = script,
                ParentEntity = this,
                JoinType = joinType,
                JoinToTable = FindTable(joinToTable)
            };
            Tables.Add(table);
            return table;
        }
        public ConnectorTable AddTable(string name)
        {
            var table = new ConnectorTable { Name = name, ParentEntity = this };
            Tables.Add(table);
            return table;
        }
        public ConnectorTable AddTable(string name, string joinToTable, ConnectorTable.ConnectorTableJoinType joinType = ConnectorTable.ConnectorTableJoinType.Left)
        {
            var table = new ConnectorTable
            {
                Name = name,
                ParentEntity = this,
                JoinType = joinType,
                JoinToTable = FindTable(joinToTable)
            };
            Tables.Add(table);
            return table;
        }
        public ConnectorField FindField(int fieldId)
        {
            return Fields.FirstOrDefault(field => field.Id == fieldId);
        }
        public ConnectorField FindField(string fieldName)
        {
            return Fields.FirstOrDefault(field => field.Name == fieldName);
        }
        public ConnectorTable FindTable(string name)
        {
            return Tables.FirstOrDefault(table => table.Name == name);
        }
        public ConnectorAction FindAction(int actionId)
        {
            return Actions.FirstOrDefault(action => action.Id == actionId);
        }

        public List<ConnectorField> FindFieldsByType(int fieldTypeId)
        {
            return Fields.Where(field => field.Type.Id == fieldTypeId).ToList();
        }

        public int GetNextFieldId()
        {
            return Fields.Select(field => field.Id).Concat(new[] {0}).Max() + 1;
        }
        public override string ToString()
        {
            return Name;
        }

        #endregion

    }
}
