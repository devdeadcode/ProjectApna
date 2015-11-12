namespace eOne.Common.Connectors.Zendesk.Models
{
    public class ZendeskCore : ConnectorEntityModel
    {

        public string next_page { get; set; }
        public string previous_page { get; set; }
        public int count { get; set; }

    }
}
