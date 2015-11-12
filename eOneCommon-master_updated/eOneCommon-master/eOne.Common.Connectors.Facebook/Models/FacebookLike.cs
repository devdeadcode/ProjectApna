using System.ComponentModel;

namespace eOne.Common.Connectors.Facebook.Models
{
    public class FacebookLike
    {

        public enum FacebookLikeProfileType
        {
            [Description("User")]
            user,
            [Description("Page")]
            page
        }

        public string id { get; set; }
        public string name { get; set; }
        public FacebookLikeProfileType profile_type { get; set; }

    }
}
