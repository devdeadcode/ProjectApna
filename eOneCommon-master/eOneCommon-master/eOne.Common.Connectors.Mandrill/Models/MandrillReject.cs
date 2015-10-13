using System;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Mandrill.Models
{
    public class MandrillReject : DataConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Email address", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string email { get; set; }

        [FieldSettings("Reason", DefaultField = true)]
        public string reason { get; set; }

        [FieldSettings("Detail", DefaultField = true)]
        public string detail { get; set; }

        [FieldSettings("Expired", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdYesNo)]
        public bool expired { get; set; }

        [FieldSettings("Subaccount", DefaultField = true)]
        public string subaccount { get; set; }

        #endregion

        #region Hidden properties

        public MandrillSender sender { get; set; }
        public DateTime created_at { get; set; }
        public DateTime last_event_at { get; set; }
        public DateTime expires_at { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Created at date", Created = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime created_at_date => created_at.Date;

        [FieldSettings("Created at time", Created = true, FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime created_at_time => Time(created_at);

        [FieldSettings("Last event at date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime last_event_at_date => last_event_at.Date;

        [FieldSettings("Last event at time", FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime last_event_at_time => Time(last_event_at);

        [FieldSettings("Expires at date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime expires_at_date => expires_at.Date;

        [FieldSettings("Expires at time", FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime expires_at_time => Time(expires_at);

        [FieldSettings("Sender email address", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string sender_address => sender.address;

        [FieldSettings("Sender emails sent", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public int sender_sent => sender.sent;

        [FieldSettings("Sender hard bounces", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public int sender_hard_bounces => sender.hard_bounces;

        [FieldSettings("Sender soft bounces", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public int sender_soft_bounces => sender.soft_bounces;

        [FieldSettings("Sender rejections", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public int sender_rejects => sender.rejects;

        [FieldSettings("Sender complaints", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public int sender_complaints => sender.complaints;

        [FieldSettings("Sender unsubscribes", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public int sender_unsubs => sender.unsubs;

        [FieldSettings("Sender opens", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public int sender_opens => sender.opens;

        [FieldSettings("Sender clicks", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public int sender_clicks => sender.clicks;

        [FieldSettings("Sender unique opens", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public int sender_unique_opens => sender.unique_opens;

        [FieldSettings("Sender unique clicks", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public int sender_unique_clicks => sender.unique_clicks;

        [FieldSettings("Sender created at date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime sender_created_at_date => sender.created_at_date;

        [FieldSettings("Sender created at time", FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime sender_created_at_time => sender.created_at_time;

        [FieldSettings("Sender open rate", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public decimal sender_open_rate => sender.open_rate;

        [FieldSettings("Sender click rate", FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public decimal sender_click_rate => sender.click_rate;

        #endregion

    }
}