namespace eOne.Common.Connectors.HappyFox.Models
{
    public class HappyFoxUpdatedStatusChange : ConnectorEntityModel
    {
        public string from_status { get; set; }

        public string to_status { get; set; }
    }
}
