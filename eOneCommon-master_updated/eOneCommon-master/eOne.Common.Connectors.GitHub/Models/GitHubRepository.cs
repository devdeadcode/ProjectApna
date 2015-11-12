using System;

namespace eOne.Common.Connectors.GitHub.Models
{
    public class GitHubRepository : ConnectorEntityModel
    {

        public int id { get; set; }
        public GitHubUser owner { get; set; }
        public string name { get; set; }
        public string full_name { get; set; }
        public string description { get; set; }
        public bool _private { get; set; }
        public bool fork { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string clone_url { get; set; }
        public string git_url { get; set; }
        public string ssh_url { get; set; }
        public string svn_url { get; set; }
        public string mirror_url { get; set; }
        public string homepage { get; set; }
        public object language { get; set; }
        public int forks_count { get; set; }
        public int stargazers_count { get; set; }
        public int watchers_count { get; set; }
        public int size { get; set; }
        public string default_branch { get; set; }
        public int open_issues_count { get; set; }
        public bool has_issues { get; set; }
        public bool has_wiki { get; set; }
        public bool has_pages { get; set; }
        public bool has_downloads { get; set; }
        public DateTime pushed_at { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public GitHubPermission permissions { get; set; }

    }
}
