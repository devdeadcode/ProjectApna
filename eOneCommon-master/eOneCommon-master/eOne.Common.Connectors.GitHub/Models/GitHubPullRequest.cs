using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.GitHub.Models
{
    public class GitHubPullRequest : DataConnectorEntityModel
    {

        public string url { get; set; }
        public string html_url { get; set; }
        public string diff_url { get; set; }
        public string patch_url { get; set; }

    }
}
