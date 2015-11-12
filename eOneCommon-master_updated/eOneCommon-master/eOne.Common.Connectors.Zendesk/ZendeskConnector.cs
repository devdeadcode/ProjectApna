using System;
using System.Collections.Generic;
using eOne.Common.Actions;
using eOne.Common.Connectors.Service;
using eOne.Common.Connectors.Zendesk.ActionModels;
using eOne.Common.Connectors.Zendesk.Models;
using eOne.Common.Query;
using eOne.Common.Setup;

namespace eOne.Common.Connectors.Zendesk
{
    public class ZendeskConnector : RestConnector
    {

        #region Entity IDs

        public const int EntityIdTicket = 1;
        public const int EntityIdUser = 2;
        public const int EntityIdTicketAudit = 3;
        public const int EntityIdTicketMetrics = 4;
        public const int EntityIdTicketComment = 5;

        public const int ActionIdChangeTicketStatus = 1;
        public const int ActionIdChangeTicketPriority = 2;
        public const int ActionIdAddTicketComment = 3;
        public const int ActionIdSetTicketSpam = 4;
        public const int ActionIdDeleteTicket = 5;

        #endregion

        public ZendeskConnector()
        {
            Name = "Zendesk";
            Group = ConnectorGroup.Helpdesk;
            
            Multicompany = true;
            CompanyPrompt = "Organization";
            CompanyPluralPrompt = "Organizations";

            AuthenticationType = ServiceConnectorAuthenticationType.Basic;
            Key = "";
            Secret = "";
            CallbackUrl = "http://www.popdock.com/callbacks/zendesk";

            AddRateLimit(ServiceConnectorRateLimiting.LimitAppliedTo.Account, 200, ServiceConnectorRateLimiting.LimitPeriod.Minute);

            AddSetup();
        }

        #region Public methods

        public override void Initialise()
        {
            base.Initialise();
            BaseUrl = $"https://{SitePrefix}.zendesk.com/";
            AuthorizationUri = $"{BaseUrl}oauth/authorizations/new";
            AccessTokenUri = $"{BaseUrl}oauth/tokens";
            BaseUrl = BaseUrl + "api/v2/";
            AddEntities();
            AddCompanies();
        }

        public override string GetEndpoint(ConnectorQuery query)
        {
            if (query == null) return string.Empty;
            switch (query.Entity.Id)
            {
                case EntityIdTicket:
                    return $"organizations/{query.Companies[0].Id}/tickets.json";
                case EntityIdUser:
                    var endpoint = $"organizations/{query.Companies[0].Id}/users.json";
                    var restriction = query.FindRestrictionByFieldAndType("role", ConnectorRestriction.ConnectorRestrictionType.Equals);
                    if (restriction != null) endpoint += $"?role={restriction.Values[0].Constant}";
                    return endpoint;
                case EntityIdTicketMetrics:
                    return "ticket_metrics.json";
            }
            return string.Empty;
        }

        public override IEnumerable<object> Deserialize(string data, ConnectorQuery query)
        {
            if (query == null || string.IsNullOrWhiteSpace(data)) return null;
            switch (query.Entity.Id)
            {
                case EntityIdTicket:
                    var tickets = DeserializeJson<ZendeskTickets>(data);
                    return tickets.tickets;
                case EntityIdUser:
                    var users = DeserializeJson<ZendeskUsers>(data);
                    return users.users;
                case EntityIdTicketMetrics:
                    var metrics = DeserializeJson<ZendeskTicketMetrics>(data);
                    return metrics.ticket_metrics;
            }
            return null;
        }

        public override void RunAction(ConnectorAction action, List<Tuple<string, string>> parameters)
        {
            var endpoint = GetActionEndpoint(action, parameters);
            var body = GetActionBody(action, parameters);
            switch (GetActionMethod(action))
            {
                case RestConnectorMethod.Put:
                    RunPutAction(endpoint, body);
                    return;
                case RestConnectorMethod.Post:
                    RunPostAction(endpoint, body);
                    return;
                case RestConnectorMethod.Delete:
                    RunDeleteAction(endpoint);
                    return;
            }
        }

        #endregion

        #region Private methods

        private new void AddSetup()
        {
            var step1 = new ConnectorSetup.ConnectorSetupStep
            {
                Number = 1,
                Header = "Connector Credentials",
                TopDescription = "With Zendesk, we need to get access to your accounts. " +
                                 "Please enter in a name for your new connector below. ",
                BottomDescription = "Click Next to grant access to your accounts."
            };
            step1.Fields.Add(new ConnectorSetupField(ConnectorSetupField.FieldNameName, "Connector name", ConnectorSetupField.ConnectorSetupFieldType.String, Name, true));
            step1.Fields.Add(new ConnectorSetupField(ConnectorSetupField.FieldNamePrefix, "Site prefix", ConnectorSetupField.ConnectorSetupFieldType.String, "", true));
            Setup.Steps.Add(step1);
            var step2 = new ConnectorSetup.ConnectorSetupStep
            {
                Number = 2,
                Header = "Complete installation",
                BottomDescription = "Click Finish to complete the installation."
            };
            Setup.Steps.Add(step2);
        }
        private static string GetActionEndpoint(ConnectorAction action, IEnumerable<Tuple<string, string>> parameters)
        {
            switch (action.Id)
            {
                case ActionIdChangeTicketStatus:
                case ActionIdChangeTicketPriority:
                case ActionIdAddTicketComment:
                case ActionIdDeleteTicket:
                    var ticketId = FindParameterValue(parameters, "id");
                    return string.IsNullOrWhiteSpace(ticketId) ? string.Empty : $"tickets/{ticketId}.json";
                case ActionIdSetTicketSpam:
                    var spamTicketId = FindParameterValue(parameters, "id");
                    return string.IsNullOrWhiteSpace(spamTicketId) ? string.Empty : $"tickets/{spamTicketId}/mark_as_spam.json";
            }
            return string.Empty;
        }
        private static RestConnectorMethod GetActionMethod(ConnectorAction action)
        {
            switch (action.Id)
            {
                case ActionIdChangeTicketStatus:
                case ActionIdChangeTicketPriority:
                case ActionIdAddTicketComment:
                    return RestConnectorMethod.Put;
            }
            return 0;
        }
        private static object GetActionBody(ConnectorAction action, List<Tuple<string, string>> parameters)
        {
            switch (action.Id)
            {
                case ActionIdChangeTicketStatus:
                    var ticketStatus = new ZendeskActionChangeTicketStatus
                    {
                        id = FindParameterValue(parameters, "id"),
                        status = FindParameterValue(parameters, "status")
                    };
                    return ticketStatus;
                case ActionIdChangeTicketPriority:
                    // todo
                    return null;
                case ActionIdAddTicketComment:
                    // todo
                    return null;
                case ActionIdDeleteTicket:
                    // todo
                    return null;
            }
            return null;
        }

        private void AddEntities()
        {
            var ticketEntity = AddEntity(EntityIdTicket, "Tickets", typeof(ZendeskTicket));
            ticketEntity.DefaultMaxRecords = 100;
            AddTicketActions(ticketEntity);
            AddTicketFavorites(ticketEntity);

            var userEntity = AddEntity(EntityIdUser, "Users", typeof(ZendeskUser));
            userEntity.DefaultMaxRecords = 100;
            userEntity.AddRelatedEntity("Tickets assigned to user", ticketEntity, "id", "assignee_id");

            var metricsEntity = AddEntity(EntityIdTicketMetrics, "Ticket metrics", typeof(ZendeskTicketMetric));
            metricsEntity.DefaultMaxRecords = 100;
        }
        private void AddCompanies()
        {
            var orgsJson = GetResponse("organizations.json");
            var orgList = DeserializeJson<ZendeskOrganizations>(orgsJson);
            foreach (var org in orgList.organizations) AddCompany(org.id, org.name);
        }
        private static void AddTicketActions(ConnectorEntity entity)
        {
            var changeTicketStatusAction = entity.AddUpdateFieldsAction(ActionIdChangeTicketStatus, "Change status", "status");
            changeTicketStatusAction.AllowMultipleSelection = true;

            var changeTicketPriorityAction = entity.AddUpdateFieldsAction(ActionIdChangeTicketStatus, "Change priority", "priority");
            changeTicketPriorityAction.AllowMultipleSelection = true;

            var addTicketCommentAction = entity.AddAction(ActionIdChangeTicketStatus, "Add comment", true);
            addTicketCommentAction.AllowMultipleSelection = true;
            addTicketCommentAction.AddParameter("Comment", FieldTypeIdString);

            entity.AddDeleteAction(ActionIdDeleteTicket, "Delete ticket", "Are you sure you want to delete this ticket?");

            var setTicketSpamAction = entity.AddAction(ActionIdChangeTicketStatus, "Set ticket as spam", true);
            setTicketSpamAction.AllowMultipleSelection = true;
        }
        private static void AddTicketFavorites(ConnectorEntity entity)
        {
            var openTicketsFavorite = entity.AddFavorite("Open tickets", true);
            openTicketsFavorite.Query.AddRestriction("status", ConnectorRestriction.ConnectorRestrictionType.Equals, "open");

            var newTicketsFavorite = entity.AddFavorite("New tickets", true);
            newTicketsFavorite.Query.AddRestriction("status", ConnectorRestriction.ConnectorRestrictionType.Equals, "new");

            var pendingTicketsFavorite = entity.AddFavorite("Pending tickets", true);
            pendingTicketsFavorite.Query.AddRestriction("status", ConnectorRestriction.ConnectorRestrictionType.Equals, "pending");

            var onholdTicketsFavorite = entity.AddFavorite("On hold tickets", true);
            onholdTicketsFavorite.Query.AddRestriction("status", ConnectorRestriction.ConnectorRestrictionType.Equals, "hold");

            var overdueTicketsFavorite = entity.AddFavorite("Overdue tickets");
            overdueTicketsFavorite.Query.AddFields("id", "priority", "subject", "created_at_date", "due_at_date", "days_overdue");
            overdueTicketsFavorite.Query.AddRestriction("days_overdue", ConnectorRestriction.ConnectorRestrictionType.GreaterThan, "0");
            overdueTicketsFavorite.Query.AddRestriction("status", ConnectorRestriction.ConnectorRestrictionType.Equals, "open");
        }

        #endregion

    }
}
