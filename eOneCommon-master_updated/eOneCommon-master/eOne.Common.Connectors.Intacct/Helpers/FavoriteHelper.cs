using eOne.Common.Query;

namespace eOne.Common.Connectors.Intacct.Helpers
{
    public class FavoriteHelper
    {

        public static void AddApBillFavorites(ConnectorEntity entity)
        {

            var overdue = entity.AddFavorite("Overdue");
            overdue.Query.AddFields("VENDORNAME", "RECORDID", "WHENDUE", "TOTALDUE");
            overdue.Query.AddRestriction("TOTALDUE", ConnectorRestriction.ConnectorRestrictionType.GreaterThan, "0");
            overdue.Query.AddRestriction("WHENDUE", ConnectorRestriction.ConnectorRestrictionType.LessThan, ConnectorValue.ConnectorDateValueType.Today);

        }

    }
}
