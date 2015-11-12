namespace eOne.Common.Connectors.GitHub.Models.Metadata
{
    public class GitHubRateLimit
    {

        public int limit { get; set; }
        public int remaining { get; set; }
        public int reset { get; set; }

    }
}


