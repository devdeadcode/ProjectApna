using System;
using System.Collections.Generic;
using System.Linq;
using eOne.Common.Actions;
using eOne.Common.Connectors.Insightly.Models;
using eOne.Common.Connectors.Service;
using eOne.Common.Query;
using eOne.Common.Setup;

namespace eOne.Common.Connectors.Insightly
{
    public class InsightlyConnector : ODataConnector
    {

        #region Entity IDs

        public const int EntityIdContacts = 1;
        public const int EntityIdTasks = 2;
        public const int EntityIdOpportunities = 3;
        public const int EntityIdProjects = 4;
        public const int EntityIdOrganizations = 5;
        public const int EntityIdContactEmails = 6;
        public const int EntityIdContactAddresses = 7;
        public const int EntityIdEmails = 8;
        public const int EntityIdUsers = 9;

        #endregion

        public InsightlyConnector()
        {
            Name = "Insightly";
            Group = ConnectorGroup.CRM;
            BaseUrl = "https://api.insight.ly/v2.1/";
            AuthenticationType = ServiceConnectorAuthenticationType.Basic;
            SerializationType = ServiceConnectorSerializationType.Json;

            ODataVersion = ODataVersionNumber.v3_0;
            ODataAllowSelect = false;

            AddSetup();
        }

        #region Public methods

        public override void Initialise()
        {
            base.Initialise();

            Username = Token;
            Password = string.Empty;

            // add entities
            var contactEntity = AddEntity(EntityIdContacts, "Contacts", typeof(InsightlyContact));
            var contactAddressEntity = AddEntity(EntityIdContactAddresses, "Contact addresses", typeof(InsightlyContactAddress));
            var taskEntity = AddEntity(EntityIdTasks, "Tasks", typeof(InsightlyTask));
            var opportunityEntity = AddEntity(EntityIdOpportunities, "Opportunities", typeof(InsightlyOpportunity));
            var projectEntity = AddEntity(EntityIdProjects, "Projects", typeof(InsightlyProject));
            var organizationEntity = AddEntity(EntityIdOrganizations, "Organizations", typeof(InsightlyOrganisation));
            var emailEntity = AddEntity(EntityIdEmails, "Emails", typeof(InsightlyEmail));
            var userEntity = AddEntity(EntityIdUsers, "Users", typeof(InsightlyUser), true);

            // add relationships between entities
            contactEntity.AddRelatedEntity("Addresses", contactAddressEntity, "CONTACT_ID", "ContactId");
            organizationEntity.AddRelatedEntity("Contacts", contactEntity, "ORGANISATION_ID", "DEFAULT_LINKED_ORGANISATION");
            userEntity.AddRelatedEntity("Organizations", organizationEntity, "USER_ID", "OWNER_USER_ID");
            contactEntity.AddRelatedEntity("Emails", emailEntity, "CONTACT_ID", "ContactId");

            // todo - add actions

            // add entity favorites
            AddOpportunityFavorites(opportunityEntity);
            AddTaskFavorites(taskEntity);
            AddProjectFavorites(projectEntity);

            foreach (var entity in Entities) entity.DefaultMaxRecords = 1000;
        }

        public override string GetEndpoint(ConnectorQuery query)
        {
            if (query == null) return string.Empty;
            switch (query.Entity.Id)
            {
                case EntityIdContacts:
                case EntityIdContactAddresses:
                    return "Contacts";
                case EntityIdTasks:
                    return "Tasks";
                case EntityIdOpportunities:
                    return "Opportunities";
                case EntityIdProjects:
                    return "Projects";
                case EntityIdOrganizations:
                    return "Organisations";
                case EntityIdEmails:
                    return "Emails";
                case EntityIdUsers:
                    return "Users";
            }
            return string.Empty;
        }

        public override IEnumerable<object> Deserialize(string data, ConnectorQuery query)
        {
            switch (query.Entity.Id)
            {
                case EntityIdContacts:
                    return DeserializeJson<List<InsightlyContact>>(data);
                case EntityIdTasks:
                    return DeserializeJson<List<InsightlyTask>>(data);
                case EntityIdOpportunities:
                    return DeserializeJson<List<InsightlyOpportunity>>(data);
                case EntityIdProjects:
                    return DeserializeJson<List<InsightlyProject>>(data);
                case EntityIdOrganizations:
                    return DeserializeJson<List<InsightlyOrganisation>>(data);
                case EntityIdContactAddresses:
                    var contacts = DeserializeJson<List<InsightlyContact>>(data);
                    return (from contact in contacts from address in contact.ADDRESSES select new InsightlyContactAddress { Contact = contact, Address = address }).ToList();
                case EntityIdEmails:
                    return DeserializeJson<List<InsightlyEmail>>(data);
                case EntityIdUsers:
                    return DeserializeJson<List<InsightlyUser>>(data);
            }
            return null;
        }

        public override void RunAction(ConnectorAction action, List<Tuple<string, string>> parameters)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private methods

        private new void AddSetup()
        {
            var step1 = new ConnectorSetup.ConnectorSetupStep
            {
                Number = 1,
                Header = "Connector Credentials",
                TopDescription = "Please enter in a name for your new connector, along with your Insightly API Key. ",
                BottomDescription = "Click Finish to complete the installation."
            };
            step1.Fields.Add(new ConnectorSetupField(ConnectorSetupField.FieldNameName, "Connector name", ConnectorSetupField.ConnectorSetupFieldType.String, "Insightly", true));
            step1.Fields.Add(new ConnectorSetupField(ConnectorSetupField.FieldNameKey, "API Key", ConnectorSetupField.ConnectorSetupFieldType.String, "", true));
            Setup.Steps.Add(step1);
        }

        private static void AddOpportunityFavorites(ConnectorEntity entity)
        {
            var openOpportunityFavorite = entity.AddFavorite("Open opportunities", true);
            openOpportunityFavorite.Query.AddRestriction("OPPORTUNITY_STATE", ConnectorRestriction.ConnectorRestrictionType.Equals, "OPEN");

            var wonOpportunityFavorite = entity.AddFavorite("Won opportunities", true);
            wonOpportunityFavorite.Query.AddRestriction("OPPORTUNITY_STATE", ConnectorRestriction.ConnectorRestrictionType.Equals, "WON");

            var abandonedOpportunityFavorite = entity.AddFavorite("Abandoned opportunities", true);
            abandonedOpportunityFavorite.Query.AddRestriction("OPPORTUNITY_STATE", ConnectorRestriction.ConnectorRestrictionType.Equals, "ABANDONED");

            var suspendedOpportunityFavorite = entity.AddFavorite("Suspended opportunities", true);
            suspendedOpportunityFavorite.Query.AddRestriction("OPPORTUNITY_STATE", ConnectorRestriction.ConnectorRestrictionType.Equals, "SUSPENDED");

            var lostOpportunityFavorite = entity.AddFavorite("Lost opportunities", true);
            lostOpportunityFavorite.Query.AddRestriction("OPPORTUNITY_STATE", ConnectorRestriction.ConnectorRestrictionType.Equals, "LOST");

            var closingThisWeekFavorite = entity.AddFavorite("Closing this week", true);
            closingThisWeekFavorite.Query.AddField("FORECAST_CLOSE_DATE");
            closingThisWeekFavorite.Query.AddRestriction("OPPORTUNITY_STATE", ConnectorRestriction.ConnectorRestrictionType.Equals, "OPEN");
            var closingThisWeekRestriction = closingThisWeekFavorite.Query.AddRestriction("FORECAST_CLOSE_DATE", ConnectorRestriction.ConnectorRestrictionType.Between, ConnectorValue.ConnectorDateValueType.StartOfWeek);
            closingThisWeekRestriction.Values.Add(new ConnectorValue(ConnectorValue.ConnectorDateValueType.StartOfWeek));

            var pastForecastDateFavorite = entity.AddFavorite("Past forecast close date", true);
            pastForecastDateFavorite.Query.AddField("FORECAST_CLOSE_DATE");
            pastForecastDateFavorite.Query.AddRestriction("OPPORTUNITY_STATE", ConnectorRestriction.ConnectorRestrictionType.Equals, "OPEN");
            pastForecastDateFavorite.Query.AddRestriction("FORECAST_CLOSE_DATE", ConnectorRestriction.ConnectorRestrictionType.LessThan, ConnectorValue.ConnectorDateValueType.Today);
        }
        private static void AddTaskFavorites(ConnectorEntity entity)
        {
            var overdueTasksFavorite = entity.AddFavorite("Overdue", true);
            overdueTasksFavorite.Query.AddFields("DueDate");
            overdueTasksFavorite.Query.AddRestriction("DUE_DATE", ConnectorRestriction.ConnectorRestrictionType.LessThan, ConnectorValue.ConnectorDateValueType.Today);
            overdueTasksFavorite.Query.AddRestriction("COMPLETED", ConnectorRestriction.ConnectorRestrictionType.Equals, "false");
        }
        private static void AddProjectFavorites(ConnectorEntity entity)
        {
            var inProgressProjectsFavorite = entity.AddFavorite("In progress", true);
            inProgressProjectsFavorite.Query.AddRestriction("Completed", ConnectorRestriction.ConnectorRestrictionType.Equals, "false");

            var completeProjectsFavorite = entity.AddFavorite("Complete", true);
            completeProjectsFavorite.Query.AddRestriction("Completed", ConnectorRestriction.ConnectorRestrictionType.Equals, "true");
        }

        #endregion

    }
}
