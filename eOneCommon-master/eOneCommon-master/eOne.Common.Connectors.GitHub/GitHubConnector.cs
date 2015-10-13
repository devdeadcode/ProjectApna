using System;
using System.Collections.Generic;
using eOne.Common.Connectors.GitHub.Models;
using eOne.Common.DataConnectors.Rest;

namespace eOne.Common.Connectors.GitHub
{
    public class GitHubConnector: RestConnector
    {

        #region Constants

        public const int EntityIdIssues = 1;
        public const int EntityIdMilestones = 2;
        public const int EntityIdRepositories = 3;
        public const int EntityIdCommits = 4;

        #endregion

        public GitHubConnector()
        {
            Name = "GitHub";
            Group = ConnectorGroup.Other;
            BaseUrl = "api.github.com";
            Multicompany = true;
            CompanyPrompt = "Organization ";
            CompanyPluralPrompt = "Organizations";
            AddSetup();
        }

        #region Public methods

        public override void Initialise()
        {
            AddEntity(EntityIdIssues, "Issues", typeof(GitHubIssue));
            AddEntity(EntityIdMilestones, "Milestones", typeof(GitHubMilestone));
            AddEntity(EntityIdRepositories, "Respositories", typeof(GitHubRepository));
            AddEntity(EntityIdCommits, "Commits", typeof(GitHubCommit));
        }

        public override string GetEndpoint(ConnectorQuery query)
        {
            if (query.Entity == null) return string.Empty;
            switch (query.Entity.Id)
            {
                case EntityIdIssues:
                    return "orgs/{0}/issues";
                case EntityIdMilestones:
                    return "orgs/{0}/milestones";
                case EntityIdRepositories:
                    return "orgs/{0}/repos";
                case EntityIdCommits:
                    return "orgs/{0}/commits";
            }
            return string.Empty;
        }

        public override IEnumerable<object> Deserialize(string data, ConnectorQuery query)
        {
            switch (query.Entity.Id)
            {
                case EntityIdIssues:
                    return DeserializeJson<List<GitHubIssue>>(data);
                case EntityIdMilestones:
                    return DeserializeJson<List<GitHubMilestone>>(data);
                case EntityIdRepositories:
                    return DeserializeJson<List<GitHubRepository>>(data);
                case EntityIdCommits:
                    return DeserializeJson<List<GitHubCommit>>(data);
            }
            return null;
        }

        public override void RunAction(ConnectorAction action, List<Tuple<string, string>> parameters)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
