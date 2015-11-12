namespace eOne.Common.Connectors.GitHub.Models
{
    public class GitHubLabel : ConnectorEntityModel
    {

        public string url { get; set; }
        public string name { get; set; }
        public string color { get; set; }

    }
}
