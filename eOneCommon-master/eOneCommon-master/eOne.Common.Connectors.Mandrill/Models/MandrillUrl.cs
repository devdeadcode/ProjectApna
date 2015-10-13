using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.Mandrill.Models
{
    public class MandrillUrl : DataConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("URL", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdUrl)]
        public string url { get; set; }

        [FieldSettings("Emails sent", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int sent { get; set; }

        [FieldSettings("Clicks", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int clicks { get; set; }

        [FieldSettings("Unique clicks", DefaultField = true, FieldTypeId = DataConnector.FieldTypeIdInteger)]
        public int unique_clicks { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Click rate", FieldTypeId = DataConnector.FieldTypeIdPercentage)]
        public decimal click_rate => clicks / sent * 100;

        #endregion

    }
}
