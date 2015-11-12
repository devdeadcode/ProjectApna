using System;
using System.Collections.Generic;
using System.Linq;
using eOne.Common.Actions;
using eOne.Common.Connectors.HubSpot.Models;
using eOne.Common.Connectors.HubSpot.Models.ContractResolver;
using eOne.Common.Connectors.Service;
using eOne.Common.Helpers;
using eOne.Common.Query;

namespace eOne.Common.Connectors.HubSpot
{
    public class HubspotConnector : RestConnector
    {

        #region Constants

        #region Entity IDs

        public const int EntityIdContact = 1;
        public const int EntityIdContactList = 2;
        public const int EntityIdDeal = 3;
        public const int EntityIdOwner = 4;
        public const int EntityIdEvent = 5;

        #endregion

        #endregion

        public HubspotConnector()
        {
            Name = "Hubspot";
            Group = ConnectorGroup.CRM;
            BaseUrl = "https://api.hubapi.com";

            AuthenticationType = ServiceConnectorAuthenticationType.Custom;
            ClientId = "9dc3c01b-3380-11e5-8bdb-532b010dd8d8";
            Secret = "a269e4be-df4a-48e2-a55d-0ca0e7444856";
            Scope = "offline+contacts-rw+contacts-ro+blog-rw+blog-ro+events-rw+keyword-rw";
            AuthorizationUri = "https://app.hubspot.com/auth/authenticate/";
            CallbackUrl = "http://www.popdock.com/callbacks/hubspot";
            RefreshTokenUri = $"{BaseUrl}/auth/v1/refresh?refresh_token={0}&client_id={ClientId}&grant_type=refresh_token";

            // limited to 10,000 requests per day
            AddRateLimit(ServiceConnectorRateLimiting.LimitAppliedTo.Account, 10000, ServiceConnectorRateLimiting.LimitPeriod.Day);
        }

        #region Methods

        public override void Initialise()
        {
            base.Initialise();
            AddEntities();
        }

        public override List<Tuple<string, string>> GetParameters(ConnectorQuery query)
        {
            var parameters = TupleHelper.CreateTupleStringList("hapikey", Token);
            switch (query.Entity.Id)
            {
                case EntityIdContact:
                    TupleHelper.AppendTupleStringList(parameters, "showListMemberships", "true");
                    parameters.AddRange(query.FieldNamesUsed.Select(field => new Tuple<string, string>("property", field)));
                    break;
            }
            return parameters;
        }

        public override void RunAction(ConnectorAction action, List<Tuple<string, string>> parameters)
        {
            throw new NotImplementedException();
        }

        public override string GetEndpoint(ConnectorQuery query)
        {
            switch (query.Entity.Id)
            {
                case EntityIdContact:
                    return "contacts/v1/lists/all/contacts/all";
                case EntityIdContactList:
                    return "contacts/v1/lists";
                case EntityIdEvent:
                    if (!query.HasOrConjunctives)
                    {
                        var eventTypeRestriction = query.FindRestrictionByFieldAndType("eventType", ConnectorRestriction.ConnectorRestrictionType.Equals);
                        if (eventTypeRestriction != null)
                        {
                            switch (eventTypeRestriction.Values[0].Constant)
                            {
                                case "CONTENT":
                                    return "calendar/v1/events/content";
                                case "SOCIAL":
                                    return "calendar/v1/events/social";
                                case "PUBLISHING_TASK":
                                    return "calendar/v1/events/task";
                            }
                        }
                    }
                    return "calendar/v1/events";
                case EntityIdOwner:
                    return "owners/v2/owners";
            }
            return string.Empty;
        }

        public override IEnumerable<object> Deserialize(string data, ConnectorQuery query)
        {
            var resolver = new HubspotCustomContractResolver(query.Entity);
            switch (query.Entity.Id)
            {
                case EntityIdContact:
                    var contacts = DeserializeJson<HubspotContactCollection>(data, resolver);
                    return contacts.contacts;
                case EntityIdContactList:
                    var contactLists = DeserializeJson<HubspotContactListCollection>(data);
                    return contactLists.lists;
                case EntityIdEvent:
                    var events = DeserializeJson<HubspotCalendarEventCollection>(data);
                    return events.events;
                case EntityIdOwner:
                    return DeserializeJson<List<HubspotOwner>>(data);
            }
            return null;
        }

        #endregion

        #region Helpers

        private void AddEntities()
        {
            var contactEntity = AddEntity(EntityIdContact, "Contacts", typeof(HubspotContact));
            var contactListEntity = AddEntity(EntityIdContactList, "Contact lists", typeof(HubspotContactList));
            var eventEntity = AddEntity(EntityIdEvent, "Events", typeof(HubspotCalendarEvent));
            var ownerEntity = AddEntity(EntityIdOwner, "Owners", typeof(HubspotOwner));

            // add favorites
            AddContactFavorites(contactEntity);
            AddContactListFavorites(contactListEntity);
            AddEventFavorites(eventEntity);

            // add relationships


            foreach (var entity in Entities) entity.DefaultMaxRecords = 100;
        }

        private static void AddContactFavorites(ConnectorEntity entity)
        {
            var leadFavorite = entity.AddFavorite("Leads", true);
            leadFavorite.Query.AddFields("hs_lead_status");
            leadFavorite.Query.AddRestriction("lifecyclestage", ConnectorRestriction.ConnectorRestrictionType.Equals, "lead");

            var subscriberFavorite = entity.AddFavorite("Subscribers", true);
            subscriberFavorite.Query.AddRestriction("lifecyclestage", ConnectorRestriction.ConnectorRestrictionType.Equals, "subscriber");

            var marketingqualifiedleadFavorite = entity.AddFavorite("Marketing qualified leads", true);
            marketingqualifiedleadFavorite.Query.AddFields("hs_lead_status");
            marketingqualifiedleadFavorite.Query.AddRestriction("lifecyclestage", ConnectorRestriction.ConnectorRestrictionType.Equals, "marketingqualifiedlead");

            var salesqualifiedleadFavorite = entity.AddFavorite("Marketing qualified leads", true);
            salesqualifiedleadFavorite.Query.AddFields("hs_lead_status");
            salesqualifiedleadFavorite.Query.AddRestriction("lifecyclestage", ConnectorRestriction.ConnectorRestrictionType.Equals, "salesqualifiedlead");

            var opportunityFavorite = entity.AddFavorite("Opportunities", true);
            opportunityFavorite.Query.AddRestriction("lifecyclestage", ConnectorRestriction.ConnectorRestrictionType.Equals, "opportunity");

            var customerFavorite = entity.AddFavorite("Customers", true);
            customerFavorite.Query.AddRestriction("lifecyclestage", ConnectorRestriction.ConnectorRestrictionType.Equals, "customer");

            var evangelistFavorite = entity.AddFavorite("Evangelists", true);
            evangelistFavorite.Query.AddRestriction("lifecyclestage", ConnectorRestriction.ConnectorRestrictionType.Equals, "evangelist");
        }
        private static void AddContactListFavorites(ConnectorEntity entity)
        {
            var staticFavorite = entity.AddFavorite("Static lists");
            staticFavorite.Query.AddFields("name", "metaData_size");
            staticFavorite.Query.AddRestriction("listType", ConnectorRestriction.ConnectorRestrictionType.Equals, "STATIC");

            var smartFavorite = entity.AddFavorite("Smart lists");
            smartFavorite.Query.AddFields("name", "metaData_size");
            smartFavorite.Query.AddRestriction("listType", ConnectorRestriction.ConnectorRestrictionType.Equals, "DYNAMIC");
        }
        private static void AddEventFavorites(ConnectorEntity entity)
        {
            var todoFavorite = entity.AddFavorite("To do");
            todoFavorite.Query.AddFields("name", "owner_name", "event_date", "event_time");
            todoFavorite.Query.AddRestriction("eventType", ConnectorRestriction.ConnectorRestrictionType.Equals, "PUBLISHING_TASK");
            todoFavorite.Query.AddRestriction("state", ConnectorRestriction.ConnectorRestrictionType.Equals, "TODO");
        }

        #endregion

    }
}
