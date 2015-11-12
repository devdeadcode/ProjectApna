namespace eOne.Common.Connectors.GitHub.Models.Metadata
{
    public class GitHubRateLimits
    {

        public GitHubResourceRateLimits resources { get; set; }
        public GitHubRateLimit rate { get; set; }

    }
}
