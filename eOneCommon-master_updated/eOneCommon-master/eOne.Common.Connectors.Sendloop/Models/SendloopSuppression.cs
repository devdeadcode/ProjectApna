namespace eOne.Common.Connectors.Sendloop.Models
{
    public class SendloopSuppression : ConnectorEntityModel
    {

        [FieldSettings("Email address", DefaultField = true, FieldTypeId = Connector.FieldTypeIdEmail, Description = true)]
        public string EmailAddress { get; set; }

        [FieldSettings("Type", DefaultField = true)]
        public string Type { get; set; }

        [FieldSettings("Suppression ID", KeyNumber = 1)]
        public int SuppressionID { get; set; }

    }
}