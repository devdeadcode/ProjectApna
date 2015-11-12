using System.Collections.Generic;

namespace eOne.Common.Connectors.Slack.Models
{
    public class SlackChannelCollection : SlackCollection
    {

        public List<SlackChannel> channels { get; set; }

    }
}
