using System.Dynamic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Stripe.Helpers
{
    class StripeFavoriteHelper
    {
        public static void AddCustomerFavorites(DataConnectorEntity entity)
        {
            //add outstanding customer favorite
            var outstandingBalanceFavorite = entity.AddFavorite("Outstanding balance");
            outstandingBalanceFavorite.Query.AddFields("Email", "Decription", "Account Balance", "Delinquent");
            outstandingBalanceFavorite.Query.AddRestriction("account_balance",
                ConnectorRestriction.ConnectorRestrictionType.GreaterThan, 0);

            //add delinquent customer favorite
            var delinquentFavorite = entity.AddFavorite("Delinquent");
            delinquentFavorite.Query.AddFields("Email", "Description", "Account balance");
            delinquentFavorite.Query.AddRestriction("delinquent", ConnectorRestriction.ConnectorRestrictionType.Equals,
                "true");

            //add credits customer favorite
            var creditsFavorite = entity.AddFavorite("Credits");
            creditsFavorite.Query.AddFields("Email","Description", "Account balance");
            creditsFavorite.Query.AddRestriction("account_balance",
                ConnectorRestriction.ConnectorRestrictionType.GreaterThan, 0);
        }

        public static void AddChargesFavorites(DataConnectorEntity entity)
        {
            //add failed charges favorite
            var failedCharges = entity.AddFavorite("Failed");
            failedCharges.Query.AddFields("Charge ID", "Amount", "Failure code", "Failure description");
            failedCharges.Query.AddRestriction("status", ConnectorRestriction.ConnectorRestrictionType.Equals, "failed");
        }

        public static void AddCouponFavorites(DataConnectorEntity entity)
        {
            //add valid coupons favorite
            var validCoupons = entity.AddFavorite("Valid coupons");
            validCoupons.Query.AddFields("Coupon ID", "Duration", "Redeem by", "Remaining redemptions");
            validCoupons.Query.AddRestriction("valid", ConnectorRestriction.ConnectorRestrictionType.Equals, "true");
        }

        public static void AddInvoiceFavorite(DataConnectorEntity entity)
        {
            //add unpaid invoices favorite
            var unpaidInvoices = entity.AddFavorite("Unpaid");
            unpaidInvoices.Query.AddFields("Invoice ID", "Invoice Date", "Description", "Invoice total", "Payment attempted", "Number of attempts");
            unpaidInvoices.Query.AddRestriction("paid", ConnectorRestriction.ConnectorRestrictionType.Equals, "false");

            //add today's invoices favorite
            var todaysInvoices = entity.AddFavorite("Today's invoices");
            todaysInvoices.Query.AddFields("Invoice ID", "Description", "Invoice total", "Invoice date");
            todaysInvoices.Query.AddRestriction("invoice_date", ConnectorRestriction.ConnectorRestrictionType.Equals,
                "DateTime.Today");

            //add this week's invoices favorite
            var thisWeeksInvoices = entity.AddFavorite("This week's invoices");
            thisWeeksInvoices.Query.AddFields("Invoice ID", "Description", "Invoice total", "Invoice date");
            thisWeeksInvoices.Query.AddRestriction("invoice_date",
                ConnectorRestriction.ConnectorRestrictionType.GreaterThan, "DateTime.Now.AddDays(DayOfWeek.Sunday - DateTime.Now.DayOfWeek)");
            thisWeeksInvoices.Query.AddRestriction("invoice_date",
                ConnectorRestriction.ConnectorRestrictionType.LessThan,
                "DateTime.Now.AddDays(DateTime.Now.DayOfWeek - DayOfWeek.Sunday)");

            //add this month's invoices favorite
            var thisMonthsInvoices = entity.AddFavorite("This month's invoices");
            thisMonthsInvoices.Query.AddFields("Invoice ID", "Description", "Invoice total", "Invoice date");
            thisMonthsInvoices.Query.AddRestriction("invoice_date",
                ConnectorRestriction.ConnectorRestrictionType.GreaterThan, "new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)");
            thisMonthsInvoices.Query.AddRestriction("invoice_date",
                ConnectorRestriction.ConnectorRestrictionType.LessThan,
                "(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).AddMonths(1).AddDays(-1)");
            
        }

        public static void AddOrderFavorite(DataConnectorEntity entity)
        {
            //add today's orders favorite
            var todaysOrders = entity.AddFavorite("Today's orders");
            todaysOrders.Query.AddFields("Order ID", "Email", "Amount");
            todaysOrders.Query.AddRestriction("created_date", ConnectorRestriction.ConnectorRestrictionType.Equals,
                "DateTime.Today");

            //add this week's invoices favorite
            var thisWeeksOrders = entity.AddFavorite("This week's orders");
            thisWeeksOrders.Query.AddFields("Order ID", "Email", "Amount");
            thisWeeksOrders.Query.AddRestriction("created_date",
                ConnectorRestriction.ConnectorRestrictionType.GreaterThan, "DateTime.Now.AddDays(DayOfWeek.Sunday - DateTime.Now.DayOfWeek)");
            thisWeeksOrders.Query.AddRestriction("created_date",
                ConnectorRestriction.ConnectorRestrictionType.LessThan,
                "DateTime.Now.AddDays(DateTime.Now.DayOfWeek - DayOfWeek.Sunday)");

            //add this month's invoices favorite
            var thisMonthsOrders = entity.AddFavorite("This month's orders");
            thisMonthsOrders.Query.AddFields("Order ID", "Email", "Amount");
            thisMonthsOrders.Query.AddRestriction("created_date",
                ConnectorRestriction.ConnectorRestrictionType.GreaterThan, "new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)");
            thisMonthsOrders.Query.AddRestriction("created_date",
                ConnectorRestriction.ConnectorRestrictionType.LessThan,
                "(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).AddMonths(1).AddDays(-1)");
        }

        public static void AddSKUFavorite(DataConnectorEntity entity)
        {
            //add in stock favorite
            var inStock = entity.AddFavorite("In stock");
            inStock.Query.AddFields("Product name", "Attributes", "Inventory quantity type", "Inventory quantity", "Inventory status");
            inStock.Query.AddRestriction("type", ConnectorRestriction.ConnectorRestrictionType.Equals,
                "infinite");

            //add out of stock favorite
            var outOfStock = entity.AddFavorite("Out of stock");
            outOfStock.Query.AddFields("Product name", "Attributes", "Inventory quantity type", "Inventory quantity");
            outOfStock.Query.AddRestriction("value", ConnectorRestriction.ConnectorRestrictionType.Equals,
                "out_of_stock");

            //add limited stock favorite
            var limitedStock = entity.AddFavorite("Limited stock");
            limitedStock.Query.AddFields("Product name", "Attributes", "Inventory quantity type", "Inventory quantity");
            limitedStock.Query.AddRestriction("status", ConnectorRestriction.ConnectorRestrictionType.Equals, "limited");
        }

        public static void AddSubscriptionFavorite(DataConnectorEntity entity)
        {
            //add active trials favorite
            var activeTrials = entity.AddFavorite("Active trials");
            activeTrials.Query.AddFields("Customer email", "Plan name", "Days left on trial");
            activeTrials.Query.AddRestriction("days_left", ConnectorRestriction.ConnectorRestrictionType.GreaterThan,
                0);
        }

        public static void AddTransferFavorite(DataConnectorEntity entity)
        {
            //add failed favorite
            var failed = entity.AddFavorite("Failed");
            failed.Query.AddFields("Transfer ID", "Transfer date", "Amount", "Failure code", "Failure message");
            failed.Query.AddRestriction("status", ConnectorRestriction.ConnectorRestrictionType.Equals, "failed");

            //add reversed favorite
            var reversed = entity.AddFavorite("Reversed");
            reversed.Query.AddFields("Transfer ID", "Transfer date", "Amount", "Amount reserved");
            reversed.Query.AddRestriction("reversed", ConnectorRestriction.ConnectorRestrictionType.Equals, "true");

            //add pending favorite
            var pending = entity.AddFavorite("Failed");
            pending.Query.AddFields("Transfer ID", "Transfer date", "Amount");
            pending.Query.AddRestriction("status", ConnectorRestriction.ConnectorRestrictionType.Equals, "pending");

            //add in_transit favorite
            var in_transit = entity.AddFavorite("In transit");
            in_transit.Query.AddFields("Transfer ID", "Transfer date", "Amount");
            in_transit.Query.AddRestriction("status", ConnectorRestriction.ConnectorRestrictionType.Equals, "in_transit");

        }
    }
}
