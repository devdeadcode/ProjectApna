using System;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Twitter.Models
{
    public class TwitterDirectMessage : DataConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Sender", DefaultField = true)]
        public string sender_screen_name { get; set; }

        [FieldSettings("Recipient", DefaultField = true)]
        public string recipient_screen_name { get; set; }

        [FieldSettings("Message", DefaultField = true)]
        public string text { get; set; }

        #endregion

        #region Hidden properties

        public int id { get; set; }
        public string id_str { get; set; }
        public int recipient_id { get; set; }
        public int sender_id { get; set; }
        public DateTime created_at { get; set; }
        public TwitterUser recipient { get; set; }
        public TwitterUser sender { get; set; }
        public TwitterEntities entities { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Created at date", Created = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime created_at_date => created_at.Date;

        [FieldSettings("Created at time", Created = true, FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime created_at_time => Time(created_at);

        [FieldSettings("Recipient description")]
        public string recipient_description => recipient.description;

        [FieldSettings("Sender description")]
        public string sender_description => sender.description;

        [FieldSettings("Recipient name")]
        public string recipient_name => recipient.name;

        [FieldSettings("Sender name")]
        public string sender_name => sender.name;

        #endregion

    }
}