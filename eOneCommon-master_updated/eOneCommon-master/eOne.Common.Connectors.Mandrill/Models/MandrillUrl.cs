namespace eOne.Common.Connectors.Mandrill.Models
{
    public class MandrillUrl : ConnectorEntityModel
    {

        #region Default properties

        [FieldSettings("URL", DefaultField = true, FieldTypeId = Connector.FieldTypeIdUrl)]
        public string url { get; set; }

        [FieldSettings("Emails sent", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int sent { get; set; }

        [FieldSettings("Clicks", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int clicks { get; set; }

        [FieldSettings("Unique clicks", DefaultField = true, FieldTypeId = Connector.FieldTypeIdInteger)]
        public int unique_clicks { get; set; }

        #endregion

        #region Calculations

        [FieldSettings("Click rate", FieldTypeId = Connector.FieldTypeIdPercentage)]
        public decimal click_rate => clicks / sent * 100;

        #endregion

    }
}
