using System;

namespace eOne.Common.Connectors.Paypal.Models
{
    public class PaypalAuthorization : PaypalResourceSalesBase
    {

        public enum PaypalAuthorizationState
        {
            pending, 
            authorized, 
            captured, 
            partially_captured, 
            expired, 
            voided
        }

        public PaypalAuthorizationState state { get; set; }
        public DateTime valid_until { get; set; }

    }
}