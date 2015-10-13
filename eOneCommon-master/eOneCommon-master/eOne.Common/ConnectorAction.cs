using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common
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
        public DataConnectorEntity BaseEntity { get; set; }
        public List<ConnectorRestriction> Conditions { get; set; }

        public void AddParameter(string name, ConnectorActionParameter.ConnectorActionParameterType type, DataConnectorField field)
        {
            if (field == null) return;
            var parameter = new ConnectorActionParameter { Field = field, Name = name, FieldType = field.Type, Type = type };
            Parameters.Add(parameter);
        }
        public void AddParameter(string name, ConnectorActionParameter.ConnectorActionParameterType type, DataConnectorFieldType fieldType)
        {
            if (fieldType == null) return;
            var parameter = new ConnectorActionParameter { Name = name, FieldType = fieldType, Type = type };
            Parameters.Add(parameter);
        }

        public void AddCondition(DataConnectorField field, ConnectorRestriction.ConnectorRestrictionType restrictionType, string value)
        {
            var condition = new ConnectorRestriction
            {
                FieldType = ConnectorRestriction.ConnectorRestrictionFieldType.Field,
                Field = field,
                RestrictionType = restrictionType,
                ConjunctiveOperator = Conditions.Count > 0 ? ConnectorRestriction.ConnectorRestrictionConjunctiveOperator.And : ConnectorRestriction.ConnectorRestrictionConjunctiveOperator.None
            };
            var conditionValue = new ConnectorRestrictionValue
            {
                Type = ConnectorRestrictionValue.ConnectorRestrictionValueType.Constant,
                Constant = value
            };
            condition.Values.Add(conditionValue);
            Conditions.Add(condition);
        }

    }
}
