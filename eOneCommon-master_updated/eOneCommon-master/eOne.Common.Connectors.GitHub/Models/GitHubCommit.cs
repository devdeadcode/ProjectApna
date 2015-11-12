namespace eOne.Common.Connectors.GitHub.Models
{
    class GitHubCommit : ConnectorEntityModel
    {

        public string url { get; set; }
        public string sha { get; set; }
        public string html_url { get; set; }
        public string comments_url { get; set; }
        public GitHubUser author { get; set; }
        public GitHubUser committer { get; set; }

    }
}
