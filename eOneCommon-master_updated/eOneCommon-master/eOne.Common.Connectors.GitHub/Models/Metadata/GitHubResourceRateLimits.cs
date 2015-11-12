namespace eOne.Common.Connectors.GitHub.Models.Metadata
{
    public class GitHubResourceRateLimits
    {

        public GitHubRateLimit core { get; set; }
        public GitHubRateLimit search { get; set; }

    }
}