using System;

namespace eOne.Common.Connectors.Infusionsoft.Models
{
    public class InfusionsoftSubscription : ConnectorEntityModel
    {

        public int AffiliateId { get; set; }
        public int AutoCharge { get; set; }
        public decimal BillingAmt { get; set; }
        public string BillingCycle { get; set; }
        public int CC1 { get; set; }
        public int CC2 { get; set; }
        public string City { get; set; }
        public int ContactId { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string EmailAddress2 { get; set; }
        public string EmailAddress3 { get; set; }
        public DateTime EndDate { get; set; }
        public string FirstName { get; set; }
        public int Frequency { get; set; }
        public string HTMLSignature { get; set; }
        public DateTime LastBillDate { get; set; }
        public string LastName { get; set; }
        public int LeadAffiliateId { get; set; }
        public int MaxRetry { get; set; }
        public int MerchantAccountId { get; set; }
        public string MiddleName { get; set; }
        public DateTime NextBillDate { get; set; }
        public string Nickname { get; set; }
        public int NumDaysBetweenRetry { get; set; }
        public DateTime PaidThruDate { get; set; }
        public string Phone1 { get; set; }
        public string Phone1Ext { get; set; }
        public string Phone1Type { get; set; }
        public string Phone2 { get; set; }
        public string Phone2Ext { get; set; }
        public string Phone2Type { get; set; }
        public string PostalCode { get; set; }
        public int ProductId { get; set; }
        public int ProgramId { get; set; }
        public string PromoCode { get; set; }
        public int Qty { get; set; }
        public string ReasonStopped { get; set; }
        public int RecurringOrderId { get; set; }
        public int ShippingOptionId { get; set; }
        public string Signature { get; set; }
        public string SpouseName { get; set; }
        public DateTime StartDate { get; set; }
        public string State { get; set; }
        public string Status { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public int SubscriptionPlanId { get; set; }
        public string Suffix { get; set; }
        public string Title { get; set; }
        public string ZipFour1 { get; set; }

    }
}
