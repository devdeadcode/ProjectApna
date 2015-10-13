using System;
using System.Collections.Generic;
using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.GitHub.Models
{
    public class GitHubIssue : DataConnectorEntityModel
    {


        public int id { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public int number { get; set; }
        public string state { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public GitHubUser user { get; set; }
        public List<GitHubLabel> labels { get; set; }
        public GitHubUser assignee { get; set; }
        public GitHubMilestone milestone { get; set; }
        public int comments { get; set; }
        public GitHubPullRequest pull_request { get; set; }
        public DateTime? closed_at { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }

    }
}
