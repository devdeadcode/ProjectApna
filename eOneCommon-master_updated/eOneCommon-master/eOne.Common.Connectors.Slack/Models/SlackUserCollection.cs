using System.Collections.Generic;
using System.Linq;

namespace eOne.Common.Connectors.Slack.Models
{
    class SlackUserCollection : SlackCollection
    {

        public List<SlackUser> members { get; set; }

        public SlackUser find_user(string id)
        {
            return members.FirstOrDefault(user => user.id == id);
        }

    }
}
