using System;
using System.Collections.Generic;
using System.ComponentModel;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Mandrill.Models
{
    public class MandrillWebhook : DataConnectorEntityModel
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

        [FieldSettings("URL", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string url { get; set; }

        [FieldSettings("Number of batches sent", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int batches_sent { get; set; }

        [FieldSettings("Number of events sent", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int events_sent { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Last error message")]
        public string last_error { get; set; }

        #endregion

        #region Hidden properties

        public int id { get; set; }
        public string auth_key { get; set; }
        public List<MandrillWebhookEvent> events { get; set; }
        public DateTime created_at { get; set; }
        public DateTime last_sent_at { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Created at date", Created = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime created_at_date => created_at.Date;

        [FieldSettings("Created at time", Created = true, FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime created_at_time => Time(created_at);

        [FieldSettings("Last sent at date", FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime last_sent_at_date => last_sent_at.Date;

        [FieldSettings("Last sent at time", FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime last_sent_at_time => Time(last_sent_at);

        #endregion

    }
}