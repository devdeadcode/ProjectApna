using System;

namespace eOne.Common.Connectors.GitHub.Models
{
    public class GitHubMilestone : ConnectorEntityModel
    {

        public string url { get; set; }
        public int number { get; set; }
        public string state { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public GitHubUser creator { get; set; }
        public int open_issues { get; set; }
        public int closed_issues { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public DateTime? closed_at { get; set; }
        public DateTime? due_on { get; set; }

    }
}
