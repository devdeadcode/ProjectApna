using System;

namespace eOne.Common.Connectors.Mandrill.Models
{
    public class MandrillSender : ConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Email address", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEmail)]
        public string address { get; set; }

        [FieldSettings("Reputation", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int reputation { get; set; }

        [FieldSettings("Emails sent", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int sent { get; set; }

        [FieldSettings("Hard bounces", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int hard_bounces { get; set; }

        [FieldSettings("Soft bounces", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int soft_bounces { get; set; }

        [FieldSettings("Rejects", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int rejects { get; set; }

        [FieldSettings("Complaints", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int complaints { get; set; }

        [FieldSettings("Unsubscribes", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int unsubs { get; set; }

        [FieldSettings("Opens", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int opens { get; set; }

        [FieldSettings("Clicks", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int clicks { get; set; }

        #endregion

        #region Properties

        [FieldSettings("Unique opens", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int unique_opens { get; set; }

        [FieldSettings("Uniqiue clicks", FieldTypeId = Connector.FieldTypeIdInteger)]
        public int unique_clicks { get; set; }

        [FieldSettings("Created at date", Created = true, FieldTypeId = Connector.FieldTypeIdDate)]
        public DateTime created_at { get; set; }

        #endregion

        #region Calculations
        
        [FieldSettings("Created at time", Created = true, FieldTypeId = Connector.FieldTypeIdTime)]
        public DateTime created_at_time => created_at;

        [FieldSettings("Open rate", FieldTypeId = Connector.FieldTypeIdPercentage)]
        public decimal open_rate => opens / sent * 100;

        [FieldSettings("Click rate", FieldTypeId = Connector.FieldTypeIdPercentage)]
        public decimal click_rate => clicks / sent * 100;

        #endregion

    }
}
