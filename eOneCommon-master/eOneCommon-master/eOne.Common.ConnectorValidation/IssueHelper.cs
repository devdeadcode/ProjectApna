using System.Collections.Generic;
using System.Linq;
using System.Text;
using eOne.Common.DataConnectors;
using eOne.Common.DataConnectors.Rest;

namespace eOne.Common.ConnectorValidation
{
    class IssueHelper
    {

        public static List<string> CheckForEntityErrors(DataConnectorEntity entity)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(entity.Name)) errors.Add("Name is not defined.");
            if (string.IsNullOrWhiteSpace(entity.ItemName)) errors.Add("Item name is note defined.");

            // check field settings
            if (entity.Fields.Count == 0) errors.Add("The entity has no fields.");
            if (entity.DefaultFields.Count == 0) errors.Add("There are no default fields.");
            
            var fieldNames = new List<string>();
            foreach (var field in entity.Fields)
            {
                if (string.IsNullOrWhiteSpace(field.DisplayName))
                {
                    errors.Add($"The field {field.Name} has no display name.");
                }
                else
                {
                    fieldNames.Add(field.DisplayName);
                }
                if (field.Type.Id == DataConnector.FieldTypeIdEnum)
                {
                    if (field.ListItems.Count == 0) errors.Add($"The enum field {field.Name} has no items.");
                    foreach (var item in field.ListItems)
                    {
                        if (string.IsNullOrWhiteSpace(item.Value)) errors.Add($"The enum item {item.Item} in the field {field.Name} does not have a description.");
                    }
                }
                if (field.CreateDate && field.Type.Id != DataConnector.FieldTypeIdDate) errors.Add($"The field {field.Name} is not a date but set as a create date.");
                if (field.ModifyDate && field.Type.Id != DataConnector.FieldTypeIdDate) errors.Add($"The field {field.Name} is not a date but set as a modify date.");
                if (field.CreateTime && field.Type.Id != DataConnector.FieldTypeIdTime) errors.Add($"The field {field.Name} is not a time but set as a create time.");
                if (field.ModifyTime && field.Type.Id != DataConnector.FieldTypeIdTime) errors.Add($"The field {field.Name} is not a time but set as a modify time.");
            }

            // check for duplicate fields
            var duplicateFieldNames = fieldNames.GroupBy(field => field).SelectMany(grp => grp.Skip(1)).ToList();
            errors.AddRange(duplicateFieldNames.Select(duplicate => $"The field name '{duplicate}' is used more than once."));
            
            // check group settings
            if (entity.ParentConnector.Groups.Count > 0)
            {
                if (entity.Group == null) errors.Add("This entity does not have a group.");
                if (!entity.ParentConnector.Groups.Contains(entity.Group)) errors.Add("This entity is not part of a connector group.");
            }

            // check create and modify date settings
            var createDateCount = entity.Fields.Count(field => field.CreateDate);
            var createTimeCount = entity.Fields.Count(field => field.CreateTime);
            var modifyDateCount = entity.Fields.Count(field => field.ModifyDate);
            var modifyTimeCount = entity.Fields.Count(field => field.ModifyTime);
            if (createDateCount > 1) errors.Add("More than one create date defined.");
            if (createTimeCount > 1) errors.Add("More than one create time defined.");
            if (modifyDateCount > 1) errors.Add("More than one modify date defined.");
            if (modifyTimeCount > 1) errors.Add("More than one modify time defined.");
            if (createDateCount == 0 && createTimeCount >= 1) errors.Add("Created time is defined without created date.");
            if (modifyDateCount == 0 && modifyTimeCount >= 1) errors.Add("Modified time is defined without modified date.");

            return errors;
        }

        public static List<string> CheckForEntityWarnings(DataConnectorEntity entity)
        {
            var warnings = new List<string>();

            if (entity.DefaultMaxRecords == 0) warnings.Add("Default max records is set to zero.");
            if (entity.Fields.Count(field => field.KeyNumber > 0) == 0) warnings.Add("There are no key fields.");

            foreach (var field in entity.Fields)
            {
                if (!IsDisplayNameValid(field.DisplayName)) warnings.Add($"The display name '{field.DisplayName}' for field {field.Name} does not conform to the PopDock standard. It should be {GetValidDisplayName(field.DisplayName)}");
                if (field.Type.Id == DataConnector.FieldTypeIdEnum)
                {
                    foreach (var item in field.ListItems)
                    {
                        if (!IsDisplayNameValid(item.Value)) warnings.Add($"The list item '{item.Value}' for enum field {field.Name} does not conform to the PopDock standard. It should be {GetValidDisplayName(item.Value)}");
                    }
                }
            }
            if (entity.DefaultFields.Count == entity.Fields.Count) warnings.Add("All fields have been set to display by default.");

            if (entity.Fields.Count(field => field.CreateDate) == 0) warnings.Add("Create date is not defined.");
            if (entity.Fields.Count(field => field.ModifyDate) == 0) warnings.Add("Modify date is not defined.");

            return warnings;
        }

        public static List<string> CheckForConnectorErrors(DataConnector connector)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(connector.Name)) errors.Add("The name of the connector is not defined.");
            if (connector.Entities.Count == 0) errors.Add("There are no entities defined for the connector.");

            // check for multicompany errors
            if (connector.Multicompany)
            {
                if (string.IsNullOrWhiteSpace(connector.CompanyPrompt)) errors.Add("Company prompt is not defined.");
                if (string.IsNullOrWhiteSpace(connector.CompanyPluralPrompt)) errors.Add("Company plural prompt is not defined.");
                if (connector.Companies.Count == 0) errors.Add("There are no companies for this connector.");
            }

            // check for rest connector errors
            var restConnector = connector as RestConnector;
            if (restConnector != null)
            {
                if (string.IsNullOrWhiteSpace(restConnector.BaseUrl)) errors.Add("Base URL is not defined.");
                if (restConnector.AuthenticationType == RestConnector.RestConnectorAuthenticationType.OAuth2)
                {
                    if (string.IsNullOrWhiteSpace(restConnector.CallbackUrl)) errors.Add("Callback URL is not defined.");
                    if (string.IsNullOrWhiteSpace(restConnector.Key)) errors.Add("Key is not defined.");
                    if (string.IsNullOrWhiteSpace(restConnector.Secret)) errors.Add("Secret is not defined.");
                    if (string.IsNullOrWhiteSpace(restConnector.AuthorizationUri)) errors.Add("Authorization URL is not defined.");
                    if (string.IsNullOrWhiteSpace(restConnector.AccessTokenUri)) errors.Add("Access token URL is not defined.");
                }
            }

            // check for duplicate entity IDs
            var entityIds = connector.Entities.Select(entity => entity.Id).ToList();
            var duplicateEntityIds = entityIds.GroupBy(entity => entity).SelectMany(grp => grp.Skip(1)).ToList();
            foreach (var duplicate in duplicateEntityIds)
            {
                var entityNames = (from entity in connector.Entities where entity.Id == duplicate select entity.Name).ToList();
                errors.Add($"These entities ({string.Join(", ", entityNames)}) have the same entity ID.");
            }

            // check for duplicate groups
            var groupIds = connector.Groups.Select(group => group.Id).ToList();
            var groupNames = connector.Groups.Select(group => group.Name).ToList();
            var duplicateGroupIds = groupIds.GroupBy(field => field).SelectMany(grp => grp.Skip(1)).ToList();
            var duplicateGroupNames = groupNames.GroupBy(field => field).SelectMany(grp => grp.Skip(1)).ToList();
            errors.AddRange(duplicateGroupIds.Select(duplicate => $"The group ID '{duplicate}' is used more than once."));
            errors.AddRange(duplicateGroupNames.Select(duplicate => $"The group name '{duplicate}' is used more than once."));

            return errors;
        }

        public static List<string> CheckForConnectorWarnings(DataConnector connector)
        {
            var warnings = new List<string>();
            if (connector.Entities.Count >= 10 && connector.Groups.Count == 0) warnings.Add("There are more than 10 entities. Consider creating groups to organize them.");
            if (connector.Entities.Sum(entity => entity.Actions.Count) == 0) warnings.Add("There are no actions defined.");
            if (connector.Entities.Sum(entity => entity.RelatedEntities.Count) == 0) warnings.Add("There are no entity relationships defined.");
            if (connector.Entities.Sum(entity => entity.Favorites.Count) == 0) warnings.Add("There are no favorites defined.");
            var restConnector = connector as RestConnector;
            if (restConnector?.RateLimits.Count == 0) warnings.Add("Rate limits are not defined");
            return warnings;
        }

        public static string GetIssuesText(List<string> issues)
        {
            var builder = new StringBuilder();
            foreach (var issue in issues)
            {
                builder.AppendLine(issue);
            }
            return builder.ToString();
        }

        private static bool IsDisplayNameValid(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return true;
            var words = name.Split(' ');
            if (!char.IsUpper(words[0][0])) return false;
            if (words[0].Substring(1) != words[0].Substring(1).ToLower() && words[0].Substring(1) != words[0].Substring(1).ToUpper()) return false;
            if (words.Length > 1)
            {
                for (int i = 1; i < words.Length; i++)
                {
                    if (words[i] != words[i].ToLower() && words[i] != words[i].ToUpper()) return false;
                }
            }
            return true;
        }

        private static string GetValidDisplayName(string name)
        {
            var words = name.Split(' ');
            var validWords = words.Select(word => word.ToUpper() == word ? word : word.ToLower()).ToList();
            validWords[0] = validWords[0].First().ToString().ToUpper() + validWords[0].Substring(1);
            return string.Join(" ", validWords);
        }

    }
}
