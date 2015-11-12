namespace eOne.Common.Connectors.Slack.Models
{
    public class SlackDescription : ConnectorEntityModel
    {

        public string value { get; set; }
        public string creator { get; set; }
        public long last_set { get; set; }

    }
}
