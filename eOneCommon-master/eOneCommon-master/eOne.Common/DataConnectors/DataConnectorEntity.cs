using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace eOne.Common.DataConnectors
{
    public class DataConnectorEntity
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
            public ConnectorEntityJoin(DataConnectorField fromField, DataConnectorField toField)
            {
                FromField = fromField;
                ToField = toField;
            }
            public DataConnectorField FromField { get; set; }
            public DataConnectorField ToField { get; set; }
        }
        
        public class RelatedEntity
        {
            public RelatedEntity()
            {
                JoinFields = new List<ConnectorEntityJoin>();
                Restrictions = new List<ConnectorRestriction>();
            }
            
            public string Name { get; set; }
            public DataConnectorEntity Entity { get; set; }
            public RelatedEntityType Type { get; set; }
            public List<ConnectorEntityJoin> JoinFields { get; set; }
            public List<ConnectorRestriction> Restrictions { get; set; }
        }

        #endregion

        #region Constructors

        public DataConnectorEntity()
        {
            Fields = new List<DataConnectorField>();
            Tables = new List<DataConnectorTable>();
            Fields = new List<DataConnectorField>();
            IdFields = new List<DataConnectorField>();
            Restrictions = new List<ConnectorRestriction>();
            Actions = new List<ConnectorAction>();
            RelatedEntities = new List<RelatedEntity>();
            Favorites = new List<Favorite>();
        }
        public DataConnectorEntity(int id, string name, DataConnector connector)
        {
            Id = id;
            Name = name;
            ItemName = name;
            Enabled = true;
            ParentConnector = connector;
            Tables = new List<DataConnectorTable>();
            Fields = new List<DataConnectorField>();
            IdFields = new List<DataConnectorField>();
            Restrictions = new List<ConnectorRestriction>();
            Actions = new List<ConnectorAction>();
            RelatedEntities = new List<RelatedEntity>();
            Favorites = new List<Favorite>();
        }
        public DataConnectorEntity(int id, string name, Type model, DataConnector connector, bool cached = false)
        {
            Id = id;
            Name = name;
            ItemName = name;
            Cached = cached;
            Enabled = true;
            RecordType = model;
            ParentConnector = connector;
            ListType = typeof(List<>).MakeGenericType(model);
            Tables = new List<DataConnectorTable>();
            Fields = new List<DataConnectorField>();
            IdFields = new List<DataConnectorField>();
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
                    var field = new DataConnectorField
                    {
                        Id = fieldId,
                        Name = property.Name,
                        ParentEntity = this,
                        DisplayName = fieldSettings.DisplayName,
                        DefaultField = fieldSettings.DefaultField,
                        Type = connector.FindFieldType(fieldSettings.FieldTypeId),
                        KeyNumber = fieldSettings.KeyNumber,
                        SearchPriority = fieldSettings.SearchPriority,
                        Hidden = fieldSettings.Hidden
                        
                    };
                    if (fieldSettings.FieldsRequiredForCalculation != null)
                    {
                        field.FieldsRequiredForCalculation = fieldSettings.FieldsRequiredForCalculation.Split(',').ToList();
                    }
                    switch (field.Type.Id)
                    {
                        case DataConnector.FieldTypeIdDate:
                            field.ModifyDate = fieldSettings.Modified;
                            field.CreateDate = fieldSettings.Created;
                            break;
                        case DataConnector.FieldTypeIdTime:
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
                                field.ListItems.Add(new DataConnectorField.ConnectorFieldListItem(item, value));
                            }
                            else
                            {
                                field.ListItems.Add(new DataConnectorField.ConnectorFieldListItem(item, item));
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
        public List<DataConnectorField> DefaultFields
        {
            get
            {
                return Fields.Where(field => field.DefaultField).ToList();
            }
        }
        public int DefaultMaxRecords { get; set; }
        public string DefaultQuery
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
        public List<DataConnectorField> Fields { get; set; }
        public string Filename { get; set; }
        public DataConnectorEntityGroup Group { get; set; }
        public bool HasPopupWindow { get; set; }
        public int Id { get; set; }
        public List<DataConnectorField> IdFields { get; set; }
        public string IdValue { get; set; }
        public string ItemName { get; set; }
        public string Name { get; set; }
        public Type ListType { get; set; }
        public DataConnector ParentConnector { get; set; }
        public string Path { get; set; }
        public DataConnectorEntity PopupDetailsList { get; set; }
        public Type RecordType { get; set; }
        public List<RelatedEntity> RelatedEntities { get; set; }
        public List<ConnectorRestriction> Restrictions { get; set; }
        public bool SummaryList { get; set; }
        public List<DataConnectorTable> Tables { get; set; }

        public List<DataConnectorField> SearchStringFields
        {
            get
            {
                return Fields.Where(field => field.IsString && field.SearchPriority > 0).OrderBy(field => field.SearchPriority).ToList();
            }
        }
        public List<DataConnectorField> SearchNumericFields
        {
            get
            {
                return Fields.Where(field => field.IsNumeric && field.SearchPriority > 0).OrderBy(field => field.SearchPriority).ToList();
            }
        }

        #endregion

        #region Methods

        public ConnectorAction AddAction(int id, string description)
        {
            var action = new ConnectorAction(id, description) { BaseEntity = this };
            Actions.Add(action);
            return action;
        }
        public DataConnectorField AddCalculation(string calculation, string displayName, int typeId, bool defaultField = false)
        {
            var field = new DataConnectorField
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
        public DataConnectorField AddField(string name, string displayName, int typeId, bool defaultField = false)
        {
            var field = new DataConnectorField
            {
                Id = GetNextFieldId(),
                ParentEntity = this,
                Name = name,
                DisplayName = displayName,
                DefaultField = defaultField,
            };
            if (ParentConnector != null) field.Type = ParentConnector.FindFieldType(typeId);
            Fields.Add(field);
            return field;
        }
        public DataConnectorField AddField(string name, string displayName, int typeId, string table, bool defaultField = false)
        {
            var field = new DataConnectorField
            {
                Id = GetNextFieldId(),
                ParentEntity = this,
                Name = name,
                DisplayName = displayName,
                DefaultField = defaultField,
                Table = table
            };
            if (ParentConnector != null) field.Type = ParentConnector.FindFieldType(typeId);
            Fields.Add(field);
            return field;
        }
        public RelatedEntity AddRelatedEntity(string name, DataConnectorEntity entity, string fromFieldName, string toFieldName, RelatedEntityType type = RelatedEntityType.Detail)
        {
            var related = new RelatedEntity { Name = name, Entity = entity, Type = type };
            var fromField = FindField(fromFieldName);
            var toField = entity.FindField(toFieldName);
            var join = new ConnectorEntityJoin(fromField, toField);
            related.JoinFields.Add(join);
            RelatedEntities.Add(related);
            return related;
        }
        public DataConnectorTable AddScript(string script, string alias)
        {
            var table = new DataConnectorTable { Script = script, ParentEntity = this };
            Tables.Add(table);
            return table;
        }
        public DataConnectorTable AddScript(string script, string alias, string joinToTable, DataConnectorTable.DataConnectorTableJoinType joinType = DataConnectorTable.DataConnectorTableJoinType.Left)
        {
            var table = new DataConnectorTable
            {
                Script = script,
                ParentEntity = this,
                JoinType = joinType,
                JoinToTable = FindTable(joinToTable)
            };
            Tables.Add(table);
            return table;
        }
        public DataConnectorTable AddTable(string name)
        {
            var table = new DataConnectorTable { Name = name, ParentEntity = this };
            Tables.Add(table);
            return table;
        }
        public DataConnectorTable AddTable(string name, string joinToTable, DataConnectorTable.DataConnectorTableJoinType joinType = DataConnectorTable.DataConnectorTableJoinType.Left)
        {
            var table = new DataConnectorTable
            {
                Name = name,
                ParentEntity = this,
                JoinType = joinType,
                JoinToTable = FindTable(joinToTable)
            };
            Tables.Add(table);
            return table;
        }
        public DataConnectorField FindField(int fieldId)
        {
            return Fields.FirstOrDefault(field => field.Id == fieldId);
        }
        public DataConnectorField FindField(string fieldName)
        {
            return Fields.FirstOrDefault(field => field.Name == fieldName);
        }
        public DataConnectorTable FindTable(string name)
        {
            return Tables.FirstOrDefault(table => table.Name == name);
        }
        public ConnectorAction FindAction(int actionId)
        {
            return Actions.FirstOrDefault(action => action.Id == actionId);
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
