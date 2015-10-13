using System;
using System.Collections.Generic;
using System.Linq;
using eOne.Common.Connectors.Shopify.Models;
using eOne.Common.DataConnectors;
using eOne.Common.DataConnectors.Rest;

namespace eOne.Common.Connectors.Shopify 
{
    public class ShopifyConnector : RestConnector
    {

        #region Constants

        public const int EntityIdCustomer = 1;
        public const int EntityIdAddress = 2;
        public const int EntityIdOrder = 3;
        public const int EntityIdOrderLine = 4;
        public const int EntityIdProduct = 5;

        public const int ActionIdAddOrderNote = 1;
        public const int ActionIdCloseOrder = 2;
        public const int ActionIdCancelOrder = 3;
        public const int ActionIdDeleteOrder = 4;
        public const int ActionIdReopenOrder = 5;
        public const int ActionIdPublishProduct = 6;
        public const int ActionIdUnpublishProduct = 7;

        #endregion

        public ShopifyConnector()
        {
            Name = "Shopify";
            Group = ConnectorGroup.WebStore;
            Key = "3197fcb70cb18f8ec65dc789a3462a27";
            Secret = "7c79434fdcaee2b5a49786d69cead597";
            AuthenticationType = RestConnectorAuthenticationType.OAuth2;
            AuthorizationUri = "https://{0}.myshopify.com/admin/oauth/authorize";
            AccessTokenUri = "https://{shop}.myshopify.com/admin/oauth/access_token";
            CallbackUrl = "http://www.popdock.com/callbacks/shopify";
            Scope = "read_products,read_customers,read_orders";
            AddSetup();
            var rateLimit = new RestConnectorRateLimiting
            {
                AppliedTo = RestConnectorRateLimiting.LimitAppliedTo.Account,
                Requests = 2,
                NumberOfPeriods = 1,
                Period = RestConnectorRateLimiting.LimitPeriod.Second
            };
            RateLimits.Add(rateLimit);
        }

        #region Public methods

        public override void Initialise()
        {
            SitePrefix = Setup.Steps[0].Fields[1].Value;
            BaseUrl = $"https://{SitePrefix}.myshopify.com/admin/";
            AddEntities();
        }

        public override string GetEndpoint(ConnectorQuery query)
        {
            if (query == null) return string.Empty;
            switch (query.Entity.Id)
            {
                case EntityIdCustomer:
                case EntityIdAddress:
                    return "customers.json";
                case EntityIdOrder:
                case EntityIdOrderLine:
                    return "orders.json";
                case EntityIdProduct:
                    return "products.json";
            }
            return string.Empty;
        }

        public override IEnumerable<object> Deserialize(string data, ConnectorQuery query)
        {
            switch (query.Entity.Id)
            {
                case EntityIdCustomer:
                case EntityIdAddress:
                    var customerList = DeserializeJson<ShopifyCustomerList>(data);
                    if (query.Entity.Id == EntityIdCustomer) return customerList.customers;
                    return (from customer in customerList.customers from address in customer.addresses select new ShopifyCustomerAddress { Customer = customer, Address = address }).ToList();
                case EntityIdOrder:
                case EntityIdOrderLine:
                    var orderList = DeserializeJson<ShopifyOrderList>(data);
                    if (query.Entity.Id == EntityIdOrder) return orderList.orders;
                    var orderLines = new List<ShopifyOrderLines>();
                    var productJson = GetResponse("products.json");
                    var products = DeserializeJson<ShopifyProductList>(productJson);
                    foreach (var order in orderList.orders)
                    {
                        foreach (var line in order.line_items)
                        {
                            var productId = line.product_id;
                            var product = new ShopifyProduct();
                            foreach (var prod in products.products.Where(prod => prod.id == productId)) product = prod;
                            orderLines.Add(new ShopifyOrderLines { Order = order, Line = line, Product = product });
                        }
                    }
                    return orderLines;
                case EntityIdProduct:
                    var productList = DeserializeJson<ShopifyProductList>(data);
                    foreach (var product in productList.products) product.site_prefix = SitePrefix;
                    return productList.products;
            }
            return null;
        }

        public override List<Tuple<string, string>> GetHeaders(DataConnectorEntity entity)
        {
            return new List<Tuple<string, string>> { new Tuple<string, string>("X-Shopify-Access-Token", Token) };
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
                TopDescription = "With Shopify, we need to get access to your shop. " +
                                 "Please enter in a name for your new connector and your shop name below. " +
                                 "Your shop name is the first part of your shops url: http://{yourshopname}.myshopify.com/",
                BottomDescription = "Click Next to grant access to your shop."
            };
            step1.Fields.Add(new ConnectorSetupField(ConnectorSetupField.FieldNameName, "Connector name", ConnectorSetupField.ConnectorSetupFieldType.String, "Shopify", true));
            step1.Fields.Add(new ConnectorSetupField(ConnectorSetupField.FieldNamePrefix, "Shop name", ConnectorSetupField.ConnectorSetupFieldType.String, "", true));
            Setup.Steps.Add(step1);
            var step2 = new ConnectorSetup.ConnectorSetupStep
            {
                Number = 2,
                Header = "Complete installation",
                BottomDescription = "Click Finish to complete the installation."
            };
            Setup.Steps.Add(step2);
        }

        private void AddEntities()
        {
            var customerEntity = AddEntity(EntityIdCustomer, "Customers", typeof(ShopifyCustomer));
            var customerAddressEntity = AddEntity(EntityIdAddress, "Customer addresses", typeof(ShopifyCustomerAddress));
            
            var orderEntity = AddEntity(EntityIdOrder, "Orders", typeof(ShopifyOrder));
            AddOrderActions(orderEntity);
            AddOrderFavorites(orderEntity);

            var orderLinesEntity = AddEntity(EntityIdOrderLine, "Order lines", typeof(ShopifyOrderLines));

            var productEntity = AddEntity(EntityIdProduct, "Products", typeof(ShopifyProduct), true);
            AddProductActions(productEntity);

            customerEntity.AddRelatedEntity("Addresses", customerAddressEntity, "id", "CustomerId");
            customerEntity.AddRelatedEntity("Orders", orderEntity, "id", "customer_id");
            orderEntity.AddRelatedEntity("Order lines", orderLinesEntity, "id", "OrderId");
        }

        private void AddOrderActions(DataConnectorEntity entity)
        {
            var addOrderNoteAction = entity.AddAction(ActionIdAddOrderNote, "Add note");
            addOrderNoteAction.AddParameter("id", ConnectorActionParameter.ConnectorActionParameterType.Field, entity.FindField("id"));
            addOrderNoteAction.AddParameter("Note", ConnectorActionParameter.ConnectorActionParameterType.Value, FindFieldType(FieldTypeIdString));

            var closeOrderAction = entity.AddAction(ActionIdAddOrderNote, "Close order");
            closeOrderAction.AllowMultipleSelection = true;
            closeOrderAction.ConfirmationPrompt = "Are you sure you want to close this order?";
            closeOrderAction.MultipleSelectionConfirmationPrompt = "Are you sure you want to close these orders?";
            closeOrderAction.AddParameter("id", ConnectorActionParameter.ConnectorActionParameterType.Field, entity.FindField("id"));
            closeOrderAction.AddCondition(entity.FindField("closed"), ConnectorRestriction.ConnectorRestrictionType.Equals, "false");

            var cancelOrderAction = entity.AddAction(ActionIdAddOrderNote, "Cancel order");
            cancelOrderAction.AllowMultipleSelection = true;
            cancelOrderAction.ConfirmationPrompt = "Are you sure you want to cancel this order?";
            cancelOrderAction.MultipleSelectionConfirmationPrompt = "Are you sure you want to canel these orders?";
            cancelOrderAction.AddParameter("id", ConnectorActionParameter.ConnectorActionParameterType.Field, entity.FindField("id"));
            cancelOrderAction.AddCondition(entity.FindField("cancelled"), ConnectorRestriction.ConnectorRestrictionType.Equals, "false");

            var reopenOrderAction = entity.AddAction(ActionIdAddOrderNote, "Re-open order");
            reopenOrderAction.AllowMultipleSelection = true;
            reopenOrderAction.AddParameter("id", ConnectorActionParameter.ConnectorActionParameterType.Field, entity.FindField("id"));
            reopenOrderAction.AddCondition(entity.FindField("closed"), ConnectorRestriction.ConnectorRestrictionType.Equals, "true");

            var deleteOrderAction = entity.AddAction(ActionIdAddOrderNote, "Delete order");
            deleteOrderAction.AllowMultipleSelection = true;
            deleteOrderAction.ConfirmationPrompt = "Are you sure you want to delete this order?";
            deleteOrderAction.MultipleSelectionConfirmationPrompt = "Are you sure you want to delete these orders?";
            deleteOrderAction.AddParameter("id", ConnectorActionParameter.ConnectorActionParameterType.Field, entity.FindField("id"));
        }
        private static void AddOrderFavorites(DataConnectorEntity entity)
        {
            var notFulfilledFavorite = entity.AddFavorite("Needs fulfillment", true);
            notFulfilledFavorite.Query.AddRestriction("fulfillment_status", ConnectorRestriction.ConnectorRestrictionType.DoesNotEqual, "fulfilled");
            notFulfilledFavorite.Query.AddRestriction("financial_status", ConnectorRestriction.ConnectorRestrictionType.Equals, "paid");

            var todayFavorite = entity.AddFavorite("Today's orders", true);
            todayFavorite.Query.AddRestriction("created_at_date", ConnectorRestriction.ConnectorRestrictionType.Equals, ConnectorRestrictionValue.ConnectorRestrictionDateValueType.Today);

            var thisWeekFavorite = entity.AddFavorite("This week's orders", true);
            var thisWeekRestriction = thisWeekFavorite.Query.AddRestriction("created_at_date", ConnectorRestriction.ConnectorRestrictionType.Between, ConnectorRestrictionValue.ConnectorRestrictionDateValueType.StartOfWeek);
            thisWeekRestriction.Values.Add(new ConnectorRestrictionValue(ConnectorRestrictionValue.ConnectorRestrictionDateValueType.EndOfWeek));

            var thisMonthFavorite = entity.AddFavorite("This month's orders", true);
            var thisMonthRestriction = thisMonthFavorite.Query.AddRestriction("created_at_date", ConnectorRestriction.ConnectorRestrictionType.Between, ConnectorRestrictionValue.ConnectorRestrictionDateValueType.StartOfMonth);
            thisMonthRestriction.Values.Add(new ConnectorRestrictionValue(ConnectorRestrictionValue.ConnectorRestrictionDateValueType.EndOfMonth));
        }
        private static void AddProductActions(DataConnectorEntity entity)
        {
            var publishProductAction = entity.AddAction(ActionIdAddOrderNote, "Publish");
            publishProductAction.AllowMultipleSelection = true;
            publishProductAction.AddParameter("id", ConnectorActionParameter.ConnectorActionParameterType.Field, entity.FindField("id"));
            publishProductAction.AddCondition(entity.FindField("published"), ConnectorRestriction.ConnectorRestrictionType.Equals, "false");

            var unpublishProductAction = entity.AddAction(ActionIdAddOrderNote, "Unpublish");
            unpublishProductAction.AllowMultipleSelection = true;
            unpublishProductAction.AddParameter("id", ConnectorActionParameter.ConnectorActionParameterType.Field, entity.FindField("id"));
            unpublishProductAction.AddCondition(entity.FindField("published"), ConnectorRestriction.ConnectorRestrictionType.Equals, "true");
        }

        #endregion

    }
}
