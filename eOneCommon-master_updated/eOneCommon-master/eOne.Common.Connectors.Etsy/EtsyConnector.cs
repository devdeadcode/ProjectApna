using System;
using System.Collections.Generic;
using eOne.Common.Actions;
using eOne.Common.Connectors.Etsy.Models;
using eOne.Common.Connectors.Service;
using eOne.Common.Query;

namespace eOne.Common.Connectors.Etsy
{
    public class EtsyConnector : RestConnector
    {

        #region Constants

        private const int EntityIdListing = 1;
        private const int EntityIdTransaction = 2;
        private const int EntityIdCoupon = 3;
        private const int EntityIdFeedback = 4;
        private const int EntityIdPayment = 5;
        private const int EntityIdReceipt = 6;
        private const int EntityIdCart = 7;
        private const int EntityIdCartListing = 8;

        private const int ActionIdActivateListing = 1;
        private const int ActionIdInactivateListing = 2;

        #endregion

        public EtsyConnector()
        {
            Name = "Etsy";
            Group = ConnectorGroup.WebStore;
            Multicompany = true;
            CompanyPrompt = "Shop";
            CompanyPrompt = "Shops";
            BaseUrl = "https://openapi.etsy.com/v2/";
            AuthenticationType = ServiceConnectorAuthenticationType.OAuth1;
            SerializationType = ServiceConnectorSerializationType.Json;

            // add rate limit of 10,000 requests per day
            AddRateLimit(ServiceConnectorRateLimiting.LimitAppliedTo.Account, 10000, ServiceConnectorRateLimiting.LimitPeriod.Day);
            
        }
        
        public override void Initialise()
        {
            base.Initialise();
            AddCompanies();
            AddEntities();
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

        public override string GetEndpoint(ConnectorQuery query)
        {
            switch (query.Entity.Id)
            {
                case EntityIdListing:
                    return $"/shops/{query.Companies[0].Id}/listings";
                case EntityIdTransaction:
                    return $"/shops/{query.Companies[0].Id}/transactions";
            }
            return string.Empty;
        }

        public override IEnumerable<object> Deserialize(string data, ConnectorQuery query)
        {
            switch (query.Entity.Id)
            {
                case EntityIdListing:
                    return DeserializeJson<List<EtsyListing>>(data);
                case EntityIdTransaction:
                    return DeserializeJson<List<EtsyTransaction>>(data);
                case EntityIdCoupon:
                    return DeserializeJson<List<EtsyCoupon>>(data);
                case EntityIdFeedback:
                    return DeserializeJson<List<EtsyFeedback>>(data);
            }
            return null;
        }

        private void AddCompanies()
        {
            var shopData = GetResponse("shops");
            var shops = DeserializeJson<List<EtsyShop>>(shopData);
            if (shops != null)
            {
                var id = 1;
                foreach (var shop in shops)
                {
                    var company = new ConnectorCompany
                    {
                        Id = id,
                        Name = shop.shop_name
                    };
                    Companies.Add(company);
                    id++;
                }
            }
        }
        private void AddEntities()
        {
            var listingEntity = AddEntity(EntityIdListing, "Listings", typeof(EtsyListing));
            AddListingActions(listingEntity);
            AddListingFavorites(listingEntity);

            AddEntity(EntityIdTransaction, "Transactions", typeof(EtsyTransaction));
            AddEntity(EntityIdCoupon, "Coupons", typeof(EtsyCoupon));
            AddEntity(EntityIdFeedback, "Feedback", typeof(EtsyFeedback));
            AddEntity(EntityIdPayment, "Payments", typeof(EtsyPayment));
            AddEntity(EntityIdReceipt, "Receipts", typeof(EtsyReceipt));
            AddEntity(EntityIdCart, "Carts", typeof(EtsyCart));
            AddEntity(EntityIdCartListing, "Cart listings", typeof(EtsyCartListing));

            // todo - add relationships
        }
        private static void AddListingActions(ConnectorEntity entity)
        {
            var activateAction = entity.AddAction(ActionIdActivateListing, "Set to active");
            activateAction.AllowMultipleSelection = true;
            activateAction.ConfirmationPrompt = "Are you sure you want to activate this listing?";
            activateAction.MultipleSelectionConfirmationPrompt = "Are you sure you want to activate these listings?";
            activateAction.AddParameter("listing_id", "listing_id");
            activateAction.AddCondition("state", ConnectorRestriction.ConnectorRestrictionType.DoesNotEqual, "active");

            var inactivateAction = entity.AddAction(ActionIdInactivateListing, "Set to inactive");
            activateAction.AllowMultipleSelection = true;
            activateAction.ConfirmationPrompt = "Are you sure you want to set this listing to inactive?";
            activateAction.MultipleSelectionConfirmationPrompt = "Are you sure you want to set these listings to inactive?";
            inactivateAction.AddParameter("listing_id", "listing_id");
            activateAction.AddCondition("state", ConnectorRestriction.ConnectorRestrictionType.Equals, "active");
        }
        private static void AddListingFavorites(ConnectorEntity entity)
        {
            var activeListingsFavorite = entity.AddFavorite("Active listings", true);
            activeListingsFavorite.Query.AddRestriction("listing_status", ConnectorRestriction.ConnectorRestrictionType.Equals, "ACTIVE");

            var soldoutListingsFavorite = entity.AddFavorite("Sold out listings", true);
            soldoutListingsFavorite.Query.AddRestriction("listing_status", ConnectorRestriction.ConnectorRestrictionType.Equals, "SOLD_OUT");

            var privateListingsFavorite = entity.AddFavorite("Sold out listings", true);
            privateListingsFavorite.Query.AddRestriction("listing_status", ConnectorRestriction.ConnectorRestrictionType.Equals, "PRIVATE");
        }

        private static string GetActionEndpoint(ConnectorAction action, IEnumerable<Tuple<string, string>> parameters)
        {
            switch (action.Id)
            {
                case ActionIdActivateListing:
                case ActionIdInactivateListing:
                    var listingId = FindParameterValue(parameters, "listing_id");
                    return string.IsNullOrWhiteSpace(listingId) ? string.Empty : $"listings/{listingId}";
            }
            return string.Empty;
        }
        private static RestConnectorMethod GetActionMethod(ConnectorAction action)
        {
            switch (action.Id)
            {
                case ActionIdActivateListing:
                case ActionIdInactivateListing:
                    return RestConnectorMethod.Put;
            }
            return 0;
        }
        private static object GetActionBody(ConnectorAction action, IEnumerable<Tuple<string, string>> parameters)
        {
            switch (action.Id)
            {
                case ActionIdActivateListing:
                    var activeListing = new EtsyListing
                    {
                        listing_id = int.Parse(FindParameterValue(parameters, "listing_id")),
                        state = "active"
                    };
                    return activeListing;
                case ActionIdInactivateListing:
                    var inactiveListing = new EtsyListing
                    {
                        listing_id = int.Parse(FindParameterValue(parameters, "listing_id")),
                        state = "inactive"
                    };
                    return inactiveListing;
            }
            return null;
        }


    }
}
