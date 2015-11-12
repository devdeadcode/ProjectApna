using System;
using System.Collections.Generic;
using eOne.Common.Actions;
using eOne.Common.Connectors.BaseCamp.Models;
using eOne.Common.Connectors.Service;
using eOne.Common.Query;

namespace eOne.Common.Connectors.BaseCamp
{
    public class BaseCampConnector: RestConnector
    {

        #region Constants

        public const int EntityIdProject = 1;
        public const int EntityIdPerson = 2;
        public const int EntityIdToDoListItem = 3;
        public const int EntityIdCalendarEvent = 4;

        #endregion

        public BaseCampConnector()
        {
            Name = "Basecamp";
            Group = ConnectorGroup.ProjectManagement;
            BaseUrl = "https://basecamp.com/999999999/api/v1/";
            AuthenticationType = ServiceConnectorAuthenticationType.OAuth2;
            AuthorizationUri = "https://launchpad.37signals.com/authorization/new";
            AccessTokenUri = "https://launchpad.37signals.com/authorization/token";
            CallbackUrl = "http://www.popdock.com/callbacks/basecamp";

            // rate limited to 500 requests per 10 second period
            AddRateLimit(ServiceConnectorRateLimiting.LimitAppliedTo.Account, 500, ServiceConnectorRateLimiting.LimitPeriod.Second, 10);

            AddSetup();
        }

        public override void Initialise()
        {
            base.Initialise();
            AddEntity(EntityIdProject, "Projects", typeof(BaseCampProject));
            AddEntity(EntityIdPerson, "People", typeof(BaseCampPerson));
            AddEntity(EntityIdToDoListItem, "To Do items", typeof(BaseCampToDo));
            AddEntity(EntityIdCalendarEvent, "Calendar events", typeof(BaseCampCalendarEvent));
        }

        public override string GetEndpoint(ConnectorQuery query)
        {
            if (query == null) return string.Empty;
            switch (query.Entity.Id)
            {
                case EntityIdProject:
                    if (query.Restrictions.Count > 0 && !query.HasOrConjunctives)
                    {
                        if (query.HasBooleanRestriction("draft")) return "projects/drafts.json";
                        if (query.HasBooleanRestriction("archived")) return "projects/archived.json";
                    }
                    return "projects.json";
                case EntityIdPerson:
                    if (query.Restrictions.Count > 0 && !query.HasOrConjunctives && query.HasBooleanRestriction("trashed")) return "people/trashed.json";
                    return "people.json";
            }
            return string.Empty;
        }

        public override IEnumerable<object> Deserialize(string data, ConnectorQuery query)
        {
            if (query == null) return null;
            switch (query.Entity.Id)
            {
                case EntityIdProject:
                    return DeserializeJson<List<BaseCampProject>>(data);
                case EntityIdPerson:
                    return DeserializeJson<List<BaseCampPerson>>(data);
                case EntityIdToDoListItem:
                    return DeserializeJson<List<BaseCampToDo>>(data);
                case EntityIdCalendarEvent:
                    return DeserializeJson<List<BaseCampCalendarEvent>>(data);
            }
            return null;
        }

        public override void RunAction(ConnectorAction action, List<Tuple<string, string>> parameters)
        {
            throw new NotImplementedException();
        }

    }
}
