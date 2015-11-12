using System.ComponentModel;

namespace eOne.Common.Connectors.Facebook.Models
{
    public class FacebookPostPrivacy
    {

        public enum FacebookPostPrivacyType
        {
            [Description("Everyone")]
            EVERYONE,
            [Description("Friends")]
            ALL_FRIENDS,
            [Description("Friends of friends")]
            FRIENDS_OF_FRIENDS,
            [Description("Self")]
            SELF,
            [Description("Custom")]
            CUSTOM
        }
        public enum FacebookPostPrivacyFriendType
        {
            ALL_FRIENDS,
            FRIENDS_OF_FRIENDS,
            SOME_FRIENDS
        }

        public FacebookPostPrivacyType value { get; set; }
        public string description { get; set; }
        public FacebookPostPrivacyFriendType friends { get; set; }
        public string allow { get; set; }
        public string deny { get; set; }

    }
}