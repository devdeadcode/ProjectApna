using System;
using eOne.Common.Connectors.Sendloop.Helpers;

namespace eOne.Common.Connectors.Sendloop.Models
{
    public class SendloopSubscriber : ConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("List name", DefaultField = true, Description = true)]
        public string ListName => List.Name;

        [FieldSettings("Email address", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEmail)]
        public string EmailAddress { get; set; }

        [FieldSettings("Status", DefaultField = true)]
        public string SubscriptionStatus { get; set; }

        [FieldSettings("Subscription date", DefaultField = true, FieldTypeId = Connector.FieldTypeIdDate, Created = true)]
        public DateTime? SubscriptionDateValue => DataConversion.ParseDateTime(SubscriptionDate);

        #endregion

        #region Properties

        [FieldSettings("Subscriber ID", FieldTypeId = Connector.FieldTypeIdInteger, KeyNumber = 1)]
        public int SubscriberID { get; set; }

        [FieldSettings("Bounce type")]
        public string BounceType { get; set; }

        [FieldSettings("Subscription IP address")]
        public string SubscriptionIP { get; set; }

        [FieldSettings("Unsubscription IP address")]
        public string UnsubscriptionIP { get; set; }

        #endregion

        #region Hidden properties

        public SendloopList List { get; set; }
        public string SubscriptionDate { get; set; }
        public string UnsubscriptionDate { get; set; }
        public string OptInDate { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("List ID", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int ListID => List.ListID;

        [FieldSettings("Unsubscription date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? UnsubscriptionDateValue => DataConversion.ParseDateTime(UnsubscriptionDate);

        [FieldSettings("Opt-in date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime? OptInDateValue => DataConversion.ParseDateTime(OptInDate);

        #endregion

    }
}