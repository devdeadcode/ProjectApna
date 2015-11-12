using System;

namespace eOne.Common.Connectors.Mandrill.Models
{
    public class MandrillReject : ConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Email address", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEmail)]
        public string email { get; set; }

        [FieldSettings("Reason", DefaultField = true)]
        public string reason { get; set; }

        [FieldSettings("Detail", DefaultField = true)]
        public string detail { get; set; }

        [FieldSettings("Expired", DefaultField = true, FieldTypeId = Connector.FieldTypeIdYesNo)]
        public bool expired { get; set; }

        [FieldSettings("Subaccount", DefaultField = true)]
        public string subaccount { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Created at date", Created = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime created_at { get; set; }

        [FieldSettings("Last event at date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime last_event_at { get; set; }

        [FieldSettings("Expires at date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime expires_at { get; set; }

        #endregion

        #region Hidden properties

        public MandrillSender sender { get; set; }

        #endregion

        #region Calculations
        
        [FieldSettings("Created at time", Created = true, FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime created_at_time => created_at;

        [FieldSettings("Last event at time", FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime last_event_at_time => last_event_at;

        [FieldSettings("Expires at time", FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime expires_at_time => expires_at;

        [FieldSettings("Sender email address", FieldTypeId = Connector.FieldTypeIdEmail)]
        public string sender_address => sender.address;

        [FieldSettings("Sender emails sent", FieldTypeId = Connector.FieldTypeIdEmail)]
        public int sender_sent => sender.sent;

        [FieldSettings("Sender hard bounces", FieldTypeId = Connector.FieldTypeIdEmail)]
        public int sender_hard_bounces => sender.hard_bounces;

        [FieldSettings("Sender soft bounces", FieldTypeId = Connector.FieldTypeIdEmail)]
        public int sender_soft_bounces => sender.soft_bounces;

        [FieldSettings("Sender rejections", FieldTypeId = Connector.FieldTypeIdEmail)]
        public int sender_rejects => sender.rejects;

        [FieldSettings("Sender complaints", FieldTypeId = Connector.FieldTypeIdEmail)]
        public int sender_complaints => sender.complaints;

        [FieldSettings("Sender unsubscribes", FieldTypeId = Connector.FieldTypeIdEmail)]
        public int sender_unsubs => sender.unsubs;

        [FieldSettings("Sender opens", FieldTypeId = Connector.FieldTypeIdEmail)]
        public int sender_opens => sender.opens;

        [FieldSettings("Sender clicks", FieldTypeId = Connector.FieldTypeIdEmail)]
        public int sender_clicks => sender.clicks;

        [FieldSettings("Sender unique opens", FieldTypeId = Connector.FieldTypeIdEmail)]
        public int sender_unique_opens => sender.unique_opens;

        [FieldSettings("Sender unique clicks", FieldTypeId = Connector.FieldTypeIdEmail)]
        public int sender_unique_clicks => sender.unique_clicks;

        [FieldSettings("Sender created at date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime sender_created_at_date => sender.created_at;

        [FieldSettings("Sender created at time", FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime sender_created_at_time => sender.created_at_time;

        [FieldSettings("Sender open rate", FieldTypeId = Connector.FieldTypeIdEmail)]
        public decimal sender_open_rate => sender.open_rate;

        [FieldSettings("Sender click rate", FieldTypeId = Connector.FieldTypeIdEmail)]
        public decimal sender_click_rate => sender.click_rate;

        #endregion

    }
}