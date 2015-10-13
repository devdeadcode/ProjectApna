using eOne.Common.DataConnectors;

namespace eOne.Common.Connectors.GitHub.Models
{
    public class GitHubLabel : DataConnectorEntityModel
    {

        public string url { get; set; }
        public string name { get; set; }
        public string color { get; set; }

    }
}
