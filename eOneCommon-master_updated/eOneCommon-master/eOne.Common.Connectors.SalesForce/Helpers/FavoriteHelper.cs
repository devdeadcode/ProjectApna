namespace eOne.Common.Connectors.SalesForce.Helpers
{
    public class FavoriteHelper
    {

        public static void AddLeadFavorites(ConnectorEntity entity)
        {
            var converted = entity.AddFavorite("Converted");
            converted.Query.AddFields("Name", "LeadSource", "ConvertedDate", "Status");
            converted.Query.AddBooleanRestriction("IsConverted", true);

            var unread = entity.AddFavorite("Unread by owner", true);
            unread.Query.AddFields("OwnerName");
            unread.Query.AddBooleanRestriction("IsUnreadByOwner", true);
            unread.Query.AddOrderBy("CreatedDate", Query.ConnectorQuery.ConnectorQuerySortOrder.Descending);
        }

        public static void AddCaseFavorites(ConnectorEntity entity)
        {
            var escalated = entity.AddFavorite("Escalated", true);
            escalated.Query.AddBooleanRestriction("IsEscalated", true);
            escalated.Query.AddOrderBy("CreatedDate", Query.ConnectorQuery.ConnectorQuerySortOrder.Descending);

            var unreadComments = entity.AddFavorite("Unread comments", true);
            unreadComments.Query.AddBooleanRestriction("HasCommentsUnreadByOwner", true);
            unreadComments.Query.AddOrderBy("CreatedDate", Query.ConnectorQuery.ConnectorQuerySortOrder.Descending);
        }

        public static void AddOpportunityFavorites(ConnectorEntity entity)
        {
            var privateOpportunities = entity.AddFavorite("Private", true);
            privateOpportunities.Query.AddBooleanRestriction("IsPrivate", true);

            var won = entity.AddFavorite("Won", true);
            won.Query.AddBooleanRestriction("IsWon", true);
            won.Query.AddOrderBy("CreatedDate", Query.ConnectorQuery.ConnectorQuerySortOrder.Descending);

            var closed = entity.AddFavorite("Closed", true);
            closed.Query.AddBooleanRestriction("IsClosed", true);

            // todo - add a favorite for each stage if possible
        }

        public static void AddOrderFavorites(ConnectorEntity entity)
        {
            var thisWeek = entity.AddFavorite("This week", true);
            thisWeek.Query.AddField("CreatedDate");
            thisWeek.Query.AddDateRestriction("CreatedDate", Query.ConnectorQuery.ConnectorQueryGenericDateType.ThisWeek);
            thisWeek.Query.AddOrderBy("CreatedDate", Query.ConnectorQuery.ConnectorQuerySortOrder.Descending);

            var thisMonth = entity.AddFavorite("This month", true);
            thisWeek.Query.AddField("CreatedDate");
            thisMonth.Query.AddDateRestriction("CreatedDate", Query.ConnectorQuery.ConnectorQueryGenericDateType.ThisMonth);
            thisWeek.Query.AddOrderBy("CreatedDate", Query.ConnectorQuery.ConnectorQuerySortOrder.Descending);

            var thisYear = entity.AddFavorite("This year", true);
            thisWeek.Query.AddField("CreatedDate");
            thisYear.Query.AddDateRestriction("CreatedDate", Query.ConnectorQuery.ConnectorQueryGenericDateType.ThisYear);
            thisWeek.Query.AddOrderBy("CreatedDate", Query.ConnectorQuery.ConnectorQuerySortOrder.Descending);
        }

        public static void AddSolutionFavorites(ConnectorEntity entity)
        {
            var notReviewed = entity.AddFavorite("Not reviewed", true);
            notReviewed.Query.AddBooleanRestriction("IsReviewed", false);

            var knowledgeBase = entity.AddFavorite("Visible in public knowledge base", true);
            knowledgeBase.Query.AddBooleanRestriction("IsPublishedInPublicKb", true);
        }

    }
}
