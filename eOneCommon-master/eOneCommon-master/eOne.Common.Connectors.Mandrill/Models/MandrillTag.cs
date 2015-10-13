using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Mandrill.Models
{
    public class MandrillTag : DataConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("Tag", DefaultField = true)]
        public string tag { get; set; }

        [FieldSettings("Reputation", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int reputation { get; set; }

        [FieldSettings("Sent", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdInteger)]
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

        [FieldSettings("Unique clicks", FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int unique_clicks { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Open rate", FieldTypeId = DataConnector.FieldTypeIdPercentage)]
        public decimal open_rate => opens / sent * 100;

        [FieldSettings("Click rate", FieldTypeId = DataConnector.FieldTypeIdPercentage)]
        public decimal click_rate => clicks / sent * 100;

        #endregion

    }
}
