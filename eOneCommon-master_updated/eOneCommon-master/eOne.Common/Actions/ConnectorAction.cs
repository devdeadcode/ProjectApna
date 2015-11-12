using System.Collections.Generic;
using eOne.Common.Connectors;
using eOne.Common.Query;

namespace eOne.Common.Actions
{
    public class ConnectorAction
    {

        public ConnectorAction(int id, string description)
        {
            Id = id;
            Description = description;
            Parameters = new List<ConnectorActionParameter>();
            Conditions = new List<ConnectorRestriction>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public List<ConnectorActionParameter> Parameters { get; set; }
        public bool AllowMultipleSelection { get; set; }
        public string ConfirmationPrompt { get; set; }
        public string MultipleSelectionConfirmationPrompt { get; set; }
        public Connector BaseConnector { get; set; }
        public ConnectorEntity BaseEntity { get; set; }
        public List<ConnectorRestriction> Conditions { get; set; }

        public ConnectorActionParameter AddParameter(string name, string fieldName, ConnectorActionParameter.ConnectorActionParameterType type = ConnectorActionParameter.ConnectorActionParameterType.Field)
        {
            var field = BaseEntity?.FindField(fieldName);
            if (field == null) return null;
            var parameter = new ConnectorActionParameter
            {
                Field = field,
                Name = name,
                FieldType = field.Type,
                Type = type
            };
            Parameters.Add(parameter);
            return parameter;
        }
        public ConnectorActionParameter AddParameter(string name, int fieldTypeId)
        {
            var fieldType = BaseConnector?.FindFieldType(fieldTypeId);
            return fieldType == null ? null : AddParameter(name, fieldType);
        }
        public ConnectorActionParameter AddParameter(string name, ConnectorFieldType fieldType)
        {
            var parameter = new ConnectorActionParameter
            {
                Name = name,
                FieldType = fieldType,
                Type = ConnectorActionParameter.ConnectorActionParameterType.Value
            };
            Parameters.Add(parameter);
            return parameter;
        }

        public void AddCondition(string fieldName, ConnectorRestriction.ConnectorRestrictionType restrictionType, string value)
        {
            var field = BaseEntity.FindField(fieldName);
            if (field == null) return;
            var condition = new ConnectorRestriction
            {
                FieldType = ConnectorRestriction.ConnectorRestrictionFieldType.Field,
                Field = field,
                RestrictionType = restrictionType,
                ConjunctiveOperator = Conditions.Count > 0 ? ConnectorRestriction.ConnectorRestrictionConjunctiveOperator.And : ConnectorRestriction.ConnectorRestrictionConjunctiveOperator.None
            };
            var conditionValue = new ConnectorValue
            {
                Type = ConnectorValue.ConnectorValueType.Constant,
                Constant = value
            };
            condition.Values.Add(conditionValue);
            Conditions.Add(condition);
        }

    }
}
