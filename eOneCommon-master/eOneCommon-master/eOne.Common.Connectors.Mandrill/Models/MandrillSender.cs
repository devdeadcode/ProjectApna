using System;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Mandrill.Models
{
    public class MandrillSender : DataConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Email address", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdEmail)]
        public string address { get; set; }

        [FieldSettings("Reputation", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int reputation { get; set; }

        [FieldSettings("Emails sent", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int sent { get; set; }

        [FieldSettings("Hard bounces", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int hard_bounces { get; set; }

        [FieldSettings("Soft bounces", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int soft_bounces { get; set; }

        [FieldSettings("Rejects", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int rejects { get; set; }

        [FieldSettings("Complaints", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int complaints { get; set; }

        [FieldSettings("Unsubscribes", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int unsubs { get; set; }

        [FieldSettings("Opens", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int opens { get; set; }

        [FieldSettings("Clicks", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int clicks { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Unique opens", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int unique_opens { get; set; }

        [FieldSettings("Uniqiue clicks", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int unique_clicks { get; set; }

        #endregion

        #region Hidden properties

        public DateTime created_at { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Created at date", Created = true, FieldTypeId = DataConnector.FieldTypeIdDate)]
        public DateTime created_at_date => created_at.Date;

        [FieldSettings("Created at time", Created = true, FieldTypeId = DataConnector.FieldTypeIdTime)]
        public DateTime created_at_time => Time(created_at);

        [FieldSettings("Open rate", FieldTypeId = DataConnector.FieldTypeIdPercentage)]
        public decimal open_rate => opens / sent * 100;

        [FieldSettings("Click rate", FieldTypeId = DataConnector.FieldTypeIdPercentage)]
        public decimal click_rate => clicks / sent * 100;

        #endregion

    }
}
