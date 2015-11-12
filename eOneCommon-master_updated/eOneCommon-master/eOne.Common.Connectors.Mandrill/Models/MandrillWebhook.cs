using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace eOne.Common.Connectors.Mandrill.Models
{
    public class MandrillWebhook : ConnectorEntityModel
    {

        #region Enums

        public enum MandrillWebhookEvent
        {
            [Description("Send")]
            send,
            [Description("Open")]
            open,
            [Description("Click")]
            click
        }

        #endregion

        #region Default properties

        [FieldSettings("Description", DefaultField = true)]
        public string description { get; set; }

        [FieldSettings("URL", DefaultField = true, FieldTypeId = Connector.FieldTypeIdUrl)]
        public string url { get; set; }

        [FieldSettings("Number of batches sent", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int batches_sent { get; set; }

        [FieldSettings("Number of events sent", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int events_sent { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Last error message")]
        public string last_error { get; set; }

        [FieldSettings("Created at date", Created = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime created_at { get; set; }

        [FieldSettings("Last sent at date", FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime last_sent_at { get; set; }

        #endregion

        #region Hidden properties

        public int id { get; set; }
        public string auth_key { get; set; }
        public List<MandrillWebhookEvent> events { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Created at time", Created = true, FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime created_at_time => created_at;

        [FieldSettings("Last sent at time", FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime last_sent_at_time => last_sent_at;

        #endregion

    }
}