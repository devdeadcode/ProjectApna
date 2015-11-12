using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.HappyFox.Models
{
    public class HappyFoxFavoriteHelper
    {
        public static void AddTicketFavorites(DataConnectorEntity entity)
        {
            // add overdue ticket favorite
            var overdueProjectFavorite = entity.AddFavorite("Overdue tickets");
            overdueProjectFavorite.Query.AddFields("id", "subject", "status", "priority", "assigned_to_name", "created_date", "due_date", "days_overdue");
            overdueProjectFavorite.Query.AddRestriction("Overdue", ConnectorRestriction.ConnectorRestrictionType.Equals, "true");
            overdueProjectFavorite.Query.AddRestriction("Status Type", ConnectorRestriction.ConnectorRestrictionType.DoesNotEqual, "completed");

            // add unresponded ticket favorite
            var unrespondedTicketFavorite = entity.AddFavorite("Unresponded tickets");
            unrespondedTicketFavorite.Query.AddFields("Ticket ID", "Subject", "Status", "Priority", "Assigend to user name", "Create date");
            unrespondedTicketFavorite.Query.AddRestriction("Number of messages", ConnectorRestriction.ConnectorRestrictionType.Equals, 0);

            // add ticket status favorite

            // add ticket category favorite

            // add ticket priority favorite
        }

        public static void ContactFavorites(DataConnectorEntity entity)
        {
            // add contact with pending tickets favorite
            var pendingTicketFavorite = entity.AddFavorite("Contacts with pending tickets");
            pendingTicketFavorite.Query.AddFields("name", "email", "num_of_pending_tickets");
            pendingTicketFavorite.Query.AddRestriction("num_of_pending_tickets", ConnectorRestriction.ConnectorRestrictionType.GreaterThan, 0);
        }

        public static void UpdateFavorites(DataConnectorEntity entity)
        {
            // add due date change favorite
            var dueDateChangeFavorite = entity.AddFavorite("Due date changes");
            dueDateChangeFavorite.Query.AddFields("ticket_subject", "update_date", "from_due_date", "to_due_date");
            dueDateChangeFavorite.Query.AddRestriction("update_type", ConnectorRestriction.ConnectorRestrictionType.Equals, "due_date_change");

            // add status change favorite
            var statusChangeFavorite = entity.AddFavorite("Status changes");
            statusChangeFavorite.Query.AddFields("ticket_subject", "update_date", "from_status", "to_status");
            statusChangeFavorite.Query.AddRestriction("update_type", ConnectorRestriction.ConnectorRestrictionType.Equals, "status_change");

            // add category changes favorite
            var categoryChangesFavorite = entity.AddFavorite("Status changes");
            categoryChangesFavorite.Query.AddFields("ticket_subject", "update_date", "from_category", "to_category");
            categoryChangesFavorite.Query.AddRestriction("update_type", ConnectorRestriction.ConnectorRestrictionType.Equals, "category_change");

            // add priority changes favorite
            var priorityChangesFavorite = entity.AddFavorite("Priority Change");
            priorityChangesFavorite.Query.AddFields("ticket_subject", "update_date", "from_priority", "to_priority");
            priorityChangesFavorite.Query.AddRestriction("update_type", ConnectorRestriction.ConnectorRestrictionType.Equals, "priority_change");

            // add user assignments
            var userAssignments = entity.AddFavorite("User assignments");
            userAssignments.Query.AddFields("ticket_subject", "update_date", "assigned_from", "assigned_to");
            userAssignments.Query.AddRestriction("update_type", ConnectorRestriction.ConnectorRestrictionType.Equals, "assign_user");
            //double check the assign_user
        }
    }
}
