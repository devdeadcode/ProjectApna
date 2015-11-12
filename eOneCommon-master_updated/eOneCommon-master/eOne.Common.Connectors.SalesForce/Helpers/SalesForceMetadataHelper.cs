using eOne.Common.Connectors.SalesForce.Models.Metadata;

namespace eOne.Common.Connectors.SalesForce.Helpers
{
    public class SalesForceMetadataHelper
    {

        public static ConnectorField GetEntityField(ConnectorEntity entity, SalesForceField field, int id, int fieldNumber)
        {
            var entityField = new ConnectorField
            {
                Id = id,
                ApiName = field.name,
                DisplayName = Connector.GetValidDisplayName(field.label),
                Name = GetEntityFieldName(field.type, fieldNumber),
                Type = GetEntityFieldType(entity, field.type),
                DecimalPlaces = field.digits,
                ParentEntity = entity,
                SearchPriority = 2
            };
            return entityField;
        }

        private static string GetEntityFieldName(SalesForceField.SalesForceFieldType type, int id)
        {
            switch (type)
            {
                case SalesForceField.SalesForceFieldType.boolean:
                    return $"CustomBoolean{id}";
                case SalesForceField.SalesForceFieldType.currency:
                case SalesForceField.SalesForceFieldType.@double:
                case SalesForceField.SalesForceFieldType.percent:
                    return $"CustomAmount{id}";
                case SalesForceField.SalesForceFieldType.@int:
                    return $"CustomNumber{id}";
                case SalesForceField.SalesForceFieldType.date:
                case SalesForceField.SalesForceFieldType.datetime:
                    return $"CustomDate{id}";
                default:
                    return $"CustomString{id}";
            }
        }

        private static ConnectorFieldType GetEntityFieldType(ConnectorEntity entity, SalesForceField.SalesForceFieldType type)
        {
            switch (type)
            {
                case SalesForceField.SalesForceFieldType.date:
                case SalesForceField.SalesForceFieldType.datetime:
                    return entity.ParentConnector.FindFieldType(Connector.FieldTypeIdDate);
                case SalesForceField.SalesForceFieldType.@int:
                    return entity.ParentConnector.FindFieldType(Connector.FieldTypeIdInteger);
                case SalesForceField.SalesForceFieldType.picklist:
                case SalesForceField.SalesForceFieldType.@string:
                case SalesForceField.SalesForceFieldType.reference:
                case SalesForceField.SalesForceFieldType.textarea:
                case SalesForceField.SalesForceFieldType.Type:
                case SalesForceField.SalesForceFieldType.id:
                    return entity.ParentConnector.FindFieldType(Connector.FieldTypeIdString);
                case SalesForceField.SalesForceFieldType.@double:
                    return entity.ParentConnector.FindFieldType(Connector.FieldTypeIdQuantity);
                case SalesForceField.SalesForceFieldType.currency:
                    return entity.ParentConnector.FindFieldType(Connector.FieldTypeIdCurrency);
                case SalesForceField.SalesForceFieldType.boolean:
                    return entity.ParentConnector.FindFieldType(Connector.FieldTypeIdYesNo);
                case SalesForceField.SalesForceFieldType.phone:
                    return entity.ParentConnector.FindFieldType(Connector.FieldTypeIdPhone);
                case SalesForceField.SalesForceFieldType.url:
                    return entity.ParentConnector.FindFieldType(Connector.FieldTypeIdUrl);
                case SalesForceField.SalesForceFieldType.email:
                    return entity.ParentConnector.FindFieldType(Connector.FieldTypeIdEmail);
                case SalesForceField.SalesForceFieldType.percent:
                    return entity.ParentConnector.FindFieldType(Connector.FieldTypeIdPercentage);
            }
            return null;
        }
    }
}
