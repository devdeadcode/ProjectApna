using System;
using System.Collections.Generic;
using System.Linq;
using eOne.Common.Connectors.Insightly.Models;
using eOne.Common.DataConnectors.Rest;

namespace eOne.Common.Connectors.Insightly
{
    public class InsightlyConnector : RestConnector
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
            AuthenticationType = RestConnectorAuthenticationType.Basic;
            SerializationType = RestConnectorSerializationType.Json;
            AddSetup();
        }

        #region Public methods

        public override void Initialise()
        {
            Username = Token;
            Password = string.Empty;
            AddEntity(EntityIdContacts, "Contacts", typeof(InsightlyContact));
            AddEntity(EntityIdContactAddresses, "Contact addresses", typeof(InsightlyContactAddress));
            AddEntity(EntityIdTasks, "Tasks", typeof(InsightlyTask));
            AddEntity(EntityIdOpportunities, "Opportunities", typeof(InsightlyOpportunity));
            AddEntity(EntityIdProjects, "Projects", typeof(InsightlyProject));
            AddEntity(EntityIdOrganizations, "Organizations", typeof(InsightlyOrganisation));
            AddEntity(EntityIdEmails, "Emails", typeof(InsightlyEmail));
            AddEntity(EntityIdUsers, "Users", typeof(InsightlyUser), true);
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

        #endregion

    }
}
