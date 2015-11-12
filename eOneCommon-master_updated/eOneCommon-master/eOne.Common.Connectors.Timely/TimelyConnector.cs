using System;
using System.Collections.Generic;
using System.Linq;
using eOne.Common.Actions;
using eOne.Common.Connectors.Service;
using eOne.Common.Connectors.Timely.Helpers;
using eOne.Common.Connectors.Timely.Models;
using eOne.Common.Query;

namespace eOne.Common.Connectors.Timely
{
    public class TimelyConnector : RestConnector
    {

        #region Constants

        public const int EntityIdClient = 1;
        public const int EntityIdProject = 2;
        public const int EntityIdUser = 3;
        public const int EntityIdEvent = 4;
        public const int EntityIdProjectSummary = 5;
        public const int EntityIdClientSummary = 6;
        public const int EntityIdProjectUsers = 7;

        public const int ActionIdArchiveClient = 1;
        public const int ActionIdUnarchiveClient = 2;
        public const int ActionIdAddEvent = 3;
        public const int ActionIdDeleteProject = 4;
        public const int ActionIdDeleteEvent = 5;

        #endregion

        public TimelyConnector()
        {
            Name = "Timely";
            Group = ConnectorGroup.TimeTracking;
            BaseUrl = "https://api.timelyapp.com/1.0/";

            AuthenticationType = ServiceConnectorAuthenticationType.OAuth2;
            Key = "da73103697355f6cf28c8772cd4f07254ad67817ac00f8d67831523042c2178b";
            Secret = "961ea7228155e6c0d320080c022e81df29d3c6c54fd94deb46bd503feb60befb";
            AuthorizationUri = $"{BaseUrl}oauth/authorize";
            AccessTokenUri = $"{BaseUrl}oauth/token"; 
            CallbackUrl = "http://www.popdock.com/callbacks/timely";

            Multicompany = true;
            CompanyPrompt = "Account";
            CompanyPluralPrompt = "Accounts";

            AddSetup();
        }

        #region Public methods

        public override void Initialise()
        {
            base.Initialise();

            // add companies
            var accounts = GetAccounts();
            if (accounts != null) foreach (var account in accounts) AddCompany(account.id, account.name);

            // add entities
            var clientEntity = AddEntity(EntityIdClient, "Clients", typeof(TimelyClient));
            var projectEntity = AddEntity(EntityIdProject, "Projects", typeof(TimelyProject));
            var userEntity = AddEntity(EntityIdUser, "Users", typeof(TimelyUser), true);
            var eventEntity = AddEntity(EntityIdEvent, "Events", typeof(TimelyEvent));
            var projectSummaryEntity = AddEntity(EntityIdProjectSummary, "Project summary", typeof(TimelyProjectEventSummary));
            var clientSummaryEntity = AddEntity(EntityIdClientSummary, "Client summary", typeof(TimelyClientEventSummary));
            var projectUserEntity = AddEntity(EntityIdProjectUsers, "Project users", typeof(TimelyProjectUser));

            // set default max records
            foreach (var entity in Entities) entity.DefaultMaxRecords = 100;

            // add relationships
            clientEntity.AddRelatedEntity("Projects", projectEntity, "id", "client_id");
            clientEntity.AddRelatedEntity("Events", projectEntity, "id", "client_id");
            projectEntity.AddRelatedEntity("Events", eventEntity, "id", "project_id");
            projectEntity.AddRelatedEntity("Users", projectUserEntity, "id", "project_id");
            userEntity.AddRelatedEntity("Events", eventEntity, "id", "user_id");
            userEntity.AddRelatedEntity("Projects", projectUserEntity, "id", "user_id");
            projectSummaryEntity.AddRelatedEntity("Events", eventEntity, "project_id", "project_id");
            clientSummaryEntity.AddRelatedEntity("Events", eventEntity, "client_id", "client_id");

            // add favorites
            TimelyFavoriteHelper.AddProjectFavorites(projectEntity);
            TimelyFavoriteHelper.AddEventFavorites(eventEntity);

            // add actions
            var archiveClientAction = clientEntity.AddAction(ActionIdArchiveClient, "Archive client", true);
            archiveClientAction.AddCondition("archived", ConnectorRestriction.ConnectorRestrictionType.Equals, "false");

            var activeClientAction = clientEntity.AddAction(ActionIdUnarchiveClient, "Make client active", true);
            activeClientAction.AddCondition("archived", ConnectorRestriction.ConnectorRestrictionType.Equals, "true");

            var addEventAction = projectEntity.AddAction(ActionIdAddEvent, "Add event", true);
            var dateParameter = addEventAction.AddParameter("Date", FieldTypeIdDate);
            dateParameter.DefaultValue = new ConnectorValue(ConnectorValue.ConnectorDateValueType.Today);
            addEventAction.AddParameter("Hours", FieldTypeIdQuantity);
            var noteParameter = addEventAction.AddParameter("Notes", FieldTypeIdString);
            noteParameter.Required = false;

            projectEntity.AddDeleteAction(ActionIdDeleteProject, "Delete project", "Are you sure you want to delete this project?");
            eventEntity.AddDeleteAction(ActionIdDeleteEvent, "Delete event", "Are you sure you want to delete this event?");

        }

        public override string GetEndpoint(ConnectorQuery query)
        {
            if (query == null || query.Companies.Count == 0) return string.Empty;
            switch (query.Entity.Id)
            {
                case EntityIdClient:
                    return GetIdRestrictedEndpoint(query, $"{query.Companies[0].Id}/clients?limit=100&offset={(query.Page - 1) * 100}");
                case EntityIdProject:
                case EntityIdProjectUsers:
                    return GetIdRestrictedEndpoint(query, $"{query.Companies[0].Id}/projects?limit=100&offset={(query.Page - 1) * 100}");
                case EntityIdUser:
                    return GetIdRestrictedEndpoint(query, $"{query.Companies[0].Id}/users?limit=100&offset={(query.Page - 1) * 100}");
                case EntityIdEvent:
                case EntityIdProjectSummary:
                case EntityIdClientSummary:
                    if (query.Restrictions.Count <= 0 || query.HasOrConjunctives) return $"{query.Companies[0].Id}/events";
                    foreach (var restriction in query.Restrictions.Where(restriction => restriction.RestrictionType == ConnectorRestriction.ConnectorRestrictionType.Equals))
                    {
                        switch (restriction.Field.Name)
                        {
                            // todo - handle user email and project name
                            case "user_id":
                                return $"{query.Companies[0].Id}/users/" + restriction.Values[0] + "/events";
                            case "project_id":
                                return $"{query.Companies[0].Id}/projects/" + restriction.Values[0] + "/events";
                        }
                    }
                    return $"{query.Companies[0].Id}/events";
            }
            return string.Empty;
        }

        public override IEnumerable<object> Deserialize(string data, ConnectorQuery query)
        {
            switch (query.Entity.Id)
            {
                case EntityIdClient:
                    return GetClients(query, data);
                case EntityIdProject:
                    return GetProjects(query, data);
                case EntityIdUser:
                    return GetUsers(query, data);
                case EntityIdEvent:
                    return GetEvents(query, data);
                case EntityIdProjectSummary:
                    return GetProjectSummary(query, data);
                case EntityIdClientSummary:
                    return GetClientSummary(query, data);
                case EntityIdProjectUsers:
                    return GetProjectUsers(query, data);
            }
            return null;
        }

        public override void RunAction(ConnectorAction action, List<Tuple<string, string>> parameters)
        {
            var endpoint = GetActionEndpoint(action.Id);
            var body = GetActionBody(action.Id, parameters);
            RunPostAction(endpoint, body);
        }

        #endregion

        #region Private methods

        private IEnumerable<TimelyAccount> GetAccounts()
        {
            // todo - add cache
            Headers = GetHeaders(null);
            var jsonData = GetResponse("accounts");
            // todo - handle filter of query by account name
            return jsonData == null ? null : DeserializeJson<List<TimelyAccount>>(jsonData);
        }
        private IEnumerable<TimelyClient> GetClients(ConnectorQuery query, string data)
        {
            var clients = new List<TimelyClient>();
            clients.AddRange(DeserializeJson<List<TimelyClient>>(data));
            var nextPage = (clients.Count > 0);
            while (nextPage)
            {
                query.Page++;
                var nextPageData = GetResponse(query);
                var nextPageClients = DeserializeJson<List<TimelyClient>>(nextPageData);
                clients.AddRange(nextPageClients);
                nextPage = (nextPageClients.Count > 0);
            }
            return clients;
        }
        private IEnumerable<TimelyProject> GetProjects(ConnectorQuery query, string data)
        {
            var projects = new List<TimelyProject>();
            projects.AddRange(DeserializeJson<List<TimelyProject>>(data));
            var nextPage = (projects.Count > 0);
            while (nextPage)
            {
                query.Page++;
                var nextPageData = GetResponse(query);
                var nextPageProjects = DeserializeJson<List<TimelyProject>>(nextPageData);
                projects.AddRange(nextPageProjects);
                nextPage = (nextPageProjects.Count > 0);
            }
            foreach (var project in projects) project.account_id = query.Companies[0].Id;
            return projects;
        }
        private IEnumerable<TimelyProjectUser> GetProjectUsers(ConnectorQuery query, string data)
        {
            var projects = GetProjects(query, data);
            var projectUsers = new List<TimelyProjectUser>();
            foreach (var project in projects) projectUsers.AddRange(project.users.Select(user => new TimelyProjectUser { Project = project, User = user }));
            var users = GetUsers();
            foreach (var projectUser in projectUsers)
            {
                var rate = projectUser.User.hour_rate;
                var user = FindUser(users, projectUser.User.id);
                if (user != null)
                {
                    projectUser.User = user;
                    projectUser.User.hour_rate = rate;
                }
            }
            foreach (var projectUser in projectUsers) projectUser.account_id = query.Companies[0].Id;
            return projectUsers;
        }
        private static TimelyUser FindUser(IEnumerable<TimelyUser> users, int id)
        {
            return users.FirstOrDefault(user => user.id == id);
        }
        private IEnumerable<TimelyUser> GetUsers(ConnectorQuery query, string data)
        {
            var users = new List<TimelyUser>();
            users.AddRange(DeserializeJson<List<TimelyUser>>(data));
            var nextPage = (users.Count > 0);
            while (nextPage)
            {
                query.Page++;
                var nextPageData = GetResponse(query);
                var nextPageUsers = DeserializeJson<List<TimelyUser>>(nextPageData);
                users.AddRange(nextPageUsers);
                nextPage = (nextPageUsers.Count > 0);
            }
            foreach (var user in users) user.account_id = query.Companies[0].Id;
            return users;
        }
        private IEnumerable<TimelyUser> GetUsers()
        {
            var data = GetResponse("users");
            return DeserializeJson<List<TimelyUser>>(data);
        }
        private static IEnumerable<TimelyEvent> GetEvents(ConnectorQuery query, string data)
        {
            // todo - handle paging
            var events = new List<TimelyEvent>();
            events.AddRange(DeserializeJson<List<TimelyEvent>>(data));
            return events;
        }
        private static IEnumerable<TimelyProjectEventSummary> GetProjectSummary(ConnectorQuery query, string data)
        {
            var events = GetEvents(query, data);
            var projectSummaries = new List<TimelyProjectEventSummary>();
            var projectId = 0;
            TimelyProjectEventSummary projectSummary = null;
            foreach (var projectEvent in events.OrderBy(x => x.project.id))
            {
                if (projectId != projectEvent.project.id)
                {
                    projectSummary = new TimelyProjectEventSummary { project = projectEvent.project };
                    projectSummaries.Add(projectSummary);
                    projectId = projectEvent.project.id;
                }
                projectSummary?.events.Add(projectEvent);
            }
            foreach (var project in projectSummaries) project.account_id = query.Companies[0].Id;
            return projectSummaries;
        }
        private static IEnumerable<TimelyClientEventSummary> GetClientSummary(ConnectorQuery query, string data)
        {
            var events = GetEvents(query, data);
            var clientSummaries = new List<TimelyClientEventSummary>();
            var clientId = 0;
            TimelyClientEventSummary clientSummary = null;
            foreach (var clientEvent in events.OrderBy(x => x.project.client.id))
            {
                if (clientId != clientEvent.project.client.id)
                {
                    clientSummary = new TimelyClientEventSummary { client = clientEvent.project.client };
                    clientSummaries.Add(clientSummary);
                    clientId = clientEvent.project.client.id;
                }
                clientSummary?.events.Add(clientEvent);
            }
            return clientSummaries;
        }
        private static string GetIdRestrictedEndpoint(ConnectorQuery query, string defaultEndpoint)
        {
            if (query.Restrictions.Count <= 0 || query.HasOrConjunctives) return defaultEndpoint;
            var restriction = query.FindRestrictionByFieldAndType("id", ConnectorRestriction.ConnectorRestrictionType.Equals);
            if (restriction != null) return defaultEndpoint + restriction.Values[0];
            return defaultEndpoint;
        }

        private static string GetActionEndpoint(int actionId)
        {
            switch (actionId)
            {
                case ActionIdArchiveClient:
                case ActionIdUnarchiveClient:
                    return "clients";
                case ActionIdAddEvent:
                case ActionIdDeleteEvent:
                    return "events";
                case ActionIdDeleteProject:
                    return "projects";
            }
            return string.Empty;
        }
        private static object GetActionBody(int actionId, List<Tuple<string, string>> parameters)
        {
            switch (actionId)
            {
                case ActionIdArchiveClient:
                    return new TimelyClient { id = int.Parse(FindParameterValue(parameters, "Client ID")), archived = true };
                case ActionIdUnarchiveClient:
                    return new TimelyClient { id = int.Parse(FindParameterValue(parameters, "Client ID")), archived = false };
                case ActionIdAddEvent:
                    var hours = decimal.Parse(FindParameterValue(parameters, "Hours"));
                    var wholeHours = Math.Floor(hours);
                    var minutes = (hours - wholeHours) * 60;
                    var newEvent = new TimelyEvent
                    {
                        project = new TimelyProject { id = int.Parse(FindParameterValue(parameters, "Project ID")) },
                        duration = new TimelyDuration { hours = wholeHours, minutes = minutes },
                        day = DateTime.Parse(FindParameterValue(parameters, "Date")),
                        note = FindParameterValue(parameters, "Note")
                    };
                    return newEvent;
                case ActionIdDeleteEvent:
                    return new TimelyEvent { id = int.Parse(FindParameterValue(parameters, "Event ID")) };
                case ActionIdDeleteProject:
                    return new TimelyProject { id = int.Parse(FindParameterValue(parameters, "Project ID")) };
            }
            return string.Empty;
        }

        #endregion

    }
}
