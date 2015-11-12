using System;
using System.Collections.Generic;
using System.Linq;
using eOne.Common.Actions;
using eOne.Common.Connectors.Service;
using eOne.Common.Connectors.Shopify.Models;
using eOne.Common.Helpers;
using eOne.Common.Query;
using eOne.Common.Setup;

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
        public const int EntityIdPage = 6;
        public const int EntityIdArticle = 7;
        public const int EntityIdComment = 8;

        public const int ActionIdAddOrderNote = 1;
        public const int ActionIdCloseOrder = 2;
        public const int ActionIdCancelOrder = 3;
        public const int ActionIdDeleteOrder = 4;
        public const int ActionIdReopenOrder = 5;
        public const int ActionIdPublishProduct = 6;
        public const int ActionIdUnpublishProduct = 7;
        public const int ActionIdPublishPage = 8;
        public const int ActionIdUnpublishPage = 9;

        #endregion

        public ShopifyConnector()
        {
            Name = "Shopify";
            Group = ConnectorGroup.WebStore;

            AuthenticationType = ServiceConnectorAuthenticationType.OAuth2;
            Key = "3197fcb70cb18f8ec65dc789a3462a27";
            Secret = "7c79434fdcaee2b5a49786d69cead597";
            AuthorizationUri = "https://{0}.myshopify.com/admin/oauth/authorize";
            AccessTokenUri = "https://{shop}.myshopify.com/admin/oauth/access_token";
            CallbackUrl = "http://www.popdock.com/callbacks/shopify";
            Scope = "read_content, write_content, read_products, write_products, read_customers, write_customers, read_orders, write_orders, read_fulfillments, write_fulfillments, read_shipping, write_shipping";

            // add rate limit for 2 requests per second
            AddRateLimit(ServiceConnectorRateLimiting.LimitAppliedTo.Account, 2, ServiceConnectorRateLimiting.LimitPeriod.Second);

            AddSetup();
        }

        #region Public methods

        public override void Initialise()
        {
            base.Initialise();
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
                case EntityIdPage:
                    return "pages.json";
                case EntityIdArticle:
                    return "articles.json";
                case EntityIdComment:
                    return "comments.json";
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
                case EntityIdPage:
                    var pageList = DeserializeJson<ShopifyPageList>(data);
                    return pageList.pages;
                case EntityIdArticle:
                    var articleList = DeserializeJson<ShopifyArticleList>(data);
                    var blogList = DeserializeJson<ShopifyBlogList>(GetResponse("blogs.json"));
                    ObjectMapper.Map(blogList.blogs, typeof(ShopifyBlog), articleList.articles, typeof(ShopifyArticle), "blog", TupleHelper.CreateTupleStringList("id", "blog_id"));
                    return articleList.articles;
                case EntityIdComment:
                    // todo
                    break;
            }
            return null;
        }

        public override List<Tuple<string, string>> GetHeaders(ConnectorQuery query)
        {
            return TupleHelper.CreateTupleStringList("X-Shopify-Access-Token", Token);
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
            AddProductFavorites(productEntity);

            var pageEntity = AddEntity(EntityIdPage, "Pages", typeof(ShopifyPage));
            AddPageActions(pageEntity);
            AddPageFavorites(pageEntity);

            var articleEntity = AddEntity(EntityIdArticle, "Articles", typeof(ShopifyArticle));
            AddArticleFavorites(articleEntity);

            var commentEntity = AddEntity(EntityIdComment, "Comments", typeof(ShopifyComment));
            AddCommentFavorites(commentEntity);

            customerEntity.AddRelatedEntity("Addresses", customerAddressEntity, "id", "CustomerId");
            customerEntity.AddRelatedEntity("Orders", orderEntity, "id", "customer_id");
            orderEntity.AddRelatedEntity("Order lines", orderLinesEntity, "id", "OrderId");
            articleEntity.AddRelatedEntity("Comments", commentEntity, "id", "article_id");

            foreach (var entity in Entities) entity.DefaultMaxRecords = 250;
        }

        private void AddOrderActions(ConnectorEntity entity)
        {
            var addOrderNoteAction = entity.AddAction(ActionIdAddOrderNote, "Add note");
            addOrderNoteAction.AddParameter("id", "id");
            addOrderNoteAction.AddParameter("Note", FieldTypeIdString);

            var closeOrderAction = entity.AddAction(ActionIdAddOrderNote, "Close order");
            closeOrderAction.AllowMultipleSelection = true;
            closeOrderAction.ConfirmationPrompt = "Are you sure you want to close this order?";
            closeOrderAction.MultipleSelectionConfirmationPrompt = "Are you sure you want to close these orders?";
            closeOrderAction.AddParameter("id", "id");
            closeOrderAction.AddCondition("closed", ConnectorRestriction.ConnectorRestrictionType.Equals, "false");

            var cancelOrderAction = entity.AddAction(ActionIdAddOrderNote, "Cancel order");
            cancelOrderAction.AllowMultipleSelection = true;
            cancelOrderAction.ConfirmationPrompt = "Are you sure you want to cancel this order?";
            cancelOrderAction.MultipleSelectionConfirmationPrompt = "Are you sure you want to canel these orders?";
            cancelOrderAction.AddParameter("id", "id");
            cancelOrderAction.AddCondition("cancelled", ConnectorRestriction.ConnectorRestrictionType.Equals, "false");

            var reopenOrderAction = entity.AddAction(ActionIdAddOrderNote, "Re-open order");
            reopenOrderAction.AllowMultipleSelection = true;
            reopenOrderAction.AddParameter("id", "id");
            reopenOrderAction.AddCondition("closed", ConnectorRestriction.ConnectorRestrictionType.Equals, "true");

            var deleteOrderAction = entity.AddAction(ActionIdAddOrderNote, "Delete order");
            deleteOrderAction.AllowMultipleSelection = true;
            deleteOrderAction.ConfirmationPrompt = "Are you sure you want to delete this order?";
            deleteOrderAction.MultipleSelectionConfirmationPrompt = "Are you sure you want to delete these orders?";
            deleteOrderAction.AddParameter("id", "id");
        }
        private static void AddOrderFavorites(ConnectorEntity entity)
        {
            var notFulfilledFavorite = entity.AddFavorite("Needs fulfillment", true);
            notFulfilledFavorite.Query.AddRestriction("fulfillment_status", ConnectorRestriction.ConnectorRestrictionType.DoesNotEqual, "fulfilled");
            notFulfilledFavorite.Query.AddRestriction("financial_status", ConnectorRestriction.ConnectorRestrictionType.Equals, "paid");

            var todayFavorite = entity.AddFavorite("Today's orders", true);
            todayFavorite.Query.AddRestriction("created_at", ConnectorRestriction.ConnectorRestrictionType.Equals, ConnectorValue.ConnectorDateValueType.Today);

            var thisWeekFavorite = entity.AddFavorite("This week's orders", true);
            var thisWeekRestriction = thisWeekFavorite.Query.AddRestriction("created_at", ConnectorRestriction.ConnectorRestrictionType.Between, ConnectorValue.ConnectorDateValueType.StartOfWeek);
            thisWeekRestriction?.Values.Add(new ConnectorValue(ConnectorValue.ConnectorDateValueType.EndOfWeek));

            var thisMonthFavorite = entity.AddFavorite("This month's orders", true);
            var thisMonthRestriction = thisMonthFavorite.Query.AddRestriction("created_at", ConnectorRestriction.ConnectorRestrictionType.Between, ConnectorValue.ConnectorDateValueType.StartOfMonth);
            thisMonthRestriction?.Values.Add(new ConnectorValue(ConnectorValue.ConnectorDateValueType.EndOfMonth));
        }
        private static void AddProductActions(ConnectorEntity entity)
        {
            var publishProductAction = entity.AddAction(ActionIdPublishProduct, "Publish");
            publishProductAction.AllowMultipleSelection = true;
            publishProductAction.AddParameter("id", "id");
            publishProductAction.AddCondition("published", ConnectorRestriction.ConnectorRestrictionType.Equals, "false");

            var unpublishProductAction = entity.AddAction(ActionIdUnpublishProduct, "Unpublish");
            unpublishProductAction.AllowMultipleSelection = true;
            unpublishProductAction.AddParameter("id", "id");
            unpublishProductAction.AddCondition("published", ConnectorRestriction.ConnectorRestrictionType.Equals, "true");
        }
        private static void AddProductFavorites(ConnectorEntity entity)
        {
            var publishedProductsFavorite = entity.AddFavorite("Published", true);
            publishedProductsFavorite.Query.AddRestriction("published", ConnectorRestriction.ConnectorRestrictionType.Equals, "true");

            var unpublishedProductsFavorite = entity.AddFavorite("Unpublished", true);
            unpublishedProductsFavorite.Query.AddRestriction("published", ConnectorRestriction.ConnectorRestrictionType.Equals, "false");
        }
        private static void AddPageActions(ConnectorEntity entity)
        {
            var publishPageAction = entity.AddAction(ActionIdPublishPage, "Publish");
            publishPageAction.AllowMultipleSelection = true;
            publishPageAction.AddParameter("id", "id");
            publishPageAction.AddCondition("published", ConnectorRestriction.ConnectorRestrictionType.Equals, "false");

            var unpublishPageAction = entity.AddAction(ActionIdUnpublishPage, "Unpublish");
            unpublishPageAction.AllowMultipleSelection = true;
            unpublishPageAction.AddParameter("id", "id");
            unpublishPageAction.AddCondition("published", ConnectorRestriction.ConnectorRestrictionType.Equals, "true");
        }
        private static void AddPageFavorites(ConnectorEntity entity)
        {
            var publishedPageFavorite = entity.AddFavorite("Published", true);
            publishedPageFavorite.Query.AddRestriction("published", ConnectorRestriction.ConnectorRestrictionType.Equals, "true");

            var unpublishedPageFavorite = entity.AddFavorite("Unpublished", true);
            unpublishedPageFavorite.Query.AddRestriction("published", ConnectorRestriction.ConnectorRestrictionType.Equals, "false");
        }
        private static void AddArticleFavorites(ConnectorEntity entity)
        {
            var publishedFavorite = entity.AddFavorite("Published", true);
            publishedFavorite.Query.AddRestriction("published", ConnectorRestriction.ConnectorRestrictionType.Equals, "true");

            var notPublishedFavorite = entity.AddFavorite("Not published", true);
            notPublishedFavorite.Query.AddRestriction("published", ConnectorRestriction.ConnectorRestrictionType.Equals, "false");
        }
        private static void AddCommentFavorites(ConnectorEntity entity)
        {
            var publishedFavorite = entity.AddFavorite("Published", true);
            publishedFavorite.Query.AddRestriction("status", ConnectorRestriction.ConnectorRestrictionType.Equals, "published");

            var unapprovedFavorite = entity.AddFavorite("Unapproved", true);
            unapprovedFavorite.Query.AddRestriction("status", ConnectorRestriction.ConnectorRestrictionType.Equals, "true");

            var spamFavorite = entity.AddFavorite("Spam", true);
            spamFavorite.Query.AddRestriction("status", ConnectorRestriction.ConnectorRestrictionType.Equals, "unapproved");

            var removedFavorite = entity.AddFavorite("Removed", true);
            removedFavorite.Query.AddRestriction("status", ConnectorRestriction.ConnectorRestrictionType.Equals, "removed");
        }

        #endregion

    }
}
